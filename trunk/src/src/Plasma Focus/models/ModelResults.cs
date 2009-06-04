/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       ModelResults.cs
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
using System.IO;
using System.Diagnostics;

namespace Plasma_Focus.models
{
    public class ModelResults
    {
        public double TradialStart { get; set; } //(us)	
        public double radialDuration { get; set; } //(us)	
        public double PinchDuration { get; set; } //(ns)	
        // public double TradialEnd { get; set; } //(us)	
        public string endReasonForSlowCompressionPhase { get; set; }
        public double SXR { get; set; }
        public double pinchDuration { get; set; }
        //  public double elapsedTimeForRadialPhase { get; set; }
        public double radialEnd { get; set; }
        // public double radial  { get; set; }

        public double IPeak { get; set; }
        public double IPinch { get; set; }
        public double pinchTime{ get; set; }
        public double SFIpeak { get; set; }
        public double SFIPinch { get; set; }
        public double VRmax { get; set; }
        public double Kmin { get; set; }

        public double NTN { get; set; }
        public double NBN { get; set; }
        public double NN { get; set; }

        public double Ecap { get; set; }
        public double EINP { get; set; }
 
        // results
        public int firstRadialRow { get; set; }
        public int lastRadialRow { get; set; } 

        public List<IterationResult> results = new List<IterationResult>();

        public Metrics metrics; 

        // to readings array
        public static CurrentReading[] getComputedCurrentArray(List<IterationResult> list)
        {
            CurrentReading[] a = new CurrentReading[list.Count];

            int i = 0;
            foreach (IterationResult r in list)
            {
                a[i++] = new CurrentReading(r.TR, r.IR);                 
            }

            return a;
        }

        public void updateModelMetrics(Metrics metrics)
        {
            Simulator s = Simulator.getInstance(); 
            metrics.peak = new CurrentReading(s.timeMax, s.IPeak);

            metrics.indexPinch = s.modelResults.lastRadialRow;//radiativeStartRow; //
            double pinchTime = s.modelResults.results[metrics.indexPinch].TR;
            double pinchCurrent =  s.modelResults.results[metrics.indexPinch].IR;

            
            metrics.pinch = new CurrentReading(pinchTime, pinchCurrent);
            
            // calculate point at mid radial phase to match
            //metrics.midRadialTime = s.timeMax + (pinchTime - s.timeMax) / 2;
            metrics.midRadialCurrent = s.IPeak + (pinchCurrent - s.IPeak) / 2;
            int i;
            for (i = 0; i < metrics.indexPinch; i++)
            {
                if (s.modelResults.results[i].IR >= s.IPeak) break;
            }
            for (; i < metrics.indexPinch; i++) {                
                if (s.modelResults.results[i].IR <= metrics.midRadialCurrent) break;
            }
            
            metrics.midRadialTime = s.modelResults.results[i].TR;
           // metrics.midRadialCurrent = s.modelResults.results[i].IR;

            metrics.radialSlope= (metrics.pinch.reading - metrics.midRadialCurrent) / 
                                             (metrics.pinch.time - metrics.midRadialTime);

          //  Debug.WriteLine("Radial slope " + metrics.radialSlope +
          //        " mid radial current " + metrics.midRadialCurrent + "  mid radial time: " + metrics.midRadialTime);
      
        }


