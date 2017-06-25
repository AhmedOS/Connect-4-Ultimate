namespace connect4
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnLan = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbxP1Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxP2Name = new System.Windows.Forms.TextBox();
            this.lblP1Color = new System.Windows.Forms.Label();
            this.lblP2Color = new System.Windows.Forms.Label();
            this.lblP1Type = new System.Windows.Forms.Label();
            this.lblP2Type = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBoxP2 = new System.Windows.Forms.PictureBox();
            this.picBoxP1 = new System.Windows.Forms.PictureBox();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxP2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOptions
            // 
            this.btnOptions.Font = new System.Drawing.Font("Electro Shackle", 21.75F);
            this.btnOptions.Location = new System.Drawing.Point(12, 571);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(155, 36);
            this.btnOptions.TabIndex = 5;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Font = new System.Drawing.Font("Electro Shackle", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.Location = new System.Drawing.Point(12, 613);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(155, 36);
            this.btnAbout.TabIndex = 6;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnLan
            // 
            this.btnLan.Font = new System.Drawing.Font("Electro Shackle", 21.75F);
            this.btnLan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLan.Location = new System.Drawing.Point(12, 529);
            this.btnLan.Name = "btnLan";
            this.btnLan.Size = new System.Drawing.Size(155, 36);
            this.btnLan.TabIndex = 4;
            this.btnLan.Text = "LAN";
            this.btnLan.UseVisualStyleBackColor = true;
            this.btnLan.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Electro Shackle", 21.75F);
            this.btnStart.Location = new System.Drawing.Point(12, 487);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(155, 36);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start!";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbxP1Name
            // 
            this.tbxP1Name.BackColor = System.Drawing.Color.Black;
            this.tbxP1Name.Font = new System.Drawing.Font("Electro Shackle", 36F);
            this.tbxP1Name.ForeColor = System.Drawing.Color.White;
            this.tbxP1Name.Location = new System.Drawing.Point(86, 350);
            this.tbxP1Name.Name = "tbxP1Name";
            this.tbxP1Name.Size = new System.Drawing.Size(240, 63);
            this.tbxP1Name.TabIndex = 1;
            this.tbxP1Name.TextChanged += new System.EventHandler(this.tbxP1Name_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Electro Shackle", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(332, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 67);
            this.label2.TabIndex = 8;
            this.label2.Text = "vs";
            // 
            // tbxP2Name
            // 
            this.tbxP2Name.BackColor = System.Drawing.Color.Black;
            this.tbxP2Name.Font = new System.Drawing.Font("Electro Shackle", 36F);
            this.tbxP2Name.ForeColor = System.Drawing.Color.White;
            this.tbxP2Name.Location = new System.Drawing.Point(455, 350);
            this.tbxP2Name.Name = "tbxP2Name";
            this.tbxP2Name.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbxP2Name.Size = new System.Drawing.Size(240, 63);
            this.tbxP2Name.TabIndex = 2;
            this.tbxP2Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbxP2Name.TextChanged += new System.EventHandler(this.tbxP2Name_TextChanged);
            // 
            // lblP1Color
            // 
            this.lblP1Color.AutoSize = true;
            this.lblP1Color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblP1Color.Font = new System.Drawing.Font("Electro Shackle", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Color.ForeColor = System.Drawing.Color.Lime;
            this.lblP1Color.Location = new System.Drawing.Point(12, 346);
            this.lblP1Color.Name = "lblP1Color";
            this.lblP1Color.Size = new System.Drawing.Size(71, 67);
            this.lblP1Color.TabIndex = 11;
            this.lblP1Color.Text = "●";
            this.lblP1Color.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblP2Color
            // 
            this.lblP2Color.AutoSize = true;
            this.lblP2Color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblP2Color.Font = new System.Drawing.Font("Electro Shackle", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Color.ForeColor = System.Drawing.Color.Lime;
            this.lblP2Color.Location = new System.Drawing.Point(701, 346);
            this.lblP2Color.Name = "lblP2Color";
            this.lblP2Color.Size = new System.Drawing.Size(71, 67);
            this.lblP2Color.TabIndex = 12;
            this.lblP2Color.Text = "●";
            this.lblP2Color.Click += new System.EventHandler(this.label4_Click);
            // 
            // lblP1Type
            // 
            this.lblP1Type.AutoSize = true;
            this.lblP1Type.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblP1Type.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblP1Type.Font = new System.Drawing.Font("Electro Shackle", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Type.ForeColor = System.Drawing.Color.Lime;
            this.lblP1Type.Location = new System.Drawing.Point(0, 0);
            this.lblP1Type.Name = "lblP1Type";
            this.lblP1Type.Size = new System.Drawing.Size(125, 30);
            this.lblP1Type.TabIndex = 18;
            this.lblP1Type.Text = "Human";
            this.lblP1Type.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblP2Type
            // 
            this.lblP2Type.AutoSize = true;
            this.lblP2Type.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblP2Type.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblP2Type.Font = new System.Drawing.Font("Electro Shackle", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Type.ForeColor = System.Drawing.Color.Lime;
            this.lblP2Type.Location = new System.Drawing.Point(484, 0);
            this.lblP2Type.Name = "lblP2Type";
            this.lblP2Type.Size = new System.Drawing.Size(125, 30);
            this.lblP2Type.TabIndex = 19;
            this.lblP2Type.Text = "Human";
            this.lblP2Type.Click += new System.EventHandler(this.label6_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblP2Type);
            this.panel1.Controls.Add(this.lblP1Type);
            this.panel1.Location = new System.Drawing.Point(86, 419);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 31);
            this.panel1.TabIndex = 22;
            // 
            // picBoxP2
            // 
            this.picBoxP2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBoxP2.Image = global::connect4.Properties.Resources.electronic_ai;
            this.picBoxP2.Location = new System.Drawing.Point(475, 144);
            this.picBoxP2.Name = "picBoxP2";
            this.picBoxP2.Size = new System.Drawing.Size(200, 200);
            this.picBoxP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxP2.TabIndex = 21;
            this.picBoxP2.TabStop = false;
            this.picBoxP2.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // picBoxP1
            // 
            this.picBoxP1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBoxP1.Image = global::connect4.Properties.Resources.electronic_ai;
            this.picBoxP1.Location = new System.Drawing.Point(107, 144);
            this.picBoxP1.Name = "picBoxP1";
            this.picBoxP1.Size = new System.Drawing.Size(200, 200);
            this.picBoxP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxP1.TabIndex = 20;
            this.picBoxP1.TabStop = false;
            this.picBoxP1.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // bgw
            // 
            this.bgw.WorkerSupportsCancellation = true;
            this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::connect4.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(151, -35);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(479, 201);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picBoxP2);
            this.Controls.Add(this.picBoxP1);
            this.Controls.Add(this.lblP2Color);
            this.Controls.Add(this.lblP1Color);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tbxP2Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxP1Name);
            this.Controls.Add(this.btnLan);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect 4 Ultimate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxP2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnLan;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbxP1Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxP2Name;
        private System.Windows.Forms.Label lblP1Color;
        private System.Windows.Forms.Label lblP2Color;
        private System.Windows.Forms.Label lblP1Type;
        private System.Windows.Forms.Label lblP2Type;
        private System.Windows.Forms.PictureBox picBoxP1;
        private System.Windows.Forms.PictureBox picBoxP2;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker bgw;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

