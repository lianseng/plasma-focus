/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       Simulator.cs
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


namespace Plasma_Focus.models
{
    public class Simulator
    {
        private static Simulator instance;

        public static Mutex sync = new Mutex(); 

        public Simulator()
        {
            modelResults = new ModelResults();
            //machine.initialize(); 
        }

        public Plasma_Focus.models.fitting.AutoFit fit { get; set; }

        public static Simulator getInstance()
        {
            if (instance == null)
            {
                instance = new Simulator();
                instance.machine = new PlasmaFocusMachine();
            }
            return instance;
        }

        #region properties

        // for auto tuning
        //public Boolean autoOn { get; set; }     // auto tuning in progress
        //public Boolean fired { get; set; }      // did simulator run ?  might not run if validateParameters() fails

        // iterative parameters, incremented every cycle
        public double dt  { get; set; } 			// integration time
        public double t { get; set; } 			// start time
        public double I  { get; set; }    			// current
        public double II { get; set; } 			    // current derivative	
        public double isum { get; set; } 			// current integral
        public double z  { get; set; } 				// axial position, normalized to z0, anode length
        public double ZZ { get; set; } 		    // speed
        public double AC { get; set; }              // acceleration

        public double IDOT, CH;

        public double V { get; set; }                      // voltage
        public double IPeak { get; set; }                 // stored value of max current, I
        public double timeMax { get; set; }                 // time of max current

        public double VRMAX { get; set; }                  // store max voltage value

        // calculated parameters
        public double TM { get; set; }      // plasma temperature
        public double TeV { get; set; }     // plasma temperature, in eV

        // energy
        public double EINP { get; set; }                   //  energy into plasma ie work done by magnetic piston, by dynamic resistance effect

        // gas parameter
        public double zeff { get; set; }                         // corona machine calculated effective charge

        // radial phase parameters
        // Radial phase RI, distances are relative to radius a.
        // KS is shock position, KP is radial piston position, 
        // ZF is focus pinch length, all normalized to inner radius a; 
        // VS and VP are radial shock and piston speed,
        // VZ is axial pinch length elongating rate
        // Distances, radius and speeds are relative to radius of anode.
        // AS BEFORE QUANTITIES WITH AN R ATTACHED HAVE BEEN RE-COMPUTED AS REAL, j.e. UN-NORMALIZED QUANTITIES EXPRESSED IN USUAL LABORATORY UNITS.

        public double KS { get; set; }                    // shock position
        public double KP { get; set; }                    // radial piston position
        public double ZF { get; set; }                    // ZF focus pinch length
        public double VSR { get; set; }                   // normalized VS
        public double VS { get; set; }                    // Radial Inward shock speed
        public double VP { get; set; }                    // Piston speed
        public double VZ { get; set; }                    // axial pinch length elongating rate

        public double CFR { get; set; }                   //CurrentFactorRatio


        // radial reflective shock phase params
        public double rp;                                   // magnetic piston (currrent sheath) position (rs = Shock front position)

        public double ZG { get; set; }                    // record last value of axial speed
        public double trradialstart { get; set; }         // record when the radial phase started 
        public int radialStartRow { get; set; }              // radial reflective shock start row
        public int rrsStartRow { get; set; }              // radial reflective shock start row
        public int radiativeStartRow { get; set; }              // radial reflective shock start row
        public int expandAxialStartRow  { get; set; }      // start of expanded axial phase 

        // radiative 
        public double Qdot { get; set; }
        public double Qj { get; set; }          // resistive heat, J
        public double Qbrem { get; set; }       // bremsstrahlung, J
        public double Qrec { get; set; }        // recombination, J
        public double Qline { get; set; }       // line radiation, J
        public double Qrad { get; set; }        // total line radiation, J
        public double Q { get; set; }

        public double ZFDOT { get; set; }

        public ModelResults modelResults { get; set; }

        public PlasmaFocusMachine machine { get; set; } // = new PlasmaFocusMachine();

        #endregion

        public void initialize()
        { 
            modelResults = new ModelResults();
            fit = null;

                // iterative parameters, incremented every cycle
             dt = 0.002;			// integration time
             t = 0.0;				// start time
             I = 0.0;   			// current
             II = 1.0;			    // current derivative	
             isum = 0.0;			// current integral
             z = 0.0;				// axial position, normalized to z0, anode length
             ZZ = 0.0;			    // speed
             AC = 0.0;             // acceleration

            IDOT= CH= IPeak= VRMAX=  TM=  TeV= EINP= zeff= KS= KP= ZF= VSR= VS= VP= VZ= CFR= rp= ZG = trradialstart=0.0; 
        }

        public void validateParameters()
        {
            Simulator.sync.WaitOne(); 
            try
            {
                initialize();
                machine.initialize();
                machine.validateMachineParameters();
            }
            catch (Exception e)
            {
                Simulator.sync.ReleaseMutex(); 
                throw e;
            }
            Simulator.sync.ReleaseMutex(); 

            if (machine.machineName == null)
                throw new ApplicationException("No machine name found! Check configuration ");
          
        }

 
        public void run()
        {
            sync.WaitOne(); 

            DateTime dt = DateTime.Now;
            try
            {
                Simulator sim = this;
                AxialModel am = new AxialModel(ref sim);
                am.start(this);

                RadialModel rm = new RadialModel(ref sim);
                rm.start(this);

                RadialReflectiveShockModel rsm = new RadialReflectiveShockModel(ref sim);
                rsm.start(this);

                RadiativeModel rdm = new RadiativeModel(ref sim);
                rdm.start(this);

                ExpandedColumnAxialModel acm = new ExpandedColumnAxialModel(ref sim);
                acm.start(this);
            }
            catch (Exception ex)
            {
               // Debug.WriteLine("Program ended abnormally - " + ex.Message);
                sync.ReleaseMutex(); 
                throw ex;
            }
            TimeSpan elapsed = DateTime.Now - dt;
           // Debug.WriteLine("Total execution time " + elapsed.Milliseconds.ToString() + " ms");
            sync.ReleaseMutex(); 
        } 

    }
}
