/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       AutoFit.cs
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
using Plasma_Focus.views;
using System.Diagnostics;
using Plasma_Focus.models.fitting.ga;
using System.Collections;

namespace Plasma_Focus.models.fitting
{
    public class AutoFit
    {
        //
        //  Runs the tuning strategy - 3 stages
        //  All the misc methods for finding curve features, calculating R2, and general fitting
        //
        //
        #region variables

        Simulator s = Simulator.getInstance();

        SortedList<double, double> measuredCurrent;
        List<IterationResult> computedCurrent;

        public Metrics measuredMetrics, computedMetrics;

        public CurrentReading[] measured;
        public CurrentReading[] computed;

        public double massf, massfr, currf, currfr, L0, R0;

        public double r2;

        MeasuredCurrent currentData;

        public System.ComponentModel.BackgroundWorker worker;
        #endregion

        public AutoFit(System.ComponentModel.BackgroundWorker worker, CurrentReading pinch)
        {
            this.worker = worker;

            // process measured current
            s = Simulator.getInstance();
            currentData = s.machine.currentData;

            this.measuredCurrent = currentData.series;
            measured = currentData.array;
            //currentData.updateMeasuredMetrics(measured);
            measuredMetrics = currentData.metrics;

            this.computedCurrent = s.modelResults.results;
            computed = ModelResults.getComputedCurrentArray(computedCurrent);
            computedMetrics = s.modelResults.metrics = new Metrics();
        }

        public void updateComputedMetrics()
        {
            s.modelResults.updateModelMetrics(computedMetrics, measuredMetrics.midRadialTime);
        }

        // where to stop processing
        public double endTime()
        {
            return currentData.endTime;

        }

        public double startTime(){
            return currentData.midRiseTime;
        }
        
        public int tune()
        {
            s = Simulator.getInstance();

            Debug.WriteLine("End time " + endTime());

            loadParametersFromMachine(s.machine);

            GAFit.w1 = 1;                   // r2 is useful but
            GAFit.w3 = s.machine.currentData.metrics.pinch.reading / (s.machine.currentData.metrics.peak.reading - s.machine.currentData.metrics.pinch.reading);
            GAFit.w4 = GAFit.w3 * 2;        // pinch is not accurate
            GAFit.w2 = GAFit.w4 * 2;        // peak is the most accurately known
            
            GAFit.a = this;

            if (worker.CancellationPending == true) return 0;

            double[] start = null;

            GA.debugOpen();
            GA.debugLine("Axial params:, " + GAFit.axialParams.population + ", " + GAFit.axialParams.generations
                + ", " + GAFit.axialParams.mutation + ", " + GAFit.axialParams.crossover);
            GA.debugLine("Radial params:, " + GAFit.radialParams.population + ", " + GAFit.radialParams.generations
                + ", " + GAFit.radialParams.mutation + ", " + GAFit.radialParams.crossover);
            GA.debugLine("Final params:, " + GAFit.finalParams.population + ", " + GAFit.finalParams.generations
                + ", " + GAFit.finalParams.mutation + ", " + GAFit.finalParams.crossover);
            GA.debugLine("Fitness params:, " + GAFit.w1 + ", " + GAFit.w2 + ", " + +GAFit.w3 + ", " + +GAFit.w4 + ", ");
            GA.debugLine("Stage,	massf,	currf,	massfr,	currfr,	fitness");

            start = new double[] { massf, currf, massfr, currfr, R0, L0 };
            GAFit.evolveAxial(2, start);
 
            if (worker.CancellationPending == true) return 0;            
            start = new double[] { massf, currf, massfr, currfr, R0, L0 };
            GAFit.evolveRadial(2, start);
             
            if (worker.CancellationPending == true) return 0;
            start = new double[] { massf, currf, massfr, currfr, R0, L0 };

            int loops = 0;
            do
            {
                start = new double[] { massf, currf, massfr, currfr, R0, L0 };
                GAFit.evolve(4, start);

                Debug.WriteLine("Model : " + massf + " " + currf + " " + massfr + " " + currfr);
                Debug.WriteLine("R2    : " + r2);
                Debug.WriteLine("Diffs : peak  " + Metrics.finalPeakDiff);
                Debug.WriteLine("        time  " + Metrics.finalPeakTimeDiff);
                Debug.WriteLine("        pinch " + Metrics.finalPinchDiff);
                Debug.WriteLine("        ime   " + Metrics.finalPinchTimeDiff);
                if (worker.CancellationPending == true) return 0;
                if (loops++ == 3) break;
                Debug.WriteLine(loops);
            } while (r2 < 0.98 || Metrics.finalPinchDiff > 0.4 || Metrics.finalPeakDiff > 0.3);

            
            // save to machine
            s = Simulator.getInstance();

            s.machine.massf = massf;
            s.machine.massfr = massfr;
            s.machine.currf = currf;
            s.machine.currfr = currfr;

            s.machine.L0 = L0;
            s.machine.R0 = R0;

            s.machine.r2 = r2;//r2 = updateR2();

            GA.debugLine("");
            GA.debugClose();

            return 0;           

        }
         
