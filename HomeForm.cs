using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections.Generic;

namespace BRS_Hostel
{
    public partial class HomeForm : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB_BRS_Hostel.mdb;";
        private OleDbConnection myConnection;
        private string StId;
        private bool login = false;
        private bool bComend = false;
        private string position = "";


        /*
            Конструктор формы
        */


        public HomeForm()
        {
            InitializeComponent();
            login = false;


            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);
            // открываем соединение с БД
            myConnection.Open();

           KPDTable.Location = сultSportVolontTable.Location = olympСonfTable.Location
                = hozChasTable.Location = dopScoresTable.Location = stipendiaTable.Location =
                StudKPDTable.Location = allProgressTable.Location = new Point(15, 85);

            allProgressTable.Size = KPDTable.Size = сultSportVolontTable.Size = olympСonfTable.Size
                = hozChasTable.Size = dopScoresTable.Size = stipendiaTable.Size =
                StudKPDTable.Size = new Size(640, 80);

            authorizationPanel.Location = new Point(400,15);
            nameApplication.Location = new Point(280, 70);
            leftPanel.Size = new Size(45, leftPanel.Size.Height);
            mainPanel.Dock = DockStyle.Fill;
            olympСonfTable.Visible = false;
            profilePanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            ratingPanel.Hide();
            selectPhoto.Filter = "PNG files(*.png)|*.png|Bitmap files (*.bmp)|*.bmp|Image files (*.jpg)|*.jpg";

