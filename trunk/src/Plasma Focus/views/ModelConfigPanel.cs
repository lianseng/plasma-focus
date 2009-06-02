/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       ModelConfigPanel.cs
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plasma_Focus.models.fitting;
using Plasma_Focus.models;

namespace Plasma_Focus.views
{
    public partial class ModelConfigPanel : UserControl
    {
        public ModelConfigPanel()
        {
            InitializeComponent();

        }


        public void updatePanel()
        {

            AxialCrossoverRate.Text = System.Convert.ToString(GAFit.axialParams.crossover);
            AxialMutationRate.Text = System.Convert.ToString(GAFit.axialParams.mutation);
            AxialPopulation.Text = System.Convert.ToString(GAFit.axialParams.population);
            AxialGenerations.Text = System.Convert.ToString(GAFit.axialParams.generations);
            AxialElitism.Text = System.Convert.ToString(GAFit.axialParams.elitism); 

            RadialCrossoverRate.Text = System.Convert.ToString(GAFit.radialParams.crossover);
            RadialMutationRate.Text = System.Convert.ToString(GAFit.radialParams.mutation);
            RadialPopulation.Text = System.Convert.ToString(GAFit.radialParams.population);
            RadialGenerations.Text = System.Convert.ToString(GAFit.radialParams.generations);
            RadialElitism.Text = System.Convert.ToString(GAFit.radialParams.elitism); 


            FinalCrossoverRate.Text = System.Convert.ToString(GAFit.finalParams.crossover);
            FinalMutationRate.Text = System.Convert.ToString(GAFit.finalParams.mutation);
            FinalPopulation.Text = System.Convert.ToString(GAFit.finalParams.population);
            FinalGenerations.Text = System.Convert.ToString(GAFit.finalParams.generations);
            FinalElitism.Text = System.Convert.ToString(GAFit.finalParams.elitism); 

            ElectricalCrossoverRate.Text = System.Convert.ToString(GAFit.electricalParams.crossover);
            ElectricalMutationRate.Text = System.Convert.ToString(GAFit.electricalParams.mutation);
            ElectricalPopulation.Text = System.Convert.ToString(GAFit.electricalParams.population);
            ElectricalGenerations.Text = System.Convert.ToString(GAFit.electricalParams.generations);
            ElectricalElitism.Text = System.Convert.ToString(GAFit.electricalParams.elitism); 

            R2Weight.Text = System.Convert.ToString(GAFit.w1);
            PeakWeight.Text = System.Convert.ToString(GAFit.w2);
            PinchWeight.Text = System.Convert.ToString(GAFit.w3);
            RadialSlopeWeight.Text = System.Convert.ToString(GAFit.w4); 

            modelsDirectory.Text = ConfigIniFile.modelDirectory;
            isFilterEnabled.Checked = ConfigIniFile.isFilterEnabled;
            if (ConfigIniFile.filterType == FilterType.MA)
                MAFilter.Checked = true;
            else
                EWMAFilter.Checked = true;


        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            ConfigIniFile ini = new ConfigIniFile("config.ini");

            GAFit.axialParams.crossover = System.Convert.ToDouble(AxialCrossoverRate.Text);
            GAFit.axialParams.mutation = System.Convert.ToDouble(AxialMutationRate.Text);
            GAFit.axialParams.population = System.Convert.ToInt16(AxialPopulation.Text);
            GAFit.axialParams.generations = System.Convert.ToInt16(AxialGenerations.Text);
            GAFit.axialParams.elitism = System.Convert.ToBoolean(AxialElitism.Text);
   

            GAFit.radialParams.crossover = System.Convert.ToDouble(RadialCrossoverRate.Text);
            GAFit.radialParams.mutation = System.Convert.ToDouble(RadialMutationRate.Text);
            GAFit.radialParams.population = System.Convert.ToInt16(RadialPopulation.Text);
            GAFit.radialParams.generations = System.Convert.ToInt16(RadialGenerations.Text);
            GAFit.radialParams.elitism = System.Convert.ToBoolean(RadialElitism.Text);
   
            GAFit.finalParams.crossover = System.Convert.ToDouble(FinalCrossoverRate.Text);
            GAFit.finalParams.mutation = System.Convert.ToDouble(FinalMutationRate.Text);
            GAFit.finalParams.population = System.Convert.ToInt16(FinalPopulation.Text);
            GAFit.finalParams.generations = System.Convert.ToInt16(FinalGenerations.Text);
            GAFit.finalParams.elitism = System.Convert.ToBoolean(FinalElitism.Text);
   
            GAFit.electricalParams.crossover = System.Convert.ToDouble(ElectricalCrossoverRate.Text);
            GAFit.electricalParams.mutation = System.Convert.ToDouble(ElectricalMutationRate.Text);
            GAFit.electricalParams.population = System.Convert.ToInt16(ElectricalPopulation.Text);
            GAFit.electricalParams.generations = System.Convert.ToInt16(ElectricalGenerations.Text);
            GAFit.electricalParams.elitism = System.Convert.ToBoolean(ElectricalElitism.Text);
   
            GAFit.w1 = System.Convert.ToDouble(R2Weight.Text);
            GAFit.w2 = System.Convert.ToDouble(PeakWeight.Text);
            GAFit.w3 = System.Convert.ToDouble(PinchWeight.Text);
            GAFit.w4 = System.Convert.ToDouble(RadialSlopeWeight.Text);
 
            ConfigIniFile.modelDirectory = modelsDirectory.Text;
            if (isFilterEnabled.Checked)
            {
                ConfigIniFile.isFilterEnabled = true;
                if (MAFilter.Checked)
                    ConfigIniFile.filterType = FilterType.MA;
                else
                    ConfigIniFile.filterType = FilterType.EWMA;
            }

            if (ini.saveConfig(GAFit.axialParams, GAFit.radialParams, GAFit.finalParams, GAFit.electricalParams,
                GAFit.w1, GAFit.w2, GAFit.w3, GAFit.w4))
            {
                MessageBox.Show("Configuration file saved");
            }
            else
                MessageBox.Show("Error saving configuration");
            
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            ConfigIniFile ini = new ConfigIniFile("config.ini");
            if (ini.Exists())
            {
                try
                {
                    ini.loadConfig(ref GAFit.axialParams, ref GAFit.radialParams, ref GAFit.finalParams, ref GAFit.electricalParams,
                        ref GAFit.w1, ref GAFit.w2, ref GAFit.w3, ref GAFit.w4);

                }
                catch (ApplicationException ex)
                {
                }

            }
        }

        private void isFilterEnabled_CheckedChanged(object sender, EventArgs e)
        {
            ConfigIniFile.isFilterEnabled = FilterGroupbox.Enabled = isFilterEnabled.Checked;

        }

        private void MAFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (MAFilter.Checked)
                ConfigIniFile.filterType = FilterType.MA;
            else
                ConfigIniFile.filterType = FilterType.EWMA;

            Simulator s = Simulator.getInstance();
            if (s.machine.currentData!=null)
                s.machine.currentData.reload();

        }
   

    }
}
