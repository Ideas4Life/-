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
        /*
            Работа коменданта с панелью управления
        */

        //загрузка начальной страницы коменданта на панели Управления и соответствующих данных

        private void loadDataComendant()
        {
            panelComendant.Dock = DockStyle.Fill;
            panelComendant.Visible = true;
            listStudComPanel.Visible = true;

            listStudComPanel.Size = dataStudComPanel.Size = KPDComPanel.Size = addStudComPanel.Size =
                studSovetComPanel.Size = new Size(657, 300);
            listStudComPanel.Location = dataStudComPanel.Location = KPDComPanel.Location = addStudComPanel.Location
                = studSovetComPanel.Location = new Point(4, 70);

            loadlistStudsComend();
            dataLoadKPDCom();
            dataLoadStudSovetCom();
        }


        /*
            Настройка отображения данных на панели "Управление" коменданта при клике на соответствующие метки
        */


        private void listStuds_Click(object sender, EventArgs e)
        {
            listStudComPanel.Visible = true;
            dataStudComPanel.Visible = false;
            KPDComPanel.Visible = false; ;
            addStudComPanel.Visible = false;
            studSovetComPanel.Visible = false;
        }

        private void lookDataStud_Click(object sender, EventArgs e)
        {
            listStudComPanel.Visible = false;
            dataStudComPanel.Visible = true;
            KPDComPanel.Visible = false; ;
            addStudComPanel.Visible = false;
            studSovetComPanel.Visible = false;
        }

        private void comendKPD_Click(object sender, EventArgs e)
        {
            listStudComPanel.Visible = false;
            dataStudComPanel.Visible = false;
            KPDComPanel.Visible = true;
            addStudComPanel.Visible = false;
            studSovetComPanel.Visible = false;
        }

        private void addStud_Click(object sender, EventArgs e)
        {
            listStudComPanel.Visible = false;
            dataStudComPanel.Visible = false;
            KPDComPanel.Visible = false; ;
            addStudComPanel.Visible = true;
            studSovetComPanel.Visible = false;
        }

        private void studSovet_Click(object sender, EventArgs e)
        {
            listStudComPanel.Visible = false;
            dataStudComPanel.Visible = false;
            KPDComPanel.Visible = false; ;
            addStudComPanel.Visible = false;
            studSovetComPanel.Visible = true;
        }

        //загрузка списка студентов на панели Управления при работе коменданта

        private void loadlistStudsComend()
        {

            listStudComendTable.Rows.Clear();
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
                listStudComendTable.Rows.Add(s);
            int sz = listStudComendTable.ColumnHeadersHeight + listStudComendTable.RowTemplate.Height * listStudComendTable.Rows.Count - Convert.ToInt32(listStudComendTable.Rows.Count * 2);
            if (sz <= 250)
                listStudComendTable.Size = new Size(640, sz);
            else listStudComendTable.Size = new Size(640, 250);
            listStudComendTable.Location = new Point(10, 10);
        }

        //Загрузка данных о КПД на панели управления коменданта

        private void dataLoadKPDCom()
        {
            KPDComTable.Rows.Clear();
            string query = "SELECT * FROM KPD ";
            OleDbCommand command = new OleDbCommand(query, myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[1].ToString();
                data[data.Count - 1][1] = reader[2].ToString();
                data[data.Count - 1][2] = reader[3].ToString();
            }
            reader.Close();
            foreach (string[] s in data)
                KPDComTable.Rows.Add(s);
            int sz = KPDComTable.ColumnHeadersHeight + KPDComTable.RowTemplate.Height * KPDComTable.Rows.Count - Convert.ToInt32(KPDComTable.Rows.Count * 2);
            if (sz <= 250)
                KPDComTable.Height = sz;
            else KPDComTable.Height = 250;

            KPDComTable.Width = 640;
            KPDComTable.Location = new Point(10, 10);
        }

        //Закрузка данных о конкретном студенте из баззы данных при нажатии на кнопку комендантом

        private void OkComendButton_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT [idStud] FROM [Students] WHERE  fullName=@name";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.Parameters.Add("name", OleDbType.VarChar).Value = nameStudTextBox.Text;
                string idSt = command.ExecuteScalar().ToString();
                StId = idSt;

                loadDataOlympKonfCom();
                loadDataCultSportVolontCom();
                loadDataHozChasCom();
                loadDataDopScoreCom();
                loadDataStipendiaCom();
                loadDataStudKPDCom();
                loadDataAllProgressCom();
            }
            catch
            {

            }
            olympConfStudComTable.Width = sportVolontStudComTable.Width = hozChasStudComTable.Width = dopScoresStudComTable.Width =
                stipendiaStudComTable.Width = KPDStudComTable.Width = allProgressStudComTable.Width = 630;
            olympConfStudComTable.Location = sportVolontStudComTable.Location = hozChasStudComTable.Location =
                dopScoresStudComTable.Location = stipendiaStudComTable.Location = KPDStudComTable.Location =
                allProgressStudComTable.Location = new Point(10, 65);

            kindProgressPanel.Visible = true;
            stipendiaStudComTable.Visible = false;
            olympConfStudComTable.Visible = false;
            sportVolontStudComTable.Visible = false;
            hozChasStudComTable.Visible = false;
            dopScoresStudComTable.Visible = false;
            KPDStudComTable.Visible = false;
            allProgressStudComTable.Visible = true;
            allScoresStudCom.Visible = true;

        }

        //Загрузка данных пользователя для коменданта о его олимпиадах, конференциях из базы данных

        double[] scoresStudCom = new double[6];
        private void loadDataOlympKonfCom()
        {
            olympConfStudComTable.Rows.Clear();

            scoresStudCom[0] = 0;
            string query = "SELECT [nameOlympConf], [levelOlympConf], [resultOlympConf], [scoreOlympConf]" +
                " FROM [OlympConf] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = StId;

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[4]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                scoresStudCom[0] += Convert.ToInt32(reader[3].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
                olympConfStudComTable.Rows.Add(s);
            int sz = olympConfStudComTable.ColumnHeadersHeight + olympConfStudComTable.RowTemplate.Height *
                olympConfStudComTable.Rows.Count - Convert.ToInt32(olympConfStudComTable.Rows.Count * 2);
            if (sz <= 190)
                olympConfStudComTable.Height = sz;
            else olympConfStudComTable.Height = 190;
        }

        //Загрузка данных пользователя для коменданта о его культурных, спортивных, волонтерских мероприятиях из базы данных

        private void loadDataCultSportVolontCom()
        {
            sportVolontStudComTable.Rows.Clear();
            scoresStudCom[1] = 0;
            string query = "SELECT [nameCultSportVolont], [levelCultSportVolont], [scoreCultSportVolont]" +
                " FROM [CultSportVolont] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = StId;

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                scoresStudCom[1] += Convert.ToInt32(reader[2].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
                sportVolontStudComTable.Rows.Add(s);
            int sz = sportVolontStudComTable.ColumnHeadersHeight + sportVolontStudComTable.RowTemplate.Height *
                sportVolontStudComTable.Rows.Count - Convert.ToInt32(sportVolontStudComTable.Rows.Count * 2);
            if (sz <= 190)
                sportVolontStudComTable.Height = sz;
            else sportVolontStudComTable.Height = 190;
        }

        //Загрузка данных пользователя для коменданта о его Хоз часах из базы данных

        private void loadDataHozChasCom()
        {
            hozChasStudComTable.Rows.Clear();
            scoresStudCom[2] = 0;
            string query = "SELECT [names], [scores], [date]" +
                " FROM [HozChas] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = StId;

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                scoresStudCom[2] += Convert.ToInt32(reader[1].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
                hozChasStudComTable.Rows.Add(s);
            int sz = hozChasStudComTable.ColumnHeadersHeight + hozChasStudComTable.RowTemplate.Height * hozChasStudComTable.Rows.Count
                - Convert.ToInt32(hozChasStudComTable.Rows.Count * 2);
            if (sz <= 190)
                hozChasStudComTable.Height = sz;
            else hozChasStudComTable.Height = 190;
        }

        //Загрузка данных пользователя для коменданта о его дополнительных баллах из базы данных

        private void loadDataDopScoreCom()
        {
            dopScoresStudComTable.Rows.Clear();
            scoresStudCom[3] = 0;
            string query = "SELECT [sanKom], [starosta], [remontRoom], [studSovet], [markStudy]" +
                " FROM [ScoresStud] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = StId;

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
                scoresStudCom[3] += (Convert.ToDouble(reader[0].ToString()) - 5) * 5;
                scoresStudCom[3] += Convert.ToInt32(reader[1].ToString());
                scoresStudCom[3] += Convert.ToInt32(reader[2].ToString());
                scoresStudCom[3] += Convert.ToInt32(reader[3].ToString());
                scoresStudCom[3] += Convert.ToDouble(reader[4].ToString()) * 20;
            }
            reader.Close();
            foreach (string[] s in data)
                dopScoresStudComTable.Rows.Add(s);
            int sz = dopScoresStudComTable.ColumnHeadersHeight + dopScoresStudComTable.RowTemplate.Height *
                dopScoresStudComTable.Rows.Count - Convert.ToInt32(dopScoresStudComTable.Rows.Count * 2);
            if (sz <= 190)
                dopScoresStudComTable.Height = sz;
            else dopScoresStudComTable.Height = 190;
        }

        //Загрузка данных пользователя для коменданта о его именных стипендиях из базы данных

        private void loadDataStipendiaCom()
        {
            stipendiaStudComTable.Rows.Clear();
            scoresStudCom[4] = 0;
            string query = "SELECT [nameStipendia], [levelStipendia], [scoreStipendia]" +
                " FROM [Stipendia] WHERE idStud=@uId ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = StId;

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                scoresStudCom[4] += Convert.ToInt32(reader[2].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
                stipendiaStudComTable.Rows.Add(s);
            int sz = stipendiaStudComTable.ColumnHeadersHeight + stipendiaStudComTable.RowTemplate.Height *
                stipendiaStudComTable.Rows.Count - Convert.ToInt32(stipendiaStudComTable.Rows.Count * 2);
            if (sz <= 190)
                stipendiaStudComTable.Height = sz;
            else stipendiaStudComTable.Height = 190;
        }

        //Загрузка данных пользователя для коменданта о его нарушениях КПД из базы данных

        private void loadDataStudKPDCom()
        {
            KPDStudComTable.Rows.Clear();
            scoresStudCom[5] = 0;
            string query = "SELECT [idKindKPD],[dateKpd], [statusKPD], [scoreKPD]" +
                " FROM  [StudKPD] WHERE idStud=@uId";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("uId", OleDbType.VarChar).Value = StId;

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
                scoresStudCom[5] -= Convert.ToInt32(reader[3].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
                KPDStudComTable.Rows.Add(s);
            int sz = KPDStudComTable.ColumnHeadersHeight + KPDStudComTable.RowTemplate.Height * KPDStudComTable.Rows.Count -
                Convert.ToInt32(KPDStudComTable.Rows.Count * 2);
            if (sz <= 190)
                KPDStudComTable.Height = sz;
            else KPDStudComTable.Height = 190;
        }

        //Загрузка данных пользователя для коменданта о его свобдных баллах из базы данных

        private void loadDataAllProgressCom()
        {
            allProgressStudComTable.Rows.Clear();
            allProgressStudComTable.Rows.Add(new string[2] { "Олимпиады и конференции", Convert.ToString(scoresStudCom[0]) });
            allProgressStudComTable.Rows.Add(new string[2] { "Спортивные, культурно-массовые, гражданско-патриотические," +
                "общественные мероприятия", Convert.ToString(scoresStudCom[1]) });
            allProgressStudComTable.Rows.Add(new string[2] { "Хоз часы", Convert.ToString(scoresStudCom[2]) });
            allProgressStudComTable.Rows.Add(new string[2] { "Дополнительные баллы", Convert.ToString(scoresStudCom[3]) });
            allProgressStudComTable.Rows.Add(new string[2] { "Именные стипендии", Convert.ToString(scoresStudCom[4]) });
            allProgressStudComTable.Rows.Add(new string[2] { "Штрафные баллы за КПД", Convert.ToString(scoresStudCom[5]) });
            allScoresStudCom.Text = "Всего баллов: " + Convert.ToString(32);
            int sz = allProgressStudComTable.ColumnHeadersHeight +
                allProgressStudComTable.RowTemplate.Height * allProgressStudComTable.Rows.Count -
                Convert.ToInt32(allProgressStudComTable.Rows.Count * 2);
            if (sz <= 180)
                allProgressStudComTable.Height = sz;
            else allProgressStudComTable.Height = 180;
        }

        //Загрузка данных о Студ совете для коменданта из базы данных

        private void dataLoadStudSovetCom()
        {
            addChangePositionSSPanel.Location = new Point(12, 5);
            addChangePositionSSPanel.Size = new Size(632, 80);
            studPositionSSTable.Location = new Point(7, 98);
            studPositionSSTable.Width = 640;

            studPositionSSTable.Rows.Clear();
            string query = "SELECT [fullName], [position] FROM [Students] WHERE [position]<>'user'";
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
                studPositionSSTable.Rows.Add(s);
            int sz = studPositionSSTable.ColumnHeadersHeight + studPositionSSTable.RowTemplate.Height *
                studPositionSSTable.Rows.Count - Convert.ToInt32(studPositionSSTable.Rows.Count * 2);
            if (sz <= 190)
                studPositionSSTable.Height = sz;
            else studPositionSSTable.Height = 170;
        }

        //Изменить должность студенту
        private void changePositionStud_Click(object sender, EventArgs e)
        {
            errorChangePosotion.Text = "";
            string query = "UPDATE [Students] SET [position]=@posit Where [fullName]=@name";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("posit", OleDbType.VarChar).Value = positionStudAddSS.Text;
            command.Parameters.Add("name", OleDbType.VarChar).Value = nameStudPosition.Text;

            try
            {
                command.ExecuteNonQuery();
                errorChangePosotion.Text = "Удачно";
            }
            catch
            {
                errorChangePosotion.Text = "Ошибка";
            }
            eventChangeDateTable();
        }



        //Добавление студента в базу данных комендантом

        private void addStudDateCom_Click(object sender, EventArgs e)
        {
            bool bTrue = true;
            string nameStud = "", instituteStud = "", numberRoom = "";
            int numberTicket, numberGroup, numberCourse;
            DateTime dateBorn;

            errorNameStudAddCom.Visible = errorNumberTicketCom.Visible = errorNumberGroupCom.Visible = errorInstituteCom.Visible =
                errorCourseCom.Visible = errorNumberRoomCom.Visible = errorDateBornCom.Visible = false;

            if (!Regex.IsMatch(nameStudAddCom.Text, @"[\sa-zа-яё]$", RegexOptions.IgnoreCase))
            {
                errorNameStudAddCom.Visible = true;
                bTrue = false;
            }
            else
                nameStud = nameStudAddCom.Text;

            if (!int.TryParse(numberTicketCom.Text, out numberTicket))
            {
                errorNumberTicketCom.Visible = true;
            }

            if (!int.TryParse(numberGroupCom.Text, out numberGroup))
            {
                errorNumberGroupCom.Visible = true;
            }

            if (!Regex.IsMatch(instituteCom.Text, @"[a-zа-яё]$", RegexOptions.IgnoreCase))
            {
                errorInstituteCom.Visible = true;
                bTrue = false;
            }
            else
                instituteStud = instituteCom.Text;

            if (!int.TryParse(courseCom.Text, out numberCourse))
            {
                errorCourseCom.Visible = true;
            }

            if (!Regex.IsMatch(dateBornCom.Text, @"\d{2}\.\d{2}\.\d{4}$", RegexOptions.IgnoreCase))
            {
                errorDateBornCom.Visible = true;
                bTrue = false;
            }
            else
            {
                string[] words = dateBornCom.Text.Split(new char[] { '.' });
                int day, month, year;
                int.TryParse(words[0], out day);
                int.TryParse(words[1], out month);
                int.TryParse(words[2], out year);
                if (day > 31 || month > 12 || year < 1960 || year > DateTime.Now.Year)
                {
                    errorDateBornCom.Visible = true;
                    bTrue = false;
                }
                else
                    dateBorn = new DateTime(year, month, day);
            }

            if (!Regex.IsMatch(numberRoomCom.Text, @"[0-9a-c]$", RegexOptions.IgnoreCase))
            {
                errorNumberRoomCom.Visible = true;
                bTrue = false;
            }
            else
                numberRoom = numberRoomCom.Text;

            if (bTrue)
            {
                string querry0 = "Select Max(idStud) From [Students]";
                OleDbCommand command = new OleDbCommand(querry0, myConnection);
                var idMax = command.ExecuteScalar();
                int imax = Convert.ToInt32(idMax);
                imax++;
                string query = "INSERT into [Students] ([idStud], [fullName], [numberStudCard], [numberRoom], [numberGroup]," +
                    " [institute], [course], [position]) VALUES (@id, @name, @nTicket, @nRoom, @nGroup, @instituteSt, @nCourse, 'user')";

                command = new OleDbCommand(query, myConnection);
                command.Parameters.Add("id", OleDbType.Integer).Value = imax;
                command.Parameters.Add("name", OleDbType.VarChar, 30).Value = nameStud;
                command.Parameters.Add("nTicket", OleDbType.Integer).Value = numberTicket;
                command.Parameters.Add("nRoom", OleDbType.VarChar, 5).Value = numberRoom;
                command.Parameters.Add("nGroup", OleDbType.Integer).Value = numberGroup;
                command.Parameters.Add("instituteSt", OleDbType.VarChar, 10).Value = instituteStud;
                command.Parameters.Add("nCourse", OleDbType.Integer).Value = numberCourse;


                // выполняем запрос к MS Access
                try
                {
                    int count = command.ExecuteNonQuery();
                }
                catch
                {

                }
                string query1 = "INSERT into [LoginUser] ([idStud], [login], [password]) VALUES (@id, @log, @pass)";
                command = new OleDbCommand(query1, myConnection);
                var rnd = new Random();
                command.Parameters.Add("id", OleDbType.Integer).Value = imax;
                string lg = "", pas = "";
                int max = rnd.Next(5, 10);
                for (int i = 0; i < max; i++)
                {
                    lg += (char)rnd.Next(97, 122);
                    pas += (char)rnd.Next(97, 122);
                }
                command.Parameters.Add("log", OleDbType.VarChar, 15).Value = lg;
                command.Parameters.Add("pass", OleDbType.VarChar, 10).Value = pas;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {

                }

                string query2 = "INSERT into [ScoresStud] VALUES (@id, @san, @star, @remont, @studSov," +
                    "@markSt, @olymp, @sport, @hoz, @stip, @kpd, @all)";
                command = new OleDbCommand(query2, myConnection);
                command.Parameters.Add("id", OleDbType.Integer).Value = imax;
                command.Parameters.Add("san", OleDbType.Double, 15).Value = 0;
                command.Parameters.Add("star", OleDbType.Integer, 15).Value = 0;
                command.Parameters.Add("remont", OleDbType.Integer, 10).Value = 0;
                command.Parameters.Add("studSov", OleDbType.Integer, 15).Value = 0;
                command.Parameters.Add("markSt", OleDbType.Double, 10).Value = 0;
                command.Parameters.Add("olymp", OleDbType.Integer, 15).Value = 0;
                command.Parameters.Add("sport", OleDbType.Integer, 10).Value = 0;
                command.Parameters.Add("hos", OleDbType.Integer, 15).Value = 0;
                command.Parameters.Add("stip", OleDbType.Integer, 10).Value = 0;
                command.Parameters.Add("kpd", OleDbType.Integer, 15).Value = 0;
                command.Parameters.Add("all", OleDbType.Integer, 10).Value = 0;
                
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        nameStudAddCom.Text = "";
                        numberTicketCom.Text = "";
                        instituteCom.Text = "";
                        courseCom.Text = "";
                        dateBornCom.Text = "";
                        numberRoomCom.Text = "";
                        numberGroupCom.Text = "";
                    }
                }
                catch
                {

                }
            }
            eventChangeDateTable();
        }

        private void cancelCom_Click(object sender, EventArgs e)
        {
            nameStudAddCom.Text = "";
            numberTicketCom.Text = "";
            instituteCom.Text = "";
            courseCom.Text = "";
            dateBornCom.Text = "";
            numberRoomCom.Text = "";
            numberGroupCom.Text = "";
        }


        /*
               Настройка отображение данных на панели "Управление" коменданта при клике на категорию данных 
               о баллах студента
        */


        private void allScoresStudComBox_Click(object sender, EventArgs e)
        {
            stipendiaStudComTable.Visible = false;
            olympConfStudComTable.Visible = false;
            sportVolontStudComTable.Visible = false;
            hozChasStudComTable.Visible = false;
            dopScoresStudComTable.Visible = false;
            KPDStudComTable.Visible = false;
            allProgressStudComTable.Visible = true;
            allScoresStudCom.Visible = true;
        }

        private void hozChasComLabel_Click(object sender, EventArgs e)
        {
            stipendiaStudComTable.Visible = false;
            olympConfStudComTable.Visible = false;
            sportVolontStudComTable.Visible = false;
            hozChasStudComTable.Visible = true;
            dopScoresStudComTable.Visible = false;
            KPDStudComTable.Visible = false;
            allProgressStudComTable.Visible = false;
            allScoresStudCom.Visible = false;
        }

        private void olympConfStudComLabel_Click(object sender, EventArgs e)
        {
            stipendiaStudComTable.Visible = false;
            olympConfStudComTable.Visible = true;
            sportVolontStudComTable.Visible = false;
            hozChasStudComTable.Visible = false;
            dopScoresStudComTable.Visible = false;
            KPDStudComTable.Visible = false;
            allProgressStudComTable.Visible = false;
            allScoresStudCom.Visible = false;
        }

        private void KPDStudComLabel_Click(object sender, EventArgs e)
        {
            stipendiaStudComTable.Visible = false;
            olympConfStudComTable.Visible = false;
            sportVolontStudComTable.Visible = false;
            hozChasStudComTable.Visible = false;
            dopScoresStudComTable.Visible = false;
            KPDStudComTable.Visible = true;
            allProgressStudComTable.Visible = false;
            allScoresStudCom.Visible = false;
        }

        private void stipendiaStudComLabel_Click(object sender, EventArgs e)
        {
            stipendiaStudComTable.Visible = true;
            olympConfStudComTable.Visible = false;
            sportVolontStudComTable.Visible = false;
            hozChasStudComTable.Visible = false;
            dopScoresStudComTable.Visible = false;
            KPDStudComTable.Visible = false;
            allProgressStudComTable.Visible = false;
            allScoresStudCom.Visible = false;
        }

        private void dopScoresStudComLabel_Click(object sender, EventArgs e)
        {
            stipendiaStudComTable.Visible = false;
            olympConfStudComTable.Visible = false;
            sportVolontStudComTable.Visible = false;
            hozChasStudComTable.Visible = false;
            dopScoresStudComTable.Visible = true;
            KPDStudComTable.Visible = false;
            allProgressStudComTable.Visible = false;
            allScoresStudCom.Visible = false;
        }

        private void sportVolontStudComLabel_Click(object sender, EventArgs e)
        {
            stipendiaStudComTable.Visible = false;
            olympConfStudComTable.Visible = false;
            sportVolontStudComTable.Visible = true;
            hozChasStudComTable.Visible = false;
            dopScoresStudComTable.Visible = false;
            KPDStudComTable.Visible = false;
            allProgressStudComTable.Visible = false;
            allScoresStudCom.Visible = false;
        }

        //закрытие Элементов коменданта при выходе
        
        private void closeDateComendant()
        {
            panelComendant.Hide();
            eventChangeDateTable -= commonDateCultOrg;
            eventChangeDateTable -= dataLoadStudSovetCom;
        }
    }
}
