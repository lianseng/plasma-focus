/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       PlasmaFocusMachine.cs
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
using System.Diagnostics;

namespace Plasma_Focus.models
{
		
	/// </summary>
	/// 
	public class PlasmaFocusMachine
    {
        #region properties

        /// Machine parameters
        /**
             G = specific heat ratio
             RADB = OUTER RADIUS (in m, for calculations in real quantities)
             RADA = INNER RADIUS (in m,  ditto )
             Z0   = LENGTH OF ANODE (in m, ditto)
             C    = RADB/RADA
             F    = Z0/RADA
             L0   = CIRCUIT STRAY INDUCTANCE (in HENRY, ditto)
             C0   = ENERGY STORAGE CAPACITANCE (in FARAD, ditto)
             AL= capacitor time T0/TA, axial run-down time
             BE= full axial phase inductance LZ0/L0
             MASSF= REDUCED MASS FACTOR DUE TO MASS SHEDDING
             currf= REDUCED CURRENT FACTOR DUE TO CURRENT SHEDDING
             R0 IS STRAY CIRCUIT RESISTANCE IN OHM; RESF=R0/Surge Impedance
             Z1 is maxPinchIndex position, to maxPinchIndex calculation of phase 5;
             MASSF, MASSFR are incorporated in TA & AL & in AA;
             currf IS INcorporated into BE
             For Phase I calculations, T = Time, Z = Axial position (normalized to Zo)
             I = Current, ZZ = Speed, AC = Acceleration
             II = Current derivative, I0 = Current Intergral, all normalized
             HOWEVER ALL QUANTITIES WITH An R ATTACHED, HAVE BEEN RE-COMPUTED TO GIVE LABORATORY VALUES; 
              e.g. TR IS TIME RE-COMPUTED IN MICROSEC;IR IS CURRENT RE-COMPUTED IN kA AND SO ON.
             D = Time increment, V = tube voltage, all normalized
        */

        public string configFile { get; set; }    // ini file name

         public string machineName { get; set; }         // machine name

         public double r2 { get; set; }         // residue error from fit

        // geometry		
        public double RADA { get; set; }  	            // Ra(m)
        public double RADB { get; set; }		        // Rb(m)
        public double z0 { get; set; }			        // z0(m)

        // capacitor bank parameters
        public double L0 { get; set; }					// circuit stray inductance (H)
        public double C0 { get; set; }					// energy storage capacitance (F)
        public double R0 { get; set; }					// STRAY CIRCUIT RESISTANCE IN OHM{ get; set; } RESF=R0/Surge Impedance
        public double V0 { get; set; }					// voltage

        // operation parameters
        public double P0 { get; set; }					// pressure of gas 
        public String fillGas { get; set; }				// gas used
        public double MW { get; set; }                    // molecular weight
        public double ZN { get; set; }                     // atomic weight
        public double RHO { get; set; }                   // density
        public double dissociatenumber { get; set; }      // dissociation number

        // taper parameters
        public bool TAPER { get; set; }
        public bool isTapered() { return TAPER == true; }

        public double TAPERSTART { get; set; }        // taper start position, cm
        public double ENDRAD { get; set; }            // taper maxPinchIndex radius, cm

        public double zTAPERSTART { get; set; }
        public double tapergrad { get; set; }         // normalized taper position

        
        public double tc5 { get; set; }                // params for taper machine, = 1 for non-tapered
         // ratios
        public double N0 { get; set; }                    // ambient number density		
        public double C { get; set; }			            // anode radius ratio
        public double f { get; set; }			            // anode length/Radius


        // characteristic parameters
        public double T0 { get; set; }                  // see page 3, 2 Pi T0 = charactreristic discharge cycle time
        public double I0 { get; set; }                  // current integral  I0 = V0/Z0
        public double TA { get; set; }                  // page 4, characteristic axial transit time, ta

        public double ZZCHAR { get; set; }              // characteristic axial transit speed, Va
        public double AL { get; set; }                  // first scaling parameter, dimensionless. capacitor time T0/TA, axial run-down time

        public double AA { get; set; }                  // scaling factor,alpha1 pg 10 MASSF, MASSFR are incorporated in TA & AL & in AA{ get; set; }currf IS INcorporated into BE   
        public double AAg { get; set; }                 // alpha normalized by g
        public double AAg1 { get; set; }                // for tapered case

