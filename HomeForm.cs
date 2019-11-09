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
            mainPanel.Dock = DockStyle.Fill;
            profilePanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            ratingPanel.Hide();
            exitPanel.Hide();
            selectPhoto.Filter = "PNG files(*.png)|*.png|Bitmap files (*.bmp)|*.bmp|Image files (*.jpg)|*.jpg";
            //инициализация студента
            InitializeStud();
        }

        private void InitializeStud()
        {
            fullNameStud.AutoSize = true;
            fullNameStud.Text = "Чепайкин Роман Николаевич";
            facultyStud.AutoSize = true;
            facultyStud.Text += "ИКТЗИ";
            groupStud.AutoSize = true;
            groupStud.Text += "4305";
            courseStud.AutoSize = true;
            courseStud.Text += "3";
            numberTicketStud.AutoSize = true;
            numberTicketStud.Text += "741069";
            numberRoom.AutoSize = true;
            numberRoom.Text += "225";
            positionStud.AutoSize = true;
            positionStud.Text += "пользователь";

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

        private void profile_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            ratingPanel.Hide();
            exitPanel.Hide();
            profilePanel.Show();
            profilePanel.Dock = DockStyle.Fill;
        }

        private void progress_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            profilePanel.Hide();
            ratingPanel.Hide();
            exitPanel.Hide();
            managementPanel.Hide();
            progressPanel.Show();
            progressPanel.Dock = DockStyle.Fill;
        }


        private void rating_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            profilePanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            exitPanel.Hide();
            ratingPanel.Show();
            ratingPanel.Dock = DockStyle.Fill;
        }
        private void management_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            profilePanel.Hide();
            progressPanel.Hide();
            ratingPanel.Hide();
            exitPanel.Hide();
            managementPanel.Show();
            managementPanel.Dock = DockStyle.Fill;
        }
        private void exit_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            profilePanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            ratingPanel.Hide();
            exitPanel.Show();
            exitPanel.Dock = DockStyle.Fill;
        }

        private void HomeForm_Click(object sender, EventArgs e)
        {
            leftPanel.Size = new Size(50, leftPanel.Size.Height);
        }

        private void mainPanel_Click(object sender, EventArgs e)
        {
            if (!hideMenu)
            {
                leftPanel.Size = new Size(50, leftPanel.Size.Height);
                hideMenu = !hideMenu;
            }
        }

        private void exitPanel_Click(object sender, EventArgs e)
        {
            if (!hideMenu)
            {
                leftPanel.Size = new Size(50, leftPanel.Size.Height);
                hideMenu = !hideMenu;
            }
        }

        private void progressPanel_Click(object sender, EventArgs e)
        {
            if (!hideMenu)
            {
                leftPanel.Size = new Size(50, leftPanel.Size.Height);
                hideMenu = !hideMenu;
            }
        }

        private void profilePanel_Click(object sender, EventArgs e)
        {
            if (!hideMenu)
            {
                leftPanel.Size = new Size(50, leftPanel.Size.Height);
                hideMenu = !hideMenu;
            }
        }

        private void ratingPanel_Click(object sender, EventArgs e)
        {
            if (!hideMenu)
            {
                leftPanel.Size = new Size(50, leftPanel.Size.Height);
                hideMenu = !hideMenu;
            }
        }

        private void managementPanel_Click(object sender, EventArgs e)
        {
            if (!hideMenu)
            {
                leftPanel.Size = new Size(50, leftPanel.Size.Height);
                hideMenu = !hideMenu;
            }
        }

        private void profileBox_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            ratingPanel.Hide();
            exitPanel.Hide();
            profilePanel.Show();
            profilePanel.Dock = DockStyle.Fill;
        }

        private void progressBox_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            profilePanel.Hide();
            ratingPanel.Hide();
            exitPanel.Hide();
            managementPanel.Hide();
            progressPanel.Show();
            progressPanel.Dock = DockStyle.Fill;
        }

        private void ratingBox_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            profilePanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            exitPanel.Hide();
            ratingPanel.Show();
            ratingPanel.Dock = DockStyle.Fill;
        }

        private void managementBox_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            profilePanel.Hide();
            progressPanel.Hide();
            ratingPanel.Hide();
            exitPanel.Hide();
            managementPanel.Show();
            managementPanel.Dock = DockStyle.Fill;
        }

        private void exitBox_Click(object sender, EventArgs e)
        {
            mainPanel.Hide();
            profilePanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            ratingPanel.Hide();
            exitPanel.Show();
            exitPanel.Dock = DockStyle.Fill;
        }
        private void changePhoto_Click(object sender, EventArgs e)
        {
            if (selectPhoto.ShowDialog() == DialogResult.OK)
            {
                photoStud.Image = Image.FromFile(selectPhoto.FileName);
            }
        }

        private void changePhoto_MouseMove(object sender, MouseEventArgs e)
        {
            changePhoto.ForeColor = Color.Blue;
        }

        private void changePhoto_MouseLeave(object sender, EventArgs e)
        {
            changePhoto.ForeColor = Color.Black;
        }

        private void changePhoto_MouseDown(object sender, MouseEventArgs e)
        {
            changePhoto.ForeColor = Color.DarkBlue;
        }

        private void changePhoto_MouseUp(object sender, MouseEventArgs e)
        {
            changePhoto.ForeColor = Color.Black;
        }

        private void HozChas_Click(object sender, EventArgs e)
        {
            hozChasTable.Visible = true;
            KPDTable.Visible = false;
        }

        private void KPD_Click(object sender, EventArgs e)
        {
            hozChasTable.Visible = false;
            KPDTable.Visible = true;
        }
    }
}
