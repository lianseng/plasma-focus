/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       ParametersIniFile.cs
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
    class ParametersIniFile
    {
        IniFile ini;

        public ParametersIniFile(string filename)
        {
            ini = new IniFile(filename);
            // setup sections and values

        }

        public bool Exists()
        {
            return ini.Exists();
        }

        public void setupParamFile()
        {
            ini.Add("[Machine Geometry]");
            ini["Machine Geometry"].Add("Machine Name=");
            ini["Machine Geometry"].Add("Inner Radius=");
            ini["Machine Geometry"].Add("Outer Radius=");
            ini["Machine Geometry"].Add("Anode Length=");


            ini.Add("[Electrical Characteristics]");
            ini["Electrical Characteristics"].Add("Resistance=");
            ini["Electrical Characteristics"].Add("Inductance=");
            ini["Electrical Characteristics"].Add("Capacitance=");
            ini["Electrical Characteristics"].Add("Voltage=");

            ini.Add("[Operational Parameters]");
            ini["Operational Parameters"].Add("Gas=");
            ini["Operational Parameters"].Add("Pressure=");
            ini["Operational Parameters"].Add("Atomic Number=");
            ini["Operational Parameters"].Add("Molecular Weight=");
            ini["Operational Parameters"].Add("Dissociation Number=");
            ini["Operational Parameters"].Add("Density=");

            ini.Add("[Anode Taper]");
            ini["Anode Taper"].Add("IsTapered=");
            ini["Anode Taper"].Add("Taper Start Position=");
            ini["Anode Taper"].Add("Taper End Radius=");

            ini.Add("[Model Parameters]");
            ini["Model Parameters"].Add("Mass shedding Axial=");
            ini["Model Parameters"].Add("Mass shedding Radial=");
            ini["Model Parameters"].Add("Current shedding Axial=");
            ini["Model Parameters"].Add("Current shedding Radial=");

            ini.Add("[Tuning]");
            ini["Tuning"].Add("Source="); 


        }

        public void saveModel(PlasmaFocusMachine model)
        {
            setupParamFile(); 
           
            // set values
            ini["Machine Geometry"]["Machine Name"] = model.machineName;
            ini["Machine Geometry"]["Inner Radius"] = System.Convert.ToString(model.RADA);
            ini["Machine Geometry"]["Outer Radius"] = System.Convert.ToString(model.RADB);
            ini["Machine Geometry"]["Anode Length"] = System.Convert.ToString(model.z0);

            ini["Electrical Characteristics"]["Resistance"] = System.Convert.ToString(model.R0);
            ini["Electrical Characteristics"]["Inductance"] = System.Convert.ToString(model.L0);
            ini["Electrical Characteristics"]["Capacitance"] = System.Convert.ToString(model.C0);
            ini["Electrical Characteristics"]["Voltage"] = System.Convert.ToString(model.V0);


            ini["Operational Parameters"]["Gas"] = model.fillGas;
            ini["Operational Parameters"]["Pressure"] = System.Convert.ToString(model.P0);
            ini["Operational Parameters"]["Atomic Number"] = System.Convert.ToString(model.ZN);
            ini["Operational Parameters"]["Molecular Weight"] = System.Convert.ToString(model.MW);
            ini["Operational Parameters"]["Dissociation Number"] = System.Convert.ToString(model.dissociatenumber);
            ini["Operational Parameters"]["Density"] = System.Convert.ToString(model.RHO);


            ini["Anode Taper"]["IsTapered"] = System.Convert.ToString(model.TAPER);
            ini["Anode Taper"]["Taper Start Position"] = System.Convert.ToString(model.TAPERSTART);
            ini["Anode Taper"]["Taper End Radius"] = System.Convert.ToString(model.ENDRAD);


            ini["Model Parameters"]["Mass shedding Axial"] = System.Convert.ToString(model.massf);
            ini["Model Parameters"]["Mass shedding Radial"] = System.Convert.ToString(model.massfr);
            ini["Model Parameters"]["Current shedding Axial"] = System.Convert.ToString(model.currf);
            ini["Model Parameters"]["Current shedding Radial"] = System.Convert.ToString(model.currfr);

            if (model.currentData != null)
              ini["Tuning"]["Source"] = model.currentData.dataFilename; 

            ini.Save();
        }

        //public void loadModelCurrentData(PlasmaFocusMachine model) 
        //{
        //    string name = model.currentData.dataFilename;
        //    model.currentData = new MeasuredCurrentData(name);
        //    SortedList<double, double> currentSeries;
            
        //    model.currentData.processReferenceFile(model.currentData.dataFilename, out currentSeries);
        //    if (currentSeries.Count == 0)
        //    {
        //        throw new ApplicationException(model.currentData.dataFilename + " has no current data !");
        //    }

        //    model.currentData.series = currentSeries;
        //}

        public void loadModel(ref PlasmaFocusMachine model)
        {
            string currentFilename="";   
            ini.Load();
            try
            {
                model.machineName = ini["Machine Geometry"]["Machine Name"];
                model.RADA = System.Convert.ToDouble(ini["Machine Geometry"]["Inner Radius"]);
                model.RADB = System.Convert.ToDouble(ini["Machine Geometry"]["Outer Radius"]);
                model.z0 = System.Convert.ToDouble(ini["Machine Geometry"]["Anode Length"]);

                model.R0 = System.Convert.ToDouble(ini["Electrical Characteristics"]["Resistance"]);
                model.L0 = System.Convert.ToDouble(ini["Electrical Characteristics"]["Inductance"]);
                model.C0 = System.Convert.ToDouble(ini["Electrical Characteristics"]["Capacitance"]);
                model.V0 = System.Convert.ToDouble(ini["Electrical Characteristics"]["Voltage"]);

                model.fillGas = ini["Operational Parameters"]["Gas"];
                model.P0 = System.Convert.ToDouble(ini["Operational Parameters"]["Pressure"]);
                model.ZN = System.Convert.ToDouble(ini["Operational Parameters"]["Atomic Number"]);
                model.MW = System.Convert.ToDouble(ini["Operational Parameters"]["Molecular Weight"]);
                model.dissociatenumber = System.Convert.ToDouble(ini["Operational Parameters"]["Dissociation Number"]);
                model.RHO = System.Convert.ToDouble(ini["Operational Parameters"]["Density"]);

                model.TAPER = System.Convert.ToBoolean(ini["Anode Taper"]["IsTapered"]);
                model.TAPERSTART = System.Convert.ToDouble(ini["Anode Taper"]["Taper Start Position"]);
                model.ENDRAD = System.Convert.ToDouble(ini["Anode Taper"]["Taper End Radius"]);

                model.massf = System.Convert.ToDouble(ini["Model Parameters"]["Mass shedding Axial"]);
                model.massfr = System.Convert.ToDouble(ini["Model Parameters"]["Mass shedding Radial"]);
                model.currf = System.Convert.ToDouble(ini["Model Parameters"]["Current shedding Axial"]);
                model.currfr = System.Convert.ToDouble(ini["Model Parameters"]["Current shedding Radial"]);

                
                try
                {
                    // assume current trace is in same directory as ini file.  
                    // Otherwise check "models" directory if the path in ini file is relative
                    currentFilename = ini["Tuning"]["Source"]; 
                    if (!System.IO.File.Exists(currentFilename) )
                        if (!System.IO.Path.IsPathRooted(currentFilename))        // relative path ?
                              currentFilename = ConfigIniFile.modelDirectory+ "\\" + currentFilename;

                    model.currentData = new MeasuredCurrent(currentFilename); 
                }
                catch (Exception e)
                {
                    model.currentData = null;
                    //throw new ApplicationException("Bad current trace file, " + currentFilename + "!\n" + e.Message);
                    
                }
                // experimental current curves from actual machine
                
               
            }
            catch (Exception ex)
            {
                string msg = "";
                if (model.currentData == null) msg = "Current trace file " + currentFilename +
                                               " not found !\n";

                throw new ApplicationException("Bad parameters file, " + msg+ini.FileName + "!\n" + ex.Message);

            }


        }

    }
}
