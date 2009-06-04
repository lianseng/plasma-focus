/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       ExpandedColumnAxialModel.cs
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
    class ExpandedColumnAxialModel
    {
        // machine parameters
        PlasmaFocusMachine m;
        // reults
        ModelResults modelResults;
        List<IterationResult> results;

        public ExpandedColumnAxialModel(ref  Simulator sim)
        {
            this.m = sim.machine;
            this.results = sim.modelResults.results;
            this.modelResults = sim.modelResults;
        }


        public void start(Simulator s)
        {
#if DEBUG1

            Debug.Write("Phase 5 - Expanded column axial phase starting ");
#endif
            // Expanded axial phase starts; integrated in normalised quantities

            s.expandAxialStartRow = results.Count;

            s.dt = 0.005;
            m.BE = m.BE / m.CurrentFactorRatio;

            s.t = s.t / m.T0;
            s.I = s.I / m.I0;
            s.isum = s.CH / (m.I0 * m.T0);

            double ZS = s.ZF / m.z0;
            s.ZZ = s.ZG;            // final value of axial speed
            s.z = 1 + ZS;

            double L = (Math.Log(m.C) + 0.25) / Math.Log(m.C);
            double H = Math.Sqrt(m.C * m.C / (m.C * m.C - 1));
            double L1 = (Math.Log(m.C) + 0.5) / Math.Log(m.C);

            // Set limit for integration to just over half cycle
            if (Double.IsInfinity(s.rp) || Double.IsNaN(s.rp))
            {
                Debug.WriteLine("No pinch");
                throw new ApplicationException("No pinch");                
            }

            while (s.t <= 3.5)
            {
#if DEBUG1
                if (results.Count % 10 == 0) Debug.Write(String.Format("{0:#.##}", s.t) + " ");
#endif
                try
                {
                    s.t = s.t + s.dt;

                    if (!m.TAPER)
                        m.tc5 = 1;

                    double AC = (m.AL * m.AL * s.I * s.I * L - H * H * s.ZZ * s.ZZ) / (1 + H * H * (s.z - 1));
                    s.II = (1 - s.isum - m.BE * s.I * s.ZZ * L1 - m.RESF * s.I) / (1 + m.BE * m.tc5 + m.BE * L1 * (s.z - 1));
                    s.ZZ = s.ZZ + AC * s.dt;
                    s.z = s.z + s.ZZ * s.dt;
                    s.I = s.I + s.II * s.dt;
                    s.isum = s.isum + s.I * s.dt;

                    double M = (1 + (1 / (2 * Math.Log(m.C)))) * (s.z - 1);
                    s.V = m.BE * ((1 * m.tc5 + M) * s.II + s.I * s.ZZ * (1 + (1 / (2 * Math.Log(m.C)))));

                    double TR = s.t * m.T0 * 1e6;
                    double VR = s.V * m.V0 * 1e-3;
                    double IR = s.I * m.I0 * 1e-3;
                    double ZZR = (m.ZZCHAR / m.AL) * s.ZZ * 1e-4;
                    double ZR = s.z * m.z0 * 100;

                    // save outputs (axial outputs)
                    IterationResult output = new IterationResult();

                    output.TR = TR;
                    output.IR = IR;
                    output.VR = VR;
                    output.ZR = ZR;
                    output.ZZR = ZZR;

                    results.Add(output);
                }
                catch (Exception /* System.OverflowException */ e)
                {
                    Debug.WriteLine("Exception: "+ e.Message);
                }

            }

          //  Debug.WriteLine();
        }
    }
}
