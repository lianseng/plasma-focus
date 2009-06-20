/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       MainForm.cs
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
using System.Drawing;
using System.Windows.Forms;
using Plasma_Focus.views;
using Plasma_Focus.models;
using System.ComponentModel;
using System.IO;
using Plasma_Focus.models.fitting;



namespace Plasma_Focus
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form //, IMainFormPresenter
	{ 
   
        public TuningPanel tuningPanel;
        public MachineConfigPanel configurationPanel;
        public GraphsPanel graphsPanel;
        public ResultsPanel resultsPanel;
        public ModelConfigPanel gaPanel;

        Plasma_Focus.models.PlasmaFocusMachine model;
       
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            // load config file
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

	        // load menu actions
            InitializeMenu();

            loadTabs();

 
            Simulator instance = Simulator.getInstance();
            model = instance.machine; 
        }


        public void showResults()
        {
            ParametersTabs.SelectTab(TuningTab);
        }
 
        void loadTabs() {
             configurationPanel = new MachineConfigPanel();
            configurationPanel.Parent = ConfigurationTab;

            tuningPanel = new TuningPanel();
            tuningPanel.Parent = TuningTab;

            graphsPanel = new GraphsPanel();
            graphsPanel.Parent = GraphsTab;
            graphsPanel.Enabled = false;


            resultsPanel = new ResultsPanel();
            resultsPanel.Parent = ResultsTab;
            resultsPanel.Enabled = false;

            gaPanel = new ModelConfigPanel();
            gaPanel.Parent = GAConfigTab;
            gaPanel.Enabled = true;

        }

        private void InitializeMenu() {
       
            mnuSaveAs.Click += delegate
            {
                OpenFileDialog FD = new OpenFileDialog();

                string filenname = "";
                string path = "";

                if (FD.ShowDialog() == DialogResult.OK)
                {
                    filenname = System.IO.Path.GetFileName(FD.FileName);
                    path = System.IO.Path.GetDirectoryName(FD.FileName);
                }
            };
        }
     
  

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            return;

            DialogResult dlgRes = MessageBox.Show(
                "Save the model ?",
                "Confirm Application Close",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dlgRes == DialogResult.No)
            {
                Application.Exit();
            }
            else
            {
                PlasmaFocusMachine machine = Simulator.getInstance().machine;
                ParametersIniFile file = new ParametersIniFile(machine.configFile);

                configurationPanel.getParameters(ref machine);
                file.saveModel(machine);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog FD = new SaveFileDialog();

            if (FD.ShowDialog() != DialogResult.OK) return;

            try
            {
                ModelResults.export(FD.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error exporting ");
            }
        }

        private void ParametersTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = (TabControl)sender;
            if (tab.SelectedTab == TuningTab)
            {
                if (!tuningPanel.tuningThread.IsBusy)
                    tuningPanel.updatePanel();

            }
            else if (tab.SelectedTab == GraphsTab)
            {
                if (!tuningPanel.tuningThread.IsBusy)                
                    graphsPanel.updatePanel(); 
            }
            else if (tab.SelectedTab == ResultsTab)
            {
                if (!tuningPanel.tuningThread.IsBusy)                
                    resultsPanel.updatePanel();

            }
            else if (tab.SelectedTab == ConfigurationTab)
            {
                configurationPanel.updatePanel();
            }
            else if (tab.SelectedTab == GAConfigTab)
            {
                gaPanel.updatePanel();
            }
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {
            configurationPanel.loadModel();
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            configurationPanel.saveAsModel();
        }

        private void About_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.ShowDialog();

        }

        private void Contents_Click(object sender, EventArgs e)
        {
            HelpForm help = new HelpForm();
            help.ShowDialog();
        }

    
 
  
  
	}
}
