﻿namespace TSWAnnouncer
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
            this.components = new System.ComponentModel.Container();
            this.LblAppName = new System.Windows.Forms.Label();
            this.filesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ButSelJSONPack = new System.Windows.Forms.Button();
            this.LblSoundPckName = new System.Windows.Forms.Label();
            this.LblSelRoute = new System.Windows.Forms.Label();
            this.ButSelJSONRoute = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // LblAppName
            // 
            this.LblAppName.AutoSize = true;
            this.LblAppName.BackColor = System.Drawing.Color.Transparent;
            this.LblAppName.Font = new System.Drawing.Font("MS Reference Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LblAppName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.LblAppName.Location = new System.Drawing.Point(12, 9);
            this.LblAppName.Name = "LblAppName";
            this.LblAppName.Size = new System.Drawing.Size(710, 40);
            this.LblAppName.TabIndex = 0;
            this.LblAppName.Text = "Welcome to the Announcment Generator";
            // 
            // filesBindingSource
            // 
            this.filesBindingSource.DataSource = typeof(TSWAnnouncer.files);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ButSelJSONPack
            // 
            this.ButSelJSONPack.Location = new System.Drawing.Point(12, 100);
            this.ButSelJSONPack.Name = "ButSelJSONPack";
            this.ButSelJSONPack.Size = new System.Drawing.Size(199, 23);
            this.ButSelJSONPack.TabIndex = 5;
            this.ButSelJSONPack.Text = "Sellect pack file";
            this.ButSelJSONPack.UseVisualStyleBackColor = true;
            this.ButSelJSONPack.Click += new System.EventHandler(this.button3_Click);
            // 
            // LblSoundPckName
            // 
            this.LblSoundPckName.AutoSize = true;
            this.LblSoundPckName.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblSoundPckName.Location = new System.Drawing.Point(217, 100);
            this.LblSoundPckName.Name = "LblSoundPckName";
            this.LblSoundPckName.Size = new System.Drawing.Size(186, 21);
            this.LblSoundPckName.TabIndex = 6;
            this.LblSoundPckName.Text = "Select TOC sound pack";
            // 
            // LblSelRoute
            // 
            this.LblSelRoute.AutoSize = true;
            this.LblSelRoute.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblSelRoute.Location = new System.Drawing.Point(217, 147);
            this.LblSelRoute.Name = "LblSelRoute";
            this.LblSelRoute.Size = new System.Drawing.Size(192, 21);
            this.LblSelRoute.TabIndex = 8;
            this.LblSelRoute.Text = "Select your route preset";
            // 
            // ButSelJSONRoute
            // 
            this.ButSelJSONRoute.Location = new System.Drawing.Point(12, 147);
            this.ButSelJSONRoute.Name = "ButSelJSONRoute";
            this.ButSelJSONRoute.Size = new System.Drawing.Size(199, 23);
            this.ButSelJSONRoute.TabIndex = 7;
            this.ButSelJSONRoute.Text = "Sellect route file";
            this.ButSelJSONRoute.UseVisualStyleBackColor = true;
            this.ButSelJSONRoute.Click += new System.EventHandler(this.ButSelJSONRoute_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(397, 228);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(723, 320);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LblSelRoute);
            this.Controls.Add(this.ButSelJSONRoute);
            this.Controls.Add(this.LblSoundPckName);
            this.Controls.Add(this.ButSelJSONPack);
            this.Controls.Add(this.LblAppName);
            this.Name = "main";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LblAppName;
        private BindingSource filesBindingSource;
        private OpenFileDialog openFileDialog1;
        private Button ButSelJSONPack;
        private Label LblSoundPckName;
        private Label LblSelRoute;
        private Button ButSelJSONRoute;
        private Button button1;
    }
}