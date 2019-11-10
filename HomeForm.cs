using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BRS_Hostel
{
    public partial class HomeForm : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB_BRS_Hostel.mdb;";
        private OleDbConnection myConnection;
        public string StId;
        public HomeForm()
        {
            InitializeComponent();
            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);
            // открываем соединение с БД
            myConnection.Open();

            leftPanel.Size = new Size(45, leftPanel.Size.Height);
            subMenu0.Size= new Size(200, 45);
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
        }

        public void InitializeStud()
        {
            fullNameStud.AutoSize = true;
            facultyStud.AutoSize = true;
            groupStud.AutoSize = true;
            courseStud.AutoSize = true;
            numberTicketStud.AutoSize = true;
            numberRoom.AutoSize = true;
            positionStud.AutoSize = true;

            string query = "SELECT * FROM Students WHERE  idStud=@uId";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = StId;
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                fullNameStud.Text = reader[1].ToString();
                numberTicketStud.Text += reader[2].ToString();
                numberRoom.Text += reader[3].ToString();
                groupStud.Text += reader[4].ToString();
                facultyStud.Text += reader[5].ToString();
                courseStud.Text += reader[6].ToString();
                positionStud.Text += reader[7].ToString();
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterForm registrForm = new RegisterForm(this);
            this.Enabled = false;
            registrForm.Show();
        }
        private void homeBox_Click(object sender, EventArgs e)
        {
            mainPanel.Show();
            mainPanel.Dock = DockStyle.Fill;
            profilePanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            ratingPanel.Hide();
            exitPanel.Hide();
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

        private void profileBox_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(StId) > 0)
                InitializeStud();
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

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // заркываем соединение с БД
            myConnection.Close();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
            if (logField.Text.Length != 0 && passField.Text.Length != 0)
            {
                string query = "SELECT idStud FROM LoginUser WHERE  login=@uLog AND password=@uPas";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.Parameters.Add("uLog", OleDbType.VarChar).Value = logField.Text;
                command.Parameters.Add("uPas", OleDbType.VarChar).Value = passField.Text;
                string idSt = command.ExecuteScalar().ToString();
                StId = idSt;
                InitializeStud();
            }
            else
            {
                errorLabel.Visible=true;
            }
        }
    }
}
