using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace connect4
{
    public partial class ExternalAIForm : Form
    {
        public bool saved = false;
        public string filePath = "";
        public string executor = "";
        public int timeout;
        public bool compatible = false;

        public ExternalAIForm()
        {
            InitializeComponent();
            LoadFonts();
            Icon = CommonProperties.GetIcon();
        }

        public void SetData(string filePath, string executor, int timeout, bool compatible)
        {
            this.filePath = filePath;
            this.executor = executor;
            this.timeout = timeout;
            textBox2.Text = filePath;
            textBox1.Text = executor;
            textBox3.Text = timeout.ToString();
            changeCompatibility(isCompatible(filePath));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changeCompatibility(isCompatible(filePath));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ret = selectFile();
            if (ret != "")
            {
                filePath = ret;
                textBox2.Text = ret;
            }
        }

        string selectFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "All Files|*.*";
            string str = "";
            if (open.ShowDialog(this) == DialogResult.OK
                && !String.IsNullOrEmpty(str = open.FileName)
                && File.Exists(str))
                return str;
            return "";
        }

        Pair p;
        ExternalAI.State state;
        int exitCode;
        bool isCompatible(string path)
        {
            int timeout;
            if (String.IsNullOrWhiteSpace(textBox3.Text) || !Int32.TryParse(textBox3.Text, out timeout))
            {
                MessageBox.Show(this, "Invalid Value For The Timeout.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            this.timeout = timeout;
            ExternalAI ai = new ExternalAI();
            ai.Init(path, executor, timeout);
            int[,] board = { {2, 1, 1, 0, 1, 1, 1},
                             {0, 0, 0, 1, 0, 0, 0},
                             {1, 1, 1, 0, 1, 1, 1},
                             {0, 0, 0, 1, 0, 0, 0},
                             {1, 1, 1, 0, 1, 1, 1},
                             {0, 0, 0, 1, 0, 0, 0} };
            p = ai.GetNextMove(1, 2, board);
            state = ai.state;
            exitCode = ai.exitCode;
            if (p.first != 0)
            {
                if (state == ExternalAI.State.OK)
                    state = ExternalAI.State.Invalid_Output;
                return false;
            }
            board = new int[,] { {1, 1, 1, 0, 1, 1, 2},
                                 {0, 0, 0, 1, 0, 0, 0},
                                 {1, 1, 1, 0, 1, 1, 1},
                                 {0, 0, 0, 1, 0, 0, 0},
                                 {1, 1, 1, 0, 1, 1, 1},
                                 {0, 0, 0, 1, 0, 0, 0} };
            p = ai.GetNextMove(1, 2, board);
            state = ai.state;
            exitCode = ai.exitCode;
            if (p.first != 6)
            {
                if (state == ExternalAI.State.OK)
                    state = ExternalAI.State.Invalid_Output;
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            changeCompatibility(isCompatible(filePath));
            saved = true;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            executor = textBox1.Text;
        }

        void changeCompatibility(bool value)
        {
            compatible = value;
            if (compatible)
            {
                label4.Text = "Successful";
                label4.ForeColor = Color.Green;
            }
            else
            {
                label4.Text = "Error: " + state;
                if (state == ExternalAI.State.Invalid_Output)
                    label4.Text += " (" + p.first + ")";
                label4.Text += '.';
                if (state != ExternalAI.State.Timeout && state != ExternalAI.State.Failed_To_Start)
                    label4.Text += " Exit Code = " + exitCode.ToString() + '.';
                label4.ForeColor = Color.Red;
            }
        }

        void LoadFonts()
        {
            this.label1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F);
            this.button4.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F);
            this.label2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.AgencyFB), 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
