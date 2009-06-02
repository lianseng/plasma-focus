/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       AxialModel.cs
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

namespace Plasma_Focus.models
{
    /// <summary>
    /// Description of AxialModel.
    /// </summary>
    public class AxialModel
    {
        // machine parameters
        PlasmaFocusMachine m;
        // reults
        List<IterationResult> results;
        //ModelResults machine.axialResults results;

        public AxialModel( ref Simulator sim) // List<IterationResult> results)
        {
            this.m = sim.machine;
            //this.results = sim.results;
            this.results = sim.modelResults.results;
        }

        public void start(Simulator s)
        {

#if DEBUG1
            Debug.Write("Phase 1 - Axial phase starting ");
#endif
            // Calculate ratio of characteristic capacitor time to sum of characteristic axial & radial times
            m.ALT = (m.AL * m.AA) / (1 + m.AA);

            // AXIAL PHASE
            // Set the first row to record data from numerical integration
            //                int rowj = 20;

            // Set time increment and initial values	            	
            s.AC = m.AL * Math.Sqrt(0.5);  // AL= capacitor time T0/TA, axial run-down time

            // Start numerical integration of AXIAL PHASE
            s.IPeak = 0;
            double V = 0;
            while (s.z < 1)
            {
                s.t = s.t + s.dt;

#if DEBUG1
                if (results.Count%100==0)   Debug.Write(String.Format("{0:#.##}",s.z)+" ");
#endif
                if (s.t > 6)
                    throw new ApplicationException("Too many cycles in axial phase !");

                s.ZZ = s.ZZ + s.AC * s.dt;
                s.z = s.z + s.ZZ * s.dt;
                s.I = s.I + s.II * s.dt;
                s.isum = s.isum + s.I * s.dt;
                

                // Convert data to real, but convenient units	
                double TR = s.t * m.T0 * 1e6;
                double VR = V * m.V0 * 1e-3;
                double IR = s.I * m.I0 * 1e-3;
                double ZZR = (m.ZZCHAR / m.AL) * s.ZZ * 1e-4;
                double ACR = ((m.ZZCHAR / m.AL) / m.T0) * s.AC * 1e-10;
                double ZR = s.z * m.z0 * 100;
                double IIR = (s.II * m.I0 / m.T0) * 1e-9;
                double IOR = s.isum * m.I0 * m.T0;

                if (IR > s.IPeak)
                {
                    s.timeMax = TR;
                    s.IPeak = IR;
                }

                // save outputs
                IterationResult output = new IterationResult();

                output.TR = TR;
                output.IR = IR;
                output.VR = VR;
                output.ZR = ZR;
                output.ZZR = ZZR;

                results.Add(output);


                if (m.TAPER && s.z >= m.zTAPERSTART)
                {
                    // Compute tapered anode  for this axial position
                    double w = m.f * m.tapergrad * (s.z - m.zTAPERSTART);
                    double c1 = m.RADB / (m.RADA * (1 - w));

                    m.TA = Math.Sqrt(4 * Constants.Pi * Constants.Pi * (m.C * m.C - 1) / (Constants.MU * Math.Log(c1)))
                                 * ((m.z0 * Math.Sqrt(m.RHO)) / (m.I0 / m.RADA))
                                 * ((Math.Sqrt(m.massf)) / m.currf);
                    m.AL1 = m.T0 / m.TA;                              // alpha, pg 10
                    m.BE = 2 * (1e-7) * Math.Log(m.C) * m.z0 * m.currf / m.L0;
                    double tc1 = 1 + (w * (4 - 3 * w)) / (2 * (m.C * m.C - 1));
                    double tc2 = w * (s.z - m.zTAPERSTART) * (2 - w) / (2 * (m.C * m.C - 1));
                    double LOG1 = Math.Log(1 / (1 - w));
                    double tc3 = 1 + LOG1 / Math.Log(m.C);
                    double tc4 = s.z + ((1 / Math.Log(m.C)) * (1 - ((1 - w) / w) * LOG1) * (s.z - m.zTAPERSTART));

                    V = (tc4) * s.II + s.ZZ * s.I * tc3;
                    V = V * m.BE;

                    s.AC = (m.AL1 * m.AL1 * s.I * s.I - s.ZZ * s.ZZ * tc1) / (s.z + tc2);
                    s.II = (1 - s.isum - m.BE * tc3 * s.ZZ * s.I - m.RESF * s.I) / (1 + m.BE * tc4);
                }
                else
                {
                    V = s.z * s.II + s.ZZ * s.I;             // page 5, eqn II.11
                    V = V * m.BE;                      // normalized form, v = V/V0, page 5

                    // Compute Generating Quantities (ie acceleration and IDOT) before loopback to continue step-by-step integration             
                    s.AC = (m.AL * m.AL * s.I * s.I - s.ZZ * s.ZZ) / s.z;
                    s.II = (1 - s.isum - m.BE * s.ZZ * s.I - m.RESF * s.I) / (1 + m.BE * s.z);
                } // taper
 
            }// while Axial phase

         //   Debug.WriteLine();

            s.ZG = s.ZZ;

            // Introduce differential in current factors for axial and radial phases
            m.CurrentFactorRatio = m.currfr / m.currf;
            //s.CFR = m.currfr / m.currf; //CurrentFactorRatio;

            m.BE = m.BE * m.CurrentFactorRatio;
            m.BF = m.BF * m.CurrentFactorRatio;
         //   m.currf = m.currf * m.CurrentFactorRatio;
            m.AAg = m.AA / Math.Sqrt(m.g + 1);

            if (m.TAPER)
            {
                double w1 = m.f * m.tapergrad * (1 - m.zTAPERSTART);
                m.c2 = m.RADB / (m.RADA * (1 - w1));
                m.AA1 = m.AA * (m.RADA / m.ENDRAD) * Math.Sqrt((Math.Log(m.C) / Math.Log(m.c2)));
                double LOG2 = Math.Log(1 / (1 - w1));

                // tapered machine parameters, needs to be global
                m.tc5 = 1 + ((1 / Math.Log(m.C)) * (1 - ((1 - w1) / w1) * LOG2) * (1 - m.zTAPERSTART));
                m.AAg1 = m.AA1 / Math.Sqrt(m.g + 1);
            }

        } // start
         
    } // class
}
