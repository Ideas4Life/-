using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BRS_Hostel
{
    public partial class HomeForm : Form
    {

       //Инициализация данных студента

        public void loadDataProfileStud()
        {
            fullNameStud.AutoSize = true;
            faculty.AutoSize = true;
            groupStud.AutoSize = true;
            courseStud.AutoSize = true;
            numberTicketStud.AutoSize = true;
            numberRoom.AutoSize = true;
            positionStud.AutoSize = true;

            string query = "SELECT * FROM [Students] WHERE  idStud=@uId";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = StId;
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                fullNameStud.Text = reader[1].ToString();
                numberTicketStud.Text = reader[2].ToString();
                numberRoom.Text = reader[3].ToString();
                groupStud.Text = reader[4].ToString();
                facultyStud.Text = reader[5].ToString();
                courseStud.Text = reader[6].ToString();
                DateTime d = DateTime.Parse(reader[7].ToString());
                dateBornLabel.Text = d.ToString("dd.MM.yyyy");
                positionStud.Text = reader[8].ToString();
            }
        }

        //обработка клика по надписи "Изменить фотографию" на панели профиля студента

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
    }
}
