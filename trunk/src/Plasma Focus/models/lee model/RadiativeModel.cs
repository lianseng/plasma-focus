/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       RadiativeModel.cs
 *  Author:     Loh Lian Seng
 *
 * 
 * The MIT License
 * 
 * Copyright (c) 2009 Loh Lian Seng
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *
 *  ----------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Plasma_Focus.models
{
    class RadiativeModel
    {

        // machine parameters
        PlasmaFocusMachine m;

        // reults
        ModelResults modelResults;
        List<IterationResult> results;

        public RadiativeModel(ref  Simulator sim)
        {
            this.m = sim.machine;
            this.results = sim.modelResults.results;
            this.modelResults = sim.modelResults;
        }


        public void start(Simulator s)
        {
#if DEBUG1
            Debug.Write("Phase 4 - Radiative (slow compression) phase starting ");
#endif
            s.radiativeStartRow = results.Count;

            // Radiative Phase is integrated in real quantities

            // As RS hits piston, the pressure exerted by the doubly shocked column on the piston shoots up by a factor of approx 6; 
            //    this will slow the piston down further or even push it back. Thie effect is included in the following section.
            // However, due to 2-D effect, the over-pressure may not be significant.
            //sflag1 = 0
            //sflag2 = 0
            //sflag3 = 0
            double sflag1 = 1;
            double sfactor = 1;

            double RPSTART = s.rp;
            double TeVSTART = s.TeV;

            double SDSPEEDSTART = Math.Sqrt((m.g * m.dissociatenumber * (1 + s.zeff) * Constants.bc * s.TM / (m.MW * Constants.mi)));
            double TRAD1 = 0.5 * RPSTART / SDSPEEDSTART;
            TRAD1 = 2 * TRAD1;
            s.dt = m.DREAL * 1e-8;

            double TStart = s.t;
            double Ipinch = s.I * m.currfr / 1000;
            double TRRadial = 0;

            double NTN = 0;  // neutron yeild

            // various energies
            double QTOTAL = 0;
            s.Qj = 0;
            s.Qbrem = 0;
            s.Qline = 0;
            s.Qrec = 0;

            s.Q = 0;
            s.Qrad = 0;

            double KPR = 0;
            double IR = 0;
            double NI = 0;

            if (Double.IsInfinity(s.rp) || Double.IsNaN(s.rp) || Double.IsNaN(s.I))
            {
                Debug.WriteLine("No pinch");
                throw new ApplicationException("No pinch");
            }
            do
            {
#if DEBUG1

                if (results.Count % 10 == 0) Debug.Write(String.Format("{0:#.##}", s.rp) + " ");
#endif
                //
                // Select Table for G & Z; according to gas
                //
                try
                {

                    double gg = 0.0, zeff = 0;
                    CoronaModel.calculateGZRadiativePhase(s.TM, m.ZN, out zeff, out gg, s.TeV);
                    m.g = gg;
                    s.zeff = zeff;

                    m.G1 = 2 / (m.g + 1);
                    m.G2 = (m.g - 1) / m.g;

                    // Compute Joule heating and radiation terms                
                    if (m.TAPER)
                    {
                        NI = m.N0 * Constants.FE * m.massfr * (m.ENDRAD / s.rp) * (m.RADA / s.rp);
                        s.TM = Constants.MUK * s.I * s.I * m.currfr * m.currfr /
                            (m.dissociatenumber * (1 + s.zeff) * m.N0 * m.ENDRAD * m.ENDRAD * Constants.FE * m.massfr);
                    }
                    else
                    {
                        // TM = MUK * I * I * currfr * currfr / (dissociatenumber * (1 + z) * N0 * RADA * RADA * FE * massfr)
                        NI = m.N0 * Constants.FE * m.massfr * (m.RADA / s.rp) * (m.RADA / s.rp);
                        s.TM = Constants.MUK * s.I * s.I * m.currfr * m.currfr /
                            (m.dissociatenumber * (1 + s.zeff) * m.N0 * m.RADA * m.RADA * Constants.FE * m.massfr);
                    }

                    
                    s.TeV = s.TM / (1.16e4); 

                    // this code is vulnerable to NaN, Infitinities in values computed, if the model parameters
                    //  are far off
                    double R = 1290 * s.zeff * s.ZF / (Constants.Pi * s.rp * s.rp * Math.Pow(s.TM, 1.5));
                    double PJ = R * s.I * s.I * m.currfr * m.currfr;
                    double PBR = -(Constants.CON11 * NI) * Math.Sqrt(s.TM) * (Constants.CON12 * NI) *
                                Constants.Pi * (s.rp * s.rp) * s.ZF * Math.Pow(s.zeff, 3);
                    double PREC = -5.92e-35 * NI * NI * Math.Pow(s.zeff, 5) * Constants.Pi * (s.rp * s.rp) * s.ZF / Math.Sqrt(s.TM);
                    double PLN = -(Constants.CON2 * NI) * NI * s.zeff * Math.Pow(m.ZN, 4) * Constants.Pi * (s.rp * s.rp) * s.ZF / s.TM;

                     
                    // Apply Plasma Self Absorption correction to PBR PREC and PLN:
                    // PM is photonic excitation number; AB is absorption corrected factor
                    // if (AB<1/2.7183, Radiation goes from volume-like PRAD to surface-like PRADS; PRADS has a limit being Blackbody Rad PBB

                    double PM = 1.66e-11 * (s.rp * 100) * Math.Sqrt(m.ZN) * NI * 1e-6 / (s.zeff * Math.Pow(s.TeV, 1.5));

                    double AB = 1 + ((1e-14 * (NI * 1e-6) * s.zeff) / Math.Pow(s.TeV, 3.5));

 
                    AB = 1 / AB;
                    AB = Math.Pow(AB, (1 + PM));
 
                    if (Double.IsInfinity(AB))
                        AB = 0;

                    PBR = AB * PBR;
                    PREC = AB * PREC;
                    PLN = AB * PLN;

                    double PRADS = -2.3e-15 * Math.Pow(m.ZN, 3.5) * Math.Sqrt(s.zeff) * Math.Pow(s.TM, 4) * 3.142 * s.rp * (2 * s.ZF);

                    // calibration factor for neon (NX2); got to check for other machines and gases that at cross-over point from volume to surface emission there is a smooth transition in power.
                    PRADS = 0.032 * PRADS;
                    double PBB = -5.7e-8 * Math.Pow(s.TM, 4) * (3.142 * s.rp * (2 * s.ZF + s.rp));

                    // only deuterium has thermal yield ?
                    if (!(m.ZN == 1 && m.MW == 2) && m.ZN != 2 && m.ZN != 10 && m.ZN != 18 && m.ZN != 54)
                    {

                        // For deuterium, compute 1. thermonuclear neutron yield component
                        double SIGV = 0;
                        if (s.TeV <= 100)
                        {
                            SIGV = 0;
                        }
                        else
                        {
                            if (s.TeV > 1e4)
                                SIGV = 24e-26 * Math.Pow(s.TeV / 1000, 1.55);
                            else if (s.TeV > 1e3)
                                SIGV = 2e-28 * Math.Pow(s.TeV / 1000, 3.63);
                            else if (s.TeV > 500)
                                SIGV = 2e-28 * Math.Pow(s.TeV / 1000, 7.7);
                            else if (s.TeV > 100)
                                SIGV = 1e-27 * Math.Pow(s.TeV / 1000, 10);

                            double NTNDOT = 0.5 * NI * NI * 3.142 * (s.rp * s.rp) * s.ZF * SIGV;
                            NTN = NTN + NTNDOT * s.dt;
                        }
                    }

                    // Calculate rate of net power emission
                    double PRAD = PBR + PLN + PREC;

                    // save outputs
                    IterationResult output = new IterationResult();
                    output.PRAD = PRAD;

                    //ActiveSheet.Cells(rowi, 38) = PRAD

                    //if (sflag1 = 1 )  4720
                    //sflag1 = 1;
                    //if (AB > 1 / 2.7183 )  4750
                    //sfactor = 1;
                    //sflag2 = 1;
                    //GoTo 4740

                    //4720 if (sflag2 = 1 )  4740
                    //4730 if (sflag3 = 1 )  4740

                    //if (AB > 1 / 2.7183 ) //4750
                    //sfactor = PRAD / PRADS;
                    //sflag3 = 1;

                    //4740 PRADS = sfactor * PRADS;
                    //PRAD = PRADS;

                    //4745 if (-PRAD > -PBB) Then PRAD = PBB;

                    //4750 s.Qdot = PJ + PRAD;


                    // sflag1 - first round, sflag2 - 2nd iteration, AB < 1/2.7183, 
                    // apply a factor to PRAD when AB goes from greater to less than 1/2.7183 (surface mode)
                    if (sflag1 == 1 && AB > 1 / 2.7183)
                    {
                        sflag1 = 2;
                    }
                    if (sflag1 == 2 && AB <= 1 / 2.7183)
                    {
                        if (sfactor == 1)
                            sfactor = PRAD / PRADS;
                        // surface mode
                        PRADS = sfactor * PRADS;
                        PRAD = PRADS;

                        if (-PRAD > -PBB)
                            PRAD = PBB;
                    }

                    s.Qdot = PJ + sfactor * PRAD;

                    // Compute slow piston speed
                    double E2 = (1 / m.g) * (s.rp / s.I) * s.IDOT;
                    double E3 = (1 / (m.g + 1)) * (s.rp / s.ZF) * s.VZ;

                    // E5 term in VP (related to dQ/dt) not corrected.
                    double correctfactor = m.dissociatenumber * (1 + s.zeff) * NI * Constants.bc;
                    correctfactor = 1;
                    double E5 = (4 * Constants.Pi * (m.g - 1) / (Constants.MU * m.g * s.ZF)) *
                                           ((s.rp * correctfactor) / (s.I * s.I * m.currfr * m.currfr)) * s.Qdot;
                    double E4 = (m.g - 1) / m.g;
                    s.VP = (-E2 - E3 + E5) / E4;               // piston speed pg 14

                    if (Double.IsNaN(s.VP) || Double.IsInfinity(s.VP))
                        throw new ApplicationException("Radiative phase failure");
                    // pg 14 di/dt, Circuit equation
                    double IDOT1 = m.V0 - s.CH / m.C0;
                    double MUP = Constants.MU / (2 * Constants.Pi);
                    double IDOT2 = -MUP * (Math.Log(m.RADB / s.rp)) * s.VZ * s.I * m.currfr;
                    double IDOT3 = MUP * s.I * s.ZF * s.VP * m.currfr / s.rp;
                    double IDOT4 = -s.I * (R * m.currfr + m.R0);
                    double IDOT5;
                    if (m.TAPER)
                        IDOT5 = m.L0 + MUP * (Math.Log(m.C)) * m.z0 * m.currfr * m.tc5 + MUP * (Math.Log(m.RADB / s.rp)) * s.ZF * m.currfr;
                    else
                        IDOT5 = m.L0 + MUP * (Math.Log(m.C)) * m.z0 * m.currfr + MUP * (Math.Log(m.RADB / s.rp)) * s.ZF * m.currfr;

                     s.IDOT = (IDOT1 + IDOT2 + IDOT3 + IDOT4) / IDOT5;
                     if (Double.IsNaN(s.IDOT) || Double.IsInfinity(s.IDOT))
                         throw new ApplicationException("Radiative phase failure");

                    // elongation speed, dzf/dt = -(2/gamma+1) drs/dt, 
                    // eqn II, pg 7      drs/dt = Sqrt(u(g+1)/rho) * fc/Sqrt(fm) * I/4*pi*rp
                    s.ZFDOT = Math.Sqrt((Constants.MU * (m.g + 1)) / (16 * Constants.Pi * Constants.Pi * m.RHO)) * s.I * m.currfr / s.rp;
                    //s.ZFDOT = (((MU * (g + 1)) / (16 * Pi * Pi * RHO)) ^ 0.5) * I * currfr / rp
                    if (!m.TAPER)
                        m.tc5 = 1;

                    // page 14, Tube Voltage, ERROR !
                    s.V = MUP * s.I * (Math.Log(m.RADB / s.rp) * s.VZ - (s.ZF / s.rp) * s.VP)
                                + MUP * ((Math.Log(m.RADB / s.rp) * s.ZF) + Math.Log(m.C) * m.z0 * m.tc5) * s.IDOT + R * s.I;
                    //else 
                    //    s.V = MUP * s.I * ((Math.Log(m.RADB / s.rp)) * s.VZ - (s.ZF / s.rp) * VP) 
                    //        + MUP * (((Math.Log(m.RADB / s.rp)) * s.ZF) + Math.Log(m.C) * m.z0) * s.IDOT + R * s.I;


                    s.V = s.V * m.currfr;
                    s.t = s.t + s.dt;
                    s.I = s.I + s.IDOT * s.dt;
                    s.CH = s.CH + s.I * s.dt;

                    // double rpOLD = s.rp;
                    s.rp = s.rp + s.VP * s.dt;
                    if (Double.IsNaN(s.rp))
                        throw new ApplicationException("Radiative model failure");

                    double SPR = -s.VP * 1e-4;

                    // Set Variable time inc//ent to suit both slow and fast piston
                    if (SPR < 1e2) s.dt = m.DREAL / 5;
                    if (SPR == 1e2) s.dt = m.DREAL / 10;
                    if (SPR > 1e2) s.dt = m.DREAL / 100;
                    if (SPR > 1e3) s.dt = m.DREAL / 1e4;
                    if (SPR > 1e4) s.dt = m.DREAL / 1e5;
                    if (SPR > 1e5) s.dt = m.DREAL / 1e6;

                    if (SPR > 1e6) s.dt = m.DREAL / 1e7;
                    if (SPR > 1e7) s.dt = m.DREAL / 1e8;
                    if (SPR > 1e8) s.dt = m.DREAL / 1e9;

                    // Set limit for piston position 
                    if (m.TAPER)
                    {
                        if (s.rp < 0.01 * m.ENDRAD)
                        {
                            modelResults.endReasonForSlowCompressionPhase = "RP limit";
                            break;
                        }
                    }
                    else
                    {
                        if (s.rp < 0.01 * m.RADA)
                        {
                            modelResults.endReasonForSlowCompressionPhase = "RP limit";
                            break;
                        }
                    }

                    s.ZF = s.ZF + s.ZFDOT * s.dt;
                    s.Qj = s.Qj + PJ * s.dt;
                    s.Qbrem = s.Qbrem + PBR * s.dt;
                    s.Qline = s.Qline + PLN * s.dt;
                    s.Qrec = s.Qrec + PREC * s.dt;

                    s.Q = s.Q + s.Qdot * s.dt;
                    s.Qrad = (s.Qrad + PRAD * s.dt);

                    // estimate proportion of each radiation component using their unabsorbed values: 
                    // hence estimate absorption corrected s.Qbrem, s.Qline, s.Qrec
                    if (QTOTAL >= 0.000001)
                    {   // ERROR will never happen since QTOTAL is not updated
                        QTOTAL = (s.Qbrem + s.Qline + s.Qrec);
                        double ratiobr = s.Qbrem / QTOTAL;
                        double ratioln = s.Qline / QTOTAL;
                        double ratiorec = s.Qrec / QTOTAL;
                        s.Qbrem = ratiobr * s.Qrad;
                        s.Qline = ratioln * s.Qrad;
                        s.Qrec = ratiobr * s.Qrec;
                    }
                    double TR = s.t * 1e6;
                    double VR = s.V * 1e-3;
                    IR = s.I * 1e-3;
                    KPR = s.rp * 1e3;
                    double ZFR = s.ZF * 1000;
                    SPR = s.VP * 1e-4;
                    double SZR = s.ZFDOT * 1e-4;
                    double IDOTKAUS = s.IDOT * 1e-9;
                    double TMB = s.TM;

                    TRRadial = TR * 1000 - s.trradialstart * 1e9;

                    if (IR > s.IPeak)
                    {
                        s.timeMax = TR;
                        s.IPeak = IR;
                    }
                    // Determine max induced voltage for beam-gas neutron yield computation
                    if (VR > s.VRMAX)
                        s.VRMAX = VR;

                    // Integrate to find EINP, energy into plasma ie work done by magnetic piston, by dynamic resistance effect

                    s.EINP = s.EINP + 1e-7 * (SZR * 1e4 * Math.Log(1000 * m.RADB / KPR) - (SPR * 1e4 * (1000 / KPR) * (ZFR / 1000)))
                                         * IR * IR * 1e6 * m.currfr * m.currfr * s.dt;
                    s.EINP = s.EINP + PJ * s.dt;

                    //output.axial.TR = TR;
                    //output.axial.IR = IR;
                    //output.axial.VR = VR;

                    output.TR = TR;
                    output.TRRadial = TRRadial;
                    output.IR = IR;
                    output.VR = VR;
                    output.KPR = KPR;
                    output.ZFR = ZFR;
                    output.SPR = SPR;
                    output.SZR = SZR;

                    output.TM = s.TM;
                    output.PJ = PJ;
                    output.PBR = PBR;
                    output.Prec = PREC;
                    output.Pline = PLN;
                    output.PRAD = PRAD;
                    output.Qdot = s.Qdot;
                    output.Qj = s.Qj;
                    output.Qbrem = s.Qbrem;
                    output.Qrec = s.Qrec;
                    output.Qline = s.Qline;
                    output.Qrad = s.Qrad;
                    output.Qtotal = s.Q;
                    output.AB = AB;
                    output.PBB = PBB;
                    output.gamma = m.g;
                    output.zeff = s.zeff;
                    output.NTN = NTN;
                    output.NBN = 0;//NBN;
                    output.NN = 0;//NN;
                    output.NI = NI;
                    output.PRADSur = PRADS;
                    output.AB = AB;
                    output.EINPJ = s.EINP;

                    results.Add(output);


                    // Set limit for duration of radiative phase (1 ns for every mm radius)
                    // TRAD = 1000 * m.RADA * 10 ^ -9
                    // TRAD2 = (16 * (10 ^ -7)) * (RPSTART * 100) / (TeVSTART ^ 0.5)
                    // Set limit for duration of radiative phase using transit time of small disturbance across pinch radius
                    double TRAD = TRAD1;

                    if (s.t > (TStart + TRAD))
                    {
                        modelResults.endReasonForSlowCompressionPhase = "Time limit";
                        break;
                    }
                }
                catch (Exception /* System.OverflowException */ e)
                {
                    throw e;//Debug.WriteLine("Exception: {0}", e);
                }
            } while (true);
#if DEBUG1
            Debug.WriteLine();
            //7000 
            Debug.WriteLine("Slow compression phase stopped on " + modelResults.endReasonForSlowCompressionPhase+ " at row " +results.Count);
#endif
            modelResults.SXR = -s.Qline;

            //ActiveSheet.Cells(7, 14) = -s.Qline

            // Slow compression Phase Stopped: Time limit or RP limit.
            //ActiveSheet.Cells(7, 15) = ENDFLAG
            double TSlowcompressionphase = (s.t - TStart) * 1e9;

            modelResults.pinchDuration = TSlowcompressionphase;
            modelResults.radialDuration = (TRRadial * 1e-3);
            modelResults.radialEnd = (TRRadial * 1e-3) + s.trradialstart * (1e6);

            // Calculate energy in inductances
            double mup = Constants.MU / (2 * Constants.Pi);
            double Ecap = 0.5 * m.C0 * m.V0 * m.V0;
            double EL0 = 0.5 * m.L0 * s.I * s.I;
            double ELt = 0.5 * mup * Math.Log(m.C) * m.z0 * s.I * s.I * m.currfr * m.currfr;
            double ELp = 0.5 * mup * Math.Log(m.RADB / s.rp) * s.ZF * s.I * s.I * m.currfr * m.currfr;
            double MAG = Constants.MU * s.I * m.currfr / (2 * Constants.Pi * s.rp);
            double EMAGp = (MAG * MAG / (2 * Constants.MU)) * Constants.Pi * s.rp * s.rp * s.ZF;

            EL0 = (EL0 / Ecap) * 100;
            ELt = (ELt / Ecap) * 100;
            ELp = (ELp / Ecap) * 100;
            EMAGp = (EMAGp / Ecap) * 100;

            // speed factor of electromagnetically driven devices, pg 4 S
            double SFI0 = (m.I0 * 1e-3) / (m.RADA * 100) / Math.Sqrt(m.P0);
            double SFIpeak = (s.IPeak) / (m.RADA * 100) / Math.Sqrt(m.P0);
            double SFIdip = IR / (m.RADA * 100) / Math.Sqrt(m.P0);

            double Kmin;
            if (m.TAPER)
                Kmin = KPR / (m.ENDRAD * 1000);
            else
                Kmin = KPR / (m.RADA * 1000);

            double Ec = 0.5 * m.C0 * (m.V0 - (s.CH / m.C0)) * (m.V0 - (s.CH / m.C0));
            Ec = (Ec / Ecap) * 100;
            double Excircuit = 100 - (EL0 + ELt + ELp + EMAGp + Ec);
            s.EINP = (s.EINP / Ecap) * 100;
            // calculate loss of energy from inductances during current dip; ignoring capacitative energy change
            double L0t = m.L0 + (Constants.MU / (2 * Constants.Pi)) * (Math.Log(m.C)) * m.z0;
            double Lp = mup * (Math.Log(m.RADB / s.rp)) * s.ZF;
            double Edip = 0.5 * L0t * m.currfr * m.currfr * (s.IPeak * s.IPeak * 1e6 - s.I * s.I) - 0.5 * Lp * m.currfr * m.currfr * s.I * s.I;
            Edip = (Edip / Ecap) * 100;

            double SFIpinch = (Ipinch) / (m.RADA * 100) / Math.Sqrt(m.P0);

            modelResults.IPeak = s.IPeak;
            modelResults.IPinch = Ipinch;
            modelResults.pinchTime = TStart*1e6;

            modelResults.SFIpeak = SFIpeak;
            modelResults.SFIPinch = SFIpinch;
            modelResults.VRmax = s.VRMAX;
            modelResults.Kmin = Kmin;

            modelResults.Ecap = Ecap / 1000;
            modelResults.EINP = s.EINP;


            modelResults.lastRadialRow = results.Count;

            // Calculate  neutron yield in D2; 2 components viz 1. thermonuclear, 2. Beam-gas :
            if (m.ZN == 1 && m.MW == 2) return;
            else if (m.ZN == 2 || m.ZN == 10 || m.ZN == 18 || m.ZN == 54) return;

            // Computed VRMAX varies typically in range of 30-60kV for small to big devices; too low compared to expt observations;
            // Multiplying by factor 2 will get the range closer to 50-100kV; the range generally observed to be reponsible for beam-target neutrons in PF
            // Multiply VRMAX by factor to get closer to experimental observations; fine tuned to fit the optimum pressure for Yn for the UNU/ICTP PFF (around 3-3.5 torr at 15 kV)

            s.VRMAX = s.VRMAX * 3;
              
            double sig = 0;

            if (m.MW == 5)
            {
                //Rem For D-T (50:50), compute 2. Beam-gas neutron yield component (ref: NRL Formulary 2006 pg 43)
                //sig = (409 + ((((1.076 - 0.01368 * (VRMAX)) ^ 2) + 1) ^ -1) * 50200) / (VRMAX * (Exp(45.95 * VRMAX ^ -0.5) - 1))
                sig = (409 + 1/(Math.Pow((1.076 - 0.01368 * s.VRMAX), 2) + 1)  * 50200) / (s.VRMAX * (Math.Exp(45.95 * 1 / Math.Sqrt(s.VRMAX)) - 1));
           }
            else
            {
                //Rem For deuterium, compute 2. Beam-gas neutron yield component (ref: NRL Formulary 2006 pg 43)
                // sig = ((((1.177 - (3.08 * (10 ^ -4)) * (VRMAX)) ^ 2) + 1) ^ -1) * 482 / (VRMAX * (Exp(47.88 * VRMAX ^ -0.5) - 1))

                // For deuterium, compute 2. Beam-gas neutron yield component (ref: NRL Formulary 2006 pg 43)
                sig = 1 / (Math.Pow(1.177 - 3.08e-4 * s.VRMAX, 2) + 1) * 482 / (s.VRMAX * Math.Exp(47.88 * 1 / Math.Sqrt(s.VRMAX)) - 1);
             }
            sig = sig * 1e-28;
     
 
            // Calibrate for UNU/ICTP PFF for max neutron yield at optimum pressure as 10^8
            sig = sig * 6.34e8;
            // Calibrate for NESSI
            sig = sig / 23.23;

            // GoTo 7200
            // Model Ni No s.rp^2 zf sig-an earlier machine
            // NBN = NI * 3.142 * s.rp * s.rp * s.ZF * N0 * sig

            // convert to machine Ni s.I^2 zf^2 LOG(b/s.rp) VRMAX^-0.5 sig 
           double NBN = NI * ((Ipinch * 1000) * (Ipinch * 1000)) * (s.ZF * s.ZF) * (Math.Log(m.RADB / s.rp)) * (1 / Math.Sqrt(s.VRMAX)) * sig;

            // neutron beam
            double NN = NBN + NTN;

            // Ytherm	Ybeam	neutron Y
            modelResults.NTN = NTN;
            modelResults.NBN = NBN;
            modelResults.NN = NN;

        }

    }
}