        public static void export(string filename)
        {
            try
            {
                // Specify file, instructions, and privelegdes
                FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);


                // Create a new stream to write to the file
                StreamWriter sw = new StreamWriter(file);

                Simulator simulator = Simulator.getInstance();
                PlasmaFocusMachine machine = simulator.machine;
                // write out machine parameters 
                sw.Write("Model file : " + machine.configFile + ",,, ");
                sw.WriteLine("Machine name : " + machine.machineName + " ");

                // geometry		
                sw.Write("Anode Inner radius : " + machine.RADA.ToString("f3") + ",,, ");
                sw.Write("Anode Outer radius : " + machine.RADB.ToString("f3") + ",,, ");
                sw.WriteLine("Anode Length : " + machine.z0.ToString("f3") + " ");

                // capacitor bank parameters
                sw.Write("Inductance : " + machine.L0.ToString("f3") + ",,, ");
                sw.Write("Capacitance : " + machine.C0.ToString("f3") + ",,, ");
                sw.Write("Resistance : " + machine.R0.ToString("f3") + ",,, ");
                sw.WriteLine("Voltage : " + machine.V0.ToString("f3") + " ");

                // operation parameters
                sw.Write("Pressure : " + machine.P0.ToString("f3") + ",,, ");
                sw.Write("Gas : " + machine.fillGas + ",,, ");
                sw.Write("Mol Wt : " + machine.MW.ToString("f3") + ",,, ");
                sw.Write("Atomic No : " + machine.ZN.ToString("f3") + ",,, ");
                sw.Write("Density : " + machine.RHO.ToString("f3") + ",,, ");
                sw.WriteLine("Dissociation No : " + machine.dissociatenumber.ToString("f3") + " ");

                // taper parameters
                sw.Write("Tapered : " + machine.TAPER.ToString() + ",,, ");
                sw.Write("Taper start : " + machine.TAPERSTART.ToString("f3") + ",,, ");
                sw.WriteLine("Taper end radius : " + machine.ENDRAD.ToString("f3") + " ");

                // model parameters
                sw.Write("massf : " + machine.massf.ToString("f3") + ",,, ");
                sw.Write("currf : " + machine.currf.ToString("f3") + ",,, ");
                sw.Write("massfr : " + machine.massfr.ToString("f3") + ",,, ");
                sw.WriteLine("currfr : " + machine.currfr.ToString("f3") + " ");

                // debug
                sw.Write("radial start : " + simulator.radialStartRow.ToString() + ",,, ");
                sw.Write("reflective start : " + simulator.rrsStartRow.ToString() + ",,, ");
                sw.Write("radiative start : " + simulator.radiativeStartRow.ToString() + ",,, ");
                sw.WriteLine("expanded axial start : " + simulator.expandAxialStartRow.ToString() + " ");

                sw.Write("Radial start time : " + simulator.trradialstart + ",,,");
                sw.Write("Radial duration : " + simulator.modelResults.radialDuration + ",,,");
                sw.Write("Pinch duration : " + simulator.modelResults.pinchDuration + ",,,");
                sw.WriteLine("Radial end : " + simulator.modelResults.radialEnd + "");

                sw.Write("Thermal yield : " + simulator.modelResults.NTN + ",,,");
                sw.Write("Beam yield : " + simulator.modelResults.NBN + ",,,");
                sw.Write("Total yield : " + simulator.modelResults.NN + ",,,");
                sw.WriteLine("Line yield : " + simulator.modelResults.SXR + "");

                sw.Write("Current peak : " + simulator.modelResults.IPeak + ",,,");
                sw.Write("IPinch : " + simulator.modelResults.IPinch + ",,,");
                sw.Write("SFIpeak : " + simulator.modelResults.SFIpeak + ",,,");
                sw.Write("SFIPinch : " + simulator.modelResults.SFIPinch + ",,,");
                sw.Write("VRmax : " + simulator.modelResults.VRmax + ",,,");
                sw.WriteLine("Kmin : " + simulator.modelResults.Kmin + "");
                sw.Write("Ecap : " + simulator.modelResults.Ecap + ",,,");
                sw.WriteLine("EINP : " + simulator.modelResults.EINP + "");

                sw.WriteLine();
                sw.WriteLine();

                // write out iteration results
                List<IterationResult> results = simulator.modelResults.results;

                sw.Write("Pt.No, TR, TRRadial, IR, VR, ZR, ZZR,  KSR, KPR, ZFR, SSR, SPR, SZR, RRF, TM, PJ, PBR,");
                sw.Write("Prec, Pline, Prad, Qdot, Qj, Qbrem, Qrec, Qline, Qrad, Qtotal, AB, PBB, gamma, zeff, NTN, NBN,");
                sw.WriteLine("NN, NI, PRADVOL, PRADSur, AB, EINP J");

                for (int i = 0; i < results.Count; i++)
                {
                    IterationResult output = results[i];
                    sw.Write(i.ToString("d") + ", ");
                    sw.Write(output.TR.ToString("f5") + ", ");
                    sw.Write(output.TRRadial.ToString("f3") + ", ");
                    sw.Write(output.IR.ToString("f3") + ", ");
                    sw.Write(output.VR.ToString("f3") + ", ");
                    sw.Write(output.ZR.ToString("f3") + ", ");
                    sw.Write(output.ZZR.ToString("f3") + ", ");
                    sw.Write(output.KSR.ToString("f3") + ", ");
                    sw.Write(output.KPR.ToString("f3") + ", ");
                    sw.Write(output.ZFR.ToString("f3") + ", ");
                    sw.Write(output.SSR.ToString("f3") + ", ");
                    sw.Write(output.SPR.ToString("f3") + ", ");
                    sw.Write(output.SZR.ToString("f3") + ", ");
                    sw.Write(output.RRF.ToString("f3") + ", ");
                    sw.Write(output.TM.ToString("f3") + ", ");

                    sw.Write(output.PJ.ToString("f3") + ", ");
                    sw.Write(output.PBR.ToString("f3") + ", ");
                    sw.Write(output.Prec.ToString("f3") + ", ");
                    sw.Write(output.PRAD.ToString("f3") + ", ");
                    sw.Write(output.Qdot.ToString("f3") + ", ");
                    sw.Write(output.Qj.ToString("f3") + ", ");
                    sw.Write(output.Qbrem.ToString("f3") + ", ");
                    sw.Write(output.Qrec.ToString("f3") + ", ");
                    sw.Write(output.Qline.ToString("f3") + ", ");
                    sw.Write(output.Qrad.ToString("f3") + ", ");
                    sw.Write(output.Qtotal.ToString("f3") + ", ");

                    sw.Write(output.AB.ToString("f3") + ", ");
                    sw.Write(output.PBB.ToString("f3") + ", ");
                    sw.Write(output.gamma.ToString("f3") + ", ");
                    sw.Write(output.zeff.ToString("f3") + ", ");

                    sw.Write(output.NTN.ToString("f3") + ", ");
                    sw.Write(output.NBN.ToString("f3") + ", ");
                    sw.Write(output.NN.ToString("f3") + ", ");
                    sw.Write(output.NI.ToString("f3") + ", ");
                    sw.Write(output.PRADVOL.ToString("f3") + ", ");
                    sw.Write(output.PRADSur.ToString("f3") + ", ");

                    sw.Write(output.EINPJ.ToString("f3") + ", ");

                    sw.WriteLine();
                }

                sw.Close();
                file.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error exporting " + e.Message);
                throw e;
            }
        }


    }

    public class IterationResult
    {

        public double TR { get; set; }          // time
        public double TRRadial { get; set; }    // time from start of radial phase
        public double IR { get; set; }          // current
        public double VR { get; set; }          // tube voltage, kV

        // axial results
        public double ZR { get; set; }          // axial position, cm
        public double ZZR { get; set; }         // axial speed, cm us-1

        // post axial
        public double KSR { get; set; }         // radial inward shock, mm            
        public double KPR { get; set; }         // radial piston, mm
        public double ZFR { get; set; }         // pinch elongation (focal length), mm
        public double SSR { get; set; }         // radial shock speed, cm us-1
        public double SPR { get; set; }         // radial piston speed, cm us-1
        public double SZR { get; set; }         // pinch elongation speed, cm us-1
        public double RRF { get; set; }         // radial reflected shock, mm
        public double TM { get; set; }          // plasma temperature

        public double PJ { get; set; }          // ohmic power, W
        public double PBR { get; set; }         // bremstrahhlung power, W
        public double Prec { get; set; }        // recombination power, W
        public double Pline { get; set; }       // line power, W
        public double PRAD { get; set; }        // total radiative power, W
        public double Qdot { get; set; }
        public double Qj { get; set; }          // resistive heat, J
        public double Qbrem { get; set; }       // bremsstrahlung, J
        public double Qrec { get; set; }        // recombination, J
        public double Qline { get; set; }       // line radiation, J
        public double Qrad { get; set; }        // total line radiation, J
        public double Qtotal { get; set; }
        public double AB { get; set; }
        public double PBB { get; set; }
        public double gamma { get; set; }       // specific heat ratio
        public double zeff { get; set; }        // charge number
        public double NTN { get; set; }
        public double NBN { get; set; }
        public double NN { get; set; }
        public double NI { get; set; }
        public double PRADVOL { get; set; }
        public double PRADSur { get; set; }
        //     public double AB { get; set; }            // repeated for convenience ?
        public double EINPJ { get; set; }   //energy into plasma ie work done by magnetic piston, by dynamic resistance effect

 
    }
}
