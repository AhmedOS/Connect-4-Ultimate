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
    public partial class CountdownForm : Form
    {
        string message = "";
        int seconds = 0;
        bool okToClose = false;

        public CountdownForm(string message, int seconds)
        {
            InitializeComponent();
            LoadFonts();
            this.message = message;
            this.seconds = seconds;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds--;
            lblTimer.Text = seconds.ToString();
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
            lblTimer.Text = seconds.ToString();
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

        private void CountdownForm_Shown(object sender, EventArgs e)
        {
            timer1.Start();
        }

        void LoadFonts()
        {
            this.lblTimer.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
