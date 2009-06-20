namespace Plasma_Focus.views
{
    partial class TuningPanel
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
            this.CurrentGraph = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.currentFilename = new System.Windows.Forms.TextBox();
            this.currf = new System.Windows.Forms.NumericUpDown();
            this.currfr = new System.Windows.Forms.NumericUpDown();
            this.currfLbl = new System.Windows.Forms.Label();
            this.currfrLbl = new System.Windows.Forms.Label();
            this.massfrLbl = new System.Windows.Forms.Label();
            this.massfLbl = new System.Windows.Forms.Label();
            this.massfr = new System.Windows.Forms.NumericUpDown();
            this.massf = new System.Windows.Forms.NumericUpDown();
            this.SaveCurrentsBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.StopBtn = new System.Windows.Forms.Button();
            this.FineTuneBtn = new System.Windows.Forms.Button();
            this.ReTuneBtn = new System.Windows.Forms.Button();
            this.FireBtn = new System.Windows.Forms.Button();
            this.resetBtn = new System.Windows.Forms.Button();
            this.R2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.R0 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.C0 = new System.Windows.Forms.NumericUpDown();
            this.lblC0 = new System.Windows.Forms.Label();
            this.L0 = new System.Windows.Forms.NumericUpDown();
            this.TuneElectrical = new System.Windows.Forms.Button();
            this.lblL0 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PickPinchBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PinchCurrent = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.PinchTime = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Fit = new System.Windows.Forms.GroupBox();
            this.SlopeError = new System.Windows.Forms.TextBox();
            this.PinchError = new System.Windows.Forms.TextBox();
            this.PeakError = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.currf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currfr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.massfr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.massf)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.R0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.C0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.L0)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PinchCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PinchTime)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.Fit.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrentGraph
            // 
            this.CurrentGraph.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.CurrentGraph.Location = new System.Drawing.Point(38, 92);
            this.CurrentGraph.Name = "CurrentGraph";
            this.CurrentGraph.ScrollGrace = 0;
            this.CurrentGraph.ScrollMaxX = 0;
            this.CurrentGraph.ScrollMaxY = 0;
            this.CurrentGraph.ScrollMaxY2 = 0;
            this.CurrentGraph.ScrollMinX = 0;
            this.CurrentGraph.ScrollMinY = 0;
            this.CurrentGraph.ScrollMinY2 = 0;
            this.CurrentGraph.Size = new System.Drawing.Size(482, 466);
            this.CurrentGraph.TabIndex = 5;
            this.CurrentGraph.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.CurrentGraph_MouseUpEvent);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Measured Current Filename";
            // 
            // currentFilename
            // 
            this.currentFilename.Location = new System.Drawing.Point(185, 24);
            this.currentFilename.Name = "currentFilename";
            this.currentFilename.ReadOnly = true;
            this.currentFilename.Size = new System.Drawing.Size(264, 20);
            this.currentFilename.TabIndex = 18;
            // 
            // currf
            // 
            this.currf.DecimalPlaces = 3;
            this.currf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currf.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.currf.Location = new System.Drawing.Point(105, 38);
            this.currf.Name = "currf";
            this.currf.Size = new System.Drawing.Size(60, 20);
            this.currf.TabIndex = 20;
            // 
            // currfr
            // 
            this.currfr.DecimalPlaces = 3;
            this.currfr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currfr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.currfr.Location = new System.Drawing.Point(105, 97);
            this.currfr.Name = "currfr";
            this.currfr.Size = new System.Drawing.Size(60, 20);
            this.currfr.TabIndex = 21;
            // 
            // currfLbl
            // 
            this.currfLbl.AutoSize = true;
            this.currfLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currfLbl.Location = new System.Drawing.Point(105, 20);
            this.currfLbl.Name = "currfLbl";
            this.currfLbl.Size = new System.Drawing.Size(28, 13);
            this.currfLbl.TabIndex = 22;
            this.currfLbl.Text = "currf";
            // 
            // currfrLbl
            // 
            this.currfrLbl.AutoSize = true;
            this.currfrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currfrLbl.Location = new System.Drawing.Point(107, 79);
            this.currfrLbl.Name = "currfrLbl";
            this.currfrLbl.Size = new System.Drawing.Size(31, 13);
            this.currfrLbl.TabIndex = 23;
            this.currfrLbl.Text = "currfr";
            // 
            // massfrLbl
            // 
            this.massfrLbl.AutoSize = true;
            this.massfrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.massfrLbl.Location = new System.Drawing.Point(29, 79);
            this.massfrLbl.Name = "massfrLbl";
            this.massfrLbl.Size = new System.Drawing.Size(37, 13);
            this.massfrLbl.TabIndex = 27;
            this.massfrLbl.Text = "massfr";
            // 
            // massfLbl
            // 
            this.massfLbl.AutoSize = true;
            this.massfLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.massfLbl.Location = new System.Drawing.Point(32, 20);
            this.massfLbl.Name = "massfLbl";
            this.massfLbl.Size = new System.Drawing.Size(34, 13);
            this.massfLbl.TabIndex = 26;
            this.massfLbl.Text = "massf";
            // 
            // massfr
            // 
            this.massfr.DecimalPlaces = 3;
            this.massfr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.massfr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.massfr.Location = new System.Drawing.Point(32, 97);
            this.massfr.Name = "massfr";
            this.massfr.Size = new System.Drawing.Size(60, 20);
            this.massfr.TabIndex = 25;
            // 
            // massf
            // 
            this.massf.DecimalPlaces = 3;
            this.massf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.massf.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.massf.Location = new System.Drawing.Point(32, 38);
            this.massf.Name = "massf";
            this.massf.Size = new System.Drawing.Size(60, 20);
            this.massf.TabIndex = 24;
            // 
            // SaveCurrentsBtn
            // 
            this.SaveCurrentsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveCurrentsBtn.Location = new System.Drawing.Point(223, 579);
            this.SaveCurrentsBtn.Name = "SaveCurrentsBtn";
            this.SaveCurrentsBtn.Size = new System.Drawing.Size(113, 32);
            this.SaveCurrentsBtn.TabIndex = 14;
            this.SaveCurrentsBtn.Text = "Save Model";
            this.SaveCurrentsBtn.UseVisualStyleBackColor = true;
            this.SaveCurrentsBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.currfr);
            this.groupBox1.Controls.Add(this.massfrLbl);
            this.groupBox1.Controls.Add(this.currf);
            this.groupBox1.Controls.Add(this.massfLbl);
            this.groupBox1.Controls.Add(this.FineTuneBtn);
            this.groupBox1.Controls.Add(this.currfLbl);
            this.groupBox1.Controls.Add(this.massfr);
            this.groupBox1.Controls.Add(this.currfrLbl);
            this.groupBox1.Controls.Add(this.massf);
            this.groupBox1.Controls.Add(this.FireBtn);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(551, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 140);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model Parameters";
            // 
            // StopBtn
            // 
            this.StopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopBtn.Location = new System.Drawing.Point(715, 537);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(113, 32);
            this.StopBtn.TabIndex = 70;
            this.StopBtn.Text = "Stop Tuning";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // FineTuneBtn
            // 
            this.FineTuneBtn.Location = new System.Drawing.Point(196, 89);
            this.FineTuneBtn.Name = "FineTuneBtn";
            this.FineTuneBtn.Size = new System.Drawing.Size(109, 32);
            this.FineTuneBtn.TabIndex = 68;
            this.FineTuneBtn.Text = "Fine Tune";
            this.FineTuneBtn.UseVisualStyleBackColor = true;
            this.FineTuneBtn.Click += new System.EventHandler(this.FineTuneBtn_Click);
            // 
            // ReTuneBtn
            // 
            this.ReTuneBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReTuneBtn.Location = new System.Drawing.Point(571, 537);
            this.ReTuneBtn.Name = "ReTuneBtn";
            this.ReTuneBtn.Size = new System.Drawing.Size(113, 32);
            this.ReTuneBtn.TabIndex = 29;
            this.ReTuneBtn.Text = "Re-Tune";
            this.ReTuneBtn.UseVisualStyleBackColor = true;
            this.ReTuneBtn.Click += new System.EventHandler(this.ReTuneBtn_Click);
            // 
            // FireBtn
            // 
            this.FireBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FireBtn.Location = new System.Drawing.Point(196, 30);
            this.FireBtn.Name = "FireBtn";
            this.FireBtn.Size = new System.Drawing.Size(109, 32);
            this.FireBtn.TabIndex = 30;
            this.FireBtn.Text = "Fire Shot";
            this.FireBtn.UseVisualStyleBackColor = true;
            this.FireBtn.Click += new System.EventHandler(this.FireBtn_Click);
            // 
            // resetBtn
            // 
            this.resetBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetBtn.Location = new System.Drawing.Point(79, 579);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(113, 32);
            this.resetBtn.TabIndex = 31;
            this.resetBtn.Text = "Reload";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // R2
            // 
            this.R2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.R2.Location = new System.Drawing.Point(28, 54);
            this.R2.Name = "R2";
            this.R2.ReadOnly = true;
            this.R2.Size = new System.Drawing.Size(54, 20);
            this.R2.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "R2 Fitness";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.R0);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.C0);
            this.groupBox6.Controls.Add(this.lblC0);
            this.groupBox6.Controls.Add(this.L0);
            this.groupBox6.Controls.Add(this.TuneElectrical);
            this.groupBox6.Controls.Add(this.lblL0);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.Location = new System.Drawing.Point(546, 270);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(331, 153);
            this.groupBox6.TabIndex = 65;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Electrical Parameters";
            // 
            // R0
            // 
            this.R0.CausesValidation = false;
            this.R0.DecimalPlaces = 3;
            this.R0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.R0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.R0.Location = new System.Drawing.Point(115, 52);
            this.R0.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.R0.Name = "R0";
            this.R0.Size = new System.Drawing.Size(61, 20);
            this.R0.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(112, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 37);
            this.label11.TabIndex = 10;
            this.label11.Text = "Resistance, R0 (mOhm)";
            // 
            // C0
            // 
            this.C0.CausesValidation = false;
            this.C0.DecimalPlaces = 3;
            this.C0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.C0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.C0.Location = new System.Drawing.Point(37, 113);
            this.C0.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.C0.Name = "C0";
            this.C0.Size = new System.Drawing.Size(61, 20);
            this.C0.TabIndex = 1;
            // 
            // lblC0
            // 
            this.lblC0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblC0.Location = new System.Drawing.Point(37, 80);
            this.lblC0.Name = "lblC0";
            this.lblC0.Size = new System.Drawing.Size(75, 33);
            this.lblC0.TabIndex = 4;
            this.lblC0.Text = "Capacitance, C0 (uF)";
            // 
            // L0
            // 
            this.L0.CausesValidation = false;
            this.L0.DecimalPlaces = 3;
            this.L0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.L0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.L0.Location = new System.Drawing.Point(37, 52);
            this.L0.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.L0.Name = "L0";
            this.L0.Size = new System.Drawing.Size(61, 20);
            this.L0.TabIndex = 0;
            // 
            // TuneElectrical
            // 
            this.TuneElectrical.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TuneElectrical.Location = new System.Drawing.Point(201, 59);
            this.TuneElectrical.Name = "TuneElectrical";
            this.TuneElectrical.Size = new System.Drawing.Size(109, 32);
            this.TuneElectrical.TabIndex = 69;
            this.TuneElectrical.Text = "Tune Electrical";
            this.TuneElectrical.UseVisualStyleBackColor = true;
            this.TuneElectrical.Click += new System.EventHandler(this.TuneElectrical_Click);
            // 
            // lblL0
            // 
            this.lblL0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblL0.Location = new System.Drawing.Point(37, 16);
            this.lblL0.Name = "lblL0";
            this.lblL0.Size = new System.Drawing.Size(67, 35);
            this.lblL0.TabIndex = 2;
            this.lblL0.Text = "Inductance, L0 (nH)";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(549, 598);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(307, 23);
            this.progressBar1.TabIndex = 66;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PickPinchBtn
            // 
            this.PickPinchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickPinchBtn.Location = new System.Drawing.Point(201, 36);
            this.PickPinchBtn.Name = "PickPinchBtn";
            this.PickPinchBtn.Size = new System.Drawing.Size(109, 32);
            this.PickPinchBtn.TabIndex = 73;
            this.PickPinchBtn.Text = "Pick Point";
            this.PickPinchBtn.UseVisualStyleBackColor = true;
            this.PickPinchBtn.Click += new System.EventHandler(this.PickPinchBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PinchCurrent);
            this.groupBox2.Controls.Add(this.PickPinchBtn);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.PinchTime);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(546, 429);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 93);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pinch Point (guess)";
            // 
            // PinchCurrent
            // 
            this.PinchCurrent.CausesValidation = false;
            this.PinchCurrent.DecimalPlaces = 3;
            this.PinchCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PinchCurrent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PinchCurrent.Location = new System.Drawing.Point(110, 44);
            this.PinchCurrent.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PinchCurrent.Name = "PinchCurrent";
            this.PinchCurrent.Size = new System.Drawing.Size(60, 20);
            this.PinchCurrent.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(122, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Current (kA)";
            // 
            // PinchTime
            // 
            this.PinchTime.CausesValidation = false;
            this.PinchTime.DecimalPlaces = 3;
            this.PinchTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PinchTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PinchTime.Location = new System.Drawing.Point(37, 44);
            this.PinchTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PinchTime.Name = "PinchTime";
            this.PinchTime.Size = new System.Drawing.Size(60, 20);
            this.PinchTime.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(37, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "Time (us)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.currentFilename);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(38, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(482, 67);
            this.groupBox3.TabIndex = 71;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Model";
            // 
            // Fit
            // 
            this.Fit.Controls.Add(this.SlopeError);
            this.Fit.Controls.Add(this.PinchError);
            this.Fit.Controls.Add(this.PeakError);
            this.Fit.Controls.Add(this.label7);
            this.Fit.Controls.Add(this.label6);
            this.Fit.Controls.Add(this.label3);
            this.Fit.Controls.Add(this.label2);
            this.Fit.Controls.Add(this.R2);
            this.Fit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Fit.Location = new System.Drawing.Point(551, 5);
            this.Fit.Name = "Fit";
            this.Fit.Size = new System.Drawing.Size(324, 105);
            this.Fit.TabIndex = 72;
            this.Fit.TabStop = false;
            this.Fit.Text = "Fit of curves";
            // 
            // SlopeError
            // 
            this.SlopeError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SlopeError.Location = new System.Drawing.Point(252, 54);
            this.SlopeError.Name = "SlopeError";
            this.SlopeError.ReadOnly = true;
            this.SlopeError.Size = new System.Drawing.Size(54, 20);
            this.SlopeError.TabIndex = 36;
            // 
            // PinchError
            // 
            this.PinchError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PinchError.Location = new System.Drawing.Point(176, 54);
            this.PinchError.Name = "PinchError";
            this.PinchError.ReadOnly = true;
            this.PinchError.Size = new System.Drawing.Size(54, 20);
            this.PinchError.TabIndex = 35;
            // 
            // PeakError
            // 
            this.PeakError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeakError.Location = new System.Drawing.Point(101, 54);
            this.PeakError.Name = "PeakError";
            this.PeakError.ReadOnly = true;
            this.PeakError.Size = new System.Drawing.Size(54, 20);
            this.PeakError.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(249, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Slope Error";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(173, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Pinch Error";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(99, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Peak Error";
            // 
            // progressStatus
            // 
            this.progressStatus.AutoSize = true;
            this.progressStatus.Location = new System.Drawing.Point(548, 576);
            this.progressStatus.Name = "progressStatus";
            this.progressStatus.Size = new System.Drawing.Size(35, 13);
            this.progressStatus.TabIndex = 67;
            this.progressStatus.Text = "label3";
            // 
            // TuningPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Fit);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.progressStatus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.SaveCurrentsBtn);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ReTuneBtn);
            this.Controls.Add(this.CurrentGraph);
            this.Name = "TuningPanel";
            this.Size = new System.Drawing.Size(885, 624);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CurrentCurveReferencePanel_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.currf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currfr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.massfr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.massf)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.R0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.C0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.L0)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PinchCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PinchTime)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Fit.ResumeLayout(false);
            this.Fit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl CurrentGraph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox currentFilename;
        private System.Windows.Forms.NumericUpDown currfr;
        private System.Windows.Forms.Label currfLbl;
        private System.Windows.Forms.Label currfrLbl;
        private System.Windows.Forms.Label massfrLbl;
        private System.Windows.Forms.Label massfLbl;
        private System.Windows.Forms.NumericUpDown massfr;
        private System.Windows.Forms.NumericUpDown massf;
        private System.Windows.Forms.Button SaveCurrentsBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox R2;
        private System.Windows.Forms.Button FireBtn;
        private System.Windows.Forms.Button ReTuneBtn;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown R0;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown C0;
        private System.Windows.Forms.Label lblC0;
        private System.Windows.Forms.NumericUpDown L0;
        private System.Windows.Forms.Label lblL0;
        internal System.Windows.Forms.NumericUpDown currf;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button FineTuneBtn;
        private System.Windows.Forms.Button TuneElectrical;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button PickPinchBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown PinchCurrent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown PinchTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox Fit;
        private System.Windows.Forms.TextBox SlopeError;
        private System.Windows.Forms.TextBox PinchError;
        private System.Windows.Forms.TextBox PeakError;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label progressStatus;

    }
}
