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
        private void personalHozChasLabel_Click(object sender, EventArgs e)
        {
            commonHozChasPanel.Hide();
            commonHozChasTable.Hide();
            personalHozChasPanel.Show();
            personalHozChasTable.Show();
            addDateHozChas.Show();
        }

        private void commonHozChasLabel_Click(object sender, EventArgs e)
        {
            commonHozChasPanel.Show();
            commonHozChasTable.Show();
            personalHozChasPanel.Hide();
            personalHozChasTable.Hide();
            addDateHozChas.Hide();
        }

        private void loadDateHozChas()
        {
            panelHozChas.Dock = DockStyle.Fill;
            panelHozChas.Show();

            commonHozChasPanel.Show();
            commonHozChasTable.Show();
            personalHozChasPanel.Hide();
            personalHozChasTable.Hide();
            addDateHozChas.Hide();



            //инициализация панели меню
            menuPanelHozChas.Location = new Point(3, 3);
            menuPanelHozChas.Size = new Size(660, 45);

            //инициализация панели для просмотра баллов всех студентов
            commonHozChasPanel.Location = new Point(3,50);
            commonHozChasPanel.Size = new Size(660, 320);

            //инициализация таблицы для просмотра баллов всех студентов
            commonHozChasTable.Location = new Point(100, 5);
            commonHozChasTable.Size = new Size(460, 73);

            //инициализация панели для просмотра баллов выбранного студентов
            personalHozChasPanel.Location = new Point(3, 50);
            personalHozChasPanel.Size = new Size(660, 320);

            //инициализация таблицы для просмотра баллов выбранного студентов
            personalHozChasTable.Location = new Point(50, 55);
            personalHozChasTable.Size = new Size(560, 73);



            commonDateHozChas();
        }

        private void commonDateHozChas()
        {
            commonHozChasTable.Rows.Clear();
            string querry = "Select Distinct [a.fullName], b.[allHozChas] From [Students] a, [ScoresStud] b Where a.[idStud]=b.[idStud]";
            OleDbCommand command = new OleDbCommand(querry, myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[2]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
            }
            reader.Close();

            foreach (string[] s in data)
                commonHozChasTable.Rows.Add(s);


            int sz = commonHozChasTable.ColumnHeadersHeight + commonHozChasTable.RowTemplate.Height * commonHozChasTable.Rows.Count
                - Convert.ToInt32(commonHozChasTable.Rows.Count * 2);
            if (sz <= 250)
                commonHozChasTable.Height = sz;
            else commonHozChasTable.Height = 250;

        }
        int idHZ;
        private void personalDateHozChas(int idS)
        {
            idHZ = idS;
            personalHozChasTable.Rows.Clear();
            string querry = "Select [names], [date], [scores]  From [HozChas] " +
                "Where [idStud]=@id";
            OleDbCommand command = new OleDbCommand(querry, myConnection);
            command.Parameters.Add("id", OleDbType.Integer).Value = idHZ;
            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1]= reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }
            reader.Close();

            foreach (string[] s in data)
                personalHozChasTable.Rows.Add(s);

            personalHozChasTable.Rows.Add("4","4","4");

            int count=personalHozChasTable.Rows.Count;
            personalHozChasTable[0, count - 1].ReadOnly = false;
            personalHozChasTable[1, count - 1].ReadOnly = false;
            personalHozChasTable[2, count - 1].ReadOnly = false;

            int sz = personalHozChasTable.ColumnHeadersHeight + personalHozChasTable.RowTemplate.Height * personalHozChasTable.Rows.Count
                - Convert.ToInt32(commonHozChasTable.Rows.Count * 2);
            if (sz <= 190)
                personalHozChasTable.Height = sz;
            else personalHozChasTable.Height = 190;
        }

        private void saveDateHozChas_Click(object sender, EventArgs e)
        {
            int k = personalHozChasTable.Rows.Count;
            if (k <= 0)
                k = 1;

            DateTime dt = new DateTime();
            string names = personalHozChasTable[0, k-1].Value.ToString();
            string date = personalHozChasTable[1, k-1].Value.ToString();
            int count = int.Parse(personalHozChasTable[2, k-1].Value.ToString());

            string[] words = date.Split(new char[] { '.' });
            int day, month, year;
            int.TryParse(words[0], out day);
            int.TryParse(words[1], out month);
            int.TryParse(words[2], out year);
            
            try
            {
                dt = new DateTime(year, month, day);
            }
            catch
            {
                personalHozChasTable[1, k - 1].Value = "ошибка";
            }

            string query = "Insert Into [HozChas] ([idStud],[names],[scores],[date]) Values (@id, @name, @num, @dat)";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("id", OleDbType.Integer).Value = idHZ;
            command.Parameters.Add("name", OleDbType.VarChar).Value = names;
            command.Parameters.Add("num", OleDbType.Integer).Value = count;
            command.Parameters.Add("dat", OleDbType.DBDate).Value = dt;
            
            command.ExecuteNonQuery();

            addScoresSanKom.Rows.Clear();

            personalDateHozChas(idHZ);
        }

        private void findStudHozchas_Click(object sender, EventArgs e)
        {
            if (nameStudHozChas.Text!="")
            {
                string querry = "Select [idStud]  From [Students] Where [fullName]=@name";
                OleDbCommand command = new OleDbCommand(querry, myConnection);
                command.Parameters.Add("name", OleDbType.VarChar).Value = nameStudHozChas.Text;
                try
                {
                    int id = int.Parse(command.ExecuteScalar().ToString());
                    personalDateHozChas(id);
                }
                catch 
                { 
                
                }
            }
        }

        private void closeDateHozChas()
        {

        }
    }
}
