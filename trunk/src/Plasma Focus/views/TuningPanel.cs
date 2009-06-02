/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       TuningPanel.cs
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

using ZedGraph;
using Plasma_Focus.models.fitting;
using System.Threading;

using System.Runtime.InteropServices;
using System.Diagnostics;



namespace Plasma_Focus.views
{

    public partial class TuningPanel : UserControl
    {
        [DllImport("kernel32.dll")]
        private static extern bool Beep(int freq, int dur);

        GraphPane pane;

        PlasmaFocusMachine machine;
        ZoomState zoom = null;

        public BackgroundWorker tuningThread, tuningElectricalsThread;

        public TuningPanel()
        {
            InitializeComponent();
            // tuningThread
            // instantiate the backgroundworker.   

            progressBar1.Maximum = 100;
            progressStatus.Visible = progressBar1.Visible = false;
            timer1.Stop();

            tuningThread = new BackgroundWorker();

            // These hook up the event handlers to the events on the BackgroundWorker.   
            tuningThread.DoWork += new DoWorkEventHandler(tuningWorker);
            tuningThread.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            tuningThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            tuningThread.WorkerReportsProgress = true;
            tuningThread.WorkerSupportsCancellation = true;

            tuningElectricalsThread = new BackgroundWorker();

            // These hook up the event handlers to the events on the BackgroundWorker.   
            tuningElectricalsThread.DoWork += new DoWorkEventHandler(tuningElectricalsWorker);
            tuningElectricalsThread.ProgressChanged += new ProgressChangedEventHandler(tuningElectricalWorker_ProgressChanged);
            tuningElectricalsThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(tuningElectricalWorker_RunWorkerCompleted);

            tuningElectricalsThread.WorkerReportsProgress = true;
            tuningElectricalsThread.WorkerSupportsCancellation = true;

            // simulator
            Simulator instance = Simulator.getInstance();
            machine = instance.machine;

            if (machine.currentData != null && machine.currentData.dataFilename.Length > 0)
            {
                currentFilename.Text = machine.configFile;
                //                loadModelCurrentData(machine);
            }
        }

        #region tuningThread

        void tuningWorker(object sender, DoWorkEventArgs e)
        {

            Simulator s = Simulator.getInstance();
            PlasmaFocusMachine machine = s.machine;

            //  DialogResult result = MessageBox.Show("Run with the current settings ?\nAnswer 'No' if you want to start with simulator guess", "Confirm Action", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            CurrentReading pinch = new CurrentReading((double)PinchTime.Value, (double)PinchCurrent.Value);

            machine.currentData.metrics.pinch.reading = (double)PinchCurrent.Value;
            machine.currentData.metrics.pinch.time = (double)PinchTime.Value;

            AutoFit a = new AutoFit(tuningThread, pinch);

            int result;

            try
            {
                result = a.tune();

                // update model results for display
                s.validateParameters();
                s.run();

                // find goodness of fit, stop before expanded column row
                CurrentReading[] computed = ModelResults.getComputedCurrentArray(s.modelResults.results);
                double end = s.machine.currentData.endTime;
                double start = s.machine.currentData.midRiseTime; 
                
                s.modelResults.metrics = new Metrics();
                s.modelResults.updateModelMetrics(s.modelResults.metrics, s.machine.currentData.metrics.midRadialTime);

                s.machine.r2 = MeasuredCurrent.calcR2(s.machine.currentData.array, computed, start,end);
                 
                Invoke(new MethodInvoker(EnableButton));
                Invoke(new MethodInvoker(updatePanel));

            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Run problem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //      machine = s.machine;
            if (result != 0)
                e.Result = "Retune";
        }


        void tuningElectricalsWorker(object sender, DoWorkEventArgs e)
        {

            Simulator s = Simulator.getInstance();
            PlasmaFocusMachine machine = s.machine;
            CurrentReading pinch = new CurrentReading((double)PinchTime.Value, (double)PinchCurrent.Value);

            AutoFit a = new AutoFit(tuningElectricalsThread, pinch);

            int result;
            try
            {
                result = a.tuneElectricals();
                s.validateParameters();
                s.run();

                // find goodness of fit, stop before expanded column row
                CurrentReading[] computed = ModelResults.getComputedCurrentArray(s.modelResults.results);

                double end = s.machine.currentData.endTime;
                double start = s.machine.currentData.midRiseTime;

                s.modelResults.metrics = new Metrics();
                s.modelResults.updateModelMetrics(s.modelResults.metrics, s.machine.currentData.metrics.midRadialTime);

                s.machine.r2 = MeasuredCurrent.calcR2(s.machine.currentData.array, computed, start,end);
 
                Invoke(new MethodInvoker(EnableButton));
                Invoke(new MethodInvoker(updatePanel));

            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Run problem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //machine = s.machine;


            if (result != 0)
                e.Result = "Retune";
        }


        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableButton(); updatePanel();
            //this.ParentForm.TopLevelControl.BringWindowToTop();
            Beep(9000, 100);
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Increment(e.ProgressPercentage);
            progressStatus.Text = e.UserState.ToString();
            if (e.ProgressPercentage < 10) return;
            updatePanel();
        }


        void tuningElectricalWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableButton();
            Beep(9000, 100);
        }

