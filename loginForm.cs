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
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width,41);
            this.loginField.Size = new Size(this.loginField.Size.Width, 41);
        }

        private void сloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void сloseButton_MouseEnter(object sender, EventArgs e)
        {
            сloseButton.ForeColor = Color.Black;
        }

        private void сloseButtom_MouseLeave(object sender, EventArgs e)
        {
            сloseButton.ForeColor = Color.White;
        }
        Point lastPoint;
        private void upPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void upPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
