using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections.Generic;

namespace BRS_Hostel
{
    public partial class HomeForm : Form
    {
        private void listStudPredsedSS_Click(object sender, EventArgs e)
        {
            commonPredsedSSPanel.Show();
            personalPredsedSSPanel.Hide();
            studSovetPredsedSSPanel.Hide();
        }

        private void dateStudPredsedSS_Click(object sender, EventArgs e)
        {
            commonPredsedSSPanel.Hide();
            personalPredsedSSPanel.Show();
            studSovetPredsedSSPanel.Hide();
        }

        private void studSovetPredsedSS_Click(object sender, EventArgs e)
        {
            commonPredsedSSPanel.Hide();
            personalPredsedSSPanel.Hide();
            studSovetPredsedSSPanel.Show();
        }

        int idSS;

        private void loadDatePredsedSS()
        {
            panelPredsedSS.Dock = DockStyle.Fill;
            panelPredsedSS.Show();

            commonPredsedSSPanel.Show();
            personalPredsedSSPanel.Hide();
            studSovetPredsedSSPanel.Hide();

            //видимость панели с баллами конкретного студента по категориям
            progressPredsedSSPanel.Hide();

            //видимость таблиц с баллами конкретного студента
            stipendiaPredsedSSTable.Hide();
            olympConfPredsedSSTable.Hide();
            cultSportVolontPredsedSSTable.Hide();
            hozChasPredsedSSTable.Hide();
            dopScorePredsedSSTable.Hide();
            studKPDPredsedSSTable.Hide();
            allProgressPredsedSSTable.Show();

            //инициализация панели меню
            menuPanelPredsedSS.Location = new Point(3, 3);
            menuPanelPredsedSS.Size = new Size(660, 45);

            //инициализация панели для просмотра баллов всех студентов
            commonPredsedSSPanel.Location = new Point(3, 50);
            commonPredsedSSPanel.Size = new Size(660, 320);

            //инициализация таблицы для просмотра баллов всех студентов
            commonPredsedSSTable.Location = new Point(10, 10);
            commonPredsedSSTable.Size = new Size(640, commonPredsedSSTable.ColumnHeadersHeight);

            //инициализация панели для просмотра баллов выбранного студентов
            personalPredsedSSPanel.Location = new Point(3, 50);
            personalPredsedSSPanel.Size = new Size(660, 320);

            //инициализация таблицы для просмотра баллов выбранного студентов по разным категориям
            olympConfPredsedSSTable.Size = cultSportVolontPredsedSSTable.Size = hozChasPredsedSSTable.Size =
                dopScorePredsedSSTable.Size = stipendiaPredsedSSTable.Size = studKPDPredsedSSTable.Size =
                allProgressPredsedSSTable.Size = new Size(630, 56);
            olympConfPredsedSSTable.Location = cultSportVolontPredsedSSTable.Location = hozChasPredsedSSTable.Location =
                dopScorePredsedSSTable.Location = stipendiaPredsedSSTable.Location = studKPDPredsedSSTable.Location =
                allProgressPredsedSSTable.Location = new Point(10, 65);

            //инициализация панели управления студсовет
            studSovetPredsedSSPanel.Size = new Size(657, 320);
            studSovetPredsedSSPanel.Location = new Point(3, 50);

            //инициализация таблицы студ совета
            studSovetPredsedSSTable.Location = new Point(80, 60);
            studSovetPredsedSSTable.Size = new Size(500, studSovetPredsedSSTable.ColumnHeadersHeight);
            
            commonDataPredsedSS();
            loadDateStudSovet();
        }
        
        private void commonDataPredsedSS()
        {
            commonPredsedSSTable.Rows.Clear();
            string query = "SELECT [fullName], [numberRoom], [numberGroup], [course], [position] FROM [Students] Where [idStud]>0 ORDER BY [fullName] ";
            OleDbCommand command = new OleDbCommand(query, myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[5]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
            }
            reader.Close();
            foreach (string[] s in data)
                commonPredsedSSTable.Rows.Add(s);

            int sz = commonPredsedSSTable.ColumnHeadersHeight + commonPredsedSSTable.RowTemplate.Height 
                * commonPredsedSSTable.Rows.Count - Convert.ToInt32(commonPredsedSSTable.Rows.Count * 2);
            if (sz <= commonPredsedSSPanel.Height - commonPredsedSSTable.Location.Y - 15)
                commonPredsedSSTable.Height = sz;
            else commonPredsedSSTable.Height = commonPredsedSSPanel.Height- commonPredsedSSTable.Location.Y-15;
        }

