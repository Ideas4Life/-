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
    public partial class RegisterForm : Form
    {
        HomeForm homeForm;
        public RegisterForm(HomeForm homefm)
        {
            InitializeComponent();
            homeForm = homefm;
        }

        private void сloseButton_Click(object sender, EventArgs e)
        {
            homeForm.Enabled = true;
            this.Close();
        }
        Point lastPoint;
        private void upPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void upPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
    }
}
