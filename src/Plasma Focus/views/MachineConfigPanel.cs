/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       MachineConfigPanel.cs
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
using Plasma_Focus.models;

using System.IO;
using ZedGraph;
using Plasma_Focus.models.fitting;

namespace Plasma_Focus.views
{
    public partial class MachineConfigPanel : UserControl
    {

        bool newLoad,
            isCurrentFilenameChanged;
        Plasma_Focus.models.PlasmaFocusMachine machine;        

        public MachineConfigPanel()
        {
            InitializeComponent();   //spnTaperStart.Enabled = spnTaperEnd.Enabled = false;

            isCurrentFilenameChanged = newLoad = false;

        }
 
        private void spnInductance_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.L0 = (double)spnInductance.Value * 1e-9;
        }

        private void spnCapacitance_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.C0 = (double)spnCapacitance.Value * 1e-6;
        }

        private void spnResistance_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.R0 = (double)spnResistance.Value/1000;
        }
   
        private void spnAnodeLength_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.z0 = (double)spnAnodeLength.Value /100;
        }

        private void spnOuterRadius_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.RADB = (double)spnOuterRadius.Value * 0.01;
        }

        private void spnInnerRadius_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.RADA = (double)spnInnerRadius.Value * 0.01;
        }


        private void spnAtomicWt_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.ZN = (double)spnAtomicWt.Value;
        }

        private void spnMolecularWt_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.MW = (double)spnMolecularWt.Value;
        }

        private void spnVoltage_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.V0 = (double)(spnVoltage.Value *1000);  // kV to V

        }

        private void spnPressure_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.P0 = (double)spnPressure.Value;
        }

        private void spnDissociationNumber_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.dissociatenumber = (double)spnDissociationNumber.Value;
        }

        private void spnMassf_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.massf = (double)spnMassf.Value;
        }

        private void spnMassfr_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.massfr = (double)spnMassfr.Value;
        }

        private void spnCurrf_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.currf = (double)spnCurrf.Value;
        }

        private void spnCurrfr_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.currfr = (double)spnCurrfr.Value;
        }

        private void cbTapered_CheckedChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.TAPER = (bool)cbTapered.Checked.Equals(true);
            TaperGroup.Enabled = machine.TAPER;
            if (machine.TAPER)
            {
                //spnTaperEnd.Enabled = true; 
                spnTaperEnd.Value = spnTaperEnd.Value;
                //spnTaperStart.Enabled = true; 
                spnTaperStart.Value = spnTaperStart.Value;
            }
        }
        
        private void spnTaperEnd_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine;
            machine.ENDRAD = (double)spnTaperEnd.Value*0.01;
        }

        private void spnTaperStart_ValueChanged(object sender, EventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine;
            machine.TAPERSTART = (double)spnTaperStart.Value *0.01;
 
        }

        public void updatePanel()
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine;

            loadModelsFromDefaultDirectory();

             //reload only model parameters
            spnMassf.Value = (decimal)machine.massf;
            spnMassfr.Value = (decimal)(decimal)machine.massfr;
            spnCurrf.Value = (decimal)machine.currf;
            spnCurrfr.Value = (decimal)machine.currfr;

            spnInductance.Value = (decimal)(machine.L0*1e9);
            spnCapacitance.Value = (decimal)(machine.C0*1e6);
            spnResistance.Value = (decimal)machine.R0*1000; 

        }

        // note in the config file the parameters are not in SI, as in the UI
        public void loadParameters(PlasmaFocusMachine model)
        {
            //C0 = C0 * 1e-6;
            //L0 = L0 * 1e-9;
            //RADB = RADB * 0.01;
            //RADA = RADA * 0.01;
            //z0 = z0 * 1e-2;
            //V0 = V0 * 1000;

            if (model.currentData != null)
                currentFilename.Text = model.currentData.dataFilename; 
            else currentFilename.Text = "";

            cbMachine.Text = model.configFile; //model.machineName;   // note this triggers SelectedIndexChanged on the combobox !
            spnInductance.Value = (decimal)(model.L0);

            spnCapacitance.Value = (decimal)(model.C0);
            spnResistance.Value = (decimal)model.R0;

            spnAnodeLength.Value = (decimal)(model.z0);

            spnOuterRadius.Value = (decimal)(model.RADB);

            spnInnerRadius.Value = (decimal)(model.RADA);

            GasComboBox.Text = model.fillGas;

            spnAtomicWt.Value = (decimal)model.ZN;

            spnMolecularWt.Value = (decimal)model.MW;

            spnVoltage.Value = (decimal)(model.V0);

            spnPressure.Value = (decimal)model.P0;

            spnDissociationNumber.Value = (decimal)model.dissociatenumber;
            spnMassf.Value = (decimal)model.massf;
            spnMassfr.Value = (decimal)(decimal)model.massfr;
            spnCurrf.Value = (decimal)model.currf;
            spnCurrfr.Value = (decimal)model.currfr;

            cbTapered.Checked = model.isTapered();
            TaperGroup.Enabled = cbTapered.Checked;
            if (cbTapered.Checked)
            {
                //spnTaperEnd.Enabled = true; 
                spnTaperEnd.Value = (decimal)model.ENDRAD;
                //spnTaperStart.Enabled = true; 
                spnTaperStart.Value = (decimal)model.TAPERSTART;
            }
        }

        public void getParameters(ref PlasmaFocusMachine model)
        { 
            model.machineName = cbMachine.Text;
            model.L0 = (double)spnInductance.Value;

            model.C0 = (double)spnCapacitance.Value;
            model.R0 = (double)spnResistance.Value;

            model.z0 = (double)spnAnodeLength.Value;

            model.RADB = (double)spnOuterRadius.Value;

            model.RADA = (double)spnInnerRadius.Value;

            model.fillGas = GasComboBox.Text;

            model.ZN = (double)spnAtomicWt.Value;

            model.MW = (double)spnMolecularWt.Value;

            model.V0 = (double)spnVoltage.Value;

            model.P0 = (double)spnPressure.Value;

            model.dissociatenumber = (double)spnDissociationNumber.Value;
            model.massf = (double)spnMassf.Value;

            model.massfr = (double)spnMassfr.Value;

            model.currf = (double)spnCurrf.Value;
            model.currfr = (double)spnCurrfr.Value;
            model.TAPER = cbTapered.Checked;
            model.TAPERSTART = (double)spnTaperStart.Value;
            model.ENDRAD = (double)spnTaperEnd.Value;

            // convert to SI
            model.C0 = model.C0 * 1e-6;
            model.L0 = model.L0 * 1e-9;
            model.RADB = model.RADB * 0.01;
            model.RADA = model.RADA * 0.01;
            model.z0 = model.z0 * 1e-2;
            model.V0 = model.V0 * 1000;
            model.TAPERSTART = model.TAPERSTART * 0.01;
            model.ENDRAD = model.ENDRAD * 0.01;
            model.R0 = model.R0 / 1000;

            if (model.currentData != null)
            //    model.currentData = new MeasuredCurrentData();
                model.currentData.dataFilename = currentFilename.Text;

            if (currentFilename.Text.Length == 0)
                model.currentData = null;

        }

        private void saveModelButton_Click(object sender, EventArgs e)
        {
            saveAsModel();
        }

        public void saveAsModel() {
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            // saveFileDlg.CheckFileExists = true; // Application.StartupPath+"\\"+
            saveFileDlg.InitialDirectory = ConfigIniFile.modelDirectory;
            saveFileDlg.Filter = "Model Files (*.ini)|*.ini|All Files (*.*)|*.*";
            saveFileDlg.FilterIndex = 1;
            saveFileDlg.RestoreDirectory = true;
            saveFileDlg.AddExtension = true;

            saveFileDlg.DefaultExt = "ini";
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                saveModel(saveFileDlg.FileName);

            }
        }

        public void saveModel(string filename)
        {
            ParametersIniFile ini = new ParametersIniFile(filename);
            
      //      string currentFile = machine.currentData.dataFilename;

            PlasmaFocusMachine m = new PlasmaFocusMachine();
            getParameters(ref m);

            // restore file name from UI
            m.currentData = new MeasuredCurrent();

            if (!System.IO.Path.IsPathRooted(currentFilename.Text))
            {
                int p = currentFilename.Text.IndexOf(System.IO.Path.DirectorySeparatorChar);
                if (currentFilename.Text.Substring(0,p) == ConfigIniFile.modelDirectory)
                    m.currentData.dataFilename = currentFilename.Text.Substring(p+1,currentFilename.Text.Length-1-p);
            }
            else
                m.currentData.dataFilename = System.IO.Path.GetFileName(currentFilename.Text);

            // convert to SI
            m.C0 = m.C0 * 1e6;
            m.L0 = m.L0 * 1e9;
            m.R0 = m.R0 * 1000;
            m.V0 = m.V0 / 1000;

            m.RADB = m.RADB * 100;
            m.RADA = m.RADA * 100;
            m.z0 = m.z0 * 100;
            m.TAPERSTART = m.TAPERSTART / 0.01;
            m.ENDRAD = m.ENDRAD / 0.01;

           // m.currentData.dataFilename = currentFile; // = new MeasuredCurrent(filename);
            ini.saveModel(m);
        }

        private void cbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newLoad) return;

             Simulator instance = Simulator.getInstance();
            instance.initialize();
            instance.machine = new PlasmaFocusMachine();
            loadMachine((string)((ComboBox)sender).SelectedItem);
        }

        // load from default directory
        public void loadMachine(string machine)
        {
            
            string filename = ConfigIniFile.startDirectory+"\\"+ConfigIniFile.modelDirectory + "\\" + machine + ".ini";
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(filename)) ) {
                MessageBox.Show("Please create a models directory","Model directory doesn't exists !");
            }
            newLoad = true;
            initializeMachine(filename);
            newLoad = false;
            this.Invalidate();
        }

        public void initializeMachine (string name) {

            MainForm main = ((MainForm)(this.ParentForm));
            main.graphsPanel.Enabled = false;
            main.resultsPanel.Enabled = false;

            Simulator instance = Simulator.getInstance();
            machine = instance.machine; 
            machine.configFile = name;
            ParametersIniFile ini = new ParametersIniFile(name);
            if (ini.Exists())
            {
                try
                {
                    ini.loadModel(ref machine);
 
                    loadParameters(machine);
                    getParameters(ref machine);         // reloads them and convert to SI

                    if (machine.currentData !=null)
                    {
                        SortedList<double, double> currents = machine.currentData.series;
                        //currents = new SortedList<double, double>();
                        //currents.Clear();

                        //// load data from data currents file
                        //machine.currentData.loadReferenceFile(machine.currentData.dataFilename, currents);
                        //if (currents.Count == 0) return;
                        //machine.currentData.series = currents;
                        //CurrentReading[] measured = machine.currentData.getMeasuredCurrentArray(currents);
                       // machine.currentData.updateMeasuredMetrics(measured);

                        main.tuningPanel.drawCurrentGraph(null);
                    }
                
                }
                catch (ApplicationException ex)
                {
                    string message = ex.Message;

                    if (ex.Message.StartsWith("Could not find"))
                    { 
                        machine.currentData.dataFilename = "";
                        message = "Bad current file configured ! " + ex.Message;
                    }
                    // Show an exception message box with an OK button (the default).
                    MessageBox.Show(message+" in "+ machine.configFile, "Invalid parameter file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
            }
        }


        void loadModelsFromDefaultDirectory()
        {
            string pathname = ConfigIniFile.startDirectory +"\\"+ ConfigIniFile.modelDirectory;
            DirectoryInfo di = new DirectoryInfo(pathname);

            FileInfo[] rgFiles = di.GetFiles("*.ini");
            cbMachine.Items.Clear();
            foreach (FileInfo fi in rgFiles)
            {
                cbMachine.Items.Add(fi.Name.Remove(fi.Name.LastIndexOf(fi.Extension)));

            }
        }

        private void ConfigurationPanel_Load(object sender, EventArgs e)
        {

            string pathname = ConfigIniFile.modelDirectory;
            DirectoryInfo di = new DirectoryInfo(pathname);

            FileInfo[] rgFiles = di.GetFiles("*.ini");
            foreach (FileInfo fi in rgFiles)
            {
                cbMachine.Items.Add(fi.Name.Remove(fi.Name.LastIndexOf(fi.Extension)));

            }
        }



        private void GasComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox b = (ComboBox)sender;
            string gas = b.SelectedItem.ToString();

            if (gas.Equals("Neon")) {
                spnAtomicWt.Value = (decimal)10.0;
                spnMolecularWt.Value = (decimal)20.0;
                spnDissociationNumber.Value = (decimal)1.0;
            }
            else if (gas.Equals("Deuterium"))
            {
                spnAtomicWt.Value = (decimal)1.0;
                spnMolecularWt.Value = (decimal)4;
                spnDissociationNumber.Value = (decimal)2;
            }
            else if (gas.Equals("Argon"))
            {
                spnAtomicWt.Value = (decimal)18.0;
                spnMolecularWt.Value = (decimal)40;
                spnDissociationNumber.Value = (decimal)1;
            }
            else if (gas.Equals("Hydrogen"))
            {
                spnAtomicWt.Value = (decimal)1.0;
                spnMolecularWt.Value = (decimal)2;
                spnDissociationNumber.Value = (decimal)2;
            }
            else if (gas.Equals("Helium"))
            {
                spnAtomicWt.Value = (decimal)2;
                spnMolecularWt.Value = (decimal)4;
                spnDissociationNumber.Value = (decimal)1;
            }
        }

        private void LoadModelBtn_Click(object sender, EventArgs e)
        {
            loadModel();
 

        }

        // model can be in any directory
        public void loadModel()
        {
            // Create the dialog box object.
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = ConfigIniFile.modelDirectory;
            ofd.CheckPathExists = true;
            ofd.AddExtension = true;
            ofd.DefaultExt = "ini";
            ofd.ShowReadOnly = false;

            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
 
            string pathname = System.IO.Path.GetFullPath(ofd.FileName);
 
            Simulator instance = Simulator.getInstance();
            machine = instance.machine;
            machine.configFile = pathname; // ofd.FileName;

            
            instance.initialize();
            instance.machine = new PlasmaFocusMachine();
  
            newLoad = true;     // prevent selectedIndex event from responding
            initializeMachine(pathname); //machine.configFile);
            newLoad = false;

            this.Invalidate();
        }

        private void btnFireShot(object sender, EventArgs e)
        {
            Simulator s = Simulator.getInstance();
            machine = s.machine;
            if (machine.machineName == null)
                return;

            if (isCurrentFilenameChanged)
            {
                machine.currentData = new MeasuredCurrent(currentFilename.Text); 
                isCurrentFilenameChanged = false;
            }
            getParameters(ref machine);  // update parameters  
            Cursor = Cursors.WaitCursor;
            try
            {
                s.validateParameters();
            }
            catch (ApplicationException ex)
            {
                Cursor = Cursors.Default;
                 DialogResult result = MessageBox.Show(ex.Message+"\nContinue ?", "Bad configuration", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                 if (result == DialogResult.No) return;
            }

            Cursor = Cursors.Default;
            // run simulator
            try
            {
                s.run();
            }
            catch (ApplicationException ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Run problem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            };


            // find goodness of fit, stop before expanded column row
            //double time = s.modelResults.results[s.expandAxialStartRow - 1].TR;

            MainForm main = ((MainForm)(this.ParentForm));

            if (s.machine.currentData!= null)
            {
                double end = s.machine.currentData.endTime;
                double start = s.machine.currentData.midRiseTime;

                s.machine.r2 = MeasuredCurrent.calcR2(ModelResults.getComputedCurrentArray(s.modelResults.results),
                s.machine.currentData.array, 0 /* start */, end);

                // find goodness of fit, stop before expanded column row
                CurrentReading[] computed = ModelResults.getComputedCurrentArray(s.modelResults.results);

                s.modelResults.metrics = new Metrics();
                s.modelResults.updateModelMetrics(s.modelResults.metrics);
                //            double fit = s.machine.r2;
                main.tuningPanel.updateFitness(); // setFitText(String.Format("{0:#.###}", fit));
            }
            // show results
            main.graphsPanel.Enabled = true;
            main.resultsPanel.Enabled = true;
         //   main.paramsPanel.graphsPanel.updatePanel();
            main.showResults();
        }

        private void HelpBtn_Click(object sender, EventArgs e)
        {

        }

        private void currentFilename_TextChanged(object sender, EventArgs e)
        {
            isCurrentFilenameChanged = true;
        }

        private void MachineConfigPanel_Leave(object sender, EventArgs e)
        {
            Simulator s = Simulator.getInstance();
            machine = s.machine; 
            if (machine.machineName == null)
                return;

            if (isCurrentFilenameChanged)
            {
                machine.currentData = new MeasuredCurrent(currentFilename.Text);
                isCurrentFilenameChanged = false;
            } 
        }

        private void cbMachine_MouseClick(object sender, MouseEventArgs e)
        {
            loadModelsFromDefaultDirectory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create the dialog box object.
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = ConfigIniFile.modelDirectory;
            ofd.CheckPathExists = true;
            ofd.AddExtension = true;
            ofd.DefaultExt = "csv";
            ofd.ShowReadOnly = false;

            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;

            string pathname = System.IO.Path.GetFullPath(ofd.FileName);
            currentFilename.Text = pathname;
            
        }
 

     
  
         
    }
}