        void tuningElectricalWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            progressBar1.Increment(e.ProgressPercentage);
            progressStatus.Text = e.UserState.ToString();
            //if (e.ProgressPercentage == -1)
            //    MessageBox.Show(progressStatus.Text);
            if (e.ProgressPercentage < 10) return;
            updatePanel();
        }

        #endregion

        #region draw current plots
        void drawMeasuredCurrent(GraphPane myPane)
        {
            PointPairList list = new PointPairList();

            if (machine.currentData != null && machine.currentData.series != null)
            {
                SortedList<double, double> measured = machine.currentData.series;
                foreach (KeyValuePair<double, double> data in measured)
                {
                    list.Add(data.Key, data.Value);
                }
            }
            LineItem curve1 = myPane.AddCurve("Measured Current", list, Color.Black, SymbolType.Plus);
            curve1.Line.IsVisible = false;
            curve1.Line.Width = 0.1F;
            curve1.Line.IsSmooth = true;
            curve1.Line.SmoothTension = 0.6F;
            curve1.Symbol.Fill = new Fill(Color.White);
            curve1.Symbol.Size = 0.1f;

            // draw processed curve
            if (machine.currentData != null)
            {
                Simulator s = Simulator.getInstance();

                CurrentReading[] processed = s.machine.currentData.array;

                Metrics metrics = s.machine.currentData.metrics;
                if (metrics != null)
                {
                    //PinchTime.Value = (decimal)metrics.pinch.time;
                    //PinchCurrent.Value = (decimal)metrics.pinch.reading;

                    myPane.AddStick("Pinch", new double[] { metrics.pinch.time },
                        new double[] { metrics.pinch.reading }, Color.Green);
                }

                list = new PointPairList();
                list.Clear();
                foreach (CurrentReading data in processed)
                {
                    list.Add(data.time, data.reading);
                }
            }
            curve1 = myPane.AddCurve("Processed Current", list, Color.Cyan, SymbolType.Plus);

            curve1.Line.Width = 0.1F;
            curve1.Line.IsSmooth = true;
            curve1.Line.SmoothTension = 0.6F;
            //curve1.Symbol.Fill = new Fill(Color.Yellow);
            curve1.Symbol.Size = 0.1f;

        }

        void drawComputedCurrent(GraphPane myPane)
        {
            Simulator simulator = Simulator.getInstance();
            machine = simulator.machine;
            List<IterationResult> results = simulator.modelResults.results;

            if (results.Count <= 0) return;


            //int lastRow = simulator.radiativeStartRow;// simulator.modelResults.lastRadialRow;

            PointPairList list = new PointPairList();
            for (int i = 0; i < simulator.modelResults.firstRadialRow; i++)
            {
                IterationResult result = results[i];

                list.Add(result.TR, result.IR);
            }
            LineItem curve = myPane.AddCurve("Axial Current", list, Color.Pink, SymbolType.Diamond);
            curve.Line.Width = 0.1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;
            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 0.1f;


            list = new PointPairList();
            list.Clear();
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.radiativeStartRow; i++)
            {
                IterationResult result = results[i];

                list.Add(result.TR, result.IR);
            }
            curve = myPane.AddCurve("Radial Current", list, Color.Crimson, SymbolType.Diamond);
            curve.Line.Width = 0.1f;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;
            curve.Line.Fill = new Fill(Color.Pink);
            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 0.1f;