        public double RESF { get; set; }
        public double BE { get; set; }                  // full axial phase inductance LZ0/L0,  page 5,  beta = La/L0, second scaling parameter. 
        public double BF { get; set; }
        public double VPINCHCH { get; set; }
        public double TPINCHCH { get; set; }
        public double DREAL { get; set; }
        public double ALT { get; set; }                 // ratio of characteristic capacitor time to sum of characteristic axial & radial times		

        //double alpha;                                   // capacitor time T0/TA, axial run-down time

        // machine parameters
        public double massf { get; set; }              //REDUCED MASS FACTOR DUE TO MASS SHEDDING, axial phase
        public double currf { get; set; }              // REDUCED CURRENT FACTOR DUE TO CURRENT SHEDDING  
        public double massfr { get; set; }             // REDUCED MASS FACTOR DUE TO MASS SHEDDING, radial phase
        public double currfr { get; set; }
        public double CurrentFactorRatio;               // ratio between axial and radial current factors, currf, currfr

        public double g { get; set; }
        public double G1 { get; set; }
        public double G2 { get; set; }
        public double GCAP { get; set; }

        // various derived parameters for tapered machine
        public double AL1 { get; set; }                 // alpha, ratio of T0/TA
      //  public double w1 { get; set; }
        public double c2  { get; set; }
        public double AA1 { get; set; }
     //   public double LOG2 { get; set; }

        
        // current plot data from actual machine
        public MeasuredCurrent currentData { get; set; }

        #endregion

        public PlasmaFocusMachine()
		{
         
		}

        void reset()
        {
            tc5 = AAg1 = N0 = C =  f = T0 = I0 = TA = ZZCHAR = AL = AA = AAg = RESF = BE =
            BF = VPINCHCH = TPINCHCH = DREAL = ALT = CurrentFactorRatio = g = G1 = G2 =GCAP = AL1 = c2 = AA1 = 0; 
        }

