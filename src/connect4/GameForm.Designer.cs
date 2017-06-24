namespace connect4
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblP2Name = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblP1Name = new System.Windows.Forms.Label();
            this.lblScore1 = new System.Windows.Forms.Label();
            this.lblTime1 = new System.Windows.Forms.Label();
            this.lblP1Score = new System.Windows.Forms.Label();
            this.lblP1Time = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblP2Time = new System.Windows.Forms.Label();
            this.lblP2Score = new System.Windows.Forms.Label();
            this.lblTime2 = new System.Windows.Forms.Label();
            this.lblScore2 = new System.Windows.Forms.Label();
            this.machineBgw = new System.ComponentModel.BackgroundWorker();
            this.lblP1Type = new System.Windows.Forms.Label();
            this.lblP2Type = new System.Windows.Forms.Label();
            this.lblP1State = new System.Windows.Forms.Label();
            this.lblP2State = new System.Windows.Forms.Label();
            this.lblP1Log = new System.Windows.Forms.Label();
            this.lblP2Log = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(822, 50);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(150, 150);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // lblP2Name
            // 
            this.lblP2Name.AutoSize = true;
            this.lblP2Name.Font = new System.Drawing.Font("Electro Shackle", 18F);
            this.lblP2Name.ForeColor = System.Drawing.Color.Lime;
            this.lblP2Name.Location = new System.Drawing.Point(825, 203);
            this.lblP2Name.Name = "lblP2Name";
            this.lblP2Name.Size = new System.Drawing.Size(126, 25);
            this.lblP2Name.TabIndex = 9;
            this.lblP2Name.Text = "Player 2";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(12, 50);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 150);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // lblP1Name
            // 
            this.lblP1Name.AutoSize = true;
            this.lblP1Name.Font = new System.Drawing.Font("Electro Shackle", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Name.ForeColor = System.Drawing.Color.Lime;
            this.lblP1Name.Location = new System.Drawing.Point(12, 203);
            this.lblP1Name.Name = "lblP1Name";
            this.lblP1Name.Size = new System.Drawing.Size(118, 25);
            this.lblP1Name.TabIndex = 11;
            this.lblP1Name.Text = "Player 1";
            // 
            // lblScore1
            // 
            this.lblScore1.AutoSize = true;
            this.lblScore1.Font = new System.Drawing.Font("Electro Shackle", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore1.ForeColor = System.Drawing.Color.Lime;
            this.lblScore1.Location = new System.Drawing.Point(12, 338);
            this.lblScore1.Name = "lblScore1";
            this.lblScore1.Size = new System.Drawing.Size(92, 25);
            this.lblScore1.TabIndex = 16;
            this.lblScore1.Text = "Score";
            // 
            // lblTime1
            // 
            this.lblTime1.AutoSize = true;
            this.lblTime1.Font = new System.Drawing.Font("Electro Shackle", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime1.ForeColor = System.Drawing.Color.Lime;
            this.lblTime1.Location = new System.Drawing.Point(12, 283);
            this.lblTime1.Name = "lblTime1";
            this.lblTime1.Size = new System.Drawing.Size(77, 25);
            this.lblTime1.TabIndex = 17;
            this.lblTime1.Text = "Time";
            // 
            // lblP1Score
            // 
            this.lblP1Score.AutoSize = true;
            this.lblP1Score.Font = new System.Drawing.Font("Agency FB", 18F);
            this.lblP1Score.ForeColor = System.Drawing.Color.Lime;
            this.lblP1Score.Location = new System.Drawing.Point(102, 336);
            this.lblP1Score.Name = "lblP1Score";
            this.lblP1Score.Size = new System.Drawing.Size(22, 28);
            this.lblP1Score.TabIndex = 18;
            this.lblP1Score.Text = "0";
            // 
            // lblP1Time
            // 
            this.lblP1Time.AutoSize = true;
            this.lblP1Time.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Time.ForeColor = System.Drawing.Color.Lime;
            this.lblP1Time.Location = new System.Drawing.Point(87, 281);
            this.lblP1Time.Name = "lblP1Time";
            this.lblP1Time.Size = new System.Drawing.Size(75, 28);
            this.lblP1Time.TabIndex = 19;
            this.lblP1Time.Text = "000.00s";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblP2Time
            // 
            this.lblP2Time.AutoSize = true;
            this.lblP2Time.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Time.ForeColor = System.Drawing.Color.Lime;
            this.lblP2Time.Location = new System.Drawing.Point(900, 281);
            this.lblP2Time.Name = "lblP2Time";
            this.lblP2Time.Size = new System.Drawing.Size(75, 28);
            this.lblP2Time.TabIndex = 23;
            this.lblP2Time.Text = "000.00s";
            // 
            // lblP2Score
            // 
            this.lblP2Score.AutoSize = true;
            this.lblP2Score.Font = new System.Drawing.Font("Agency FB", 18F);
            this.lblP2Score.ForeColor = System.Drawing.Color.Lime;
            this.lblP2Score.Location = new System.Drawing.Point(915, 336);
            this.lblP2Score.Name = "lblP2Score";
            this.lblP2Score.Size = new System.Drawing.Size(22, 28);
            this.lblP2Score.TabIndex = 22;
            this.lblP2Score.Text = "0";
            // 
            // lblTime2
            // 
            this.lblTime2.AutoSize = true;
            this.lblTime2.Font = new System.Drawing.Font("Electro Shackle", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime2.ForeColor = System.Drawing.Color.Lime;
            this.lblTime2.Location = new System.Drawing.Point(825, 283);
            this.lblTime2.Name = "lblTime2";
            this.lblTime2.Size = new System.Drawing.Size(77, 25);
            this.lblTime2.TabIndex = 21;
            this.lblTime2.Text = "Time";
            // 
            // lblScore2
            // 
            this.lblScore2.AutoSize = true;
            this.lblScore2.Font = new System.Drawing.Font("Electro Shackle", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore2.ForeColor = System.Drawing.Color.Lime;
            this.lblScore2.Location = new System.Drawing.Point(825, 338);
            this.lblScore2.Name = "lblScore2";
            this.lblScore2.Size = new System.Drawing.Size(92, 25);
            this.lblScore2.TabIndex = 20;
            this.lblScore2.Text = "Score";
            // 
            // machineBgw
            // 
            this.machineBgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.machineBgw_DoWork);
            this.machineBgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.machineBgw_RunWorkerCompleted);
            // 
            // lblP1Type
            // 
            this.lblP1Type.AutoSize = true;
            this.lblP1Type.Font = new System.Drawing.Font("Electro Shackle", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Type.ForeColor = System.Drawing.Color.Lime;
            this.lblP1Type.Location = new System.Drawing.Point(12, 228);
            this.lblP1Type.Name = "lblP1Type";
            this.lblP1Type.Size = new System.Drawing.Size(76, 25);
            this.lblP1Type.TabIndex = 24;
            this.lblP1Type.Text = "Type";
            // 
            // lblP2Type
            // 
            this.lblP2Type.AutoSize = true;
            this.lblP2Type.Font = new System.Drawing.Font("Electro Shackle", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Type.ForeColor = System.Drawing.Color.Lime;
            this.lblP2Type.Location = new System.Drawing.Point(825, 228);
            this.lblP2Type.Name = "lblP2Type";
            this.lblP2Type.Size = new System.Drawing.Size(76, 25);
            this.lblP2Type.TabIndex = 25;
            this.lblP2Type.Text = "Type";
            // 
            // lblP1State
            // 
            this.lblP1State.AutoSize = true;
            this.lblP1State.Font = new System.Drawing.Font("Electro Shackle", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1State.ForeColor = System.Drawing.Color.Lime;
            this.lblP1State.Location = new System.Drawing.Point(12, 9);
            this.lblP1State.Name = "lblP1State";
            this.lblP1State.Size = new System.Drawing.Size(154, 25);
            this.lblP1State.TabIndex = 26;
            this.lblP1State.Text = "Your Turn!";
            // 
            // lblP2State
            // 
            this.lblP2State.AutoSize = true;
            this.lblP2State.Font = new System.Drawing.Font("Electro Shackle", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2State.ForeColor = System.Drawing.Color.Lime;
            this.lblP2State.Location = new System.Drawing.Point(822, 9);
            this.lblP2State.Name = "lblP2State";
            this.lblP2State.Size = new System.Drawing.Size(154, 25);
            this.lblP2State.TabIndex = 27;
            this.lblP2State.Text = "Your Turn!";
            // 
            // lblP1Log
            // 
            this.lblP1Log.AutoSize = true;
            this.lblP1Log.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Log.ForeColor = System.Drawing.Color.Lime;
            this.lblP1Log.Location = new System.Drawing.Point(14, 393);
            this.lblP1Log.MaximumSize = new System.Drawing.Size(135, 0);
            this.lblP1Log.Name = "lblP1Log";
            this.lblP1Log.Size = new System.Drawing.Size(30, 16);
            this.lblP1Log.TabIndex = 28;
            this.lblP1Log.Text = "Info";
            // 
            // lblP2Log
            // 
            this.lblP2Log.AutoSize = true;
            this.lblP2Log.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Log.ForeColor = System.Drawing.Color.Lime;
            this.lblP2Log.Location = new System.Drawing.Point(827, 393);
            this.lblP2Log.MaximumSize = new System.Drawing.Size(135, 0);
            this.lblP2Log.Name = "lblP2Log";
            this.lblP2Log.Size = new System.Drawing.Size(30, 16);
            this.lblP2Log.TabIndex = 29;
            this.lblP2Log.Text = "Info";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(984, 501);
            this.Controls.Add(this.lblP2Log);
            this.Controls.Add(this.lblP1Log);
            this.Controls.Add(this.lblP2State);
            this.Controls.Add(this.lblP1State);
            this.Controls.Add(this.lblP2Type);
            this.Controls.Add(this.lblP1Type);
            this.Controls.Add(this.lblP1Score);
            this.Controls.Add(this.lblP1Time);
            this.Controls.Add(this.lblScore1);
            this.Controls.Add(this.lblP1Name);
            this.Controls.Add(this.lblTime1);
            this.Controls.Add(this.lblP2Time);
            this.Controls.Add(this.lblP2Score);
            this.Controls.Add(this.lblTime2);
            this.Controls.Add(this.lblScore2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.lblP2Name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.Shown += new System.EventHandler(this.GameForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblP2Name;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblP1Name;
        private System.Windows.Forms.Label lblScore1;
        private System.Windows.Forms.Label lblTime1;
        private System.Windows.Forms.Label lblP1Score;
        private System.Windows.Forms.Label lblP1Time;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblP2Time;
        private System.Windows.Forms.Label lblP2Score;
        private System.Windows.Forms.Label lblTime2;
        private System.Windows.Forms.Label lblScore2;
        private System.ComponentModel.BackgroundWorker machineBgw;
        private System.Windows.Forms.Label lblP1Type;
        private System.Windows.Forms.Label lblP2Type;
        private System.Windows.Forms.Label lblP1State;
        private System.Windows.Forms.Label lblP2State;
        private System.Windows.Forms.Label lblP1Log;
        private System.Windows.Forms.Label lblP2Log;
    }
}