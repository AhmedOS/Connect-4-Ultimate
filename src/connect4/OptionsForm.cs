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
    public partial class OptionsForm : Form
    {
        public Options options;
        Options _options;
        public OptionsForm(Options options)
        {
            InitializeComponent();
            LoadFonts();
            Icon = CommonProperties.GetIcon();
            this.options = options;
            _options = options;
            label2.ForeColor = options.boardColor;
            label4.ForeColor = options.backColor;
            textBox1.Text = options.searchDepth.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nm;
            if (textBox1.Text.Length > 0 &&
               textBox1.Text.Length <= 9 &&
               Int32.TryParse(textBox1.Text, out nm))
            {
                options.searchDepth = nm;
                DialogResult result = DialogResult.OK;
                if (nm > 7)
                    result = MessageBox.Show(this, "You are searching so deep!, that's may take a lot of time, or even the application may crash (stack overflow).\nDo you want to continue with searching in " + textBox1.Text + " levels?!", "WARNING!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK) {
                    options = _options;
                    Close();
                }
            }
            else
                MessageBox.Show(this, "Invalid input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            options.boardColor = selectColor(options.boardColor);
            label2.ForeColor = options.boardColor;
        }
        Color selectColor(Color clr)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = clr;
            if (dialog.ShowDialog(this) == DialogResult.OK)
                return dialog.Color;
            return clr;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            options.backColor = selectColor(options.backColor);
            label4.ForeColor = options.backColor;
        }

        void LoadFonts()
        {
            this.label3.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 24F);
            this.button1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F);
            this.button2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F);
            this.label4.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
