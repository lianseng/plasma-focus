/*  ----------------------------------------------------------------------------
 *  National Institute of Education, NTU, Singapore
 *  ----------------------------------------------------------------------------
 *  Lee Model Plasma Focus
 *  ----------------------------------------------------------------------------
 *  File:       HelpForm.cs
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plasma_Focus.views
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            textBox1.Text = System.IO.File.ReadAllText("help.txt", System.Text.Encoding.Default);
            Close.Select();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    #region about
    public class AboutBox : Form
    {
        public String Author = "Loh Lian Seng ";
        public String AppName = "Plasma Focus Simulator";
        public String Version = "Version 1.0";

        public AboutBox()
        {
            InitDialog();
        }

        private void InitDialog()
        {
            this.ClientSize = new Size(290, 140);
            this.Text = "About";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            Button wndClose = new Button();
            wndClose.Text = "OK";
            wndClose.Location = new Point(90, 100);
            wndClose.Size = new Size(72, 24);
            wndClose.Click += new EventHandler(About_OK);

            Label wndAuthorLabel = new Label();
            wndAuthorLabel.Text = "Author:";
            wndAuthorLabel.Location = new Point(5, 5);
            wndAuthorLabel.Size = new Size(72, 24);

            Label wndAuthor = new Label();
            wndAuthor.Text = Author;
            wndAuthor.Location = new Point(80, 5);
            wndAuthor.Size = new Size(80, 24);

            Label wndProdNameLabel = new Label();
            wndProdNameLabel.Text = "Website: ";
            wndProdNameLabel.Location = new Point(5, 30);
            wndProdNameLabel.Size = new Size(50, 24);

            Label wndProdName = new Label();
            wndProdName.Text = "http://code.google.com/p/plasma-focus/";
            wndProdName.Location = new Point(80, 30);
            wndProdName.Size = new Size(210, 24);

            Label wndVersionLabel = new Label();
            wndVersionLabel.Text = AppName;
            wndVersionLabel.Location = new Point(5, 55);
            wndVersionLabel.Size = new Size(210, 24);

            Label wndVersion = new Label();
            wndVersion.Text = Version;
            wndVersion.Location = new Point(5, 80);
            wndVersion.Size = new Size(72, 24);

            this.Controls.AddRange(new Control[] {
                        wndClose,
                        wndVersionLabel,
                        wndAuthorLabel,
                        wndProdNameLabel,
                        wndAuthor,
                        wndProdName,
                        wndVersion
                        });
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowDialog();
        }

        private void About_OK(Object source, EventArgs e)
        {
            //this.Close();
            Control wndCtrl = ((Button)source).Parent;
            ((Form)wndCtrl).Close();
        }
    
    }

#endregion about
}
