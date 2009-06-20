namespace Plasma_Focus.views
{
    partial class GraphsPanel
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

 

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Graph1 = new ZedGraph.ZedGraphControl();
            this.MainPanel.SuspendLayout();
            this.TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.AutoScroll = true;
            this.MainPanel.AutoSize = true;
            this.MainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainPanel.Controls.Add(this.TableLayoutPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(862, 658);
            this.MainPanel.TabIndex = 11;
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.AutoScroll = true;
            this.TableLayoutPanel.AutoSize = true;
            this.TableLayoutPanel.ColumnCount = 1;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel.Controls.Add(this.Graph1, 0, 0);
            this.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 1;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(862, 658);
            this.TableLayoutPanel.TabIndex = 8;
            // 
            // Graph1
            // 
            this.Graph1.AutoScroll = true;
            this.Graph1.AutoSize = true;
            this.Graph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Graph1.Location = new System.Drawing.Point(4, 4);
            this.Graph1.Margin = new System.Windows.Forms.Padding(4);
            this.Graph1.Name = "Graph1";
            this.Graph1.ScrollGrace = 0;
            this.Graph1.ScrollMaxX = 0;
            this.Graph1.ScrollMaxY = 0;
            this.Graph1.ScrollMaxY2 = 0;
            this.Graph1.ScrollMinX = 0;
            this.Graph1.ScrollMinY = 0;
            this.Graph1.ScrollMinY2 = 0;
            this.Graph1.Size = new System.Drawing.Size(854, 650);
            this.Graph1.TabIndex = 7;
            this.Graph1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Graph1_MouseDoubleClick);
            // 
            // GraphsPanel
            // 
            this.AutoSize = true;
            this.Controls.Add(this.MainPanel);
            this.Name = "GraphsPanel";
            this.Size = new System.Drawing.Size(862, 658);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        private ZedGraph.ZedGraphControl Graph1;

 
    }
}