            list = new PointPairList();
            list.Clear();
            for (int i = simulator.radiativeStartRow; i < simulator.expandAxialStartRow; i++)
            {
                IterationResult result = results[i];

                list.Add(result.TR, result.IR);
            }


            curve = myPane.AddCurve("Pinch Current", list, Color.DarkOrange, SymbolType.Diamond);
            curve.Line.Width = 0.1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;
            curve.Line.Fill = new Fill(Color.Orange);
            curve.Symbol.Size = 0.1f;


            list = new PointPairList();
            list.Clear();
            for (int i = simulator.expandAxialStartRow; i < results.Count; i++)
            {
                IterationResult result = results[i];

                list.Add(result.TR, result.IR);
            }


            curve = myPane.AddCurve("Expanded Column Current", list, Color.Crimson, SymbolType.Diamond);
            curve.Line.Width = 0.1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;
            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 0.1f;


        }

        public void drawCurrentGraph(ZoomState zoom)
        {
            pane = CurrentGraph.GraphPane;
            if (pane == null) return;
            pane.CurveList.Clear();

            this.SuspendLayout();

            // draw data current first
            if (machine.currentData != null)
                drawMeasuredCurrent(pane);

            drawComputedCurrent(pane);

            pane.Title.Text = "Current and Voltage";
            pane.XAxis.Title.Text = "Time (us)";
            pane.YAxis.Title.Text = "Current (kA), Voltage (V*10)";
            // Enable the X and Y axis grids
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;

            // reset zoom
            if (zoom != null)
            {
                zoom.ApplyState(pane);
                zoom = null;
            }

            this.ResumeLayout();
            pane.AxisChange();
        }
        #endregion

        #region current data
        private void CurrentCurveReferencePanel_Paint(object sender, PaintEventArgs e)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine;
            // loadModelCurrentData(machine);
            if (machine.currentData == null) return;
            currentFilename.Text = machine.currentData.dataFilename;
        }

        private void updateCurrentData(DataGridView grid)
        {
            if (machine == null) return;
            SortedList<double, double> currents = machine.currentData.series;
            if (currents.Count == 0) return;

            currents.Clear();
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                DataGridViewRow row = grid.Rows[i];
                double current, time;

                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                    break;

                time = System.Convert.ToDouble(row.Cells[0].Value);
                current = System.Convert.ToDouble(row.Cells[1].Value);

                currents.Add(time, current);
            }

            this.Invalidate();
        }


        private void ImportCurrentBtn_Click(object sender, EventArgs e)
        {
            // Create the dialog box object.
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.CheckPathExists = true;
            ofd.AddExtension = true;
            ofd.DefaultExt = "ini";
            ofd.ShowReadOnly = false;

            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;

            SortedList<double, double> currents;
            currents = new SortedList<double, double>();
            currents.Clear();

            machine.currentData = new MeasuredCurrent(ofd.FileName);

            // replot curve
            drawCurrentGraph(null);

            //CurrentDataGrid.Rows.Clear();
            //PointPairList list = new PointPairList();
            //foreach (KeyValuePair<double, double> data in currents)
            //{
            //    CurrentDataGrid.Rows.Add(new string[] {System.Convert.ToString(data.Key),
            //        System.Convert.ToString(data.Value)});
            //    list.Add(data.Key, data.Value);
            //}

            //CreateChart(CurrentGraph, list);
            this.Invalidate();
        }

        private void LoadRefBtn_Click(object sender, EventArgs e)
        {
            if (machine.currentData == null) return;

            if (machine.currentData.dataFilename.Length <= 0)
                return;

            machine.currentData.reload();

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            MainForm main = ((MainForm)(this.ParentForm));
            main.configurationPanel.saveAsModel();

            this.Invalidate();
        }
        #endregion

        public void reLoadConfig(ref PlasmaFocusMachine machine)
        {
            MainForm main = ((MainForm)(this.ParentForm));
            main.configurationPanel.getParameters(ref machine); // reload machine parameters

            // take new input values if any
            machine.massf = (double)massf.Value;
            machine.massfr = (double)massfr.Value;
            machine.currf = (double)currf.Value;
            machine.currfr = (double)currfr.Value;

            // convert to SI
            machine.L0 = (double)L0.Value * 1e-9;
            machine.R0 = (double)R0.Value / 1000;
            machine.C0 = (double)C0.Value * 1e-6;

        }

        public void setFitText(string t)
        {
            R2.Text = t;
        }

        // draw including the results curve
        private void redraw(ZoomState zoom)
        {
            Simulator instance = Simulator.getInstance();
            machine = instance.machine;
            if (machine == null || machine.machineName == null) return;

            if (machine.currentData != null)
            {
                currentFilename.Text = machine.currentData.dataFilename;
                R2.Text = String.Format("{0:0.####}", machine.r2);

                Metrics metrics = instance.machine.currentData.metrics;
                if (metrics != null)
                {
                    PinchTime.Value = (decimal)metrics.pinch.time;
                    PinchCurrent.Value = (decimal)metrics.pinch.reading;

                    pane = CurrentGraph.GraphPane;
                    pane.AddStick("Pinch", new double[] { metrics.pinch.time },
                        new double[] { metrics.pinch.reading }, Color.Green);
                }

            }

            currf.Value = (decimal)machine.currf;
            currfr.Value = (decimal)machine.currfr;
            massf.Value = (decimal)machine.massf;
            massfr.Value = (decimal)machine.massfr;

            R0.Value = (decimal)machine.R0 * 1000;
            L0.Value = (decimal)(machine.L0 * 1e9);
            C0.Value = (decimal)(machine.C0 * 1e6);
            R2.Text = String.Format("{0:0.####}", machine.r2);
            

            drawCurrentGraph(zoom);
            this.Invalidate();

        }

        public void updatePanel()
        {

            Simulator.sync.WaitOne();

            redraw(null);

           Simulator.sync.ReleaseMutex(); 
        }

      
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (progressBar1.Value == progressBar1.Maximum)
                progressBar1.Value = 0;
        }

        #region buttons

        private void FireBtn_Click(object sender, EventArgs e)
        {
            Simulator s = Simulator.getInstance();
            machine = s.machine;

            reLoadConfig(ref machine);

            // save zoom settings
            ZoomStateStack stack = CurrentGraph.GraphPane.ZoomStack;
            if (stack.Count != 0)
                zoom = new ZoomState(CurrentGraph.GraphPane, ZoomState.StateType.Zoom);

            Cursor = Cursors.WaitCursor;
            // validate parameters
            try
            {
                s.validateParameters();
            }
            catch (ApplicationException ex)
            {
                Cursor = Cursors.Default;
                DialogResult result = MessageBox.Show(ex.Message + "\nContinue ?", "Bad configuration", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // run simulator
            try
            {
                s.run();
            }
            catch (ApplicationException ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Run problem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            redraw(zoom);

            // calculate r2 if measure data is available
            if (s.machine.currentData != null)
            {
                // s.machine.currentData.updateMeasuredMetrics(s.machine.currentData.array);
                s.machine.currentData.reload();

                // find goodness of fit, stop before expanded column row
                CurrentReading[] computed = ModelResults.getComputedCurrentArray(s.modelResults.results);

                double end = s.machine.currentData.endTime;
                double start = s.machine.currentData.midRiseTime;

                s.modelResults.metrics = new Metrics();
                s.modelResults.updateModelMetrics(s.modelResults.metrics, s.machine.currentData.metrics.midRadialTime);

                s.machine.r2 = MeasuredCurrent.calcR2(s.machine.currentData.array, computed, start,end);

                double fit = s.machine.r2;

                R2.Text = String.Format("{0:0.####}", fit);

                //    PinchCurrent.Value = (decimal)s.machine.currentData.metrics.pinch.reading;
                //    PinchTime.Value = (decimal)s.machine.currentData.metrics.pinch.time;

                CurrentGraph.Invalidate();
            }
            // plot other curves
            MainForm main = ((MainForm)(this.ParentForm));
            main.graphsPanel.updatePanel();
            Cursor = Cursors.Default;

        }


        void DisableButtons()
        {

            MainForm main = ((MainForm)(this.ParentForm));
            main.resultsPanel.Enabled = main.graphsPanel.Enabled = false;

            TuneElectrical.Enabled = FineTuneBtn.Enabled = FireBtn.Enabled = ReTuneBtn.Enabled = false;
            progressStatus.Visible = progressBar1.Visible = true;
            StopBtn.Select();
            timer1.Start();
        }


        void EnableButton()
        {
            MainForm main = ((MainForm)(this.ParentForm));
            main.resultsPanel.Enabled = main.graphsPanel.Enabled = true;

            progressStatus.Visible = progressBar1.Visible = false;
            timer1.Stop();
            TuneElectrical.Enabled = FineTuneBtn.Enabled = FireBtn.Enabled = ReTuneBtn.Enabled = true;
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {

            MainForm main = ((MainForm)(this.ParentForm));
            main.configurationPanel.initializeMachine(machine.configFile);

            // convert to SI
            machine.R0 = machine.R0 / 1000;
            machine.L0 = (machine.L0 * 1e-9);
            machine.C0 = (machine.C0 * 1e-6);

            redraw(null);
            CurrentGraph.Invalidate();

        }

        private void HelpBtn_Click(object sender, EventArgs e)
        {

        }


        private void ReTuneBtn_Click(object sender, EventArgs e)
        {
            Simulator s = Simulator.getInstance();
            machine = s.machine;

            DisableButtons();

            // load guess
            AutoFit.resetTuneGuesses(ref machine);

            currf.Value = (decimal)machine.currf;
            currfr.Value = (decimal)machine.currfr;
            massf.Value = (decimal)machine.massf;
            massfr.Value = (decimal)machine.massfr;


            this.Invalidate();

            reLoadConfig(ref machine);

            machine.currentData.metrics.pinch.reading = (double)PinchCurrent.Value;
            machine.currentData.metrics.pinch.time = (double)PinchTime.Value;


            if (!tuningThread.IsBusy)
                tuningThread.RunWorkerAsync();

            //Thread t = new Thread(new ThreadStart(tuningThread));
            //t.IsBackground = true;
            //t.Start();
        }

        private void FineTuneBtn_Click(object sender, EventArgs e)
        {
            Simulator s = Simulator.getInstance();
            machine = s.machine;

            DisableButtons();

            // continue from current settings
            reLoadConfig(ref machine);

            machine.currentData.metrics.pinch.reading = (double)PinchCurrent.Value;
            machine.currentData.metrics.pinch.time = (double)PinchTime.Value;

            if (!tuningThread.IsBusy)
                tuningThread.RunWorkerAsync();

        }

        private void TuneElectrical_Click(object sender, EventArgs e)
        {
            Simulator s = Simulator.getInstance();
            machine = s.machine;

            DisableButtons();

            // load guess
            AutoFit.resetTuneGuesses(ref machine);

            currf.Value = (decimal)machine.currf;
            currfr.Value = (decimal)machine.currfr;
            massf.Value = (decimal)machine.massf;
            massfr.Value = (decimal)machine.massfr;

            // continue from current settings
            reLoadConfig(ref machine);

            machine.currentData.metrics.pinch.reading = (double)PinchCurrent.Value;
            machine.currentData.metrics.pinch.time = (double)PinchTime.Value;

            if (!tuningElectricalsThread.IsBusy)
                tuningElectricalsThread.RunWorkerAsync(); 

        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (tuningThread.IsBusy)
                tuningThread.CancelAsync();
            else if (tuningElectricalsThread.IsBusy)
                tuningElectricalsThread.CancelAsync();
        }


        bool pick = false;

        private void PickPinchBtn_Click(object sender, EventArgs e)
        {
            pick = true;

            //CurrentGraph.Cursor = Cursors.Arrow;
        }


        private bool CurrentGraph_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            CurveItem curve;
            int point;

            if (!pick) return false;

            pick = false;

            if (pane.FindNearestPoint(new PointF(e.X, e.Y), out curve, out point))
            {

                double time = curve.Points[point].X;
                double current = curve.Points[point].Y;


                Debug.WriteLine(time + "," + current);

                PinchTime.Value = (decimal)time;
                PinchCurrent.Value = (decimal)current;
            }
            //CurrentGraph.Cursor = Cursors.Default;
            return default(bool);
        }

        #endregion



    }


}
