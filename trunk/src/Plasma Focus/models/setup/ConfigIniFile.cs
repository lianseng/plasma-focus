/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       ConfigIniFile.cs
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
using Plasma_Focus.models.fitting;

namespace Plasma_Focus.models
{
    enum FilterType { MA=1, EWMA }

    class ConfigIniFile
    {
        public static string modelDirectory { get; set; }
        public static bool isFilterEnabled { get; set; }
        public static FilterType filterType { get; set; }
        public static string startDirectory { get; set; }
        
        //
        //  methods for config.ini files
        //
        IniFile ini;

        public ConfigIniFile(string filename)
        {
            startDirectory = System.IO.Directory.GetCurrentDirectory();
            modelDirectory = "\\models";
            
            ini = new IniFile(filename);
            // setup sections and values 
           // setupConfigFile(); 
           
        }

        public bool Exists()
        {
            return ini.Exists();
        }

        public void setupConfigFile()
        {
            ini.Add("[Models]");
            ini["Models"].Add("Model Directory=");

            ini.Add("[Axial GA Algorithm]");
            ini["Axial GA Algorithm"].Add("Mutation Rate=");
            ini["Axial GA Algorithm"].Add("Crossover Rate=");
            ini["Axial GA Algorithm"].Add("Population Size=");
            ini["Axial GA Algorithm"].Add("Generations=");
            ini["Axial GA Algorithm"].Add("Elitism=");

            ini.Add("[Radial GA Algorithm]");
            ini["Radial GA Algorithm"].Add("Mutation Rate=");
            ini["Radial GA Algorithm"].Add("Crossover Rate=");
            ini["Radial GA Algorithm"].Add("Population Size=");
            ini["Radial GA Algorithm"].Add("Generations=");
            ini["Radial GA Algorithm"].Add("Elitism=");


            ini.Add("[Final GA Algorithm]");
            ini["Final GA Algorithm"].Add("Mutation Rate=");
            ini["Final GA Algorithm"].Add("Crossover Rate=");
            ini["Final GA Algorithm"].Add("Population Size=");
            ini["Final GA Algorithm"].Add("Generations=");
            ini["Final GA Algorithm"].Add("Elitism=");

            ini.Add("[Electrical GA Algorithm]");
            ini["Electrical GA Algorithm"].Add("Mutation Rate=");
            ini["Electrical GA Algorithm"].Add("Crossover Rate=");
            ini["Electrical GA Algorithm"].Add("Population Size=");
            ini["Electrical GA Algorithm"].Add("Generations=");
            ini["Electrical GA Algorithm"].Add("Elitism=");

            ini.Add("[Fitness Weights]");
            ini["Fitness Weights"].Add("R2 Weight=");
            ini["Fitness Weights"].Add("Peak Weight=");
            ini["Fitness Weights"].Add("Pinch Weight=");
            ini["Fitness Weights"].Add("Radial Slope Weight="); 

            ini.Add("[Filtering]");
            ini["Filtering"].Add("Enabled=");
            ini["Filtering"].Add("Type=");
        }

        public bool saveConfig(GAFit.GAParams axialParams, GAFit.GAParams radialParams,
                                GAFit.GAParams finalParams, GAFit.GAParams electricalParams,
                                double w1,double w2,double w3,double w4)
        {
            setupConfigFile();
            // set values
            ini["Models"]["Model Directory"] = modelDirectory;

            ini["Axial GA Algorithm"]["Mutation Rate"] = System.Convert.ToString(axialParams.mutation);
            ini["Axial GA Algorithm"]["Crossover Rate"] = System.Convert.ToString(axialParams.crossover);
            ini["Axial GA Algorithm"]["Population Size"] = System.Convert.ToString(axialParams.population);
            ini["Axial GA Algorithm"]["Generations"] = System.Convert.ToString(axialParams.generations);
            ini["Axial GA Algorithm"]["Elitism"] = System.Convert.ToString(axialParams.elitism);

            ini["Radial GA Algorithm"]["Mutation Rate"] = System.Convert.ToString(radialParams.mutation);
            ini["Radial GA Algorithm"]["Crossover Rate"] = System.Convert.ToString(radialParams.crossover);
            ini["Radial GA Algorithm"]["Population Size"] = System.Convert.ToString(radialParams.population);
            ini["Radial GA Algorithm"]["Generations"] = System.Convert.ToString(radialParams.generations);
            ini["Radial GA Algorithm"]["Elitism"] = System.Convert.ToString(radialParams.elitism);

            ini["Final GA Algorithm"]["Mutation Rate"] = System.Convert.ToString(finalParams.mutation);
            ini["Final GA Algorithm"]["Crossover Rate"] = System.Convert.ToString(finalParams.crossover);
            ini["Final GA Algorithm"]["Population Size"] = System.Convert.ToString(finalParams.population);
            ini["Final GA Algorithm"]["Generations"] = System.Convert.ToString(finalParams.generations);
            ini["Final GA Algorithm"]["Elitism"] = System.Convert.ToString(finalParams.elitism);
            
            ini["Electrical GA Algorithm"]["Mutation Rate"] = System.Convert.ToString(electricalParams.mutation);
            ini["Electrical GA Algorithm"]["Crossover Rate"] = System.Convert.ToString(electricalParams.crossover);
            ini["Electrical GA Algorithm"]["Population Size"] = System.Convert.ToString(electricalParams.population);
            ini["Electrical GA Algorithm"]["Generations"] = System.Convert.ToString(electricalParams.generations);
            ini["Electrical GA Algorithm"]["Elitism"] = System.Convert.ToString(electricalParams.elitism);
            
           ini["Fitness Weights"]["R2 Weight"]= System.Convert.ToString(w1);
           ini["Fitness Weights"]["Peak Weight"]= System.Convert.ToString(w2);
           ini["Fitness Weights"]["Pinch Weight"]= System.Convert.ToString(w3);
           ini["Fitness Weights"]["Radial Slope Weight"]= System.Convert.ToString(w4);
           
           ini["Filtering"]["Enabled"] = System.Convert.ToString(isFilterEnabled);
           ini["Filtering"]["Type"] = System.Convert.ToString(filterType);
           return ini.Save();
        }


