/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       RadialReflectiveShockModel.cs
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
    class RadialReflectiveShockModel  
    {
        // machine parameters
        PlasmaFocusMachine m;
        // reults
        ModelResults model;
        List<IterationResult> results;

        public RadialReflectiveShockModel(ref  Simulator sim)
        {
            this.m = sim.machine;
            this.results = sim.modelResults.results;
            this.model = sim.modelResults;
        }


        public void start(Simulator s)
        {
#if DEBUG1
             Debug.Write( "Phase 3 - Radial reflected shock starting ");
#endif
             int startRow = s.rrsStartRow = results.Count;

            // Reflected Shock Phase is computed in SI units.
            // Convert initial values of RS Phase into SI units

            if (m.TAPER) {             
                 s.VS = s.VS * m.ENDRAD / m.T0;
                 // not used ?  RS = s.KS * m.ENDRAD;
                 s.rp = s.KP * m.ENDRAD;
                 s.ZF = s.ZF * m.ENDRAD;
                 s.VZ = s.VZ * m.ENDRAD / m.T0;
                 s.VP = s.VP * m.ENDRAD / m.T0;
            } else {
                 s.VS = s.VS * m.RADA / m.T0;
                // RS = s.KS * m.RADA;          not used ?    // KS = shock position
                 s.rp = s.KP * m.RADA;
                 s.ZF = s.ZF * m.RADA;
                 s.VZ = s.VZ * m.RADA / m.T0;
                 s.VP = s.VP * m.RADA / m.T0;
            }

             s.t = s.t * m.T0;
             s.dt = s.dt * m.T0;
             s.I = s.I * m.I0;                      // T0 = Sqr(L0 * C0)

             s.CH = s.isum * m.I0 * m.T0;           // isum current integral, II current derivative
             s.IDOT = s.II * m.I0 / m.T0;
             
          
            // Take Reflected shock temperature to be twice on-axis incident shock temperature
            double TMRS = 2 * s.TM;
            s.TeV = TMRS / (1.16 * 1e4);


            //FLAG = 10;
            //GoTo 1005
            //
            // Select Table for G & Z; according to gas
            //
            double gg = 0.0, zeff = 0;
            CoronaModel.calculateGZRadiativePhase(s.TM, m.ZN, out zeff, out gg, s.TeV);
            m.g = gg;
            s.zeff = zeff;

            // local variables
            double RRF = 0;
            double FRF = 0.33;                  
            // VRF is reflected shock speed taken as a constant value at 0.3 of on-axis forward shock speed
            double VSV = s.VS;

            double VRF = -FRF * s.VS;          // reflected shock speed, pg 13 bottom
            m.G1 = 2 / (m.g + 1);
            m.G2 = (m.g - 1) / m.g;
            double MUP = Constants.MU / (2 * Constants.Pi);
            s.VZ = -m.G1 * s.VS;               // axial pinch length elongating rate, pg 7 eqn IV


            // TODO  recalculate IR or use the previous iteration ?????
            IterationResult output = results[results.Count - 1];
            double KSR = output.KSR;
            double KPR = output.KPR;
            double ZFR = output.ZFR;
            double SZR = output.SZR;
            double SPR = output.SPR;

            double IR = output.IR;            
            
            while (RRF <= s.rp) {
#if DEBUG1

                if (results.Count % 10 == 0) Debug.Write(String.Format("{0:#.###}", s.rp) + " ");
#endif       
                try {
                s.t = s.t + s.dt;

                RRF = RRF + VRF * s.dt;
                double VRFCMUS = VRF * 1e-4;

                // this region of code is vulnerable to NaN, Infitinities in values computed, if the model parameters
                //  are far off

                // VP, piston speed, pg 14
                double K1 = 0;
                double E1 = m.G1 * K1 * VSV;
                double E2 = (1 / m.g) * (s.rp / s.I) * (1 - K1 * K1) * s.IDOT;
                double E3 = (m.G1 / 2) * (s.rp / s.ZF) * (1 - K1 * K1) * s.VZ;
                double E4 = m.G2 + (1 / m.g) * K1 * K1;
                s.VP = (E1 - E2 - E3) / E4; 
             
                // circuit eqn , pg 14
                //IDOT = (V0 - (CH / C0) - I * R0 - I * currf * MUP * ((Log(RADB / rp)) * VZ - (ZF / rp) * VP)) / 
                //(L0 + MUP * currf * ((Log(C)) * z0 * tc5 + (Log(RADB / rp)) * ZF))

                //
                // TODO  ERROR
                if (!m.isTapered()) m.tc5 = 1;
               // m.tc5 = 0;
                //s.IDOT = (m.V0 - (s.CH / m.C0) - s.I * m.R0 - s.I * m.currf * MUP * ((Math.Log(m.RADB / s.rp)) * s.VZ - (s.ZF / s.rp) * s.VP)) /
                //              (m.L0 + MUP * m.currf * (Math.Log(m.C) * m.z0 * m.tc5 + Math.Log(m.RADB / s.rp) * s.ZF));
                //// TODO - error !!! missing brackets

                double rad = Math.Log(m.RADB / s.rp);
                
                    
//3180 IDOT = (V0 - (CH / C0) - I * R0 - I * CURRF * MUP *( (Log(RADB / rp) * VZ - (ZF / rp) * VP)) / (L0 + MUP * CURRF * ((Log(C)) * z0 * tc5 + (Log(RADB / rp)) * ZF))


                double n1 = (m.V0 - s.CH / m.C0 - s.I * m.R0 - s.I * m.currfr * MUP * (rad * s.VZ - s.ZF / s.rp * s.VP));
                double d1 = m.L0 + MUP * m.currfr *( Math.Log(m.C) * m.z0 * m.tc5 + rad * s.ZF);
                s.IDOT = n1 / d1;

                if (Double.IsNaN(s.IDOT))
                    throw new ApplicationException("RS model failure");

//                V = MUP * I * ((Log(RADB / rp)) * VZ - (ZF / rp) * VP) + MUP * ((Log(RADB / rp)) * ZF + (Log(C)) * z0 * tc5) * IDOT
                s.V = MUP * s.I * (Math.Log(m.RADB / s.rp) * s.VZ - (s.ZF / s.rp) * s.VP) +
                                MUP * (Math.Log(m.RADB / s.rp) * s.ZF + Math.Log(m.C) * m.z0 * m.tc5) * s.IDOT;
                s.V = s.V * m.currfr;
                s.I = s.I + s.IDOT * s.dt;
                s.CH = s.CH + s.I * s.dt;
                s.rp = s.rp + s.VP * s.dt;
                s.ZF = s.ZF + s.VZ * s.dt;

                ////SSR = s.VS * (RADA / T0) * 10 ^ -4
                //double SPR = s.VP * (RADA / T0) * 1e-4;
                //double SZR = s.VZ * (RADA / T0) * 1e-4;
                //double KSR = s.KS * RADA * 1000;
                //double KPR = s.KP * RADA * 1000;
                //double ZFR = s.ZF * RADA * 1000;

                // Integrate to find EINP, energy into plasma ie work done by magnetic piston, by dynamic resistance effect
                s.EINP = s.EINP + 1e-7 * (SZR * 1E4 * Math.Log(1000 * m.RADB / KPR) - (SPR * 1E4 * (1000 / KPR) * (ZFR / 1000))) *
                                 IR * IR * 1e6 * m.currfr * m.currfr * s.dt;

                // Convert to Real convenient units for print out    
                double TR = s.t * 1e6;
                double VR = s.V * 1e-3;
                IR = s.I * 1e-3;
                KPR = s.rp * 1e3;
                ZFR = s.ZF * 1e3;
                SPR = s.VP * 1e-4;
                SZR = s.VZ * 1e-4;
                double IDOTKAUS = s.IDOT * 1e-9;
                //double RRFMM = RRF * 1e3             // not used ?

                //s.trradialstart = s.t * m.T0;
                double TRRadial = TR * 1000 - s.trradialstart * 1e9;

                if (IR > s.IPeak)
                {
                    s.timeMax = TR;
                    s.IPeak = IR;
                }
                // Determine max induced voltage for beam-gas neutron yield computation
                if (VR > s.VRMAX) 
                    s.VRMAX = VR;

                // save outputs
                output = new IterationResult();

                output.TR = TR;
                output.TRRadial = TRRadial;
                output.IR = IR;
                output.VR = VR;
                output.KSR = KSR;
                output.KPR = KPR;
                output.ZFR = ZFR;
             //   output.SSR = SSR;
                output.SPR = SPR;
                output.SZR = SZR;

                output.RRF = RRF * 1000.0;   // in mm, RRFMM
                output.TM = TMRS;

                output.gamma = m.g;
                output.zeff = s.zeff;
                output.EINPJ = s.EINP;

                //output.radial.ZR = ZR;
                //output.radial.ZZR = ZZR;
                
                results.Add(output);
                //rowi = rowi + 1
                //rowj = rowj + 1

                }
                catch (Exception /* System.OverflowException */ e)
                {
                    Debug.WriteLine("Exception "+ e.Message);
                }
                //3500 If RRF > s.rp Then GoTo 3990
                //3600 GoTo 3080
            } // maxPinchIndex while RRF <= s.rp
            //3990 // "RS HAS HIT PISTON. RS PHASE ENDS"

            // TODO what are these for ?
            //FLAG = 0;
            //NBN = 0;
            //NTN = 0;
            //NN = 0;


           // Debug.WriteLine();
            //3995 Debug.Print "RS HAS HIT PISTON. RS PHASE ENDS"
            //Debug.WriteLine("TR   TRRad    IR      VR   ");
            //Debug.WriteLine("====================");

            
            //for (int j = startRow; j < results.Count; j++)
            //{
            //    IterationResult result = results[j];
            //    Debug.Write(j.ToString("f0") + ", ");
            //    Debug.Write(result.TR.ToString("f3") + ", ");
            //    //Debug.Write(result.TRRadial.ToString("f3") + ", ");
            //    Debug.Write(result.IR.ToString("f3") + ", ");
            //    //Debug.Write(result.VR.ToString("f3") + ", ");
            //    //Debug.Write(result.KSR.ToString("f3") + ", ");
            //    //Debug.Write(result.KPR.ToString("f3") + ", ");
            //    //Debug.Write(result.ZFR.ToString("f3") + ", ");
            //    //Debug.Write(result.SSR.ToString("f3") + ", ");
            //    //Debug.Write(result.SPR.ToString("f3") + ", ");
            //    //Debug.Write(result.SZR.ToString("f3") + ", ");
            //    //Debug.Write(result.RRF.ToString("f3") + ", ");
            //    //Debug.Write(result.TM.ToString("f3") + ", ");

            //    //Debug.Write(result.gamma.ToString("f3") + ", ");
            //    //Debug.Write(result.zeff.ToString("f3") + ", ");
            //    //Debug.Write(result.EINPJ.ToString("f3") + ", ");

            //    Debug.WriteLine();
            //}
        }  // while
        
    }
}
