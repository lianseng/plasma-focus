/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       Metrics.cs
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

namespace Plasma_Focus.models
{
    public class CurrentReading
    {

        public double time { get; set; }
        public double reading { set; get; }

        public CurrentReading(double t, double r)
        {
            time = t;
            reading = r;
        }


        #region linear fit
        double linearFit(CurrentReading[] data, out double slope, out double intercept)
        {
            int n = data.Length;
            double sumX = 0.0;
            double sumY = 0.0;
            double sumXX = 0.0;
            double sumXY = 0.0;
            double sumYY = 0.0;

            foreach (CurrentReading d in data)
            {
                double x = d.time;
                double y = d.reading;

                sumX += x;
                sumY += y;
                sumXX += x * x;
                sumXY += x * y;
                sumYY += y * y;
            }

            double xMean = sumX / n;
            double yMean = sumY / n;

            slope = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
            intercept = yMean - slope * xMean;

            // Calculate the accuracy of this model 
            double RNumerator = (n * sumXY) - (sumX * sumY);
            double RDenom = (n * sumXX - sumX * sumX) * (n * sumYY - sumY * sumY);
            double dblR = RNumerator / Math.Sqrt(RDenom);
            return dblR * dblR;
        }

        #endregion
    }


    public class Metrics
    {
        public double sd;
        public int indexPeak;            // index of peak in data curve
        public CurrentReading peak;

        public int indexPinch;            // index of pinch
        public double maxPinchTime;       // max of all computed pinch time, used in fitting
        public CurrentReading pinch;

        public double midRadialTime;
        public double midRadialCurrent;

        public double radialSlope;

        public static double finalPeakDiff, finalPeakTimeDiff, finalPinchDiff, finalPinchTimeDiff, finalRadialSlope;

        #region metrics

        public static double peakDiff(Metrics measuredMetrics, Metrics computedMetrics)
        {
            finalPeakDiff = Math.Abs((measuredMetrics.peak.reading - computedMetrics.peak.reading) / (measuredMetrics.peak.reading - measuredMetrics.pinch.reading));
            if (finalPeakDiff > 1)
                finalPeakDiff = 1;
            return finalPeakDiff;
        }

        public static double peakTimeDiff(Metrics measuredMetrics, Metrics computedMetrics)
        {
             finalPeakTimeDiff = Math.Abs((measuredMetrics.peak.time - computedMetrics.peak.time) / (measuredMetrics.peak.time- measuredMetrics.pinch.time));
             if (finalPeakTimeDiff > 1)
                 finalPeakTimeDiff = 1;
             return finalPeakTimeDiff;            
        }

        public static double pinchTimeDiff(Metrics measuredMetrics, Metrics computedMetrics)
        {
            if (measuredMetrics.pinch == null)
                return 0;
            finalPinchTimeDiff = Math.Abs((measuredMetrics.pinch.time - computedMetrics.pinch.time) / (measuredMetrics.peak.time- measuredMetrics.pinch.time));
            if (finalPinchTimeDiff > 1)
                finalPinchTimeDiff = 1;
            return finalPinchTimeDiff;

  
        }

        public static double pinchDiff(Metrics measuredMetrics, Metrics computedMetrics)
        {
            if (measuredMetrics.pinch == null)
                return 0;
            finalPinchDiff = Math.Abs((measuredMetrics.pinch.reading - computedMetrics.pinch.reading) / (measuredMetrics.peak.reading -measuredMetrics.pinch.reading));
            if (finalPinchDiff > 1)
                finalPinchDiff = 1;
            return finalPinchDiff;
        }

        public static double radialSlopeDiff(Metrics measuredMetrics, Metrics computedMetrics)
        {
            finalRadialSlope = Math.Abs((Math.Abs(measuredMetrics.radialSlope) - Math.Abs(computedMetrics.radialSlope)) / measuredMetrics.radialSlope);
            if (finalRadialSlope > 1)
                finalRadialSlope = 1;
            return finalRadialSlope;
        }
        #endregion
    
    }
}
