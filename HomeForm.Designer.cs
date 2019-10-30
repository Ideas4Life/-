namespace BRS_Hostel
{
    partial class HomeForm
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
            this.leftPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.subMenu1 = new System.Windows.Forms.Panel();
            this.fon1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.subMenu2 = new System.Windows.Forms.Panel();
            this.fon2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.registerButton = new System.Windows.Forms.Button();
            this.menuBox = new System.Windows.Forms.PictureBox();
            this.upPanel = new System.Windows.Forms.Panel();
            this.subMenu0 = new System.Windows.Forms.Panel();
            this.leftPanel.SuspendLayout();
            this.subMenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.subMenu2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuBox)).BeginInit();
            this.upPanel.SuspendLayout();
            this.subMenu0.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.leftPanel.Controls.Add(this.subMenu0);
            this.leftPanel.Controls.Add(this.subMenu1);
            this.leftPanel.Controls.Add(this.subMenu2);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(200, 359);
            this.leftPanel.TabIndex = 2;
            // 
            // subMenu1
            // 
            this.subMenu1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.subMenu1.BackColor = System.Drawing.Color.Red;
            this.subMenu1.Controls.Add(this.fon1);
            this.subMenu1.Controls.Add(this.pictureBox1);
            this.subMenu1.Location = new System.Drawing.Point(0, 50);
            this.subMenu1.Margin = new System.Windows.Forms.Padding(0);
            this.subMenu1.Name = "subMenu1";
            this.subMenu1.Size = new System.Drawing.Size(200, 45);
            this.subMenu1.TabIndex = 0;
            this.subMenu1.Click += new System.EventHandler(this.subMenu_Click);
            // 
            // fon1
            // 
            this.fon1.Dock = System.Windows.Forms.DockStyle.Right;
            this.fon1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fon1.Location = new System.Drawing.Point(50, 0);
            this.fon1.Margin = new System.Windows.Forms.Padding(0);
            this.fon1.Name = "fon1";
            this.fon1.Size = new System.Drawing.Size(150, 45);
            this.fon1.TabIndex = 1;
            this.fon1.Text = "фон1";
            this.fon1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fon1.Click += new System.EventHandler(this.fon1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::BRS_Hostel.Properties.Resources.menu;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // subMenu2
            // 
            this.subMenu2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.subMenu2.BackColor = System.Drawing.Color.OrangeRed;
            this.subMenu2.Controls.Add(this.fon2);
            this.subMenu2.Controls.Add(this.pictureBox2);
            this.subMenu2.Location = new System.Drawing.Point(0, 95);
            this.subMenu2.Margin = new System.Windows.Forms.Padding(0);
            this.subMenu2.Name = "subMenu2";
            this.subMenu2.Size = new System.Drawing.Size(200, 45);
            this.subMenu2.TabIndex = 2;
            // 
            // fon2
            // 
            this.fon2.Dock = System.Windows.Forms.DockStyle.Right;
            this.fon2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fon2.Location = new System.Drawing.Point(50, 0);
            this.fon2.Name = "fon2";
            this.fon2.Size = new System.Drawing.Size(150, 45);
            this.fon2.TabIndex = 1;
            this.fon2.Text = "фон2";
            this.fon2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fon2.Click += new System.EventHandler(this.fon2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = global::BRS_Hostel.Properties.Resources.menu;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(45, 45);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // loginButton
            // 
            this.loginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loginButton.AutoSize = true;
            this.loginButton.BackColor = System.Drawing.Color.Lime;
            this.loginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.33962F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginButton.Location = new System.Drawing.Point(347, 3);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(101, 43);
            this.loginButton.TabIndex = 0;
            this.loginButton.Text = "Войти";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // registerButton
            // 
            this.registerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.registerButton.AutoSize = true;
            this.registerButton.BackColor = System.Drawing.Color.Lime;
            this.registerButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.registerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.33962F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.registerButton.Location = new System.Drawing.Point(146, 3);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(200, 43);
            this.registerButton.TabIndex = 1;
            this.registerButton.Text = "Регистрация";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // menuBox
            // 
            this.menuBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.menuBox.Image = global::BRS_Hostel.Properties.Resources.menu;
            this.menuBox.Location = new System.Drawing.Point(0, 0);
            this.menuBox.Margin = new System.Windows.Forms.Padding(0);
            this.menuBox.Name = "menuBox";
            this.menuBox.Size = new System.Drawing.Size(50, 50);
            this.menuBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.menuBox.TabIndex = 0;
            this.menuBox.TabStop = false;
            this.menuBox.Click += new System.EventHandler(this.menuBox_Click);
            // 
            // upPanel
            // 
            this.upPanel.BackColor = System.Drawing.Color.Fuchsia;
            this.upPanel.Controls.Add(this.loginButton);
            this.upPanel.Controls.Add(this.registerButton);
            this.upPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.upPanel.Location = new System.Drawing.Point(200, 0);
            this.upPanel.Name = "upPanel";
            this.upPanel.Size = new System.Drawing.Size(451, 50);
            this.upPanel.TabIndex = 3;
            // 
            // subMenu0
            // 
            this.subMenu0.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.subMenu0.BackColor = System.Drawing.Color.Fuchsia;
            this.subMenu0.Controls.Add(this.menuBox);
            this.subMenu0.Location = new System.Drawing.Point(0, 0);
            this.subMenu0.Margin = new System.Windows.Forms.Padding(0);
            this.subMenu0.Name = "subMenu0";
            this.subMenu0.Size = new System.Drawing.Size(200, 50);
            this.subMenu0.TabIndex = 3;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(106F, 106F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(651, 359);
            this.Controls.Add(this.upPanel);
            this.Controls.Add(this.leftPanel);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "HomeForm";
            this.Text = "HomeForm";
            this.Click += new System.EventHandler(this.HomeForm_Click);
            this.leftPanel.ResumeLayout(false);
            this.subMenu1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.subMenu2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuBox)).EndInit();
            this.upPanel.ResumeLayout(false);
            this.upPanel.PerformLayout();
            this.subMenu0.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel leftPanel;
        private System.Windows.Forms.Panel subMenu1;
        private System.Windows.Forms.Label fon1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel subMenu2;
        private System.Windows.Forms.Label fon2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox menuBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Panel upPanel;
        private System.Windows.Forms.Panel subMenu0;
    }
}