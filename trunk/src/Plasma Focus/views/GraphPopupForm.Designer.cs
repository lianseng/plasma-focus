namespace Plasma_Focus.views
{
    partial class GraphPopupForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Graph = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // Graph
            // 
            this.Graph.AutoSize = true;
            this.Graph.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Graph.Location = new System.Drawing.Point(0, 0);
            this.Graph.Margin = new System.Windows.Forms.Padding(4);
            this.Graph.Name = "Graph";
            this.Graph.ScrollGrace = 0;
            this.Graph.ScrollMaxX = 0;
            this.Graph.ScrollMaxY = 0;
            this.Graph.ScrollMaxY2 = 0;
            this.Graph.ScrollMinX = 0;
            this.Graph.ScrollMinY = 0;
            this.Graph.ScrollMinY2 = 0;
            this.Graph.Size = new System.Drawing.Size(777, 638);
            this.Graph.TabIndex = 8;
            // 
            // GraphPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 638);
            this.Controls.Add(this.Graph);
            this.Name = "GraphPopupForm";
            this.Text = "GraphPopupForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ZedGraph.ZedGraphControl Graph;


    }
}