        private void loadDateStudSovet()
        {
            studSovetPredsedSSTable.Rows.Clear();

            string query = "SELECT [fullName], [position] FROM [Students] WHERE [position]<>'Пользователь' And [position]<>'Комендант'";
            OleDbCommand command = new OleDbCommand(query, myConnection);

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
                studSovetPredsedSSTable.Rows.Add(s);

            int sz = studSovetPredsedSSTable.ColumnHeadersHeight + studSovetPredsedSSTable.RowTemplate.Height *
                studSovetPredsedSSTable.Rows.Count - Convert.ToInt32(studSovetPredsedSSTable.Rows.Count * 2);
            if (sz <= studSovetPredsedSSPanel.Height - studSovetPredsedSSTable.Location.Y - 15)
                studSovetPredsedSSTable.Height = sz;
            else studSovetPredsedSSTable.Height = studSovetPredsedSSPanel.Height- studSovetPredsedSSTable.Location.Y-15;
        }

        private void findSSPredsedSS_Click(object sender, EventArgs e)
        {
            if (namePositionPredsedSSTB.Text != "")
            {
                studSovetPredsedSSTable.Rows.Clear();

                string query = "SELECT [fullName], [position] FROM [Students] WHERE [fullName]=@name";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.Parameters.Add("name", OleDbType.VarChar).Value = namePositionPredsedSSTB.Text;

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
                    studSovetPredsedSSTable.Rows.Add(s);

                int sz = studSovetPredsedSSTable.ColumnHeadersHeight + studSovetPredsedSSTable.RowTemplate.Height *
                    studSovetPredsedSSTable.Rows.Count - Convert.ToInt32(studSovetPredsedSSTable.Rows.Count * 2);
                if (sz <= studSovetPredsedSSPanel.Height - studSovetPredsedSSTable.Location.Y - 15)
                    studSovetPredsedSSTable.Height = sz;
                else studSovetPredsedSSTable.Height = studSovetPredsedSSPanel.Height - studSovetPredsedSSTable.Location.Y - 15;
            }
            else
            {
                loadDateStudSovet();
            }
        }

        private void savePositionPredsedSS_Click(object sender, EventArgs e)
        {
            int k = studSovetPredsedSSTable.Rows.Count;
            if (k <= 0)
                k = 1;

            for (int i = 0; i < k; i++)
            {
                string posit = studSovetPredsedSSTable[0, i].Value.ToString();
                string name = studSovetPredsedSSTable[1, i].Value.ToString();

                string query = "UPDATE [Students] SET [position]=@posit Where [fullName]=@name";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.Parameters.Add("name", OleDbType.VarChar).Value = name;
                command.Parameters.Add("posit", OleDbType.VarChar).Value = posit;

                try
                {
                    command.ExecuteNonQuery();
                    
                }
                catch
                {
                }
            }
            loadDateStudSovet();
            eventChangeDataTable?.Invoke();
            if (idSS>0)
            {
                loadDataOlympKonfPredsedSS();
                loadDataCultSportVolontPredsedSS();
                loadDataHozChasPredsedSS();
                loadDataDopScorePredsedSS();
                loadDataStipendiaPredsedSS();
                loadDataStudKPDPredsedSS();
                loadDataAllProgressPredsedSS();
            }
        }

        private void findStudPredsedSSButton_Click(object sender, EventArgs e)
        {
            if (namePredsedSSTB.Text != "")
            {
                string query = "SELECT [idStud] FROM [Students] WHERE  fullName=@name";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.Parameters.Add("name", OleDbType.VarChar).Value = namePredsedSSTB.Text;
                try
                {
                    string idSt = command.ExecuteScalar().ToString();
                    idSS = int.Parse(idSt);
                    
                    loadDataOlympKonfPredsedSS();
                    loadDataCultSportVolontPredsedSS();
                    loadDataHozChasPredsedSS();
                    loadDataDopScorePredsedSS();
                    loadDataStipendiaPredsedSS();
                    loadDataStudKPDPredsedSS();
                    loadDataAllProgressPredsedSS();
                    progressPredsedSSPanel.Show();
                }
                catch
                {
                    namePredsedSSTB.Text = "Ошибка! Попробуйте ещё раз.";
                }
            }
            else namePredsedSSTB.Text = "Введите не корректное имя студента.";
        }

