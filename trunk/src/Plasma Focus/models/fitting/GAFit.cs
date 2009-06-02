/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       GAFit.cs
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
using System.Threading;
using System.Diagnostics;
using Plasma_Focus.models.fitting.ga;
using System.Collections; 

namespace Plasma_Focus.models.fitting
{
    public class GAFit
    {

        // Callbacks for GA class
        // Runs the GA algorithms and defines the fitness functions callback for each stage
        // 

        public struct GAParams
        {
            public double crossover;
            public double mutation;
            public int population;
            public int generations;
            public bool elitism;
         }

        public static double w1, w2, w3, w4;

        public static GAParams axialParams, radialParams, finalParams, electricalParams;

        public static AutoFit a { get; set; }
        public static double massf, currf, massfr, currfr;
        public static double fitness;

        static double run(double w1, double w2, double w3, double w4)
        {
            double r2 = 0;
            try
            {             
                // reload parameters
                a.loadParameters();

                // run simulator
                a.run();

                // re-calc metrics
                a.updateComputedMetrics();

                r2 = MeasuredCurrent.calcR2(a.computed, a.measured,a.startTime(), a.endTime());
                double p1 = 1 - (Metrics.peakDiff(a.measuredMetrics, a.computedMetrics));

                double t1 = 1 - (Metrics.peakTimeDiff(a.measuredMetrics, a.computedMetrics));

                double p2 = 1 - (Metrics.pinchDiff(a.measuredMetrics, a.computedMetrics));

                double t2 = 1 - (Metrics.pinchTimeDiff(a.measuredMetrics, a.computedMetrics));

                double s = 1- (Metrics.radialSlopeDiff(a.measuredMetrics, a.computedMetrics));

                r2 = (w1 * r2 + w2 * (p1 + t1) + w3 *( p2 + t2)+ w4 * s) / (w1+2*(w2+w3)+w4); 
            }
            catch (Exception e)
            {
                // nothing we can do, likely to be application defined exceptions
            }
            return r2;
        }

        public static void reportProgress(int progress, string status)
        {
            if (progress == -1)
            {
                GAFit.w2 = GAFit.w3 = GAFit.w2 = GAFit.w4;

                double peakDiff = 1 - (Metrics.peakTimeDiff(a.measuredMetrics, a.computedMetrics));

                if (peakDiff > 0.1) GAFit.w1 *= 2;

                //GAFit.w3 *= 5;
                //GAFit.w4 *= 2;
            
            }
            a.worker.ReportProgress(progress, status);
        }

        [Conditional("QA")]
        public static void writeQuality(string stage)
        {
            //GA.debugLine(",,,,,,,,"+a.massf + ", " + a.currf + ", " + a.massfr + ", " + a.currfr + ", " + 
            //    a.L0 + ", " + a.R0 + ", " + a.r2 + stage);//+ ", " + totalFitness + "," + stage);

        }
        
        #region axial
        public static double axialFitness(double[] values)
        {

            a.massf = values[0];
            a.currf = values[1];

            double fit = run(w1, w2, w3, w4);
            if (Double.IsNaN(fit) || Double.IsInfinity(fit)) fit = 0;
            return fit;
        }

        public static ArrayList evolveAxial( int genomeSize, double[] start)
        {

            GA ga = new GA(axialParams.crossover, axialParams.mutation, axialParams.population, axialParams.generations,
                           axialParams.elitism, genomeSize);

            ga.FitnessFunction = new GAFunction(axialFitness);
             
            a.massf = start[0];
            a.currf = start[1];

            double[] gene = new double[] { start[0], start[1] };

            ga.stage = "Axial stage";
            ga.worker = a.worker;            
            GA.report = new ReportProgress(reportProgress);

            reportProgress(0, "Tuning axial stage");
           

            ArrayList genome = ga.Go(gene, false);


            double[] values;
            ga.GetBest(out values, out fitness);

            a.massf = values[0];
            a.currf = values[1];
            a.r2 = fitness;

            writeQuality(ga.stage);
            
            return genome;


        }
        #endregion axial

        #region radial
        public static double radialFitness(double[] values)
        {

            a.massfr = values[0];
            a.currfr = values[1];

            double fit = run(w1,w2,w3,w4);
            if (Double.IsNaN(fit) || Double.IsInfinity(fit) ) fit = 0;
            return fit;
        }

