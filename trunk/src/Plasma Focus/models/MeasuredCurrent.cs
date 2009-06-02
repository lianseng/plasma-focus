/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       MeasuredCurrent.cs
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
using Plasma_Focus.models.fitting;
using System.Diagnostics;

namespace Plasma_Focus.models
{
    public class MeasuredCurrent
    {
        public CurrentReading[] array;

        public string dataFilename { get; set; }

        public SortedList<double, double> series { get; set; }

        public Metrics metrics;

        double currentTimeStep;  // time step of original current trace, big steps are a problem !

        public double endTime;   // where to stop processing
        public double midRiseTime;  // halfway up the peak
         
        public MeasuredCurrent()
        {

        }

        public MeasuredCurrent(String name)
        {
            dataFilename = name;
            series = new SortedList<double, double>();
            series.Clear();

            loadReferenceFile(name, series);
            array = getMeasuredCurrentArray(series);
            updateMeasuredMetrics(array); 
        }

        #region data file
        public void reload()
        {
            series = new SortedList<double, double>();
            series.Clear();

            loadReferenceFile(dataFilename, series);
            array = getMeasuredCurrentArray(series);
            updateMeasuredMetrics(array);
        }

      
        //
        // the data currents for some machine (UNU PFF Ar) lost their resolution and the time readings became duplicated
        public void handleDuplicates(ref StreamReader SR, SortedList<double, double> currents, ref double dup, ref double current)
        {
            List<double> duplicates = new List<double>();
            double time = 0, step = 0;

            duplicates.Add(current);

            while (!SR.EndOfStream)
            {
                string str = SR.ReadLine();
                if (str == null) continue;

                string[] words = str.Split(',');

                if (!double.TryParse(words[0], out time)) continue;
                if (!double.TryParse(words[1], out current)) continue;

                if (time == dup) duplicates.Add(current);
                else break;
            }

            step = (time - dup) / (duplicates.Count + 1);

            //          Debug.WriteLine(duplicates.Count + " duplicates  at time = " + dup);
            foreach (double duplicate in duplicates)
            {
                dup = dup + step;
                if (dup == time) break;    // overrun into next data point !
                try
                {
                    currents.Add(dup, duplicate);
                    //step += step;

                }
                catch (Exception)
                {
                    // ignore duplicates
                    // Debug.WriteLine("Duplicated reading");

                }
            }

            dup = time;  // return next reading

        }

        public bool Exists(string name)
        {

            try
            {
                string filename = System.IO.Path.GetFileName(name);
                string pathname = ConfigIniFile.modelDirectory + "//" + filename;
                return File.Exists(pathname);
            }
            catch (Exception e)
            {
            }
            return false;
        }