        //Загрузка данных пользователя для предсета СС о его олимпиадах, конференциях из базы данных

        private void loadDataOlympKonfPredsedSS()
        {
            olympConfPredsedSSTable.Rows.Clear();

            string query = "SELECT [nameOlympConf], [levelOlympConf], [resultOlympConf], [scoreOlympConf]" +
                " FROM [OlympConf] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = idSS;

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[4]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
            }
            reader.Close();

            foreach (string[] s in data)
                olympConfPredsedSSTable.Rows.Add(s);

            int sz = olympConfPredsedSSTable.ColumnHeadersHeight + olympConfPredsedSSTable.RowTemplate.Height *
                olympConfPredsedSSTable.Rows.Count - Convert.ToInt32(olympConfPredsedSSTable.Rows.Count * 2);
            if (sz <= personalPredsedSSPanel.Height - olympConfPredsedSSTable.Location.Y - 15)
                olympConfPredsedSSTable.Height = sz;
            else olympConfPredsedSSTable.Height = personalPredsedSSPanel.Height- olympConfPredsedSSTable.Location.Y-15;
        }

        //Загрузка данных пользователя для предсета СС о его культурных, спортивных, волонтерских мероприятиях из базы данных

        private void loadDataCultSportVolontPredsedSS()
        {
            cultSportVolontPredsedSSTable.Rows.Clear();

            string query = "SELECT [nameCultSportVolont], [levelCultSportVolont], [scoreCultSportVolont]" +
                " FROM [CultSportVolont] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = idSS;

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
                cultSportVolontPredsedSSTable.Rows.Add(s);

            int sz = cultSportVolontPredsedSSTable.ColumnHeadersHeight + cultSportVolontPredsedSSTable.RowTemplate.Height *
                cultSportVolontPredsedSSTable.Rows.Count - Convert.ToInt32(cultSportVolontPredsedSSTable.Rows.Count * 2);
            if (sz <= personalPredsedSSPanel.Height - cultSportVolontPredsedSSTable.Location.Y - 15)
                cultSportVolontPredsedSSTable.Height = sz;
            else cultSportVolontPredsedSSTable.Height = personalPredsedSSPanel.Height - cultSportVolontPredsedSSTable.Location.Y - 15;
        }

        //Загрузка данных пользователя для предсета СС о его Хоз часах из базы данных

        private void loadDataHozChasPredsedSS()
        {
            hozChasPredsedSSTable.Rows.Clear();

            string query = "SELECT [names], [scores], [date]" +
                " FROM [HozChas] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = idSS;

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
                hozChasPredsedSSTable.Rows.Add(s);

            int sz = hozChasPredsedSSTable.ColumnHeadersHeight + hozChasPredsedSSTable.RowTemplate.Height * hozChasPredsedSSTable.Rows.Count
                - Convert.ToInt32(hozChasPredsedSSTable.Rows.Count * 2);
            if (sz <= personalPredsedSSPanel.Height - hozChasPredsedSSTable.Location.Y - 15)
                hozChasPredsedSSTable.Height = sz;
            else hozChasPredsedSSTable.Height = personalPredsedSSPanel.Height- hozChasPredsedSSTable.Location.Y -15;
        }

        //Загрузка данных пользователя для предсета СС о его дополнительных баллах из базы данных

        private void loadDataDopScorePredsedSS()
        {
            dopScorePredsedSSTable.Rows.Clear();
            scoresStudCom[3] = 0;
            string query = "SELECT [sanKom], [starosta], [remontRoom], [studSovet], [markStudy]" +
                " FROM [ScoresStud] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = idSS;

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[5]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
            }
            reader.Close();

            foreach (string[] s in data)
                dopScorePredsedSSTable.Rows.Add(s);

