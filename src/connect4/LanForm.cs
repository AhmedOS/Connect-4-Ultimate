using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;

namespace connect4
{
    public partial class LanForm : Form
    {
        Connection connection;
        public int myID = -1;
        ProgressForm progressForm;

        public LanForm(Connection connection)
        {
            InitializeComponent();
            LoadFonts();
            Icon = CommonProperties.GetIcon();
            this.connection = connection;
            textBox1.Text = connection.endPoint.Address.ToString();
            if (connection.connectionMode != Connection.Mode.Disconnected)
                SwitchComponents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Init("0.0.0.0", Connection.Mode.Server);
            bgw.RunWorkerAsync();
            progressForm = new ProgressForm();
            progressForm.DisplayedText = "Waiting";
            progressForm.ShowDialog(this);
            if (connection.Connected())
            {
                myID = 0;
                Connected();
            }
            else
                connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text;
            if (connection.goodIp(ip))
            {
                connection.Init(ip, Connection.Mode.Client);
                bgw.RunWorkerAsync();
                progressForm = new ProgressForm();
                progressForm.DisplayedText = "Connecting";
                progressForm.ShowDialog(this);
                if (connection.Connected())
                {
                    myID = 1;
                    Connected();
                }
                else
                    connection.Close();
            }
            else
                MessageBox.Show(this, "Invalid IP Address.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (connection.connectionMode == Connection.Mode.Server)
                    connection.Start();
                else if (connection.connectionMode == Connection.Mode.Client)
                    connection.Connect();
            }
            catch { }
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                progressForm.InstantClose();
            }
            catch { }
        }

        public event EventHandler DisconnectRequested;
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            connection.Close();
            if (DisconnectRequested != null)
                DisconnectRequested(this, EventArgs.Empty);
            SwitchComponents();
        }

        void Connected()
        {
            SwitchComponents();
            Close();
        }

        void SwitchComponents()
        {
            btnDisconnect.Enabled = !btnDisconnect.Enabled;
            button1.Enabled = !button1.Enabled;
            button2.Enabled = !button2.Enabled;
            textBox1.ReadOnly = !textBox1.ReadOnly;
            comboBox1.Enabled = !comboBox1.Enabled;
        }

        private void LanForm_Load(object sender, EventArgs e)
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        comboBox1.Items.Add(ip.Address.ToString());
                        /*
                        if (connection.endPoint.Address.ToString() == ip.Address.ToString())
                            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
                        */
                    }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        if (ip.Address.ToString() == comboBox1.SelectedItem.ToString())
                        {
                            label3.Text = ni.Name;
                            label4.Text = ni.Description;
                            return;
                        }
            }
            label3.Text = label4.Text = "-";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
                try
                {
                    Clipboard.SetText(comboBox1.SelectedItem.ToString(), TextDataFormat.Text);
                }
                catch
                {
                    MessageBox.Show(this, "Failed To Copy.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        void LoadFonts()
        {
            this.textBox1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.AgencyFB), 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F);
            this.button2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F);
            this.label1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F);
            this.label2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.AgencyFB), 14.25F);
            this.tabControl1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F);
        }
    }
}
