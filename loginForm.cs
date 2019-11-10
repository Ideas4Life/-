using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BRS_Hostel
{
    public partial class LoginForm : Form
    {
        private OleDbConnection myConnection;

        HomeForm homeForm;
        public LoginForm(HomeForm homefm)
        {
            InitializeComponent();
            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(HomeForm.connectString);
            // открываем соединение с БД
            myConnection.Open();

            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width,41);
            this.loginField.Size = new Size(this.loginField.Size.Width, 41);
            homeForm = homefm;
        }

        private void сloseButton_Click(object sender, EventArgs e)
        {
            homeForm.Enabled = true;
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

        private void loginButtom_Click(object sender, EventArgs e)
        {
            if (loginField.Text != null && passField.Text != null)
            {
                string query = "SELECT idStud FROM LoginUser WHERE  login=@uLog AND password=@uPas";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.Parameters.Add("uLog", OleDbType.VarChar).Value = loginField.Text;
                command.Parameters.Add("uPas", OleDbType.VarChar).Value = passField.Text;                
                var idSt = command.ExecuteScalar().ToString();
                HomeForm.StId = Convert.ToInt32(idSt);
                сloseButton_Click(sender,  e);
            }
            else
            {
                errorLabel.Text = "Заполните все поля";
            }
        }
    }
}
