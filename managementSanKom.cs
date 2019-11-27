using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;

namespace BRS_Hostel
{
    public partial class HomeForm : Form
    {
        //Панель управления Санитарной комиссии

        private void changeScoresStudSanKom_Click(object sender, EventArgs e)
        {
            addScoresSanKom.Show();
            lookScoresSanKom.Hide();
            addNewDate.Show();
        }

        private void lookScoresStudSanKom_Click(object sender, EventArgs e)
        {
            addScoresSanKom.Hide();
            lookScoresSanKom.Show();
            addNewDate.Hide();
        }

        private void loadDateSanKom()
        {
            panelSanKom.Dock = DockStyle.Fill;
            panelSanKom.Show();

            //инициализация панели меню
            menuPanelSanKom.Location = new Point(3, 3);
            menuPanelSanKom.Size = new Size(660, 45);

            //инициализация таблицы для добавления баллов
            addScoresSanKom.Location = new Point(50, 65);
            addScoresSanKom.Size = new Size(570, 73);

            //инициализация таблицы для просмотра баллов студентов
            lookScoresSanKom.Location = new Point(50, 65);
            lookScoresSanKom.Size = new Size(570, 73);

            addScoresSanKom.Hide();
            lookScoresSanKom.Show();
            addNewDate.Hide();

            lookDateSanKom();
            changeDateSanKom();
        }

        private void lookDateSanKom()
        {
            // заполнение таблицы добавления баллов
            string querry = "Select Distinct [a.fullName], a.[numberRoom], b.[sanKom] From [Students] a, [ScoresStud] b Where a.[idStud]=b.[idStud]";
            OleDbCommand command = new OleDbCommand(querry, myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }
            reader.Close();

            foreach (string[] s in data)
                lookScoresSanKom.Rows.Add(s);


            int sz = lookScoresSanKom.ColumnHeadersHeight + lookScoresSanKom.RowTemplate.Height * lookScoresSanKom.Rows.Count
                - Convert.ToInt32(lookScoresSanKom.Rows.Count * 2);
            if (sz <= 250)
                lookScoresSanKom.Height = sz;
            else lookScoresSanKom.Height = 250;

        }

        private void changeDateSanKom()
        {
            // заполнение таблицы добавления баллов
            string querry = "Select Distinct a.[numberRoom], b.[sanKom] From [Students] a, [ScoresStud] b Where a.[idStud]=b.[idStud]";
            OleDbCommand command = new OleDbCommand(querry, myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = "";
                data[data.Count - 1][2] = reader[1].ToString();
            }
            reader.Close();

            foreach (string[] s in data)
                addScoresSanKom.Rows.Add(s);


            int sz = addScoresSanKom.ColumnHeadersHeight + addScoresSanKom.RowTemplate.Height * addScoresSanKom.Rows.Count
                - Convert.ToInt32(addScoresSanKom.Rows.Count * 2);
            if (sz <= 250)
                addScoresSanKom.Height = sz;
            else addScoresSanKom.Height = 250;

        }

        private void addNewData_Click(object sender, EventArgs e)
        {
            int k = addScoresSanKom.Rows.Count;

            for (int i = 0; i < k; i++)
            {
                string value0 = addScoresSanKom[0, i].Value.ToString();
                double value1;
                bool tr = double.TryParse(addScoresSanKom[1, i].Value.ToString(), out value1);
                double value2 = double.Parse(addScoresSanKom[2, i].Value.ToString());
                if (value2 > 0)
                {
                    if (tr)
                        value2 = (value2 + value1) / 2;
                }
                else
                    value2 = value1;

                value2 = Math.Round(value2, 2);

                string query = "Select [idStud] From [Students] Where [numberRoom]=@room";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.Parameters.Add("room", OleDbType.VarChar).Value = value0;
                OleDbDataReader reader = command.ExecuteReader();

                List<int> data = new List<int>();

                while (reader.Read())
                {
                    data.Add(int.Parse(reader[0].ToString()));
                }
                reader.Close();

                for (int j = 0; j < data.Count; j++)
                {
                    query = "Update [ScoresStud] Set [sanKom]=@val Where [idStud] = @id";
                    command = new OleDbCommand(query, myConnection);
                    command.Parameters.Add("val", OleDbType.Double).Value = value2;
                    command.Parameters.Add("id", OleDbType.Integer).Value = data[j];
                    command.ExecuteNonQuery();
                }
            }
            addScoresSanKom.Rows.Clear();

            changeDateSanKom();
        }

        private void closeDateSanKom()
        {

        }
    }
}
