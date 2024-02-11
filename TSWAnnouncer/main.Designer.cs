namespace TSWAnnouncer
{
    partial class main
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
            this.LblCurRoute = new System.Windows.Forms.Label();
            this.LblCurStat = new System.Windows.Forms.Label();
            this.LblCurAnon = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.BtnPlay = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblCurRoute
            // 
            this.LblCurRoute.AutoSize = true;
            this.LblCurRoute.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblCurRoute.Location = new System.Drawing.Point(12, 9);
            this.LblCurRoute.Name = "LblCurRoute";
            this.LblCurRoute.Size = new System.Drawing.Size(152, 25);
            this.LblCurRoute.TabIndex = 0;
            this.LblCurRoute.Text = "Current route: ";
            // 
            // LblCurStat
            // 
            this.LblCurStat.AutoSize = true;
            this.LblCurStat.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblCurStat.Location = new System.Drawing.Point(12, 34);
            this.LblCurStat.Name = "LblCurStat";
            this.LblCurStat.Size = new System.Drawing.Size(215, 25);
            this.LblCurStat.TabIndex = 1;
            this.LblCurStat.Text = "Current/next station: ";
            // 
            // LblCurAnon
            // 
            this.LblCurAnon.AutoSize = true;
            this.LblCurAnon.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblCurAnon.Location = new System.Drawing.Point(12, 59);
            this.LblCurAnon.Name = "LblCurAnon";
            this.LblCurAnon.Size = new System.Drawing.Size(206, 25);
            this.LblCurAnon.TabIndex = 2;
            this.LblCurAnon.Text = "Next announcement:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Previous record";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Next record";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // BtnPlay
            // 
            this.BtnPlay.BackColor = System.Drawing.Color.CadetBlue;
            this.BtnPlay.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnPlay.Location = new System.Drawing.Point(12, 142);
            this.BtnPlay.Name = "BtnPlay";
            this.BtnPlay.Size = new System.Drawing.Size(188, 23);
            this.BtnPlay.TabIndex = 5;
            this.BtnPlay.Text = "Play next record";
            this.BtnPlay.UseVisualStyleBackColor = false;
            this.BtnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LightCoral;
            this.button4.Location = new System.Drawing.Point(12, 224);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Return to main screen";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.BtnPlay);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LblCurAnon);
            this.Controls.Add(this.LblCurStat);
            this.Controls.Add(this.LblCurRoute);
            this.Name = "main";
            this.Text = "main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LblCurRoute;
        private Label LblCurStat;
        private Label LblCurAnon;
        private Button button1;
        private Button button2;
        private Button BtnPlay;
        private Button button4;
    }
}