        public void loadConfig(ref GAFit.GAParams axialParams, ref GAFit.GAParams radialParams,
            ref GAFit.GAParams finalParams, ref GAFit.GAParams electricalParams,
            ref double w1, ref double w2, ref double w3, ref double w4 )
        {

            ini.Load(); 
       
            try
            {
                modelDirectory = ini["Models"]["Model Directory"];

                isFilterEnabled = System.Convert.ToBoolean(ini["Filtering"]["Enabled"]);
                if (ini["Filtering"]["Type"]=="MA")
                    filterType = FilterType.MA;
                else
                    filterType = FilterType.EWMA;

                axialParams.mutation = System.Convert.ToDouble(ini["Axial GA Algorithm"]["Mutation Rate"]);
                axialParams.crossover = System.Convert.ToDouble(ini["Axial GA Algorithm"]["Crossover Rate"]);
                axialParams.population = System.Convert.ToInt32(ini["Axial GA Algorithm"]["Population Size"]);
                axialParams.generations = System.Convert.ToInt32(ini["Axial GA Algorithm"]["Generations"]);
                axialParams.elitism = System.Convert.ToBoolean(ini["Axial GA Algorithm"]["Elitism"]);

                radialParams.mutation = System.Convert.ToDouble(ini["Radial GA Algorithm"]["Mutation Rate"]);
                radialParams.crossover = System.Convert.ToDouble(ini["Radial GA Algorithm"]["Crossover Rate"]);
                radialParams.population = System.Convert.ToInt32(ini["Radial GA Algorithm"]["Population Size"]);
                radialParams.generations = System.Convert.ToInt32(ini["Radial GA Algorithm"]["Generations"]);
                radialParams.elitism = System.Convert.ToBoolean(ini["Radial GA Algorithm"]["Elitism"]);


                finalParams.mutation = System.Convert.ToDouble(ini["Final GA Algorithm"]["Mutation Rate"]);
                finalParams.crossover = System.Convert.ToDouble(ini["Final GA Algorithm"]["Crossover Rate"]);
                finalParams.population = System.Convert.ToInt32(ini["Final GA Algorithm"]["Population Size"]);
                finalParams.generations = System.Convert.ToInt32(ini["Final GA Algorithm"]["Generations"]);
                finalParams.elitism = System.Convert.ToBoolean(ini["Final GA Algorithm"]["Elitism"]);
                
                electricalParams.mutation = System.Convert.ToDouble(ini["Electrical GA Algorithm"]["Mutation Rate"]);
                electricalParams.crossover = System.Convert.ToDouble(ini["Electrical GA Algorithm"]["Crossover Rate"]);
                electricalParams.population = System.Convert.ToInt32(ini["Electrical GA Algorithm"]["Population Size"]);
                electricalParams.generations = System.Convert.ToInt32(ini["Electrical GA Algorithm"]["Generations"]);
                electricalParams.elitism = System.Convert.ToBoolean(ini["Electrical GA Algorithm"]["Elitism"]);

                w1 = System.Convert.ToDouble(ini["Fitness Weights"]["R2 Weight"]);
                w2 = System.Convert.ToDouble(ini["Fitness Weights"]["Peak Weight"]);
                w3 = System.Convert.ToDouble(ini["Fitness Weights"]["Pinch Weight"]);
                w4 = System.Convert.ToDouble(ini["Fitness Weights"]["Radial Slope Weight"]);
                 
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Bad configuration file, " + ini.FileName + "!\n" + ex.Message);

            }

           

        }

    }
}