            int sz = dopScorePredsedSSTable.ColumnHeadersHeight + dopScorePredsedSSTable.RowTemplate.Height *
                dopScorePredsedSSTable.Rows.Count - Convert.ToInt32(dopScorePredsedSSTable.Rows.Count * 2);
            if (sz <= personalPredsedSSPanel.Height - dopScorePredsedSSTable.Location.Y - 15)
                dopScorePredsedSSTable.Height = sz;
            else dopScorePredsedSSTable.Height = personalPredsedSSPanel.Height- dopScorePredsedSSTable.Location.Y-15;
        }

        //Загрузка данных пользователя для предсета СС о его именных стипендиях из базы данных

        private void loadDataStipendiaPredsedSS()
        {
            stipendiaPredsedSSTable.Rows.Clear();
            string query = "SELECT [nameStipendia], [levelStipendia], [scoreStipendia]" +
                " FROM [Stipendia] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = idSS;

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
                stipendiaPredsedSSTable.Rows.Add(s);

            int sz = stipendiaPredsedSSTable.ColumnHeadersHeight + stipendiaPredsedSSTable.RowTemplate.Height *
                stipendiaPredsedSSTable.Rows.Count - Convert.ToInt32(stipendiaPredsedSSTable.Rows.Count * 2);
            if (sz <= personalPredsedSSPanel.Height - stipendiaPredsedSSTable.Location.Y - 15)
                stipendiaPredsedSSTable.Height = sz;
            else stipendiaPredsedSSTable.Height = personalPredsedSSPanel.Height- stipendiaPredsedSSTable.Location.Y -15;
        }

        //Загрузка данных пользователя для предсета СС о его нарушениях КПД из базы данных

        private void loadDataStudKPDPredsedSS()
        {
            studKPDPredsedSSTable.Rows.Clear();
            string query = "SELECT [idKindKPD], [dateKpd], [statusKPD], [scoreKPD]" +
                " FROM  [StudKPD] WHERE idStud=@uId";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = idSS;

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[4]);

                string query1 = "SELECT [kindKPD] FROM [KPD] WHERE idKindKPD=@idKpd";
                OleDbCommand command1 = new OleDbCommand(query1, myConnection);
                command1.Parameters.Add("idKpd", OleDbType.VarChar).Value = reader[0].ToString();
                var reader1 = command1.ExecuteScalar().ToString();

                data[data.Count - 1][0] = reader1;
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
            }
            reader.Close();

            foreach (string[] s in data)
                studKPDPredsedSSTable.Rows.Add(s);

            int sz = studKPDPredsedSSTable.ColumnHeadersHeight + studKPDPredsedSSTable.RowTemplate.Height * studKPDPredsedSSTable.Rows.Count -
                Convert.ToInt32(studKPDPredsedSSTable.Rows.Count * 2);
            if (sz <= personalPredsedSSPanel.Height - studKPDPredsedSSTable.Location.Y - 15)
                studKPDPredsedSSTable.Height = sz;
            else studKPDPredsedSSTable.Height = personalPredsedSSPanel.Height- studKPDPredsedSSTable.Location.Y -15 ;
        }

        //Загрузка данных пользователя для предсета СС о его свобдных баллах из базы данных

        private void loadDataAllProgressPredsedSS()
        {
            allProgressPredsedSSTable.Rows.Clear();

            string query = "SELECT * FROM [ScoresStud] Where [idStud]=@id ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("id", OleDbType.VarChar).Value = idSS;

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                allProgressPredsedSSTable.Rows.Add(new string[2] { "Олимпиады и конференции", reader[6].ToString() });
                allProgressPredsedSSTable.Rows.Add(new string[2] { "Спортивные, культурно-массовые, гражданско-патриотические," +
                "общественные мероприятия", reader[7].ToString() });
                allProgressPredsedSSTable.Rows.Add(new string[2] { "Хоз часы", reader[8].ToString() });
                allProgressPredsedSSTable.Rows.Add(new string[2] { "Именные стипендии", reader[9].ToString() });
                allProgressPredsedSSTable.Rows.Add(new string[2] { "Штрафные баллы за КПД", reader[10].ToString() });
                double dopSc = double.Parse(reader[1].ToString());
                if (dopSc < 2.0)
                    dopSc = 0.0;
                else dopSc = (dopSc - 5) * 5;
                dopSc += double.Parse(reader[2].ToString()) +
                    double.Parse(reader[3].ToString()) + double.Parse(reader[4].ToString()) +
                    double.Parse(reader[5].ToString()) * 20;
                allProgressPredsedSSTable.Rows.Add(new string[2] { "Дополнительные баллы", dopSc.ToString() });
                allProgressPredsedSSTable.Rows.Add(new string[2] { "Всего баллов", reader[11].ToString() });
            }
            reader.Close();

            int sz = allProgressPredsedSSTable.ColumnHeadersHeight +
                allProgressPredsedSSTable.RowTemplate.Height * allProgressPredsedSSTable.Rows.Count -
                Convert.ToInt32(allProgressPredsedSSTable.Rows.Count * 2);
            if (sz <= personalPredsedSSPanel.Height - allProgressPredsedSSTable.Location.Y - 15)
                allProgressPredsedSSTable.Height = sz;
            else allProgressPredsedSSTable.Height = personalPredsedSSPanel.Height- allProgressPredsedSSTable.Location.Y -15;
        }
        private void mainScoresPredsedSSLabel_Click(object sender, EventArgs e)
        {
            stipendiaPredsedSSTable.Hide();
            olympConfPredsedSSTable.Hide();
            cultSportVolontPredsedSSTable.Hide();
            hozChasPredsedSSTable.Hide();
            dopScorePredsedSSTable.Hide();
            studKPDPredsedSSTable.Hide();
            allProgressPredsedSSTable.Show();
        }

        private void hozChasPredsedSSLabel_Click(object sender, EventArgs e)
        {
            stipendiaPredsedSSTable.Hide();
            olympConfPredsedSSTable.Hide();
            cultSportVolontPredsedSSTable.Hide();
            hozChasPredsedSSTable.Show();
            dopScorePredsedSSTable.Hide();
            studKPDPredsedSSTable.Hide();
            allProgressPredsedSSTable.Hide();
        }

        private void olympConfPredsedSSLabel_Click(object sender, EventArgs e)
        {
            stipendiaPredsedSSTable.Hide();
            olympConfPredsedSSTable.Show();
            cultSportVolontPredsedSSTable.Hide();
            hozChasPredsedSSTable.Hide();
            dopScorePredsedSSTable.Hide();
            studKPDPredsedSSTable.Hide();
            allProgressPredsedSSTable.Hide();
        }

        private void studKPDPredsedSSLabel_Click(object sender, EventArgs e)
        {
            stipendiaPredsedSSTable.Hide();
            olympConfPredsedSSTable.Hide();
            cultSportVolontPredsedSSTable.Hide();
            hozChasPredsedSSTable.Hide();
            dopScorePredsedSSTable.Hide();
            studKPDPredsedSSTable.Show();
            allProgressPredsedSSTable.Hide();
        }

        private void dopScoressPredsedSSLabel_Click(object sender, EventArgs e)
        {
            stipendiaPredsedSSTable.Hide();
            olympConfPredsedSSTable.Hide();
            cultSportVolontPredsedSSTable.Hide();
            hozChasPredsedSSTable.Hide();
            dopScorePredsedSSTable.Show();
            studKPDPredsedSSTable.Hide();
            allProgressPredsedSSTable.Hide();
        }

        private void stipendiaPredsedSSLabel_Click(object sender, EventArgs e)
        {
            stipendiaPredsedSSTable.Show();
            olympConfPredsedSSTable.Hide();
            cultSportVolontPredsedSSTable.Hide();
            hozChasPredsedSSTable.Hide();
            dopScorePredsedSSTable.Hide();
            studKPDPredsedSSTable.Hide();
            allProgressPredsedSSTable.Hide();
        }

        private void cultSportVolontPredsedSSLabel_Click(object sender, EventArgs e)
        {
            stipendiaPredsedSSTable.Hide();
            olympConfPredsedSSTable.Hide();
            cultSportVolontPredsedSSTable.Show();
            hozChasPredsedSSTable.Hide();
            dopScorePredsedSSTable.Hide();
            studKPDPredsedSSTable.Hide();
            allProgressPredsedSSTable.Hide();
        }
        private void closeDatePredsedSS()
        {
            panelPredsedSS.Hide();
            eventLoadD -= loadDatePredsedSS;
            eventChangeDataTable -= commonDataPredsedSS;
            eventCloseD -= closeDatePredsedSS;
        } 
    }
}