        public int tuneElectricals()
        {

            s = Simulator.getInstance();

            loadParametersFromMachine(s.machine);
             
            GAFit.w1 = 1;                   // r2 is useful but
            GAFit.w3 = s.machine.currentData.metrics.pinch.reading / (s.machine.currentData.metrics.peak.reading - s.machine.currentData.metrics.pinch.reading);
            GAFit.w4 = GAFit.w3 * 2;        // pinch is not accurate
            GAFit.w2 = GAFit.w4 * 2;        // peak is the most accurately known
            
            // initial guess
            R0 = Math.Sqrt(s.machine.L0 / s.machine.C0) * 0.1;
            L0 = s.machine.L0;

            GAFit.a = this;

            GA.debugOpen();
            GA.debugLine("Axial params:, " + GAFit.axialParams.population + ", " + GAFit.axialParams.generations
                + ", " + GAFit.axialParams.mutation + ", " + GAFit.axialParams.crossover);
            GA.debugLine("Radial params:, " + GAFit.radialParams.population + ", " + GAFit.radialParams.generations
                + ", " + GAFit.radialParams.mutation + ", " + GAFit.radialParams.crossover);
            GA.debugLine("Final params:, " + GAFit.finalParams.population + ", " + GAFit.finalParams.generations
                + ", " + GAFit.finalParams.mutation + ", " + GAFit.finalParams.crossover);
            GA.debugLine("Fitness params:, " + GAFit.w1 + ", " + GAFit.w2 + ", " + +GAFit.w3 + ", " + +GAFit.w4 + ", ");
            GA.debugLine("Stage,	massf,	currf,	massfr,	currfr,	fitness");

            int loops = 0;
            do
            {
                double[] start = new double[] { massf, currf, massfr, currfr, R0, L0 };
                GAFit.evolveR0(4, start);

                s.machine.L0 = L0;
                s.machine.R0 = R0;
                s.machine.massf = massf;
                s.machine.currf = currf;

                Debug.WriteLine("Model : " + massf + " " + currf + " " + R0 + " " + L0);
                Debug.WriteLine("R2    : " + r2);
                Debug.WriteLine("Diffs : peak  " + Metrics.finalPeakDiff);
                Debug.WriteLine("        time  " + Metrics.finalPeakTimeDiff);
                Debug.WriteLine("        pinch " + Metrics.finalPinchDiff);
                Debug.WriteLine("        ime   " + Metrics.finalPinchTimeDiff);
                if (loops++ == 3) break;
                
                if (worker.CancellationPending == true) return 0;
               
            } while (r2 < 0.97);

            loops = 0;
            do
            {
                double[] start = new double[] { massf, currf, massfr, currfr, R0, L0 };
                GAFit.evolve(4, start);

                Debug.WriteLine("Model : " + massf + " " + currf + " " + massfr + " " + currfr);
                Debug.WriteLine("R2    : " + r2);
                Debug.WriteLine("Diffs : peak  " + Metrics.finalPeakDiff);
                Debug.WriteLine("        time  " + Metrics.finalPeakTimeDiff);
                Debug.WriteLine("        pinch " + Metrics.finalPinchDiff);
                Debug.WriteLine("        ime   " + Metrics.finalPinchTimeDiff);
                if (worker.CancellationPending == true) return 0;
                if (loops++ == 3) break;
                
            } while (r2 < 0.98 || Metrics.finalPinchDiff > 0.4 || Metrics.finalPeakDiff > 0.3);

           
            // save to machine
            s.machine.massf = massf;
            s.machine.massfr = massfr;
            s.machine.currf = currf;
            s.machine.currfr = currfr;

            s.machine.L0 = L0;
            s.machine.R0 = R0;

            s.machine.r2 = r2; // = updateR2();

            GA.debugClose();

            return 0;

        }

        // fit up to start of expanded axial phase (which had not been modeled accurately)
        // massfr > massf
        //Features for comparison include current risetime and rising shape, peak current, current 'roll off' and pinch, both shape and amplitude
        public void loadParameters()
        {
            s.machine.massf = massf;
            s.machine.massfr = massfr;
            s.machine.currf = currf;
            s.machine.currfr = currfr;

            s.machine.R0 = R0;
            s.machine.L0 = L0;

            s.machine.r2 = r2;

            try
            {
                s.validateParameters();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void run()
        {
            try
            {
                s.run();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #region public


        // recommended by Lee's model
        //Gas  MW	At No.		    massf	    massfr	    currf
        //H	    2	1	ZN=1	    0.06-0.15	0.15-0.4	0.7-0.8
        //D	    4	1	UNU/ICTP	0.073	    0.16	    0.7
        //Ne	20	10	NX2	        0.1	        0.2	        0.7
        //Ar	40	18				
        //Xe	132	54	ZN=10, 18	0.04-0.12	0.1-0.3	    0.7-0.8
        //He	4	2	UNU/ICTP	0.046	    0.3	        0.8
        //              NX2	        0.095	    0.16	    0.7
        public static void resetTuneGuesses(ref PlasmaFocusMachine m)
        {
            double ZN = m.ZN;

            if (ZN == 1)
            {
                m.massf = 0.05;
                m.massfr = 0.15;
                m.currf = 0.7;
                m.currfr = 0.7;
            }
            else
            {
                m.massf = 0.04;
                m.massfr = 0.1;
                m.currf = 0.7;
                m.currfr = 0.7;
            }
        }

        public void loadParametersFromMachine(PlasmaFocusMachine m)
        {
            massf = m.massf;
            massfr = m.massfr;
            currf = m.currf;
            currfr = m.currfr;
            R0 = m.R0;
            L0 = m.L0;
        }

        #endregion
    }
}
