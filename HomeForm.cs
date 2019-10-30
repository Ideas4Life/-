using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRS_Hostel
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
            leftPanel.Size = new Size(50, leftPanel.Size.Height);
            subMenu0.Size= new Size(200, 50);
            upPanel.Size = new Size(this.Width, subMenu0.Size.Height);
            upPanel.BackColor = subMenu0.BackColor;
            this.Size=new Size((int)(Screen.PrimaryScreen.Bounds.Size.Width/1.7), (int)(Screen.PrimaryScreen.Bounds.Size.Height /1.5));
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this);
            this.Enabled = false;
            loginForm.Show();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterForm registrForm = new RegisterForm(this);
            this.Enabled = false;
            registrForm.Show();
        }
        bool hideMenu = true;
        private void menuBox_Click(object sender, EventArgs e)
        {
            if (hideMenu)
                leftPanel.Size = new Size(200, leftPanel.Size.Height);
            else leftPanel.Size = new Size(50, leftPanel.Size.Height);
            hideMenu = !hideMenu;
        }

        private void subMenu_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;
        }

        private void fon1_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;
        }

        private void fon2_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
        }

        private void HomeForm_Click(object sender, EventArgs e)
        {
            leftPanel.Size = new Size(50, leftPanel.Size.Height);
        }
    }
}
