using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace connect4
{
    public partial class InfoForm : Form
    {
        string message = "";
        int seconds = 0;
        bool okToClose = false;

        public InfoForm(string message, int seconds)
        {
            InitializeComponent();
            LoadFonts();
            this.message = message;
            this.seconds = seconds;
        }

        void LoadFonts()
        {
            this.label1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds--;
            if (seconds == 0)
            {
                okToClose = true;
                timer1.Enabled = false;
                Close();
            }
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            label1.Text = message;
            Icon = Properties.Resources.chip;
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !okToClose;
            if (okToClose)
            {
                timer1.Stop();
                timer1.Dispose();
            }
        }

        private void InfoForm_Shown(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
