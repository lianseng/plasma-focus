/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       ResultsPanel.cs
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
    public partial class ResultsPanel : UserControl
    {
        public ResultsPanel()
        {
            InitializeComponent();
        }


        public void updatePanel()
        {
            Simulator.sync.WaitOne(); 
            this.Enabled = true;
            Simulator s = Simulator.getInstance();

            if (s.modelResults != null)
            {

                Ecap.Text = String.Format("{0:0.000}", s.modelResults.Ecap);
                EINP.Text = String.Format("{0:0.000}", s.modelResults.EINP);

                radialStart.Text = String.Format("{0:0.000}", s.modelResults.TradialStart);
                radialEnd.Text = String.Format("{0:0.000}", s.modelResults.radialEnd);
                radialDuration.Text = String.Format("{0:0.000}", s.modelResults.radialDuration);
                pinchDuration.Text = String.Format("{0:0.000}", s.modelResults.pinchDuration);

                thermalYield.Text = String.Format("{0:0.000}", s.modelResults.NTN);
                beamYield.Text = String.Format("{0:0.000}", s.modelResults.NBN);
                totalYield.Text = String.Format("{0:0.000}", s.modelResults.NN);

                lineYield.Text = String.Format("{0:0.0000}", s.modelResults.SXR);
                PinchCurrent.Text = String.Format("{0:0.0000}", s.modelResults.IPinch);
            }

            // redraw curve
            MainForm main = ((MainForm)(this.ParentForm)); 
            //if (s.machine.currentData!=null )
            { 
                GraphPane pane = CurrentGraph.GraphPane;

                if (pane.ZoomStack.Count > 0)
                    pane.ZoomStack.Last().ApplyState(pane);

                pane.CurveList.Clear();
                main.graphsPanel.DrawAxialCurrentChart(pane);


                pane.AxisChange();
            }
            Invalidate();
            Simulator.sync.ReleaseMutex(); 

        }
    }
}
