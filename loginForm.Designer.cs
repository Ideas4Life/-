﻿namespace BRS_Hostel
{
    partial class LoginForm
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.loginButtom = new System.Windows.Forms.Button();
            this.passField = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.loginField = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.сloseButton = new System.Windows.Forms.Label();
            this.upPanel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.mainPanel.Controls.Add(this.errorLabel);
            this.mainPanel.Controls.Add(this.loginButtom);
            this.mainPanel.Controls.Add(this.passField);
            this.mainPanel.Controls.Add(this.pictureBox2);
            this.mainPanel.Controls.Add(this.loginField);
            this.mainPanel.Controls.Add(this.pictureBox1);
            this.mainPanel.Controls.Add(this.panel2);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(365, 450);
            this.mainPanel.TabIndex = 0;
            // 
            // loginButtom
            // 
            this.loginButtom.BackColor = System.Drawing.Color.Lime;
            this.loginButtom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginButtom.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.loginButtom.FlatAppearance.BorderSize = 0;
            this.loginButtom.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.loginButtom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LawnGreen;
            this.loginButtom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginButtom.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginButtom.ForeColor = System.Drawing.Color.Black;
            this.loginButtom.Location = new System.Drawing.Point(94, 349);
            this.loginButtom.Name = "loginButtom";
            this.loginButtom.Size = new System.Drawing.Size(178, 52);
            this.loginButtom.TabIndex = 5;
            this.loginButtom.Text = "Войти";
            this.loginButtom.UseVisualStyleBackColor = false;
            this.loginButtom.Click += new System.EventHandler(this.loginButtom_Click);
            // 
            // passField
            // 
            this.passField.Font = new System.Drawing.Font("Times New Roman", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passField.Location = new System.Drawing.Point(147, 260);
            this.passField.Name = "passField";
            this.passField.Size = new System.Drawing.Size(183, 41);
            this.passField.TabIndex = 4;
            this.passField.UseSystemPasswordChar = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::BRS_Hostel.Properties.Resources._lock;
            this.pictureBox2.Location = new System.Drawing.Point(50, 249);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 64);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // loginField
            // 
            this.loginField.Font = new System.Drawing.Font("Times New Roman", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginField.Location = new System.Drawing.Point(147, 152);
            this.loginField.Name = "loginField";
            this.loginField.Size = new System.Drawing.Size(183, 41);
            this.loginField.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BRS_Hostel.Properties.Resources.user;
            this.pictureBox1.Location = new System.Drawing.Point(50, 139);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Blue;
            this.panel2.Controls.Add(this.сloseButton);
            this.panel2.Controls.Add(this.upPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(365, 106);
            this.panel2.TabIndex = 0;
            // 
            // сloseButton
            // 
            this.сloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.сloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.сloseButton.ForeColor = System.Drawing.Color.White;
            this.сloseButton.Location = new System.Drawing.Point(338, -7);
            this.сloseButton.Name = "сloseButton";
            this.сloseButton.Size = new System.Drawing.Size(28, 33);
            this.сloseButton.TabIndex = 1;
            this.сloseButton.Text = "х";
            this.сloseButton.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.сloseButton.Click += new System.EventHandler(this.сloseButton_Click);
            this.сloseButton.MouseEnter += new System.EventHandler(this.сloseButton_MouseEnter);
            this.сloseButton.MouseLeave += new System.EventHandler(this.сloseButtom_MouseLeave);
            // 
            // upPanel
            // 
            this.upPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.upPanel.Font = new System.Drawing.Font("Comic Sans MS", 26F, System.Drawing.FontStyle.Bold);
            this.upPanel.ForeColor = System.Drawing.Color.White;
            this.upPanel.Location = new System.Drawing.Point(0, 0);
            this.upPanel.Name = "upPanel";
            this.upPanel.Size = new System.Drawing.Size(365, 106);
            this.upPanel.TabIndex = 0;
            this.upPanel.Text = "Авторизация";
            this.upPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.upPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.upPanel_MouseDown);
            this.upPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.upPanel_MouseMove);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(171, 408);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 15);
            this.errorLabel.TabIndex = 6;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 450);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.Text = "loginForm";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label upPanel;
        private System.Windows.Forms.Label сloseButton;
        private System.Windows.Forms.TextBox passField;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox loginField;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button loginButtom;
        private System.Windows.Forms.Label errorLabel;
    }
}