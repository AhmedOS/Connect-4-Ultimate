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
    public partial class PlayerTypeForm : Form
    {
        public string filePath = "";
        public string executor = "";
        public int timeout;
        public bool compatible = false, saved = false;
        public int type = 0;
        RadioButton[] rb = new RadioButton[3];
        public PlayerTypeForm()
        {
            InitializeComponent();
            LoadFonts();
            Icon = CommonProperties.GetIcon();
        }
        public void setData(int type, string filePath, string executor, int timeout)
        {
            rb[0] = radioButton1;
            rb[1] = radioButton2;
            rb[2] = radioButton3;
            rb[type].Checked = true;
            compatible = type == 2;
            this.type = type;
            this.filePath = filePath;
            this.executor = executor;
            this.timeout = timeout;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (type == 2 && !compatible)
                MessageBox.Show(this, "Check Custom AI settings and make sure it is compatible.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                saved = true;
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                type = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                type = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button3.Enabled = radioButton3.Checked;
            if (radioButton3.Checked)
                type = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExternalAIForm extAIForm = new ExternalAIForm();
            extAIForm.SetData(filePath, executor, timeout, compatible);
            extAIForm.ShowDialog(this);
            if (extAIForm.saved)
            {
                compatible = extAIForm.compatible;
                filePath = extAIForm.filePath;
                executor = extAIForm.executor;
                timeout = extAIForm.timeout;
            }
        }

        void LoadFonts()
        {
            this.radioButton1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 24F);
            this.radioButton2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 24F);
            this.radioButton3.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 24F);
            this.button1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F);
            this.button2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F);
            this.button3.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
