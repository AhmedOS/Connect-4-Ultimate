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
    public partial class ProgressForm : Form
    {
        public bool okToClose = false;
        bool value, waitBgw = false, waitPlayer = false;
        BackgroundWorker bgw;
        Connection connection;
        Connection.State state;
        public string DisplayedText = "";
        
        public ProgressForm()
        {
            InitializeComponent();
            LoadFonts();
            Icon = CommonProperties.GetIcon();
        }

        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i++ % 8 == 0)
                lblDisplayedText.Text = DisplayedText;
            else
                lblDisplayedText.Text += '.';
            if (okToClose)
                Close();
        }

        public void InstantClose()
        {
            okToClose = true;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InstantClose();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            lblDisplayedText.Text = DisplayedText;
            timer2.Enabled = waitBgw || waitPlayer;
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !okToClose;
            if (okToClose)
            {
                timer1.Enabled = false;
                timer1.Dispose();
                timer2.Enabled = false;
                timer2.Dispose();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if ((waitBgw && bgw.IsBusy == value) || (waitPlayer && connection.otherPlayerState == state))
            {
                timer2.Enabled = false;
                okToClose = true;
                Close();
            }
        }

        public void WaitBackgroundWorker(BackgroundWorker bgw, bool value, bool cancelButton)
        {
            this.bgw = bgw;
            this.value = value;
            button1.Enabled = cancelButton;
            waitBgw = true;
        }

        public void WaitOtherPlayer(Connection connection, Connection.State state, bool cancelButton)
        {
            this.connection = connection;
            this.state = state;
            button1.Enabled = cancelButton;
            waitPlayer = true;
        }

        void LoadFonts()
        {
            this.lblDisplayedText.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
