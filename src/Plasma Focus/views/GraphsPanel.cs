/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       GraphsPanel.cs
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
using ZedGraph;

using Plasma_Focus.models;

namespace Plasma_Focus.views
{
    public partial class GraphsPanel : UserControl
    {
        PlasmaFocusMachine machine;
        Simulator simulator = Simulator.getInstance();
        MasterPane masterPane;

        public GraphsPanel()
        {
            Simulator simulator = Simulator.getInstance();
            machine = simulator.machine;

            InitializeComponent();

            //ParametersList.SelectedIndices={ 0,1 };
            masterPane = Graph1.MasterPane;
            masterPane.PaneList.Clear();
            Graph1.IsShowPointValues = true; 
        }


        public void DrawAxialCurrentChart(GraphPane myPane)
        {

            Simulator simulator = Simulator.getInstance();
            machine = simulator.machine;
            if (myPane == null) return;

            List<IterationResult> results = simulator.modelResults.results;

            // draw data current first
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

            if (results.Count > 0)
            {
                int lastRow = simulator.radiativeStartRow;// simulator.modelResults.lastRadialRow;
                int firstRow = simulator.modelResults.firstRadialRow;

                list = new PointPairList();
                for (int i = 0; i < firstRow; i++)
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
                for (int i = firstRow; i <= lastRow; i++)
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

                this.SuspendLayout();

                myPane.Title.Text = "Current and Voltage";
                myPane.XAxis.Title.Text = "Time (us)";
                myPane.YAxis.Title.Text = "Current (kA), Voltage (V*10)";


            }
            myPane.AxisChange();

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawAxialVoltageChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();


            Simulator simulator = Simulator.getInstance();
            List<IterationResult> results = simulator.modelResults.results;

            for (int i = 0; i < results.Count; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TR, result.VR * 10);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            //myPane.Title.Text = "Voltage";
            //myPane.XAxis.Title.Text = "Time (us)";
            //myPane.YAxis.Title.Text = "Voltage (kV)";

            LineItem curve = myPane.AddCurve("Voltage", list, Color.Blue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawAxialPositionChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            int lastRow = simulator.modelResults.firstRadialRow;

            for (int i = 0; i < lastRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TR, result.ZR);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Axial Trajectories";
            myPane.XAxis.Title.Text = "Time (us)";
            myPane.YAxis.Title.Text = "Speed (cm/us), Position (cm)";

            LineItem curve = myPane.AddCurve("Position", list, Color.Red, SymbolType.Circle);


            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;



            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawAxialSpeedChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            int lastRow = simulator.modelResults.firstRadialRow;

            for (int i = 0; i < lastRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TR, result.ZZR);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Axial Trajectories";
            myPane.XAxis.Title.Text = "Time (us)";
            myPane.YAxis.Title.Text = "Speed (cm/us), Position (cm)";

            LineItem curve = myPane.AddCurve("Speed", list, Color.Blue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawRadialVoltageChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.VR);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Tube Voltage";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "Voltage (kV)";

            LineItem curve = myPane.AddCurve("Voltage", list, Color.Blue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }


        private void DrawPlasmaTempChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.TM);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Plasma temperature";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "Plasma temp (K)";

            LineItem curve = myPane.AddCurve("Plasma temperature", list, Color.Red, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }


        private void DrawRadialShockChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.KSR);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Radial Speeds";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "Positions (cm)";

            LineItem curve = myPane.AddCurve("Radial Shock", list, Color.Black, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawPinchElongationChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.ZFR);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Radial Trajectories";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "Positions (cm)";

            LineItem curve = myPane.AddCurve("Pinch Elongation", list, Color.Blue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawRadialPistonChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.KPR);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Radial Speeds";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "Positions (cm)";

            LineItem curve = myPane.AddCurve("Radial Piston", list, Color.Red, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }


        private void DrawRadialReflectedPositionChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.RRF);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Radial Trajectories";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "Positions (cm)";

            LineItem curve = myPane.AddCurve("Reflected Shock", list, Color.LightBlue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }


        private void DrawRadialShockSpeedChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.SSR);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Radial Speeds";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "Speed (cm/us)";

            LineItem curve = myPane.AddCurve("Radial Shock Speed", list, Color.Black, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawPinchElongationSpeedChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.SZR);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Radial Speeds";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "Speed (cm/us)";

            LineItem curve = myPane.AddCurve("Pinch Elongation Speed", list, Color.Red, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawRadialPistonSpeedChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.SPR);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Radial Speeds";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "peed (cm/us)";

            LineItem curve = myPane.AddCurve("Radial Piston Speed", list, Color.Blue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawGammaChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.gamma);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Specific Heat Ratio and Charge Number";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "";

            LineItem curve = myPane.AddCurve("Specific Heat Ratio", list, Color.Red, SymbolType.Square);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }
        private void DrawEffectiveChargeChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.zeff);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            myPane.Title.Text = "Specific Heat Ratio and Charge Number";
            myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane.YAxis.Title.Text = "";

            LineItem curve = myPane.AddCurve("Charge Number", list, Color.Blue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawResistiveHeatChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.Qj);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            //myPane.Title.Text = "Heat and Radiation";
            //myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            //myPane.YAxis.Title.Text = "Joules";

            LineItem curve = myPane.AddCurve("Resistive Heate", list, Color.Blue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawBremsstrahlungChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.Qbrem);

            }
            this.SuspendLayout();

            //// Set up the title and axis labels
            //myPane.Title.Text = "Heat and Radiation";
            //myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            //myPane.YAxis.Title.Text = "Joules";

            LineItem curve = myPane.AddCurve("Bremsstrahlung", list, Color.Pink, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawRecombinationChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.Qrec);

            }
            this.SuspendLayout();

            //// Set up the title and axis labels
            //myPane.Title.Text = "Heat and Radiation";
            //myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            //myPane.YAxis.Title.Text = "Joules";

            LineItem curve = myPane.AddCurve("Recombination", list, Color.Yellow, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }


        private void DrawLineRadiationChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.Qline);

            }
            this.SuspendLayout();

            // Set up the title and axis labels
            //myPane.Title.Text = "Heat and Radiation";
            //myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            //myPane.YAxis.Title.Text = "Joules";

            LineItem curve = myPane.AddCurve("Line Radiation", list, Color.LightBlue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawTotalRadiationChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.Qrad);

            }
            this.SuspendLayout();

            //// Set up the title and axis labels
            //myPane.Title.Text = "Heat and Radiation";
            //myPane.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            //myPane.YAxis.Title.Text = "Joules";

            LineItem curve = myPane.AddCurve("Total Radiation", list, Color.Purple, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            // Enable the X and Y axis grids
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            this.ResumeLayout();
        }

        private void DrawResistivePowerChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.PJ);

            }
            this.SuspendLayout();

            LineItem curve = myPane.AddCurve("Ohmic Power", list, Color.Blue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            this.ResumeLayout();
        }

        private void DrawBremsstrahlungPowerChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.PBR);

            }
            this.SuspendLayout();

            LineItem curve = myPane.AddCurve("Bremsstrahlung Power", list, Color.Pink, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            this.ResumeLayout();
        }

        private void DrawLineRadiationPowerChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.Pline);

            }
            this.SuspendLayout();

            LineItem curve = myPane.AddCurve("Line radiation Power", list, Color.LightBlue, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            this.ResumeLayout();
        }

        private void DrawRecombinationPowerChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.Prec);

            }
            this.SuspendLayout();

            LineItem curve = myPane.AddCurve("Recombination Power", list, Color.Yellow, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            this.ResumeLayout();
        }

        private void DrawTotalRadiationPowerChart(GraphPane myPane)
        {
            if (myPane == null) return;

            PointPairList list = new PointPairList();

            List<IterationResult> results = simulator.modelResults.results;
            for (int i = simulator.modelResults.firstRadialRow; i < simulator.modelResults.lastRadialRow; i++)
            {
                IterationResult result = results[i];
                list.Add(result.TRRadial, result.PRAD);

            }
            this.SuspendLayout();

            LineItem curve = myPane.AddCurve("Total Power", list, Color.Purple, SymbolType.Circle);

            curve.Line.Width = 1F;
            curve.Line.IsSmooth = true;
            curve.Line.SmoothTension = 0.6F;

            curve.Symbol.Fill = new Fill(Color.White);
            curve.Symbol.Size = 1;

            this.ResumeLayout();
        }
   

        private void Graph1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // find clicked pane
            MasterPane masterPane = ((ZedGraphControl)sender).MasterPane;
            GraphPane pane = masterPane.FindPane(new PointF(e.X, e.Y));

            GraphPopupForm form = new GraphPopupForm();
            ZedGraphControl graph = form.Graph;
            graph.IsShowPointValues = true;

            GraphPane newPane = graph.GraphPane;
            newPane.CurveList = pane.CurveList;

            newPane.Title.Text = pane.Title.Text;
            newPane.XAxis.Title.Text = pane.XAxis.Title.Text;
            newPane.YAxis.Title.Text = pane.YAxis.Title.Text;

            newPane.XAxis.MajorGrid.IsVisible = true;
            newPane.YAxis.MajorGrid.IsVisible = true;
            newPane.XAxis.MinorGrid.IsVisible = true;
            newPane.YAxis.MinorGrid.IsVisible = true;

            graph.AxisChange();
            form.ShowDialog();

            // this.masterPane.PaneList[0].DoubleClick
            //Graph1(new Point(e.X, e.Y));
        }


        public void updatePanel()
        {
            Simulator.sync.WaitOne();
            Simulator simulator = Simulator.getInstance();
            machine = simulator.machine;
    
            #region graphs

            masterPane.PaneList.Clear();

            GraphPane myPane = new GraphPane();
            DrawAxialVoltageChart(myPane);
            DrawAxialCurrentChart(myPane);
            myPane.AxisChange();

            masterPane.Add(myPane);

            GraphPane myPane1 = new GraphPane();
            DrawAxialPositionChart(myPane1);
            DrawAxialSpeedChart(myPane1);
            myPane1.AxisChange();
            masterPane.Add(myPane1);

            GraphPane myPane2 = new GraphPane();
            DrawRadialVoltageChart(myPane2);
            myPane2.AxisChange();
            masterPane.Add(myPane2);

            GraphPane myPane3 = new GraphPane();
            DrawPlasmaTempChart(myPane3);
            myPane3.AxisChange();
            masterPane.Add(myPane3);

            GraphPane myPane4 = new GraphPane();
            DrawRadialShockSpeedChart(myPane4);
            DrawRadialPistonSpeedChart(myPane4);
            DrawPinchElongationSpeedChart(myPane4);
            myPane4.AxisChange();
            masterPane.Add(myPane4);

            GraphPane myPane5 = new GraphPane();
            DrawRadialShockChart(myPane5);
            DrawRadialPistonChart(myPane5);
            DrawPinchElongationChart(myPane5);
            DrawRadialReflectedPositionChart(myPane5);
            myPane5.AxisChange();
            masterPane.Add(myPane5);

            GraphPane myPane6 = new GraphPane();
            DrawGammaChart(myPane6);
            DrawEffectiveChargeChart(myPane6);
            myPane6.AxisChange();
            masterPane.Add(myPane6);

            GraphPane myPane7 = new GraphPane();
            myPane7.Title.Text = "Heat and Radiation";
            myPane7.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane7.YAxis.Title.Text = "Joules";

            DrawResistiveHeatChart(myPane7);
            DrawBremsstrahlungChart(myPane7);
            DrawRecombinationChart(myPane7);
            DrawLineRadiationChart(myPane7);
            DrawTotalRadiationChart(myPane7);
            myPane7.AxisChange();
            masterPane.Add(myPane7);

            GraphPane myPane8 = new GraphPane();
            myPane8.Title.Text = "Radiation Power";
            myPane8.XAxis.Title.Text = "Time, from start of radial phase (ns)";
            myPane8.YAxis.Title.Text = "Watt";

            DrawResistivePowerChart(myPane8);
            DrawBremsstrahlungPowerChart(myPane8);
            DrawRecombinationPowerChart(myPane8);
            DrawLineRadiationPowerChart(myPane8);
            DrawTotalRadiationPowerChart(myPane8);

            // Enable the X and Y axis grids
            myPane8.XAxis.MajorGrid.IsVisible = true;
            myPane8.YAxis.MajorGrid.IsVisible = true;
            myPane8.AxisChange();
            masterPane.Add(myPane8);

            using (Graphics g = CreateGraphics())
            {
                masterPane.SetLayout(g, PaneLayout.SquareColPreferred);
                Graph1.AxisChange();
            }
            Graph1.Invalidate();
#endregion
            Simulator.sync.ReleaseMutex();

        }
    }
}