            //Выведение подсказки при наведении на иконки
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(homeBox, "Главная"); 
            toolTip.SetToolTip(profileBox, "Профиль");
            toolTip.SetToolTip(progressBox, "Статистика");
            toolTip.SetToolTip(ratingBox, "Рейтинг");
            toolTip.SetToolTip(managementBox, "Управление");

        }


        /*
            Авторизация пользователей
        */


        private void loginButton_Click(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
            if (logField.Text.Length != 0 && passField.Text.Length != 0)
            {
                try
                {
                    string query = "SELECT idStud FROM LoginUser WHERE  login=@uLog AND password=@uPas";
                    OleDbCommand command = new OleDbCommand(query, myConnection);
                    command.Parameters.Add("uLog", OleDbType.VarChar).Value = logField.Text;
                    command.Parameters.Add("uPas", OleDbType.VarChar).Value = passField.Text;
                    string idSt = command.ExecuteScalar().ToString();
                    StId = idSt;

                    login = true;
                    authorizationPanel.Visible = false;
                    nameApplication.Visible = true;

                    InitializeStud();
                    loadDataOlympKonf();
                    loadDataCultSportVolont();
                    loadDataHozChas();
                    loadDataDopScore();
                    loadDataStipendia();
                    loadDataKPD();
                    loadDataStudKPD();
                    loadDataAllProgress();
                    loadDataRating();
                }
                catch
                {
                    if (logField.Text == "com" && passField.Text == "moc")
                    {
                        loadDataRating();
                        bComend = true;
                        authorizationPanel.Visible = false;
                        nameApplication.Visible = true;
                        loadDataComendant();
                    }
                    else
                        errorLabel.Visible = true;
                }
                logField.Text = "";
                passField.Text = "";
            }
            else
            {
                errorLabel.Visible = true;
            }
            if (authorizationPanel.Visible == false)
                exitBotton.Visible = true;
            allProgressTable.Visible = true;
            hozChasTable.Visible = false;
            сultSportVolontTable.Visible = false;
            olympСonfTable.Visible = false;
            dopScoresTable.Visible = false;
            stipendiaTable.Visible = false;
            StudKPDTable.Visible = false;
            KPDTable.Visible = false;
        }


        /*
            Выходи пользователя из учётной записи
        */


        private void exitBotton_Click(object sender, EventArgs e)
        {
            login = false;
            authorizationPanel.Visible = true;
            exitBotton.Visible = false;
            nameApplication.Visible = false;

            panelComendant.Visible = false;
        }

        //Закрытие базы данных при закрытие формы

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // заркываем соединение с БД
            myConnection.Close();
        }


        /*
            Инициализация данных студента
        */


        public void InitializeStud()
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
                position = positionStud.Text = reader[7].ToString();
            }
        }

        //загрузка данных пользователя о его олимпиадах, конференциях из базы данных

        double[] scoresStud = new double[6];
        private void loadDataOlympKonf()
        {
            olympСonfTable.Rows.Clear();

            scoresStud[0] = 0;
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
                scoresStud[0]+= Convert.ToInt32(reader[3].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
                olympСonfTable.Rows.Add(s);
            int sz = olympСonfTable.ColumnHeadersHeight + olympСonfTable.RowTemplate.Height * olympСonfTable.Rows.Count - Convert.ToInt32(olympСonfTable.Rows.Count * 2);
            if (sz <= 250)
                olympСonfTable.Height = sz;
            else olympСonfTable.Height = 250;
        }

        //загрузка данных пользователя о его спортивных, культурных, волонтерских мероприятиях из базы данных

        private void loadDataCultSportVolont()
        {
            сultSportVolontTable.Rows.Clear();
            scoresStud[1] = 0;
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
                scoresStud[1] += Convert.ToInt32(reader[2].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
                сultSportVolontTable.Rows.Add(s);
            int sz = сultSportVolontTable.ColumnHeadersHeight + сultSportVolontTable.RowTemplate.Height * сultSportVolontTable.Rows.Count - Convert.ToInt32(сultSportVolontTable.Rows.Count * 2);
            if (sz <= 250)
                сultSportVolontTable.Height = sz;
            else сultSportVolontTable.Height = 250;
        }

        //загрузка данных пользователя и его Хоз часах из базы данных

        private void loadDataHozChas()
        {
            hozChasTable.Rows.Clear();
            scoresStud[2] = 0;
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
                scoresStud[2] += Convert.ToInt32(reader[1].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
                hozChasTable.Rows.Add(s);
            int sz = hozChasTable.ColumnHeadersHeight + hozChasTable.RowTemplate.Height * hozChasTable.Rows.Count - Convert.ToInt32(hozChasTable.Rows.Count * 2);
            if (sz <= 250)
                hozChasTable.Height = sz;
            else hozChasTable.Height = 250;
        }

        //загрузка данных пользователя и его дополнительных баллах из базы данных
        private void loadDataDopScore()
        {
            dopScoresTable.Rows.Clear();
            scoresStud[3] = 0;
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
                scoresStud[3] += (Convert.ToDouble(reader[0].ToString()) - 5) * 5;
                scoresStud[3] += Convert.ToInt32(reader[1].ToString());
                scoresStud[3] += Convert.ToInt32(reader[2].ToString());
                scoresStud[3] += Convert.ToInt32(reader[3].ToString());
                scoresStud[3] += Convert.ToDouble(reader[4].ToString()) * 20;
            }
            reader.Close();
            foreach (string[] s in data)
                dopScoresTable.Rows.Add(s);
            int sz = dopScoresTable.ColumnHeadersHeight + dopScoresTable.RowTemplate.Height * dopScoresTable.Rows.Count - Convert.ToInt32(dopScoresTable.Rows.Count * 2);
            if (sz <= 250)
                dopScoresTable.Height = sz;
            else dopScoresTable.Height = 250;
        }

        //загрузка данных пользователя и его именных стипендяих из базы данных

        private void loadDataStipendia()
        {
            stipendiaTable.Rows.Clear();
            scoresStud[4] = 0;
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
                scoresStud[4] += Convert.ToInt32(reader[2].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
               stipendiaTable.Rows.Add(s);
            int sz = stipendiaTable.ColumnHeadersHeight + stipendiaTable.RowTemplate.Height * stipendiaTable.Rows.Count - Convert.ToInt32(stipendiaTable.Rows.Count * 2);
            if (sz <= 250)
                StudKPDTable.Height = sz;
            else StudKPDTable.Height = 250;
        }

        //загрузка данных пользователя и его нарушениях КПД из базы данных

        private void loadDataStudKPD()
        {
            StudKPDTable.Rows.Clear();
            scoresStud[5] = 0;
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
                scoresStud[5] -= Convert.ToInt32(reader[3].ToString());
            }
            reader.Close();
            foreach (string[] s in data)
                StudKPDTable.Rows.Add(s);
            int sz = StudKPDTable.ColumnHeadersHeight + StudKPDTable.RowTemplate.Height * StudKPDTable.Rows.Count - Convert.ToInt32(StudKPDTable.Rows.Count * 2);
            if (sz <= 250)
                StudKPDTable.Height = sz;
            else StudKPDTable.Height = 250;
        }

        //загрузка данных КПД из базы данных

        private void loadDataKPD()
        {
            KPDTable.Rows.Clear();
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
                KPDTable.Rows.Add(s);
            int sz = KPDTable.ColumnHeadersHeight + KPDTable.RowTemplate.Height * KPDTable.Rows.Count - Convert.ToInt32(KPDTable.Rows.Count*2);
            if (sz <= 250) 
                KPDTable.Height = sz;
            else KPDTable.Height = 250;
        }

        //загрузка данных пользователя и его общих сводных данных из базы данных

        private void loadDataAllProgress()
        {
            allProgressTable.Rows.Clear();
            allProgressTable.Rows.Add(new string[2] { "Олимпиады и конференции", Convert.ToString(scoresStud[0]) });
            allProgressTable.Rows.Add(new string[2] { "Спортивные, культурно-массовые, гражданско-патриотические," +
                "общественные мероприятия", Convert.ToString(scoresStud[1]) });
            allProgressTable.Rows.Add(new string[2] { "Хоз часы", Convert.ToString(scoresStud[2]) });
            allProgressTable.Rows.Add(new string[2] { "Дополнительные баллы", Convert.ToString(scoresStud[3]) });
            allProgressTable.Rows.Add(new string[2] { "Именные стипендии", Convert.ToString(scoresStud[4]) });
            allProgressTable.Rows.Add(new string[2] { "Штрафные баллы за КПД", Convert.ToString(scoresStud[5]) });
            allScoresLabel.Text = "Всего баллов: " + Convert.ToString(32);
            int sz = allProgressTable.ColumnHeadersHeight+
                allProgressTable.RowTemplate.Height* allProgressTable.Rows.Count - Convert.ToInt32(allProgressTable.Rows.Count*2);
            if (sz <= 250)
                allProgressTable.Height = sz;
            else allProgressTable.Height = 250;
        }

        //Формирование рейтинга пользователей

        private void loadDataRating()
        {
            ratingTable.Rows.Clear();
            string query = "SELECT [a.fullName], [b.allHozChas] FROM Students a, ScoresStud b WHERE b.idStud=a.idStud ORDER BY b.allHozChas DESC";
            OleDbCommand command = new OleDbCommand(query, myConnection);

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();
            int k = 1;
            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = Convert.ToString(k++);
                data[data.Count - 1][1] = reader[0].ToString();
                data[data.Count - 1][2] = reader[1].ToString();
            }
            reader.Close();
            foreach (string[] s in data)
                ratingTable.Rows.Add(s);
            int sz = ratingTable.ColumnHeadersHeight + ratingTable.RowTemplate.Height * ratingTable.Rows.Count - Convert.ToInt32(ratingTable.Rows.Count * 2);
            if (sz <= 250)
                ratingTable.Height = sz;
            else ratingTable.Height = 250;
        }

        //обработка клика по икенке homeBox

        private void homeBox_Click(object sender, EventArgs e)
        {
            mainPanel.Show();
            mainPanel.Dock = DockStyle.Fill;
            profilePanel.Hide();
            progressPanel.Hide();
            managementPanel.Hide();
            ratingPanel.Hide();
        }

        //обработка клика по икенке profileBox

        private void profileBox_Click(object sender, EventArgs e)
        {
            if (login)
            {
                if (Convert.ToInt32(StId) > 0)
                    InitializeStud();
                mainPanel.Hide();
                progressPanel.Hide();
                managementPanel.Hide();
                ratingPanel.Hide();
                profilePanel.Show();
                profilePanel.Dock = DockStyle.Fill;
            }
        }

        //обработка клика по икенке progressBox

        private void progressBox_Click(object sender, EventArgs e)
        {
            if (login)
            {
                mainPanel.Hide();
                profilePanel.Hide();
                ratingPanel.Hide();
                managementPanel.Hide();
                progressPanel.Show();
                progressPanel.Dock = DockStyle.Fill;
            }
        }

        //обработка клика по икенке ratingBox

        private void ratingBox_Click(object sender, EventArgs e)
        {
            if (login || bComend)
            {
                mainPanel.Hide();
                profilePanel.Hide();
                progressPanel.Hide();
                managementPanel.Hide();
                ratingPanel.Show();
                ratingPanel.Dock = DockStyle.Fill;
            }
        }

        //обработка клика по икенке managementBox

        private void managementBox_Click(object sender, EventArgs e)
        {
            if (login && position !="user" || bComend)
            {
                mainPanel.Hide();
                profilePanel.Hide();
                progressPanel.Hide();
                ratingPanel.Hide();
                managementPanel.Show();
                managementPanel.Dock = DockStyle.Fill;
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

        /*
            Настройка отображение данных на панели "Достижения" пользователя при клике на соответствующие метки
        */

        private void allProgress_Click(object sender, EventArgs e)
        {
            allScoresLabel.Visible = true;
            allProgressTable.Visible = true;
            hozChasTable.Visible = false;
            сultSportVolontTable.Visible = false;
            olympСonfTable.Visible = false;
            dopScoresTable.Visible = false;
            stipendiaTable.Visible = false;
            StudKPDTable.Visible = false;
            KPDTable.Visible = false;
        }

        private void HozChas_Click(object sender, EventArgs e)
        {
            hozChasTable.Visible = true;
            сultSportVolontTable.Visible = false;
            olympСonfTable.Visible = false;
            dopScoresTable.Visible = false;
            stipendiaTable.Visible = false;
            StudKPDTable.Visible = false;
            KPDTable.Visible = false;
            allProgressTable.Visible = false;
            allScoresLabel.Visible = false;
        }

        private void KPD_Click(object sender, EventArgs e)
        {
            KPDTable.Visible = true;
            сultSportVolontTable.Visible = false;
            olympСonfTable.Visible = false;
            hozChasTable.Visible = false;
            dopScoresTable.Visible = false;
            stipendiaTable.Visible = false;
            StudKPDTable.Visible = false;
            allProgressTable.Visible = false;
            allScoresLabel.Visible = false;
        }

        private void cultSportVolont_Click(object sender, EventArgs e)
        {
            сultSportVolontTable.Visible = true;
            olympСonfTable.Visible = false;
            hozChasTable.Visible = false;
            dopScoresTable.Visible = false;
            stipendiaTable.Visible = false;
            StudKPDTable.Visible = false;
            KPDTable.Visible = false;
            allProgressTable.Visible = false;
            allScoresLabel.Visible = false;
        }

        private void stipendia_Click(object sender, EventArgs e)
        {
            stipendiaTable.Visible = true;
            olympСonfTable.Visible = false;
            сultSportVolontTable.Visible = false;
            hozChasTable.Visible = false;
            dopScoresTable.Visible = false;
            StudKPDTable.Visible = false;
            KPDTable.Visible = false;
            allProgressTable.Visible = false;
            allScoresLabel.Visible = false;
        }

        private void dop_Click(object sender, EventArgs e)
        {
            dopScoresTable.Visible = true;
            olympСonfTable.Visible = false;
            сultSportVolontTable.Visible = false;
            hozChasTable.Visible = false;
            stipendiaTable.Visible = false;
            StudKPDTable.Visible = false;
            KPDTable.Visible = false;
            allProgressTable.Visible = false;
            allScoresLabel.Visible = false;
        }

        private void MyKPD_Click(object sender, EventArgs e)
        {
            StudKPDTable.Visible = true;
            dopScoresTable.Visible = false;
            olympСonfTable.Visible = false;
            сultSportVolontTable.Visible = false;
            hozChasTable.Visible = false;
            stipendiaTable.Visible = false;
            KPDTable.Visible = false;
            allProgressTable.Visible = false;
            allScoresLabel.Visible = false;
        }

        private void olympKonf_Click(object sender, EventArgs e)
        {
            olympСonfTable.Visible = true;
            сultSportVolontTable.Visible = false;
            hozChasTable.Visible = false;
            dopScoresTable.Visible = false;
            stipendiaTable.Visible = false;
            StudKPDTable.Visible = false;
            KPDTable.Visible = false;
            allProgressTable.Visible = false;
            allScoresLabel.Visible = false;
        }


        /*
            Работа коменданта с панелью управления
        */

        //загрузка начальной страницы коменданта на панели Управления и соответствующих данных

        private void loadDataComendant()
        {
            //loadlistStudsComend();

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
            KPDComPanel.Visible = true ;
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
            string query = "SELECT [fullName], [numberRoom], [numberGroup], [course], [position] FROM [Students] ORDER BY [fullName]";
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
                allProgressStudComTable.Location = new Point(10,65);

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

    }
}
