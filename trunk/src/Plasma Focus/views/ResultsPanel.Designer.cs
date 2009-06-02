namespace Plasma_Focus.views
{
    partial class ResultsPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lineYield = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.totalYield = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.beamYield = new System.Windows.Forms.TextBox();
            this.thermalYield = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EINP = new System.Windows.Forms.TextBox();
            this.Ecap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CurrentGraph = new ZedGraph.ZedGraphControl();
            this.RadialGroup = new System.Windows.Forms.GroupBox();
            this.PinchCurrent = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pinchDuration = new System.Windows.Forms.TextBox();
            this.radialDuration = new System.Windows.Forms.TextBox();
            this.radialEnd = new System.Windows.Forms.TextBox();
            this.radialStart = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.RadialGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.RadialGroup);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(924, 639);
            this.panel2.TabIndex = 77;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lineYield);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.totalYield);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.beamYield);
            this.groupBox2.Controls.Add(this.thermalYield);
            this.groupBox2.Location = new System.Drawing.Point(621, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 172);
            this.groupBox2.TabIndex = 79;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Neutron Yields";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(138, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 79;
            this.label8.Text = "Line";
            // 
            // lineYield
            // 
            this.lineYield.Location = new System.Drawing.Point(34, 136);
            this.lineYield.Name = "lineYield";
            this.lineYield.ReadOnly = true;
            this.lineYield.Size = new System.Drawing.Size(71, 20);
            this.lineYield.TabIndex = 80;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(134, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Total";
            // 
            // totalYield
            // 
            this.totalYield.Location = new System.Drawing.Point(34, 99);
            this.totalYield.Name = "totalYield";
            this.totalYield.ReadOnly = true;
            this.totalYield.Size = new System.Drawing.Size(71, 20);
            this.totalYield.TabIndex = 78;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(134, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Beam";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(131, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Thermal";
            // 
            // beamYield
            // 
            this.beamYield.Location = new System.Drawing.Point(34, 64);
            this.beamYield.Name = "beamYield";
            this.beamYield.ReadOnly = true;
            this.beamYield.Size = new System.Drawing.Size(71, 20);
            this.beamYield.TabIndex = 77;
            // 
            // thermalYield
            // 
            this.thermalYield.Location = new System.Drawing.Point(34, 29);
            this.thermalYield.Name = "thermalYield";
            this.thermalYield.ReadOnly = true;
            this.thermalYield.Size = new System.Drawing.Size(71, 20);
            this.thermalYield.TabIndex = 76;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EINP);
            this.groupBox1.Controls.Add(this.Ecap);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(621, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 100);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Energy";
            // 
            // EINP
            // 
            this.EINP.Location = new System.Drawing.Point(34, 63);
            this.EINP.Name = "EINP";
            this.EINP.ReadOnly = true;
            this.EINP.Size = new System.Drawing.Size(71, 20);
            this.EINP.TabIndex = 9;
            // 
            // Ecap
            // 
            this.Ecap.BackColor = System.Drawing.SystemColors.Control;
            this.Ecap.Location = new System.Drawing.Point(34, 31);
            this.Ecap.Name = "Ecap";
            this.Ecap.ReadOnly = true;
            this.Ecap.Size = new System.Drawing.Size(71, 20);
            this.Ecap.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "EINP (%)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ecap (kJ)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CurrentGraph);
            this.panel1.Location = new System.Drawing.Point(37, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 535);
            this.panel1.TabIndex = 76;
            // 
            // CurrentGraph
            // 
            this.CurrentGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentGraph.Location = new System.Drawing.Point(0, 0);
            this.CurrentGraph.Name = "CurrentGraph";
            this.CurrentGraph.ScrollGrace = 0;
            this.CurrentGraph.ScrollMaxX = 0;
            this.CurrentGraph.ScrollMaxY = 0;
            this.CurrentGraph.ScrollMaxY2 = 0;
            this.CurrentGraph.ScrollMinX = 0;
            this.CurrentGraph.ScrollMinY = 0;
            this.CurrentGraph.ScrollMinY2 = 0;
            this.CurrentGraph.Size = new System.Drawing.Size(543, 535);
            this.CurrentGraph.TabIndex = 0;
            // 
            // RadialGroup
            // 
            this.RadialGroup.Controls.Add(this.PinchCurrent);
            this.RadialGroup.Controls.Add(this.label9);
            this.RadialGroup.Controls.Add(this.pinchDuration);
            this.RadialGroup.Controls.Add(this.radialDuration);
            this.RadialGroup.Controls.Add(this.radialEnd);
            this.RadialGroup.Controls.Add(this.radialStart);
            this.RadialGroup.Controls.Add(this.label15);
            this.RadialGroup.Controls.Add(this.label13);
            this.RadialGroup.Controls.Add(this.label3);
            this.RadialGroup.Controls.Add(this.label2);
            this.RadialGroup.Location = new System.Drawing.Point(621, 362);
            this.RadialGroup.Name = "RadialGroup";
            this.RadialGroup.Size = new System.Drawing.Size(233, 196);
            this.RadialGroup.TabIndex = 74;
            this.RadialGroup.TabStop = false;
            this.RadialGroup.Text = "Radial Phase";
            // 
            // PinchCurrent
            // 
            this.PinchCurrent.Location = new System.Drawing.Point(34, 168);
            this.PinchCurrent.Name = "PinchCurrent";
            this.PinchCurrent.ReadOnly = true;
            this.PinchCurrent.Size = new System.Drawing.Size(71, 20);
            this.PinchCurrent.TabIndex = 81;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(131, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 80;
            this.label9.Text = "Pinch Curent(kA)";
            // 
            // pinchDuration
            // 
            this.pinchDuration.Location = new System.Drawing.Point(34, 133);
            this.pinchDuration.Name = "pinchDuration";
            this.pinchDuration.ReadOnly = true;
            this.pinchDuration.Size = new System.Drawing.Size(71, 20);
            this.pinchDuration.TabIndex = 7;
            // 
            // radialDuration
            // 
            this.radialDuration.Location = new System.Drawing.Point(34, 96);
            this.radialDuration.Name = "radialDuration";
            this.radialDuration.ReadOnly = true;
            this.radialDuration.Size = new System.Drawing.Size(71, 20);
            this.radialDuration.TabIndex = 6;
            // 
            // radialEnd
            // 
            this.radialEnd.Location = new System.Drawing.Point(34, 59);
            this.radialEnd.Name = "radialEnd";
            this.radialEnd.ReadOnly = true;
            this.radialEnd.Size = new System.Drawing.Size(71, 20);
            this.radialEnd.TabIndex = 5;
            // 
            // radialStart
            // 
            this.radialStart.Location = new System.Drawing.Point(34, 21);
            this.radialStart.Name = "radialStart";
            this.radialStart.ReadOnly = true;
            this.radialStart.Size = new System.Drawing.Size(71, 20);
            this.radialStart.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(131, 136);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(97, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Pinch Duration (ns)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(131, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Radial Duration (us)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Radial End (us)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Radial Start (us)";
            // 
            // ResultsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Name = "ResultsPanel";
            this.Size = new System.Drawing.Size(924, 639);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.RadialGroup.ResumeLayout(false);
            this.RadialGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox EINP;
        private System.Windows.Forms.TextBox Ecap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox totalYield;
        private System.Windows.Forms.TextBox beamYield;
        private System.Windows.Forms.TextBox thermalYield;
        private System.Windows.Forms.Panel panel1;
        private ZedGraph.ZedGraphControl CurrentGraph;
        private System.Windows.Forms.GroupBox RadialGroup;
        private System.Windows.Forms.TextBox pinchDuration;
        private System.Windows.Forms.TextBox radialDuration;
        private System.Windows.Forms.TextBox radialEnd;
        private System.Windows.Forms.TextBox radialStart;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox lineYield;
        private System.Windows.Forms.TextBox PinchCurrent;
        private System.Windows.Forms.Label label9;


    }
}