		public void initialize() {
            reset();
            // Calculate ambient number density and some ratios
            //R0 = R0 / 1000;

            // If operating in Deuterium, set value of G
            if (ZN == 1 || ZN == 2)
            {
                // Deuterium values of G
                g = 1.66667;
                G1 = 2 / (g + 1);
                G2 = (g - 1) / g;
                GCAP = (g + 1) / (g - 1);
            }
            else
            {
                // If Ne or argon or Xenon, set approx initial values of G
                g = 1.3;
                G1 = 2 / (g + 1);
                G2 = (g - 1) / g;
                GCAP = (g + 1) / (g - 1);
            }

            // Calculate ambient number density and some ratios
            N0 = 2.69e25 * P0 / 760;
            C = RADB / RADA;
            f = z0 / RADA;

            // Convert to SI units
            //C0 = C0 * 1e-6;
            //L0 = L0 * 1e-9;
            //RADB = RADB * 0.01;
            //RADA = RADA * 0.01;
            //z0 = z0 * 1e-2;
            //V0 = V0 * 1000;
            RHO = P0 * 2.33e-4 * MW / 4;
            ////

            if (isTapered())
            { 
                //TAPERSTART = TAPERSTART * 0.01;
                //ENDRAD = ENDRAD * 0.01;
                zTAPERSTART = TAPERSTART / z0;
                tapergrad = (RADA - ENDRAD) / (z0 - TAPERSTART);
            }
            //				
            // Calculate characteristic quantities and scaling parameters
            T0 = Math.Sqrt(L0 * C0);               // see page 3, 2 Pi T0 = charactreristic discharge cycle time
            I0 = V0 / Math.Sqrt(L0 / C0);          // current integral  I0 = V0/Z0

            // page 4, characteristic axial transit time, ta
            TA = Math.Sqrt(4 * Constants.Pi * Constants.Pi * (C * C - 1) / (Constants.MU * Math.Log(C)))
                           * (z0 * Math.Sqrt(RHO) / (I0 / RADA)) * (Math.Sqrt(massf) / currf);
            
            ZZCHAR = z0 / TA;            // characteristic axial transit speed, Va
            AL = T0 / TA;                // first scaling parameter, dimensionless. capacitor time T0/TA, axial run-down time

            // MASSF, MASSFR are incorporated in TA & AL & in AA; currf IS INcorporated into BE, alpha1 squared, pg 10
            AA = Math.Sqrt((g + 1) * (C * C - 1) / Math.Log(C)) * (f / 2) * (Math.Sqrt(massf / massfr)) * (currfr / currf);
            RESF = R0 / (Math.Sqrt(L0 / C0));

            // full axial phase inductance LZ0/L0,  page 5,  beta = La/L0, second scaling parameter   
            BE = 2e-7 * Math.Log(C) * z0 * currf / L0;
            BF = BE / (Math.Log(C) * f);
            VPINCHCH = ZZCHAR * AA / f;
            TPINCHCH = RADA / VPINCHCH;

            // Calculate ratio of characteristic capacitor time to sum of characteristic axial & radial times
            ALT = (AL * AA) / (1 + AA);

            tc5 = 1;

		}
		
        
         public void validateMachineParameters() {
             // pressure too high
             if (P0 >= 20)
             {
                 Debug.WriteLine( " WARNING! In real experiments, Pressure above 20 torr will not produce focussing"); 
                // MessageBox ("WARNING! In real experiments, Pressure above 20 torr will not produce focussing\nACTION RECOMMENDED: REDUCE FILL PRESSURE below 20 torr",
                 //    "Invalid configuration", 
                 throw new ApplicationException ("Invalid configuration: in real experiments, ressure above 20 torr will not produce focussing");

             }
            
            if (ZN == 1 ||  ZN == 2) {

             if (P0 < 0.1)  
               throw new ApplicationException("WARNING! In real experiments, Pressure below 0.1 torr does not yield good focus");
                 //Debug.Print "ACTION RECOMMENDED: INCREASE FILL PRESSURE above 0.1 torr"
                 //462 Stop
                 //Debug.Print "WARNING! In real experiments in D2, Pressure below 0.1 torr does not yield good focus"
                 //Debug.Print "ACTION RECOMMENDED: INCREASE FILL PRESSURE above 0.1 torr"
                 //Debug.Print "Click on red cross on top right hand corner to return to spread sheet"

            else if(ALT > 0.68) return;
                 // throw new ApplicationException("Total TRANSIT TIME (axial + radial) MAY BE TOO LONG COMPARED TO effective DISCHARGE Drive TIME");

                 // WARNING! Total TRANSIT TIME (axial + radial) MAY BE TOO LONG COMPARED TO effective DISCHARGE Drive TIME
                //  FOLLOWING ACTION RECOMMENDED:
                //                               REDUCE FILL PRESSURE  OR
                //                               INCREASE CHARGE VOLTAGE  OR
                //                               SHORTEN AXIAL LENGTH
                //  It may also be necessary to check that you have not unreasonably reduced the value of C or L or
                //  unreasonably increased the value of radius or length
                // You may attempt to OVER-RIDE this stop; go to run; continue
            }
 
            if ( ZN==7 || ZN==10 || ZN==18 || ZN==36 || ZN==54 )
            {
                if (P0 < 0.05)
                    throw new ApplicationException("WARNING! In real experiments in neon or argon or xenon, Pressure below 0.05 torr may not yield good focus");
                //Debug.Print "WARNING! In real experiments in neon or argon or xenon, Pressure below 0.05 torr may not yield good focus"
                //Debug.Print "ACTION RECOMMENDED: INCREASE FILL PRESSURE above 0.05 torr"
                //Debug.Print "Click on red cross on top right hand corner to return to spread sheet"
                else if ( ALT > 0.65) return;

            }
            throw new ApplicationException("Total TRANSIT TIME (axial + radial) MAY BE TOO LONG COMPARED TO effective DISCHARGE Drive TIME");
       
            //Stop
            // WARNING! Total TRANSIT TIME (axial + radial) MAY BE TOO LONG COMPARED TO effective DISCHARGE Drive TIME
            //  FOLLOWING ACTION RECOMMENDED:
            //                               REDUCE FILL PRESSURE  OR
            //                               INCREASE CHARGE VOLTAGE  OR
            //                               SHORTEN AXIAL LENGTH
            //  It may also be necessary to check that you have not unreasonably reduced the value of C or L or
            //  unreasonably increased the value of radius or length
            // You may attempt to OVER-RIDE this stop; go to run; continue

            //  or Click on red cross on top right hand corner to get back to spread sheet
            // You may attempt to OVER-RIDE this stop; go to run; continue
  
        } 
       
	}
}
