using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HocLapTrinhWeb.Utilities.Text;

namespace Watermar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnWatemark_Click(object sender, EventArgs e)
        {
            string[] filePaths = Directory.GetFiles(txtInput.Text, "*.pdf");
            string waterImaeger = Directory.GetCurrentDirectory() + @"\Images\logowatermark.png";
            string output = txtOutput.Text;
            for (int i = 0; i < filePaths.Length; i++)
            {
                try
                {
                    var filenameinput = Path.GetFileNameWithoutExtension(filePaths[i]);
                    var fileExt = Path.GetExtension(filePaths[i]);
                    var t = PDF.AddWatermarkImage(filePaths[i], output + filenameinput + fileExt, waterImaeger, false, EnumWatermarkPDF.Align.Center, EnumWatermarkPDF.Valign.Bottom);
                }
                catch { }
            }
            Application.Exit();
        }
    }
}
