/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       CoronaModel.cs
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
    public class CoronaModel
    {
        // TM - plasma temperature
        // ZN = atomic number
        // z - effective charge
        // g - specific heat ratio        

        public static void calculateGZ(double TM, double ZN, out double z, out double g) {

            z = 0; g = 1.66667;

            if (ZN==1 || ZN==2) {

                g = 1.66667;
                z = ZN;

            } else if (ZN==18) {
                // Table of G for ARGON; pre-calculated from Corona Model

                if (TM > 1.5E8)
                    g = 1.66667;
                else if (TM > 1.2E7)
                    g = 1.54 + 9 * (1e-10) * (TM - 1.2E7);
                else if (TM > 1.9E6)
                    g = 1.31 + 2.6E-8 * (TM - 1.9E6);
                else if (TM > 9.3E5)
                    g = 1.3;
                else if (TM > 5.7E5)
                     g = 1.34 - 1.6 * (1e-7) * (TM - 5.7E5);
                else if (TM > 1E5)
                    g = 1.17 + 3.8 * (1e-7) * (TM - 1E5);
                else if (TM > 1.3E4)
                    g = 1.15 + 2.3 * (1e-7) * (TM - 1.3E4);   
                else if (TM > 9000)
                    g = 1.66667 - 1.29 * (1e-4) * (TM - 9000);
            
                // Table for Z for Argon, pre-calculated from Corona Model

                if (TM > 1.3E8)
                    z = 18;
                else if (TM > 1.3E7)
                    z = 16 + 1.8 * (1e-8) * (TM - 1.3E7);
                else if (TM > 3.5E6)
                    z = 16;
                else if (TM > 4.7E5)
                    z = 8 + 2.9 * (1e-6) * (TM - 4.7E5);
                else if (TM > 2E5)
                    z = 8;
                else if (TM > 1.9E4)
                    z = 1 + 3.7 * (1e-5) * (TM - 1.9E4);
                else if (TM > 1.4E4)
                    z = 1;
                else if (TM > 9000)
                    z = 0.0002 * (TM - 9000);
                else z = 0;
                
            
            } else if (ZN==10) {
                // Table for G, for Neon, pre-calculated from Corona Model

                if (TM > 1E8)  
                        g = 1.6667;
                else if (TM > 2E7) 
                    g = 1.6 + 0.83E-9 * (TM - 2E7);
                else if(TM > 4.5E6) 
                    g = 1.47 + 0.84E-8 * (TM - 4.5E6);
                else if (TM > 2.3E6)  
                    g = 1.485;
                else if (TM > 3.4E5) 
                    g = 1.23 + 1.2E-7 * (TM - 3.4E5);
                else if (TM > 2.4E4)  
                    g = 1.15 + 2.22E-7 * (TM - 2.4E4);
                else if (TM > 1.7E4) 
                    g = 1.66667 - 0.0000767 * (TM - 10000);
                else if (TM > 1E4)  
                    g = 1.66667;
                else  g = 1.66667;

                // Table for Z for Neon, pre-calculated from Corona Model

                if  (TM > 7E6) 
                    z = 10;
                else if (TM > 2.3E6)
                     z = 8 + 0.4255 * (1e-6) * (TM - 2.3E6);
                else if (TM > 4.5E5)
                     z = 8;
                else if (TM > 4.5E4)
                    z = 1.9 + 1.5 * (1e-5) * (TM - 4.5E4);
                else if (TM > 15000)
                     z = 0.000063 * (TM - 15000);
                else
                    z = 0;

            } else if (ZN==54) {

                double TeV = TM / (1.16E4);

                //Table of G for Xenon; pre-calculated from Corona Model

                if (TM > 9 * 10E10) 
                    g = 1.66667;
                if (TM > 1.16E9) 
                     g = 0.0053 * Math.Log(TeV) / Math.Log(10) + 1.631;
                if (TM > 1.01E8)
                     g = 0.063 * Math.Log(TeV) / Math.Log(10) + 1.342;   
                if (TM > 2.02E7)
                    g = 0.166 * Math.Log(TeV) / Math.Log(10) + 0.936;
                if (TM > 6.23E6)
                    g = 0.096 * Math.Log(TeV) / Math.Log(10) + 1.163;
                if (TM > 9.4E5)
                    g = 0.1775 * Math.Log(TeV) / Math.Log(10) + 0.9404;
                if (TM > 3.3E5)
                    g = 1.27;
                if (TM > 6E4)
                    g = 0.122 * Math.Log(TeV) / Math.Log(10) + 1.093;
                if (TM > 1.2E4)
                    g = 1.17;
                if (TM > 8E3)
                    g = -2.624 * Math.Log(TeV) / Math.Log(10) + 1.229;
 

                // Table for Z for Xenon, pre-calculated from Corona Model

                if (TM > 9E10)
                    z = 54;
                else if (TM > 2.85E8)
                    z = 1.06 * Math.Log(TeV) / Math.Log(10) + 46.4;
                else if (TM > 8.8E7)
                    z = 10.72 * Math.Log(TeV) / Math.Log(10) + 3.99;
                else if (TM > 2.11E7)
                    z = 5.266 * Math.Log(TeV) / Math.Log(10) + 25.3;
                else if (TM > 5.68E6)
                    z = 25.23 * Math.Log(TeV) / Math.Log(10) - 40;
                else if (TM > 3.35E6)
                    z = 9.53 * Math.Log(TeV) / Math.Log(10) + 2.326;
                else if (TM > 2.37E5)
                    z = 15.39 * Math.Log(TeV) / Math.Log(10) - 12.1;
                else if (TM > 10000)
                    z = 5.8 * Math.Log(TeV) / Math.Log(10) + 0.466;
                else z = 0;
            
            }
             
        }

        public static void calculateGZRadiativePhase(double TM, double ZN, out double z, out double g, double TeV)
        {
            // Select Table for G & Z; according to which gas is used

            z = 0; g = 1.66667;

            if (ZN == 1 || ZN == 2)
            {

                g = 1.66667;
                z = ZN;
                //    G1 = 2 / (g + 1);
                //    G2 = (g - 1) / g;
            }
            else if (ZN == 18)
            {
                // Table of G for ARGON

                if (TM > 1.5 * 1e8)
                    g = 1.66667;
                else if (TM > 1.2 * 1e7)
                    g = 1.54 + 9e-10 * (TM - 1.2e7);
                else if (TM > 1.9 * 1e6)
                    g = 1.31 + 2.6e-8 * (TM - 1.9e6);
                else if (TM > 9.3 * 1e5)
                    g = 1.3;
                else if (TM > 5.7 * 1e5)
                    g = 1.34 - 1.6e-7 * (TM - 5.7e5);
                else if (TM > 1e5)
                    g = 1.17 + 3.8e-7 * (TM - 1e5);

                // Table for Z for Argon
                if (TM > 1.3 * 1e8)
                    z = 18;
                else if (TM > 1.3e7)
                    z = 16 + 1.8 * (1e-8) * (TM - 1.3e7);
                else if (TM > 3.5e6)
                    z = 16;
                else if (TM > 4.7 * 1e5)
                    z = 8 + 2.9 * (1e-6) * (TM - 4.7e5);
                else if (TM > 2 * 1e5)
                    z = 8;
                else if (TM > 3.5e4)
                    z = 2.2 + 3.5e-5 * (TM - 3.5e4);

            }
            else if (ZN == 10)
            {

                // Table for G for Neon

                if (TM > 1e8)
                    g = 1.66667;
                else if (TM > 2e7)
                    g = 1.6 + 0.83e-9 * (TM - 2e7);
                else if (TM > 4.5 * 1e6)
                    g = 1.47 + 0.84 * (1e-8) * (TM - 4.5 * 1e6);
                else if (TM > 2.3 * 1e6)
                    g = 1.485;
                else if (TM > 3.4 * 1e5)
                    g = 1.23 + 1.2 * (1e-7) * (TM - 3.4 * 1e5);
                else if (TM > 2.4 * 1e4)
                    g = 1.18;
                else if (TM > 1.5 * 1e4)
                    g = 1.667 - 0.000054 * (TM - 15000);


                // Table for Z for Neon
                if (TM > 7 * 1e6)
                    z = 10;
                else if (TM > 2.3 * 1e6)
                    z = 8 + 0.4255 * (1e-6) * (TM - 2.3 * 1e6);
                else if (TM > 4.5 * 1e5)
                    z = 8;
                else if (TM > 4.5 * 1e4)
                    z = 1.9 + 1.5 * (1e-5) * (TM - 4.5 * 1e4);
                else if (TM > 15000)
                    z = 0.000063 * (TM - 15000);
                else
                    z = 0;


            }
            else if (ZN == 54)
            {
                // Table of G for Xenon; pre-calculated from Corona Model

                if (TM > 9 * 1e10)
                    g = 1.66667;
                else if (TM > 1.16 * 1e9)
                    g = 0.0053 * Math.Log(TeV) / Math.Log(10) + 1.631;
                else if (TM > 1.01 * 1e8)
                    g = 0.063 * Math.Log(TeV) / Math.Log(10) + 1.342;
                else if (TM > 2.02 * 1e7)
                    g = 0.166 * Math.Log(TeV) / Math.Log(10) + 0.936;
                else if (TM > 6.23 * 1e6)
                    g = 0.096 * Math.Log(TeV) / Math.Log(10) + 1.163;
                else if (TM > 9.4 * 1e5)
                    g = 0.1775 * Math.Log(TeV) / Math.Log(10) + 0.9404;
                else if (TM > 3.3 * 1e5)
                    g = 1.27;
                else if (TM > 6 * 1e4)
                    g = 0.122 * Math.Log(TeV) / Math.Log(10) + 1.093;
                else if (TM > 1.2 * 1e4)
                    g = 1.17;
                else if (TM > 8 * 1e3)
                    g = -2.624 * Math.Log(TeV) / Math.Log(10) + 1.229;

                // Table for Z for Xenon, pre-calculated from Corona Model

                if (TM > 9 * 1e10)
                    z = 54;
                else if (TM > 2.85 * 1e8)
                    z = 1.06 * Math.Log(TeV) / Math.Log(10) + 46.4;
                else if (TM > 8.8 * 1e7)
                    z = 10.72 * Math.Log(TeV) / Math.Log(10) + 3.99;
                else if (TM > 2.11 * 1e7)
                    z = 5.266 * Math.Log(TeV) / Math.Log(10) + 25.3;
                else if (TM > 5.68 * 1e6)
                    z = 25.23 * Math.Log(TeV) / Math.Log(10) - 40;
                else if (TM > 3.35 * 1e6)
                    z = 9.53 * Math.Log(TeV) / Math.Log(10) + 2.326;
                else if (TM > 2.37 * 1e5)
                    z = 15.39 * Math.Log(TeV) / Math.Log(10) - 12.1;
                else if (TM > 10000)
                    z = 5.8 * Math.Log(TeV) / Math.Log(10) + 0.466;
                else
                    z = 0;


            }
        }
    }
}
