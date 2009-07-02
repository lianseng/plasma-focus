namespace Plasma_Focus.views
{
    partial class MachineConfigPanel
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
            this.cbMachine = new System.Windows.Forms.ComboBox();
            this.MachineNameLbl = new System.Windows.Forms.Label();
            this.gboxPresetMachines = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.lblL0 = new System.Windows.Forms.Label();
            this.spnInductance = new System.Windows.Forms.NumericUpDown();
            this.lblC0 = new System.Windows.Forms.Label();
            this.spnCapacitance = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.spnResistance = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.spnMolecularWt = new System.Windows.Forms.NumericUpDown();
            this.spnAtomicWt = new System.Windows.Forms.NumericUpDown();
            this.lblV0 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.spnPressure = new System.Windows.Forms.NumericUpDown();
            this.spnDissociationNumber = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.spnVoltage = new System.Windows.Forms.NumericUpDown();
            this.GasComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.spnTaperStart = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.spnTaperEnd = new System.Windows.Forms.NumericUpDown();
            this.TaperGroup = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.spnMassf = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.spnMassfr = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.spnCurrf = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblCurrfr = new System.Windows.Forms.Label();
            this.spnCurrfr = new System.Windows.Forms.NumericUpDown();
            this.spnInnerRadius = new System.Windows.Forms.NumericUpDown();
            this.lblRa = new System.Windows.Forms.Label();
            this.lblZ0 = new System.Windows.Forms.Label();
            this.spnOuterRadius = new System.Windows.Forms.NumericUpDown();
            this.lblRb = new System.Windows.Forms.Label();
            this.spnAnodeLength = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbTapered = new System.Windows.Forms.CheckBox();
            this.saveModelButton = new System.Windows.Forms.Button();
            this.LoadModelBtn = new System.Windows.Forms.Button();
            this.currentFilename = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gboxPresetMachines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnInductance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnCapacitance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnResistance)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnMolecularWt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnAtomicWt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnPressure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDissociationNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnVoltage)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnTaperStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTaperEnd)).BeginInit();
            this.TaperGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnMassf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMassfr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnCurrf)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnCurrfr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnInnerRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnOuterRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnAnodeLength)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMachine
            // 
            this.cbMachine.AllowDrop = true;
            this.cbMachine.FormattingEnabled = true;
            this.cbMachine.ItemHeight = 13;
            this.cbMachine.Location = new System.Drawing.Point(121, 21);
            this.cbMachine.Name = "cbMachine";
            this.cbMachine.Size = new System.Drawing.Size(387, 21);
            this.cbMachine.TabIndex = 0;
            this.cbMachine.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbMachine_MouseClick);
            this.cbMachine.SelectedIndexChanged += new System.EventHandler(this.cbMachine_SelectedIndexChanged);
            // 
            // MachineNameLbl
            // 
            this.MachineNameLbl.AutoSize = true;
            this.MachineNameLbl.Location = new System.Drawing.Point(17, 24);
            this.MachineNameLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MachineNameLbl.Name = "MachineNameLbl";
            this.MachineNameLbl.Size = new System.Drawing.Size(83, 13);
            this.MachineNameLbl.TabIndex = 22;
            this.MachineNameLbl.Text = "Select  machine";
            // 
            // gboxPresetMachines
            // 
            this.gboxPresetMachines.Controls.Add(this.MachineNameLbl);
            this.gboxPresetMachines.Controls.Add(this.cbMachine);
            this.gboxPresetMachines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gboxPresetMachines.Location = new System.Drawing.Point(54, 20);
            this.gboxPresetMachines.Name = "gboxPresetMachines";
            this.gboxPresetMachines.Size = new System.Drawing.Size(558, 59);
            this.gboxPresetMachines.TabIndex = 68;
            this.gboxPresetMachines.TabStop = false;
            this.gboxPresetMachines.Text = "Machine Name";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(667, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 32);
            this.button2.TabIndex = 62;
            this.button2.Text = "Fire shot";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnFireShot);
            // 
            // lblL0
            // 
            this.lblL0.Location = new System.Drawing.Point(118, 28);
            this.lblL0.Name = "lblL0";
            this.lblL0.Size = new System.Drawing.Size(110, 23);
            this.lblL0.TabIndex = 2;
            this.lblL0.Text = "Inductance, L0 (nH)";
            // 
            // spnInductance
            // 
            this.spnInductance.CausesValidation = false;
            this.spnInductance.DecimalPlaces = 3;
            this.spnInductance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnInductance.Location = new System.Drawing.Point(20, 26);
            this.spnInductance.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spnInductance.Name = "spnInductance";
            this.spnInductance.Size = new System.Drawing.Size(76, 20);
            this.spnInductance.TabIndex = 0;
            this.spnInductance.ValueChanged += new System.EventHandler(this.spnInductance_ValueChanged);
            // 
            // lblC0
            // 
            this.lblC0.Location = new System.Drawing.Point(118, 71);
            this.lblC0.Name = "lblC0";
            this.lblC0.Size = new System.Drawing.Size(117, 23);
            this.lblC0.TabIndex = 4;
            this.lblC0.Text = "Capacitance, C0 (uF)";
            // 
            // spnCapacitance
            // 
            this.spnCapacitance.CausesValidation = false;
            this.spnCapacitance.DecimalPlaces = 3;
            this.spnCapacitance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnCapacitance.Location = new System.Drawing.Point(20, 69);
            this.spnCapacitance.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spnCapacitance.Name = "spnCapacitance";
            this.spnCapacitance.Size = new System.Drawing.Size(76, 20);
            this.spnCapacitance.TabIndex = 1;
            this.spnCapacitance.ValueChanged += new System.EventHandler(this.spnCapacitance_ValueChanged);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(118, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 23);
            this.label11.TabIndex = 10;
            this.label11.Text = "Resistance, R0 (mOhm)";
            // 
            // spnResistance
            // 
            this.spnResistance.CausesValidation = false;
            this.spnResistance.DecimalPlaces = 3;
            this.spnResistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnResistance.Location = new System.Drawing.Point(20, 112);
            this.spnResistance.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spnResistance.Name = "spnResistance";
            this.spnResistance.Size = new System.Drawing.Size(76, 20);
            this.spnResistance.TabIndex = 2;
            this.spnResistance.ValueChanged += new System.EventHandler(this.spnResistance_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.spnResistance);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.spnCapacitance);
            this.groupBox6.Controls.Add(this.lblC0);
            this.groupBox6.Controls.Add(this.spnInductance);
            this.groupBox6.Controls.Add(this.lblL0);
            this.groupBox6.Location = new System.Drawing.Point(54, 268);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(260, 152);
            this.groupBox6.TabIndex = 64;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Electrical Parameters";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(120, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 2;
            this.label10.Text = "Atomic Wt.";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(120, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 23);
            this.label9.TabIndex = 4;
            this.label9.Text = "Molecular Wt.";
            // 
            // spnMolecularWt
            // 
            this.spnMolecularWt.CausesValidation = false;
            this.spnMolecularWt.DecimalPlaces = 3;
            this.spnMolecularWt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnMolecularWt.Location = new System.Drawing.Point(26, 120);
            this.spnMolecularWt.Name = "spnMolecularWt";
            this.spnMolecularWt.Size = new System.Drawing.Size(75, 20);
            this.spnMolecularWt.TabIndex = 1;
            this.spnMolecularWt.ValueChanged += new System.EventHandler(this.spnMolecularWt_ValueChanged);
            // 
            // spnAtomicWt
            // 
            this.spnAtomicWt.CausesValidation = false;
            this.spnAtomicWt.DecimalPlaces = 3;
            this.spnAtomicWt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnAtomicWt.Location = new System.Drawing.Point(26, 82);
            this.spnAtomicWt.Name = "spnAtomicWt";
            this.spnAtomicWt.Size = new System.Drawing.Size(75, 20);
            this.spnAtomicWt.TabIndex = 0;
            this.spnAtomicWt.ValueChanged += new System.EventHandler(this.spnAtomicWt_ValueChanged);
            // 
            // lblV0
            // 
            this.lblV0.Location = new System.Drawing.Point(120, 207);
            this.lblV0.Name = "lblV0";
            this.lblV0.Size = new System.Drawing.Size(100, 23);
            this.lblV0.TabIndex = 16;
            this.lblV0.Text = "Voltage, V0 (kV)";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(120, 160);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 23);
            this.label12.TabIndex = 20;
            this.label12.Text = "Dissociation Number";
            // 
            // spnPressure
            // 
            this.spnPressure.CausesValidation = false;
            this.spnPressure.DecimalPlaces = 3;
            this.spnPressure.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnPressure.Location = new System.Drawing.Point(25, 249);
            this.spnPressure.Name = "spnPressure";
            this.spnPressure.Size = new System.Drawing.Size(75, 20);
            this.spnPressure.TabIndex = 3;
            this.spnPressure.ValueChanged += new System.EventHandler(this.spnPressure_ValueChanged);
            // 
            // spnDissociationNumber
            // 
            this.spnDissociationNumber.CausesValidation = false;
            this.spnDissociationNumber.DecimalPlaces = 3;
            this.spnDissociationNumber.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnDissociationNumber.Location = new System.Drawing.Point(26, 160);
            this.spnDissociationNumber.Name = "spnDissociationNumber";
            this.spnDissociationNumber.Size = new System.Drawing.Size(75, 20);
            this.spnDissociationNumber.TabIndex = 4;
            this.spnDissociationNumber.ValueChanged += new System.EventHandler(this.spnDissociationNumber_ValueChanged);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(119, 250);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 23);
            this.label14.TabIndex = 14;
            this.label14.Text = "Pressure, P0 (torr)";
            // 
            // spnVoltage
            // 
            this.spnVoltage.CausesValidation = false;
            this.spnVoltage.DecimalPlaces = 3;
            this.spnVoltage.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnVoltage.Location = new System.Drawing.Point(26, 206);
            this.spnVoltage.Name = "spnVoltage";
            this.spnVoltage.Size = new System.Drawing.Size(75, 20);
            this.spnVoltage.TabIndex = 2;
            this.spnVoltage.ValueChanged += new System.EventHandler(this.spnVoltage_ValueChanged);
            // 
            // GasComboBox
            // 
            this.GasComboBox.FormattingEnabled = true;
            this.GasComboBox.Items.AddRange(new object[] {
            "Hydrogen",
            "Deuterium",
            "Helium",
            "Argon",
            "Neon"});
            this.GasComboBox.Location = new System.Drawing.Point(24, 34);
            this.GasComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.GasComboBox.Name = "GasComboBox";
            this.GasComboBox.Size = new System.Drawing.Size(76, 21);
            this.GasComboBox.TabIndex = 21;
            this.GasComboBox.SelectedIndexChanged += new System.EventHandler(this.GasComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Fill Gas";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.GasComboBox);
            this.groupBox5.Controls.Add(this.spnVoltage);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.spnDissociationNumber);
            this.groupBox5.Controls.Add(this.spnPressure);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.lblV0);
            this.groupBox5.Controls.Add(this.spnAtomicWt);
            this.groupBox5.Controls.Add(this.spnMolecularWt);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(342, 89);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(270, 331);
            this.groupBox5.TabIndex = 65;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Operational Parameters";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(115, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 6;
            this.label8.Text = "Taper start (cm)";
            // 
            // spnTaperStart
            // 
            this.spnTaperStart.CausesValidation = false;
            this.spnTaperStart.DecimalPlaces = 3;
            this.spnTaperStart.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnTaperStart.Location = new System.Drawing.Point(17, 38);
            this.spnTaperStart.Name = "spnTaperStart";
            this.spnTaperStart.Size = new System.Drawing.Size(76, 20);
            this.spnTaperStart.TabIndex = 1;
            this.spnTaperStart.ValueChanged += new System.EventHandler(this.spnTaperStart_ValueChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(115, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 7;
            this.label7.Text = "End radius (cm)";
            // 
            // spnTaperEnd
            // 
            this.spnTaperEnd.CausesValidation = false;
            this.spnTaperEnd.DecimalPlaces = 3;
            this.spnTaperEnd.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnTaperEnd.Location = new System.Drawing.Point(17, 80);
            this.spnTaperEnd.Name = "spnTaperEnd";
            this.spnTaperEnd.Size = new System.Drawing.Size(76, 20);
            this.spnTaperEnd.TabIndex = 2;
            this.spnTaperEnd.ValueChanged += new System.EventHandler(this.spnTaperEnd_ValueChanged);
            // 
            // TaperGroup
            // 
            this.TaperGroup.Controls.Add(this.spnTaperEnd);
            this.TaperGroup.Controls.Add(this.label7);
            this.TaperGroup.Controls.Add(this.spnTaperStart);
            this.TaperGroup.Controls.Add(this.label8);
            this.TaperGroup.Location = new System.Drawing.Point(57, 456);
            this.TaperGroup.Name = "TaperGroup";
            this.TaperGroup.Size = new System.Drawing.Size(257, 132);
            this.TaperGroup.TabIndex = 66;
            this.TaperGroup.TabStop = false;
            this.TaperGroup.Text = "Anode Taper";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(120, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "massf";
            // 
            // spnMassf
            // 
            this.spnMassf.CausesValidation = false;
            this.spnMassf.DecimalPlaces = 3;
            this.spnMassf.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnMassf.Location = new System.Drawing.Point(24, 24);
            this.spnMassf.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.spnMassf.Name = "spnMassf";
            this.spnMassf.Size = new System.Drawing.Size(75, 20);
            this.spnMassf.TabIndex = 0;
            this.spnMassf.ValueChanged += new System.EventHandler(this.spnMassf_ValueChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(119, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "massfr";
            // 
            // spnMassfr
            // 
            this.spnMassfr.DecimalPlaces = 3;
            this.spnMassfr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnMassfr.Location = new System.Drawing.Point(24, 56);
            this.spnMassfr.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.spnMassfr.Name = "spnMassfr";
            this.spnMassfr.Size = new System.Drawing.Size(75, 20);
            this.spnMassfr.TabIndex = 1;
            this.spnMassfr.ValueChanged += new System.EventHandler(this.spnMassfr_ValueChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(119, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "currf";
            // 
            // spnCurrf
            // 
            this.spnCurrf.DecimalPlaces = 3;
            this.spnCurrf.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnCurrf.Location = new System.Drawing.Point(24, 89);
            this.spnCurrf.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.spnCurrf.Name = "spnCurrf";
            this.spnCurrf.Size = new System.Drawing.Size(75, 20);
            this.spnCurrf.TabIndex = 2;
            this.spnCurrf.ValueChanged += new System.EventHandler(this.spnCurrf_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.spnCurrf);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.spnMassfr);
            this.groupBox3.Controls.Add(this.lblCurrfr);
            this.groupBox3.Controls.Add(this.spnCurrfr);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.spnMassf);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(342, 432);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 156);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Model Parameters";
            // 
            // lblCurrfr
            // 
            this.lblCurrfr.Location = new System.Drawing.Point(119, 126);
            this.lblCurrfr.Name = "lblCurrfr";
            this.lblCurrfr.Size = new System.Drawing.Size(100, 23);
            this.lblCurrfr.TabIndex = 63;
            this.lblCurrfr.Text = "currfr";
            // 
            // spnCurrfr
            // 
            this.spnCurrfr.DecimalPlaces = 3;
            this.spnCurrfr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnCurrfr.Location = new System.Drawing.Point(24, 124);
            this.spnCurrfr.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.spnCurrfr.Name = "spnCurrfr";
            this.spnCurrfr.Size = new System.Drawing.Size(75, 20);
            this.spnCurrfr.TabIndex = 59;
            this.spnCurrfr.ValueChanged += new System.EventHandler(this.spnCurrfr_ValueChanged);
            // 
            // spnInnerRadius
            // 
            this.spnInnerRadius.CausesValidation = false;
            this.spnInnerRadius.DecimalPlaces = 3;
            this.spnInnerRadius.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnInnerRadius.Location = new System.Drawing.Point(19, 119);
            this.spnInnerRadius.Name = "spnInnerRadius";
            this.spnInnerRadius.Size = new System.Drawing.Size(76, 20);
            this.spnInnerRadius.TabIndex = 5;
            this.spnInnerRadius.ValueChanged += new System.EventHandler(this.spnInnerRadius_ValueChanged);
            // 
            // lblRa
            // 
            this.lblRa.Location = new System.Drawing.Point(117, 121);
            this.lblRa.Name = "lblRa";
            this.lblRa.Size = new System.Drawing.Size(118, 23);
            this.lblRa.TabIndex = 8;
            this.lblRa.Text = "Inner Radius, Ra (cm)";
            // 
            // lblZ0
            // 
            this.lblZ0.Location = new System.Drawing.Point(118, 35);
            this.lblZ0.Name = "lblZ0";
            this.lblZ0.Size = new System.Drawing.Size(100, 23);
            this.lblZ0.TabIndex = 12;
            this.lblZ0.Text = "Anode Length, z0 (m)";
            // 
            // spnOuterRadius
            // 
            this.spnOuterRadius.CausesValidation = false;
            this.spnOuterRadius.DecimalPlaces = 3;
            this.spnOuterRadius.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnOuterRadius.Location = new System.Drawing.Point(19, 76);
            this.spnOuterRadius.Name = "spnOuterRadius";
            this.spnOuterRadius.Size = new System.Drawing.Size(76, 20);
            this.spnOuterRadius.TabIndex = 4;
            this.spnOuterRadius.ValueChanged += new System.EventHandler(this.spnOuterRadius_ValueChanged);
            // 
            // lblRb
            // 
            this.lblRb.Location = new System.Drawing.Point(117, 78);
            this.lblRb.Name = "lblRb";
            this.lblRb.Size = new System.Drawing.Size(129, 23);
            this.lblRb.TabIndex = 6;
            this.lblRb.Text = "Outer Radius, Rb (cm)";
            // 
            // spnAnodeLength
            // 
            this.spnAnodeLength.CausesValidation = false;
            this.spnAnodeLength.DecimalPlaces = 3;
            this.spnAnodeLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spnAnodeLength.Location = new System.Drawing.Point(20, 33);
            this.spnAnodeLength.Name = "spnAnodeLength";
            this.spnAnodeLength.Size = new System.Drawing.Size(76, 20);
            this.spnAnodeLength.TabIndex = 3;
            this.spnAnodeLength.ValueChanged += new System.EventHandler(this.spnAnodeLength_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.spnAnodeLength);
            this.groupBox1.Controls.Add(this.lblRb);
            this.groupBox1.Controls.Add(this.spnOuterRadius);
            this.groupBox1.Controls.Add(this.lblZ0);
            this.groupBox1.Controls.Add(this.lblRa);
            this.groupBox1.Controls.Add(this.spnInnerRadius);
            this.groupBox1.Location = new System.Drawing.Point(54, 92);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(260, 168);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geometry";
            // 
            // cbTapered
            // 
            this.cbTapered.Location = new System.Drawing.Point(57, 426);
            this.cbTapered.Name = "cbTapered";
            this.cbTapered.Size = new System.Drawing.Size(104, 24);
            this.cbTapered.TabIndex = 60;
            this.cbTapered.Text = "Tapered Anode";
            this.cbTapered.UseVisualStyleBackColor = true;
            this.cbTapered.CheckedChanged += new System.EventHandler(this.cbTapered_CheckedChanged);
            // 
            // saveModelButton
            // 
            this.saveModelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saveModelButton.Location = new System.Drawing.Point(667, 293);
            this.saveModelButton.Name = "saveModelButton";
            this.saveModelButton.Size = new System.Drawing.Size(116, 32);
            this.saveModelButton.TabIndex = 61;
            this.saveModelButton.Text = "Save Model";
            this.saveModelButton.UseVisualStyleBackColor = true;
            this.saveModelButton.Click += new System.EventHandler(this.saveModelButton_Click);
            // 
            // LoadModelBtn
            // 
            this.LoadModelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LoadModelBtn.Location = new System.Drawing.Point(667, 239);
            this.LoadModelBtn.Name = "LoadModelBtn";
            this.LoadModelBtn.Size = new System.Drawing.Size(116, 32);
            this.LoadModelBtn.TabIndex = 70;
            this.LoadModelBtn.Text = "Load Model";
            this.LoadModelBtn.UseVisualStyleBackColor = true;
            this.LoadModelBtn.Click += new System.EventHandler(this.LoadModelBtn_Click);
            // 
            // currentFilename
            // 
            this.currentFilename.Location = new System.Drawing.Point(233, 609);
            this.currentFilename.Name = "currentFilename";
            this.currentFilename.Size = new System.Drawing.Size(379, 20);
            this.currentFilename.TabIndex = 72;
            this.currentFilename.TextChanged += new System.EventHandler(this.currentFilename_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(57, 612);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Measured Current Filename";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(672, 602);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 32);
            this.button1.TabIndex = 73;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MachineConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.currentFilename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LoadModelBtn);
            this.Controls.Add(this.cbTapered);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveModelButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.TaperGroup);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.gboxPresetMachines);
            this.Name = "MachineConfigPanel";
            this.Size = new System.Drawing.Size(847, 658);
            this.Load += new System.EventHandler(this.ConfigurationPanel_Load);
            this.Leave += new System.EventHandler(this.MachineConfigPanel_Leave);
            this.gboxPresetMachines.ResumeLayout(false);
            this.gboxPresetMachines.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnInductance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnCapacitance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnResistance)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnMolecularWt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnAtomicWt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnPressure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDissociationNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnVoltage)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnTaperStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTaperEnd)).EndInit();
            this.TaperGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnMassf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMassfr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnCurrf)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnCurrfr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnInnerRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnOuterRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnAnodeLength)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMachine;
        private System.Windows.Forms.Label MachineNameLbl;
        private System.Windows.Forms.GroupBox gboxPresetMachines;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblL0;
        private System.Windows.Forms.NumericUpDown spnInductance;
        private System.Windows.Forms.Label lblC0;
        private System.Windows.Forms.NumericUpDown spnCapacitance;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown spnResistance;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown spnMolecularWt;
        private System.Windows.Forms.NumericUpDown spnAtomicWt;
        private System.Windows.Forms.Label lblV0;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown spnPressure;
        private System.Windows.Forms.NumericUpDown spnDissociationNumber;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown spnVoltage;
        private System.Windows.Forms.ComboBox GasComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown spnTaperStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown spnTaperEnd;
        private System.Windows.Forms.GroupBox TaperGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown spnMassf;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown spnMassfr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown spnCurrf;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblCurrfr;
        private System.Windows.Forms.NumericUpDown spnCurrfr;
        private System.Windows.Forms.NumericUpDown spnInnerRadius;
        private System.Windows.Forms.Label lblRa;
        private System.Windows.Forms.Label lblZ0;
        private System.Windows.Forms.NumericUpDown spnOuterRadius;
        private System.Windows.Forms.Label lblRb;
        private System.Windows.Forms.NumericUpDown spnAnodeLength;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbTapered;
        private System.Windows.Forms.Button saveModelButton;
        private System.Windows.Forms.Button LoadModelBtn;
        private System.Windows.Forms.TextBox currentFilename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;


    }
}
