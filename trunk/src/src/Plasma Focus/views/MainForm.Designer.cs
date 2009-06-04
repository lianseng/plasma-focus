/*
 * Created by SharpDevelop.
 * User: lohl1
 * Date: 12/11/2008
 * Time: 2:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Plasma_Focus
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the dataFilename code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Contents = new System.Windows.Forms.ToolStripMenuItem();
            this.About = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ParametersTabs = new System.Windows.Forms.TabControl();
            this.ConfigurationTab = new System.Windows.Forms.TabPage();
            this.ResultsTab = new System.Windows.Forms.TabPage();
            this.GraphsTab = new System.Windows.Forms.TabPage();
            this.TuningTab = new System.Windows.Forms.TabPage();
            this.GAConfigTab = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.ParametersTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.Size = new System.Drawing.Size(605, 635);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(951, 24);
            this.menuStrip1.TabIndex = 27;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLoad,
            this.mnuSave,
            this.mnuSaveAs,
            this.exportToolStripMenuItem,
            this.mnuExit});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // mnuLoad
            // 
            this.mnuLoad.Name = "mnuLoad";
            this.mnuLoad.Size = new System.Drawing.Size(128, 22);
            this.mnuLoad.Text = "&Load";
            this.mnuLoad.Click += new System.EventHandler(this.mnuLoad_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(128, 22);
            this.mnuSave.Text = "Save";
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.Size = new System.Drawing.Size(128, 22);
            this.mnuSaveAs.Text = "Save As ...";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(128, 22);
            this.mnuExit.Text = "&Exit";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Contents,
            this.About});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // Contents
            // 
            this.Contents.Name = "Contents";
            this.Contents.Size = new System.Drawing.Size(152, 22);
            this.Contents.Text = "Contents";
            this.Contents.Click += new System.EventHandler(this.Contents_Click);
            // 
            // About
            // 
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(152, 22);
            this.About.Text = "About";
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.AutoScroll = true;
            this.MainPanel.AutoSize = true;
            this.MainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.MainPanel.Controls.Add(this.ParametersTabs);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(951, 691);
            this.MainPanel.TabIndex = 29;
            // 
            // ParametersTabs
            // 
            this.ParametersTabs.Controls.Add(this.ConfigurationTab);
            this.ParametersTabs.Controls.Add(this.ResultsTab);
            this.ParametersTabs.Controls.Add(this.GraphsTab);
            this.ParametersTabs.Controls.Add(this.TuningTab);
            this.ParametersTabs.Controls.Add(this.GAConfigTab);
            this.ParametersTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParametersTabs.Location = new System.Drawing.Point(0, 0);
            this.ParametersTabs.Name = "ParametersTabs";
            this.ParametersTabs.SelectedIndex = 0;
            this.ParametersTabs.Size = new System.Drawing.Size(951, 691);
            this.ParametersTabs.TabIndex = 3;
            this.ParametersTabs.SelectedIndexChanged += new System.EventHandler(this.ParametersTabs_SelectedIndexChanged);
            // 
            // ConfigurationTab
            // 
            this.ConfigurationTab.BackColor = System.Drawing.Color.Transparent;
            this.ConfigurationTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigurationTab.Name = "ConfigurationTab";
            this.ConfigurationTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConfigurationTab.Size = new System.Drawing.Size(943, 665);
            this.ConfigurationTab.TabIndex = 0;
            this.ConfigurationTab.Text = "Configuration";
            this.ConfigurationTab.UseVisualStyleBackColor = true;
            // 
            // ResultsTab
            // 
            this.ResultsTab.BackColor = System.Drawing.Color.Transparent;
            this.ResultsTab.Location = new System.Drawing.Point(4, 22);
            this.ResultsTab.Name = "ResultsTab";
            this.ResultsTab.Size = new System.Drawing.Size(943, 665);
            this.ResultsTab.TabIndex = 3;
            this.ResultsTab.Text = "Results";
            this.ResultsTab.UseVisualStyleBackColor = true;
            // 
            // GraphsTab
            // 
            this.GraphsTab.BackColor = System.Drawing.Color.Transparent;
            this.GraphsTab.Location = new System.Drawing.Point(4, 22);
            this.GraphsTab.Name = "GraphsTab";
            this.GraphsTab.Size = new System.Drawing.Size(943, 665);
            this.GraphsTab.TabIndex = 2;
            this.GraphsTab.Text = "Graphs";
            this.GraphsTab.UseVisualStyleBackColor = true;
            // 
            // TuningTab
            // 
            this.TuningTab.AutoScroll = true;
            this.TuningTab.BackColor = System.Drawing.Color.Transparent;
            this.TuningTab.Location = new System.Drawing.Point(4, 22);
            this.TuningTab.Name = "TuningTab";
            this.TuningTab.Padding = new System.Windows.Forms.Padding(3);
            this.TuningTab.Size = new System.Drawing.Size(943, 665);
            this.TuningTab.TabIndex = 1;
            this.TuningTab.Text = "Tuning";
            this.TuningTab.UseVisualStyleBackColor = true;
            // 
            // GAConfigTab
            // 
            this.GAConfigTab.Location = new System.Drawing.Point(4, 22);
            this.GAConfigTab.Name = "GAConfigTab";
            this.GAConfigTab.Padding = new System.Windows.Forms.Padding(3);
            this.GAConfigTab.Size = new System.Drawing.Size(943, 665);
            this.GAConfigTab.TabIndex = 4;
            this.GAConfigTab.Text = "System Configuration";
            this.GAConfigTab.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(951, 715);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Plasma Focus";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.ParametersTabs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		
		void NumericUpDown2ValueChanged(object sender, System.EventArgs e)
		{

        }
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuLoad;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.TabControl ParametersTabs;
        private System.Windows.Forms.TabPage ConfigurationTab;
        private System.Windows.Forms.TabPage ResultsTab;
        private System.Windows.Forms.TabPage GraphsTab;
        private System.Windows.Forms.TabPage TuningTab;
        private System.Windows.Forms.TabPage GAConfigTab;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Contents;
        private System.Windows.Forms.ToolStripMenuItem About;
	}
}
