/*
 * Created by SharpDevelop.
 * User: Keane
 * Date: 7/9/2017
 * Time: 8:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kompare
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        private void button1_Click(object sender, EventArgs e)
        {
            if(ss== null)
                ss = new screenshot();
            pictureBox1.Image = ss.train();
            ss.createfile();
            ss.closeChrome();
        }
        private screenshot ss = null;
        private void button2_Click(object sender, EventArgs e)
        {
            if (ss == null)
                ss = new screenshot();
            ss.loadData();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image i = ss.screenshotToBitmap(ss.takeScreenshot(textBox1.Text));
            button1.BackColor = ss.compare(i) ?  Color.Green : Color.Red;
        }
    }
}