        public static ArrayList evolveRadial( int genomeSize, double[] start)
        {

            GA ga = new GA(radialParams.crossover, radialParams.mutation, radialParams.population,
                              radialParams.generations, radialParams.elitism, genomeSize);

            ga.FitnessFunction = new GAFunction(radialFitness);
             
            a.massfr = start[2];
            a.currfr = start[3];

            double[] gene = new double[] { start[2], start[3] };

            reportProgress(0, "Tuning radial stage");
            GA.report = new ReportProgress(reportProgress);
            ga.worker = a.worker;

            ga.stage = "Radial stage"; 
            ArrayList genome = ga.Go(gene, false);

            double[] values;
            ga.GetBest(out values, out fitness);

            a.massfr = values[0];
            a.currfr = values[1];
            a.r2 = fitness;

            writeQuality(ga.stage);
           return genome;

        }


        #endregion radial

        #region final
        //
        //  Tune the 4 model params
        //
        public static ArrayList evolve(int genomeSize, double[] start)
        {

            GA ga = new GA(finalParams.crossover, finalParams.mutation, finalParams.population,
                             finalParams.generations, finalParams.elitism, genomeSize);

            ga.FitnessFunction = new GAFunction(Fitness);
             
            a.massf = start[0];
            a.currf = start[1];
            a.massfr = start[2];
            a.currfr = start[3];

            double[] gene = new double[] { start[0], start[1], start[2], start[3] };

            reportProgress(0, "Final stage");
            GA.report = new ReportProgress(reportProgress);
            ga.worker = a.worker;

            ga.stage = "Final stage"; 
            ArrayList genome = ga.Go(gene, true);

            double[] values;
            ga.GetBest(out values, out fitness);

            a.massf = values[0];
            a.currf = values[1];
            a.massfr = values[2];
            a.currfr = values[3];
            a.r2 = fitness;

            writeQuality(ga.stage);
            return genome;
        }


        public static double Fitness(double[] values)
        {

            a.massf = values[0];
            a.currf = values[1];
            a.massfr = values[2];
            a.currfr = values[3];

            double fit = run(w1, w2, w3, w4);
            if (Double.IsNaN(fit) || Double.IsInfinity(fit)) fit = 0;
            return fit; 
        }


        #endregion finishing

        #region electricals

        public static double R0Fitness(double[] values)
        {
            //  Assume that R0 and L0 are between (0,100)
            // the values were converted to units on the User Interface and normalized to 1 so that the GA algo can work
            // revert back to SI for the simulator
            //
            a.R0 = values[0] * 100 / 1000;
            a.L0 = values[1] * 100 * 1e-9;

            return run(w1,w2,w3,w4);
        }

        public static double ElectricalFitness(double[] values)
        {
            //  Assume that R0 and L0 are between (0,100)
            // the values were converted to units on the User Interface and normalized to 1 so that the GA algo can work
            // revert back to SI for the simulator
            //
            a.massf = values[0];
            a.currf = values[1];
            a.R0 = values[2] * 100 / 1000;
            a.L0 = values[3] * 100 * 1e-9;

            double fit = run(w1, w2, w3, w4);
            if (Double.IsNaN(fit) || Double.IsInfinity(fit)) fit = 0;
            return fit;
        }

        //
        //  Tune only R0 and L0
        //
        public static ArrayList evolveR0( int genomeSize, double[] start)
        {
            GA ga = new GA(electricalParams.crossover, electricalParams.mutation, electricalParams.population,
                            electricalParams.generations, electricalParams.elitism, genomeSize);

            ga.FitnessFunction = new GAFunction(ElectricalFitness);
 
            // normalized and scaled so that the values are between (0,1) for GA to work
            a.massf = start[0];     // start in metric units
            a.currf = start[1];

            a.R0 = start[4] * 1000 / 100;
            a.L0 = start[5] * 1e9 / 100;

            double[] gene = new double[] {a.massf, a.currf,  a.R0, a.L0 }; 

            reportProgress(0, "Tuning R0");
            GA.report = new ReportProgress(reportProgress);
            ga.worker = a.worker;
            ga.stage = "Tuning R0 stage";
 
            ArrayList genome = ga.Go(gene, false);

            double[] values;
            ga.GetBest(out values, out fitness);

            // un-normalized for the UI displays
            a.massf = values[0];// *100;
            a.currf = values[1];// *100;

            a.R0 = values[2] / 1000 * 100;
            a.L0 = values[3] * 1e-9 * 100;

            a.r2 = fitness;

            writeQuality(ga.stage);
            return genome;
        }
          
        #endregion  electricals


    }
}
