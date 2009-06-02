/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       RadialModel.cs
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
    public class RadialModel
    {

        // machine parameters
        PlasmaFocusMachine m;

        // results
        ModelResults model;
        List<IterationResult> results;

        public RadialModel(ref  Simulator sim)
        {
            this.m = sim.machine;
            this.results = sim.modelResults.results;
            this.model = sim.modelResults;
        }


        public void start(Simulator s)
        {
#if DEBUG1             
            Debug.Write("Phase 2 - Radial phase starting at " + results.Count.ToString() + " ");
#endif
            // * Radial phase RI, distances are relative to radius a.
            // * KS is shock position, KP is radial piston position, ZF is focus
            // * pinch length, all normalized to inner radius a; s.VS and s.VP are
            // * radial shock and piston speed,s.VZ is axial pinch length elongating rate
            // * Distances, radius and speeds are relative to radius of anode.
            // AS BEFORE QUANTITIES WITH AN R ATTACHED HAVE BEEN RE-COMPUTED AS REAL, I.e. UN-NORMALIZED QUANTITIES EXPRESSED IN USUAL LABORATORY UNITS.

            // End of Axial Phase; Start of Radial Inward Shock Phase

            // : FOR THIS PHASE Z=EFFECTIVE CHARGE NUMBER!!!

            // int rowi = 20;
            s.radialStartRow = model.firstRadialRow = results.Count;

            // Set some initial values for Radial Inward Phase step-by-step integration
            // Reset time inc//ent to finer step-size
            s.KS = 1;             // shock position
            s.KP = 1;             // radial piston position
            s.ZF = 0.00001;    // ZF focus pinch length

            // SET TIME INC//ENT TO HAVE ABOUT 1500 (up to 3000 for high pressure) STEPS IN RADIAL INWARD SHOCK PHASE

            if (m.TAPER)
                m.TPINCHCH = m.TPINCHCH * (m.ENDRAD / m.RADA) * (m.ENDRAD / m.RADA);

            m.DREAL = m.TPINCHCH / 500.0;

            s.dt = m.DREAL / m.T0;

            // Set initial 'LookBack' values, for compensation of finite small disturbance speed
            double IDELAY = s.I;
            double KPDELAY = s.KP;
            double KSDELAY = s.KS;
            double VSDELAY = -1;

            // Set initial value, approximately, for CHARGE NUMBER Z
            // For H2,D2 and He, assume fully ionized with gamma=1.667 during all of radial phase
            s.z = s.zeff = m.ZN;

            double TRRadial = 0.0;
            s.trradialstart = s.t * m.T0;
            //ActiveSheet.Cells(16, 8)
            model.TradialStart = s.trradialstart * 1e6;

            while (s.KS > 0)
            {
#if DEBUG1

                if (results.Count % 10 == 0) Debug.Write(String.Format("{0:#.##}", s.KS) + " ");
#endif
                try
                {

                    // Start Step-by-step integration of Radial Inward Shock Phase, in non-dimensional units
                    // First, compute Inward shock speed, s.VS            

                    if (m.TAPER)
                    {
                        m.AA1 = m.AAg1 * Math.Sqrt(m.g + 1);
                        s.VS = -m.AL1 * m.AA1 * IDELAY / (KPDELAY);
                        s.VSR = s.VS * m.ENDRAD / m.T0;

                    }
                    else
                    {
                        m.GCAP = (m.g + 1) / (m.g - 1);
                        m.AA = m.AAg * Math.Sqrt(m.g + 1);
                        s.VS = -m.AL * m.AA * IDELAY / (KPDELAY);
                        s.VSR = s.VS * m.RADA / m.T0;
                    }
                    // Real temperature is needed to DETERMINE SMALL DISTURBANCE SPEED FOR COMMUNICATION TIME CORRECTION.
                    // Hence the shock speed is re-calculated in SI units,  Plasma Temp TM is calculated, based on shock theory
                    // pg 13, shocked plasma, 8315 = R universal gas constant

                    s.TM = (m.MW / (8315.0)) * ((m.GCAP - 1) / (m.GCAP * m.GCAP)) * ((s.VSR * s.VSR) / ((1 + s.zeff) * m.dissociatenumber));
                    s.TeV = s.TM / (1.16e4);

                    //
                    // Select Table for G & Z; according to gas
                    //
                    double gg = 0.0, zeff = 0;
                    CoronaModel.calculateGZRadiativePhase(s.TM, m.ZN, out zeff, out gg, s.TeV);
                    m.g = gg;
                    s.zeff = zeff;

                    m.G1 = 2 / (m.g + 1);
                    m.G2 = (m.g - 1) / m.g;

                    //if FLAG = 10  GoTo 3030

                    // GoTo 2000

                    // Next compute Axial elongation speed and Piston speed, using 'lookback' values to correct for finite small disturbance speed effect
                    s.VZ = -m.G1 * s.VS;     // pg 10 eqn IV.1
                    double K1 = s.KS / s.KP;

                    double E1 = m.G1 * K1 * VSDELAY;

                    // Piston Speed, s.VP
                    double E2 = (1 / m.g) * (s.KP / s.I) * (1 - K1 * K1) * s.II;
                    double E3 = (m.G1 / 2) * (s.KP / s.ZF) * (1 - K1 * K1) * s.VZ;
                    double E4 = m.G2 + (1 / m.g) * K1 * K1;
                    s.VP = (E1 - E2 - E3) / E4;                   // pg 10 Radial Piston Speed V.1

                    if (m.TAPER)
                    {
                        s.V = (m.BE * m.tc5 - m.BF * (Math.Log(s.KP / m.c2)) * s.ZF) * s.II - s.I * (m.BF * (s.ZF / s.KP) * s.VP + (m.BF * (Math.Log(s.KP / m.c2))) * s.VZ);
                        s.II = (1 - s.isum + m.BF * s.I * (s.ZF / s.KP) * s.VP + m.BF * (Math.Log(s.KP / m.c2)) * s.I * s.VZ - m.RESF * s.I) /
                            (1 + m.BE * m.tc5 - m.BF * (Math.Log(s.KP / m.c2)) * s.ZF);

                    }
                    else
                    {
                        s.V = (m.BE - m.BF * (Math.Log(s.KP / m.C)) * s.ZF) * s.II - s.I * (m.BF * (s.ZF / s.KP) * s.VP + (m.BF * (Math.Log(s.KP / m.C))) * s.VZ);
                        s.II = (1 - s.isum + m.BF * s.I * (s.ZF / s.KP) * s.VP + m.BF * (Math.Log(s.KP / m.C)) * s.I * s.VZ - m.RESF * s.I) /
                                  (1 + m.BE - m.BF * (Math.Log(s.KP / m.C)) * s.ZF);
                    }

                    // Inc//ent time and Integrate, by linear approx, for I, flowed charge I0, KS, KP and ZF
                    s.t = s.t + s.dt;
                    s.I = s.I + s.II * s.dt; //D


                    if (Double.IsNaN(s.I) || Double.IsNaN(s.II) || Double.IsNaN(s.V) || Double.IsNaN(s.VP))
                        break;
                    s.isum = s.isum + s.I * s.dt;
                    s.KS = s.KS + s.VS * s.dt;         // shock position
                    s.KP = s.KP + s.VP * s.dt;         // radial piston position
                    s.ZF = s.ZF + s.VZ * s.dt;         // ZF focus pinch length

                    //if (s.KS < 0 || s.KP < 0 || s.ZF < 0)
                    //    Debug.WriteLine();

                    // * Re-scales speeds, distances and time to real, convenient units
                    double SSR,
                            SPR,
                            SZR,
                            KSR,
                            KPR,
                            ZFR;

                    if (m.TAPER)
                    {
                        SSR = s.VS * (m.ENDRAD / m.T0) * 1E-4;
                        SPR = s.VP * (m.ENDRAD / m.T0) * 1E-4;
                        SZR = s.VZ * (m.ENDRAD / m.T0) * 1E-4;

                        KSR = s.KS * m.ENDRAD * 1000;
                        KPR = s.KP * m.ENDRAD * 1000;
                        ZFR = s.ZF * m.ENDRAD * 1000;
                    }
                    else
                    {

                        SSR = s.VS * (m.RADA / m.T0) * 1e-4;
                        SPR = s.VP * (m.RADA / m.T0) * 1e-4;
                        SZR = s.VZ * (m.RADA / m.T0) * 1e-4;
                        KSR = s.KS * m.RADA * 1000;
                        KPR = s.KP * m.RADA * 1000;
                        ZFR = s.ZF * m.RADA * 1000;
                    }

                    double TR = s.t * m.T0 * 1e6;
                    double VR = s.V * m.V0 * 1e-3;
                    double IR = s.I * m.I0 * 1e-3;
                    double IIR = (s.II * m.I0 / m.T0) * 1e-9;
                    double DR = s.dt * m.T0;

                    if (IR > s.IPeak)
                    {
                        s.timeMax = TR;
                        s.IPeak = IR;
                    }
 
                    // Obtain Max induced voltage
                    if (VR > s.VRMAX) s.VRMAX = VR;

                    TRRadial = TR * 1e3 - s.trradialstart * 1e9;
                     
                    // Integrate to find EINP, energy into plasma ie work done by magnetic piston, by dynamic resistance effect
                    //EINP = EINP + (10 ^ -7) * (SZR * (10 ^ 4) * Log(1000 * RADB / KPR) - (SPR * (10 ^ 4) * (1000 / KPR) * (ZFR / 1000))) * IR * IR * (10 ^ 6) * currfr * currfr * DR
                    //double temp1 = s.EINP +  
                    //         (SZR * 1e4 * Math.Log(1000 * m.RADB / KPR) - (SPR * 1e4 * (1000 / KPR) * (ZFR / 1000))) * 1e-7
                    //         * IR * IR * 1e6 * m.currfr * m.currfr * DR;

                    if (KPR > 0)                
                      s.EINP = s.EINP +
                             ( SZR  * Math.Log(1000 * m.RADB / KPR) - (SPR  * ZFR / KPR) ) * 1e3
                             * IR * IR  * m.currfr * m.currfr * DR;
    
                    if (Double.IsNaN(s.EINP))
                        throw new ApplicationException("Energy in radial phase cannot be calculated "+KPR);

                    // save outputs
                    IterationResult output = new IterationResult();

                    output.TR = TR;
                    output.IR = IR;
                    output.VR = VR;

                    output.TR = TR;
                    output.TRRadial = TRRadial;
                    output.IR = IR;
                    output.VR = VR;
                    output.KSR = KSR;
                    output.KPR = KPR;
                    output.ZFR = ZFR;
                    output.SSR = SSR;
                    output.SPR = SPR;
                    output.SZR = SZR;
                    output.TM = s.TM;

                    output.gamma = m.g;
                    output.zeff = s.zeff;
                    output.EINPJ = s.EINP;

                    //output.radial.ZR = ZR;
                    //output.radial.ZZR = ZZR;

                    results.Add(output);
                    //  Debug.WriteLine(results.Count);

                    // To apply finite small disturbance speed correction. Compute propagation time and the 'lookback' row number
                    // see pg 12  SDS correction
                    if (KSR <= KPR)
                    {
                        //int FIRSTRADIALROW = 0;
                        double SDSPEED = Math.Sqrt(m.g * m.dissociatenumber * (1 + s.zeff) * Constants.bc * s.TM / (m.MW * Constants.mi));

                        double SDDELAYTIME = ((KPR - KSR) / 1000.0) / SDSPEED;

                        int backhowmanyrows = (int)Math.Round(SDDELAYTIME / DR);

                        // since we are zero-based
                        int delayrow = results.Count - backhowmanyrows - 1;
                        if (delayrow < model.firstRadialRow) delayrow = model.firstRadialRow;
                        // Look back to appropriate row to obtain 'lookback' quantities; also non-dimensionalize these quantities                    
                        IDELAY = results[delayrow].IR / (m.I0 / 1000);

                        KPDELAY = results[delayrow].KPR / (m.RADA * 1000);

                        VSDELAY = results[delayrow].SSR / ((m.RADA / m.T0) / 10000);
                        KSDELAY = results[delayrow].KSR / (m.RADA * 1000);


                        // In case 'lookback' row number falls outside range of radial phase data table
                    }
                    else
                    {
                        IDELAY = s.I;
                        KPDELAY = s.KP;
                        VSDELAY = s.VS;
                        KSDELAY = s.KS;
                    }
                }
                catch (Exception /* System.OverflowException */ e)
                {
                    Debug.WriteLine("Exception " + e.Message);
                }
                // Check whether inward shock front has reached axia

                //2314 if KS > 0  GoTo 980

                // Inward shock front has reached axis, we have exited from Radial Inward Phase and now go on to the Reflected Shock Phase
            }  // while KS > 0

           // Debug.WriteLine();

            //TR	TRRadial	IR	VR	KSR	KPR	ZFR	SSR	SPR	SZR	RRF	TM
            //usec	ns	kA	kV	mm	mm	mm	cm/us	cm/us	cm/us	mm	K

            //PJ	PBR	Prec	Pline	Prad	Qdot	Qj	Qbrem	Qrec	Qline	Qrad	Qtotal	AB	PBB
            //W	W	W	W	W	W	J	J	J	J	J	J		



            //Debug.WriteLine("TR   TRRad    IR      VR   ");
            ////Debug.WriteLine("====================");

            //     for (int j = 0; j < results.Count; j++)
            //    {
            //        IterationResult output = results[j];
            //   Debug.Write(j.ToString("p2") + ", ");
            //   Debug.Write(output.TR.ToString("f3") + ", ");
            //    Debug.Write(output.TRRadial.ToString("f3") + ", ");
            //    Debug.Write(output.IR.ToString("f3") + ", ");
            //    Debug.Write(output.VR.ToString("f3") + ", ");
            //    Debug.Write(output.KSR.ToString("f3") + ", ");
            //  Debug.Write(output.KPR.ToString("f3") + ", ");
            //    Debug.Write(output.ZFR.ToString("f3") + ", ");
            //    Debug.Write(output.SSR.ToString("f3") + ", ");
            //    Debug.Write(output.SPR.ToString("f3") + ", ");
            //    Debug.Write(output.SZR.ToString("f3") + ", ");
            //    Debug.Write(output.RRF.ToString("f3") + ", ");
            //   Debug.Write(output.TM.ToString("f3") + ", ");

            //    Debug.Write(output.gamma.ToString("f3") + ", ");
            //    Debug.Write(output.zeff.ToString("f3") + ", ");
            //    Debug.Write(output.EINPJ.ToString("f3") + ", ");

            // Debug.WriteLine();
            //}

            model.radialDuration = (TRRadial * 1e-3 - s.trradialstart) * 1e3;
#if (DEBUG1)
            Debug.Write("Radial Start Row : " + System.Convert.ToString(model.firstRadialRow + " "));
            Debug.Write("Radial Start time : " + System.Convert.ToString(s.trradialstart + " "));
            Debug.Write("Radial End Row : " + System.Convert.ToString(results.Count + " "));
            Debug.WriteLine("Radial End time : " + System.Convert.ToString(s.t));
#endif

        }
    }
}
