/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       KS-Test.cs
 *  Author:     ADS\lohl1
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


    //
    //  Translated from http://root.cern.ch/root/html/src/TMath.cxx.html
    //  License :http://root.cern.ch/drupal/content/license
    //
    public class KS
    {
        public void toEmpiricalDistributionDistribution(double[] data, double[] edf, double tmax, double dt)
        {
            for (double t = 0; t < tmax; t += dt)
            {
            }
        }

        public double KolmogorovProb(double z)
        {
            // Calculates the Kolmogorov distribution function,
            //Begin_Html
            /*
            <img src="gif/kolmogorov.gif">
            */
            //End_Html
            // which gives the probability that Kolmogorov's test statistic will exceed
            // the value z assuming the null hypothesis. This gives a very powerful
            // test for comparing two one-dimensional distributions.
            // see, for example, Eadie et al, "statistocal Methods in Experimental
            // Physics', pp 269-270).
            //
            // This function returns the confidence level for the null hypothesis, where:
            //   z = dn*sqrt(n), and
            //   dn  is the maximum deviation between a hypothetical distribution
            //       function and an experimental distribution with
            //   n    events
            //
            // NOTE: To compare two experimental distributions with m and n events,
            //       use z = sqrt(m*n/(m+n))*dn
            //
            // Accuracy: The function is far too accurate for any imaginable application.
            //           Probabilities less than 10^-15 are returned as zero.
            //           However, remember that the formula is only valid for "large" n.
            // Theta function inversion formula is used for z <= 1
            //
            // This function was translated by Rene Brun from PROBKL in CERNLIB.

            double[] fj = { -2, -8, -18, -32 };
            double[] r = new double[4]; 

            const double w = 2.50662827;
            // c1 - -pi**2/8, c2 = 9*c1, c3 = 25*c1
            const double c1 = -1.2337005501361697;
            const double c2 = -11.103304951225528;
            const double c3 = -30.842513753404244;

            double u = Math.Abs(z);
            double p;
            if (u < 0.2)
            {
                p = 1;
            }
            else if (u < 0.755)
            {
                double v = 1.0 / (u * u);
                p = 1 - w * (Math.Exp(c1 * v) + Math.Exp(c2 * v) + Math.Exp(c3 * v)) / u;
            }
            else if (u < 6.8116)
            {
                r[1] = 0;
                r[2] = 0;
                r[3] = 0;
                double v = u * u;
                int maxj = (int)Math.Max(1, Math.Round(3.0 / u));
                for (int j = 0; j < maxj; j++)
                {
                    r[j] = Math.Exp(fj[j] * v);
                }
                p = 2 * (r[0] - r[1] + r[2] - r[3]);
            }
            else
            {
                p = 0;
            }
            return p;
        }
 
        double KolmogorovTest(double[] a, double[] b, out double rdmax)
        {
            //  Statistical test whether two one-dimensional sets of points are compatible
            //  with coming from the same parent distribution, using the Kolmogorov test.
            //  That is, it is used to compare two experimental distributions of unbinned data.
            //
            //  Input:
            //  a,b: One-dimensional arrays of length na, nb, respectively.
            //       The elements of a and b must be given in ascending order.
            //  option is a character string to specify options
            //         "D" Put out a line of "Debug" printout
            //         "M" Return the Maximum Kolmogorov distance instead of prob
            //
            //  Output:
            // The returned value prob is a calculated confidence level which gives a
            // statistical test for compatibility of a and b.
            // Values of prob close to zero are taken as indicating a small probability
            // of compatibility. For two point sets drawn randomly from the same parent
            // distribution, the value of prob should be uniformly distributed between
            // zero and one.
            //   in case of error the function return -1
            //   If the 2 sets have a different number of points, the minimum of
            //   the two sets is used.
            //
            // Method:
            // The Kolmogorov test is used. The test statistic is the maximum deviation
            // between the two integrated distribution functions, multiplied by the
            // normalizing factor (rdmax*sqrt(na*nb/(na+nb)).
            //
            //  Code adapted by Rene Brun from CERNLIB routine TKOLMO (Fred James)
            //   (W.T. Eadie, D. Drijard, F.E <../TMath.html#TMath:E>. James, M. Roos and B. Sadoulet,
            //      Statistical Methods in Experimental Physics, (North-Holland,
            //      Amsterdam 1971) 269-271)
            //
            //  Method Improvement by Jason A Detwiler (JADetwiler@lbl.gov)
            //  -----------------------------------------------------------
            //   The nuts-and-bolts of the Math.KolmogorovTest <../TMath.html#TMath:KolmogorovTest>() algorithm is a for-loop
            //   over the two sorted arrays a and b representing empirical distribution
            //   functions. The for-loop handles 3 cases: when the next points to be
            //   evaluated satisfy a>b, a<b, or a=b:
            //
            //      for (int j=0;j<na+nb;j++) {
            //         if (a[ia-1] < b[ib-1]) {
            //            rdiff -= sa;
            //            ia++;
            //            if (ia > na) {ok = true; break;}
            //         } else if (a[ia-1] > b[ib-1]) {
            //            rdiff += sb;
            //            ib++;
            //            if (ib > nb) {ok = true; break;}
            //         } else {
            //            rdiff += sb - sa;
            //            ia++;
            //            ib++;
            //            if (ia > na) {ok = true; break;}
            //            if (ib > nb) {ok = true; break;}
            //        }
            //         rdmax = Math.Max(rdmax,Math.Abs <../TMath.html#TMath:Abs>(rdiff));
            //      }
            //
            //   For the last case, a=b, the algorithm advances each array by one index in an
            //   attempt to move through the equality. However, this is incorrect when one or
            //   the other of a or b (or both) have a repeated value, call it x. For the KS
            //   statistic to be computed properly, rdiff needs to be calculated after all of
            //   the a and b at x have been tallied (this is due to the definition of the
            //   empirical distribution function; another way to convince yourself that the
            //   old CERNLIB method is wrong is that it implies that the function defined as the
            //   difference between a and b is multi-valued at x -- besides being ugly, this
            //   would invalidate Kolmogorov's theorem).
            //
            //   The solution is to just add while-loops into the equality-case handling to
            //   perform the tally:
            //
            //         } else {
            //            double  x = a[ia-1];
            //            while(a[ia-1] == x && ia <= na) {
            //              rdiff -= sa;
            //              ia++;
            //            }
            //            while(b[ib-1] == x && ib <= nb) {
            //              rdiff += sb;
            //              ib++;
            //            }
            //            if (ia > na) {ok = true; break;}
            //            if (ib > nb) {ok = true; break;}
            //         }
            //
            //  NOTE1
            //  A good description of the Kolmogorov test can be seen at:
            //    http://www.itl.nist.gov/div898/handbook/eda/section3/eda35g.htm

    
            double prob = -1;
            //      Require at least two points in each graph
            if (a.Length <= 2 || b.Length <= 2)
            {
                Console.WriteLine("KolmogorovTest", "Sets must have more than 2 points");
                rdmax = -1;
                return prob;
            }
            //     Constants needed
            double rna = a.Length;
            double rnb = b.Length;
            double sa = 1.0 / rna;
            double sb = 1.0 / rnb;
            double rdiff;
            int ia, ib;
            //     Starting values for main loop
            if (a[0] < b[0])
            {
                rdiff = -sa;
                ia = 2;
                ib = 1;
            }
            else
            {
                rdiff = sb;
                ib = 2;
                ia = 1;
            }
            rdmax = Math.Abs(rdiff);

            //    Main loop over point sets to find max distance
            //    rdiff is the running difference, and rdmax the max.
            bool ok = false;
            for (int i = 0; i < a.Length + b.Length; i++)
            {
                if (a[ia - 1] < b[ib - 1])
                {
                    rdiff -= sa;
                    ia++;
                    if (ia > a.Length) { ok = true; break; }
                }
                else if (a[ia - 1] > b[ib - 1])
                {
                    rdiff += sb;
                    ib++;
                    if (ib > b.Length) { ok = true; break; }
                }
                else
                {
                    double x = a[ia - 1];
                    while (a[ia - 1] == x && ia <= a.Length)
                    {
                        rdiff -= sa;
                        ia++;
                    }
                    while (b[ib - 1] == x && ib <= b.Length)
                    {
                        rdiff += sb;
                        ib++;
                    }
                    if (ia > a.Length) { ok = true; break; }
                    if (ib > b.Length) { ok = true; break; }
                }
                rdmax = Math.Max(rdmax, Math.Abs(rdiff));
            }
            //    Should never terminate this loop with ok = false!

            if (ok)
            {
                rdmax = Math.Max(rdmax, Math.Abs(rdiff));
                double z = rdmax * Math.Sqrt(rna * rnb / (rna + rnb));
                prob = KolmogorovProb(z);
            }
            // debug printout

            
            return prob;
        }
    }


}