        public void loadReferenceFile(String name, SortedList<double, double> currents)
        {
            StreamReader SR;
            string str;

            if (name == null) return;

            try
            {
                SR = File.OpenText(name);


                double dup = -1e10;
                double prev = dup;

                while (!SR.EndOfStream)
                {
                    str = SR.ReadLine();
                    if (str == null) continue;

                    string[] words = str.Split(',');
                    double time, current;
                    if (!double.TryParse(words[0], out time)) continue;
                    if (!double.TryParse(words[1], out current)) continue;

                    if (time == dup)
                        handleDuplicates(ref SR, currents, ref time, ref current);
                    dup = time;

                    try
                    {
                        currents.Add(time, current);
                    }
                    catch (Exception)
                    {
                        // ignore duplicates
                        // Debug.WriteLine("Duplicated reading");

                    }

                }
                SR.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }
        #endregion data file

        #region filters
        //  Exponentially weighted moving average filterEWMA 
        //
        public void filterEWMA(ref CurrentReading[] data, int start, int end)
        {
            Debug.WriteLine("Exp. MA filter");
            double alpha = 0.7;
            for (int i = start + 1; i < end; i++)
                data[i].reading = alpha * data[i - 1].reading + (1 - alpha) * data[i].reading;

        }

        //   moving average filterEWMA 
        //
        public void filterMA(ref CurrentReading[] data, int start, int end)
        {
            Debug.WriteLine("MA filter");
            int i = 0;
            if (start == 0) start = 1;
            if (end > data.Length) end = data.Length;

            for (i = start + 1; i < end - 2; i++)
            {
                data[i].reading = (data[i - 1].reading + data[i].reading + data[i + 1].reading) / 3;
                // (data[i - 2].reading + data[i - 1].reading + data[i].reading + data[i + 1].reading + data[i + 2].reading) / 5;
            }


        }

        #endregion

        // calculate CV(RMSD)
        // 
        public static double calcSD(CurrentReading[] measured, int start, int end)
        {
            int j = 0, i = 0;
            double[] di = new double[end];
            double sum = 0; double sumDiff = 0;
            for (i = start; i < end; i++)
            {
                sum += measured[i].reading;
                sumDiff += Math.Abs(measured[i].reading - measured[i - 1].reading);
            }

            double mean = sum / (end - start);

            //for (i = start; i < end; i++)
            //{
            //    double p2 =  measured[i].reading-mean;
            //    sum += p2 * p2;
            //}

            double sigma = sumDiff / mean; // Math.Sqrt(sum) / (end - start)/mean;

            return sigma;

        }

        public void updateMeasuredMetrics(CurrentReading[] measured)
        {
            metrics = new Metrics();

            int peakIndex = MeasuredCurrent.findPeak(measured);

            CurrentReading peak = measured[peakIndex];
            metrics.peak = peak;
            metrics.indexPeak = peakIndex;

            
            //// pinch should be < 60% of peak current
            //int maxPinchIndex; double maxPinchTime = 0.6 * measured[metrics.indexPeak].time;
            //for (maxPinchIndex = metrics.indexPeak + 1; maxPinchIndex < measured.Length; maxPinchIndex++)
            //    if ((measured[maxPinchIndex].time - measured[metrics.indexPeak].time) > maxPinchTime)
            //        break;

            //// if (metrics.indexMax == data.Length)
            //if (maxPinchIndex == measured.Length) maxPinchIndex = measured.Length - 1;
            
            int maxPinchIndex = measured.Length - 1;
            Debug.WriteLine("Pinch max at " + measured[maxPinchIndex].time);

       //     metrics.sd = calcSD(measured, metrics.indexPeak, maxPinchIndex);
       //     Debug.WriteLine("SD " + metrics.sd);

            // find the maxPinchIndex of radial phase starting from peak location, add 3 to clear any bumps around peak
            int pinchPos = MeasuredCurrent.findPinch(measured, metrics.indexPeak + 3, maxPinchIndex);
            if (pinchPos == metrics.indexPeak + 1)
                return;

            metrics.indexPinch = pinchPos;
            metrics.pinch = measured[pinchPos];

            Debug.WriteLine("Pinch at " + measured[pinchPos].time + " current " + measured[pinchPos].reading);
            // return the time 1us after the end of the radiative phase

             int inx = metrics.indexPinch;
            // decide where the end of the curve should be, coarse traces are a problem...
            //double time1;
            //if (currentTimeStep >= 0.01)
            //    time1 = measured[inx].time + 0.05;
            //else
            //    time1 = measured[inx].time + 4 * currentTimeStep;

            //int i;
            //for (i = inx; i < measured.Length; i++)
            //{
            //    if (measured[i].time > time1) break;
            //}
            //endTime = measured[i].time;

            if (currentTimeStep >= 0.01)
                endTime = measured[inx +4].time;
            else if (currentTimeStep >= 0.005)
                endTime = measured[inx +5].time;
            else
                endTime = measured[inx + 8].time;
            int i;
            // calculate point at mid radial phase to match
            metrics.midRadialTime = measured[peakIndex].time + (measured[pinchPos].time - measured[peakIndex].time) / 2;
            for (i = peakIndex; i < measured.Length; i++)
            {
                if (measured[i].time > metrics.midRadialTime) break;
            }
        
            metrics.midRadialCurrent = measured[i].reading;

            metrics.radialSlope = (metrics.pinch.reading - metrics.midRadialCurrent) / (metrics.pinch.time - metrics.midRadialTime);

            // calculate a point to start measured R2
            midRiseTime = measured[0].time + (measured[peakIndex].time - measured[0].time) / 2;

            Debug.WriteLine("End time " + endTime + "  radial slope " + metrics.radialSlope +
                  " mid radial current " + metrics.midRadialCurrent + "  mid radial time: " + metrics.midRadialTime);
            return ;
        }

 
        public static int findPeak(CurrentReading[] measured)
        {
            int peakIndex = 0;
            double peak = 0, peakTime = 0;

            // get max current, current rise slope and current pinch time of measure current
            peak = -1;
            for (int i = 0; i < measured.Length; i++)
            {
                CurrentReading current = measured[i];

                if (current.reading > peak)
                {
                    peak = current.reading;
                    peakTime = current.time;
                    peakIndex = i;
                }
            }
            Debug.WriteLine("Peak at " + peakTime);
            return peakIndex;

        }

        // Find end of radial phase (~ pinch location) based on 2nd difference of data
        //
        public static int findPinch(CurrentReading[] measured, int start, int end)
        {
            double[] di = new double[end - start + 1];       // first difference of current
            //    double[] ddi = new double[end - start + 1];  // second difference

            int j = 0, i = 0;
            for (i = start + 1; i < end; i++)
            {
                di[j++] = measured[i].reading - measured[i - 1].reading;
            }

            //double prevDi = di[1] - di[0];
            int iPinch = 0; double pinchMax = 0;
            for (i = 2; i < j; i++)
            {
                // ddi[i]
                double dd = di[i] - di[i - 2];

                // Debug.WriteLine(measured[i + start + 1].time +" " +measured[i + start + 1].reading + " d " + dd);

                if (dd > pinchMax)  // minima are +ve
                {
                    iPinch = i;
                    pinchMax = dd;
                    Debug.WriteLine("pinch ? " + measured[iPinch + start + 1].time + "    d2 " + pinchMax);
                }

            }
            iPinch += start + 1;

            return (iPinch);
        }

        public CurrentReading[] getMeasuredCurrentArray(SortedList<double, double> data)
        {
            // remove all the negative values from data curve
            for (int c = 0; c < data.Count; c++)
            {
                if (data.Keys[c] < 0 || data.Values[c] < 0)
                {
                    data.Remove(data.Keys[c]);
                    c--;
                }
            }

            // copy to array
            List<CurrentReading> a = new List<CurrentReading>();
            foreach (KeyValuePair<double, double> d in data)
            {
                CurrentReading x = new CurrentReading(d.Key, d.Value);
                a.Add(x);
            }
            CurrentReading[] m = a.ToArray();

            //
            // put at each step, steps that are too large gives problems
            int len = data.Count;
            double step = currentTimeStep = (data.Keys[data.Count - 1] - data.Keys[0]) / len;

            //const double MINSTEP = 0.01;
            //if (step > MINSTEP)
            //    step = MINSTEP;

            if (ConfigIniFile.isFilterEnabled)
            {
                if (ConfigIniFile.filterType == FilterType.EWMA)
                    filterEWMA(ref m, 1, m.Length);     // exponential filterEWMA, causes a lag
                else
                {

                    filterMA(ref m, 1, m.Length);    // moving average, not very effective for noisy data                
                }
            }
            //if (step > 0.01) step = 0.01;

            int start = 0; double time = m[0].time;
            a.Clear();
            while (time < m[len - 1].time)
            {
                CurrentReading x = new CurrentReading(time, getReadingAtTime(m, time, ref start));
                a.Add(x);
                time += step;
                  //Debug.WriteLine(x.time+","+x.reading);
            }

            m = a.ToArray();

            return m;
        }

        // Calculates residual for the 2 plots
        // pass in numRows to restrict where you want the calculations to maxPinchIndex e.g. before phase 5
        public static double calcR2(CurrentReading[] measured, CurrentReading[] computed, 
            double startTime, double endTime)
        {
            double SSE = 0; int len = 0;
            double ysum = 0; int n = 0;

            

            foreach (CurrentReading d in measured)
            {
                if (d.time < startTime) continue;
                if (d.time >= endTime)
                    break;
                // get equivalent point on the computed curve
                double r = MeasuredCurrent.getReadingAtTime(computed, d.time, ref len);

                double diff = d.reading - r;
                ysum += d.reading;
                n++;

                SSE = SSE + diff * diff;
                //  Debug.WriteLine(  p2.time+"   "+y + "   " + p2.reading + "   " + String.Format("{0:#.#}", diff));
            }
            // average y 
            double ybar = ysum / n;

            double SST = 0;
            foreach (CurrentReading d in measured)
            {
                if (d.time < startTime) continue;
              
                if (d.time >= endTime)
                    break;
                SST += (d.reading - ybar) * (d.reading - ybar);
            }

            double R2 = 1 - SSE / SST;

            if (R2 > 1) R2 = 0;
            if (R2 < 0) R2 = 0;

            return R2;
        }

        // get vertical reading for each time
        public static double getReadingAtTime(CurrentReading[] measured, double time, ref int start)
        {
            double a = 0;

            // get next relevant point
            int j = start;
            while (measured[j].time < time)
            {
                j++;
                if (j == measured.Length)
                    return Double.NaN;
            }

            start = j;
            if (measured[start].time == time)
            {
                a = measured[start].reading;
            }
            else //if (data[start].time > time)
            {
                // do linear interpolation to get reading
                double y0 = 0, x0 = 0, c = 0;

                if (start == 0)
                {
                    y0 = measured[1].reading;
                    x0 = measured[1].time;
                    c = measured[0].reading;
                }
                else
                {
                    y0 = measured[start - 1].reading;
                    x0 = measured[start - 1].time;
                    c = y0;
                }
                double grad = (measured[start].reading - y0) / (measured[start].time - x0);

                a = c + grad * (time - x0);
            }
            return a;
        }


    }
}
