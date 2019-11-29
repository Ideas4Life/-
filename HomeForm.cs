﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BRS_Hostel
{
    public partial class HomeForm : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB_BRS_Hostel.mdb;";
        private OleDbConnection myConnection;
        private string StId="";
        private bool login = false;
        private string position = "";

        delegate void LoadDate();
        event LoadDate eventLoadD;
        event LoadDate eventCloseD;
        event LoadDate eventChangeDateTable;
        bool bProf = false;
        bool bProgress = false;
        bool bManag = false;

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

            //Добавление обработчиков события загрузки данных студентов
            eventLoadD += loadDataKPD;
            eventLoadD += loadDataProfileStud;
            eventLoadD += loadDataOlympKonf;
            eventLoadD += loadDataCultSportVolont;
            eventLoadD += loadDataHozChas;
            eventLoadD += loadDataDopScore;
            eventLoadD += loadDataStipendia;
            eventLoadD += loadDataStudKPD;
            eventLoadD += loadDataRating;
            eventLoadD += loadDataAllProgress;

            //Добавление обработчиков события для обновления данных в таблицах
            eventChangeDateTable += loadDataProfileStud;
            eventChangeDateTable += loadDataOlympKonf;
            eventChangeDateTable += loadDataCultSportVolont;
            eventChangeDateTable += loadDataHozChas;
            eventChangeDateTable += loadDataDopScore;
            eventChangeDateTable += loadDataStipendia;
            eventChangeDateTable += loadDataStudKPD;
            eventChangeDateTable += loadDataRating;
            eventChangeDateTable += loadDataAllProgress;
        }
        
        //Авторизация пользователей

        private void loginButton_Click(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
            if (logField.Text.Length != 0 && passField.Text.Length != 0)
            {
                //запрос на проверку логина и пароля и получения id студента
                string query = "Select [idStud], [position] From [Students] Where [idStud]=" +
                    "(SELECT [idStud] FROM [LoginUser] Where  [login]=@uLog AND [password]=@uPas)";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.Parameters.Add("uLog", OleDbType.VarChar).Value = logField.Text;
                command.Parameters.Add("uPas", OleDbType.VarChar).Value = passField.Text;
                
                try
                {
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        StId = reader[0].ToString();
                        position = reader[1].ToString();
                    }
                    bProf = true;
                    login = true;
                    authorizationPanel.Visible = false;
                    nameApplication.Visible = true;
                }
                catch
                {
                        errorLabel.Visible = true;
                }
                logField.Text = "";
                passField.Text = "";

                switch (position)
                {
                    case "Пользователь":
                        {
                            bProgress = true;
                            break;
                        }
                    case "Комендант":
                        {
                            eventLoadD += loadDataComendant;
                            eventCloseD += closeDateComendant;
                            eventChangeDateTable += loadlistStudsComend;
                            eventChangeDateTable += dataLoadStudSovetCom;
                            bManag = true;
                            break;
                        }
                    case "СанКом":
                        {
                            eventLoadD += loadDateSanKom;
                            eventCloseD += closeDateSanKom;
                            eventChangeDateTable += commonDateSanKom;
                            bProgress= true;
                            bManag = true;
                            break;
                        }
                    case "Отв. хоз часы":
                        {
                            eventLoadD +=loadDateHozChas;
                            eventCloseD += closeDateHozChas;
                            eventChangeDateTable += commonDateHozChas;
                            bProgress = true;
                            bManag = true;
                            break;
                        }
                    case "КультОрг":
                        {
                            eventLoadD += loadDateCultOrg;
                            eventCloseD += closeDateCultOrg;
                            eventChangeDateTable += commonDateCultOrg;
                            bProgress = true;
                            bManag = true;
                            break;
                        }
                    case "Отв. науч деят":
                        {
                            eventLoadD +=loadDateScienceOrg;
                            eventCloseD += closeDateScienceOrg;
                            eventChangeDateTable += commonDateScienceOrg;
                            bProgress = true;
                            bManag = true;
                            break;
                        }
                    case "Староста этажа":
                        {

                            break;
                        }
                    case "Председ. КПД":
                        {
                            bProgress = true;
                            bManag = true;
                            eventLoadD += loadDatePredsedKPD;
                            eventCloseD += closeDatePredsedKPD;
                            eventChangeDateTable += commonDatePredsedKPD;
                            break;
                        }
                    case "Председатель СС":
                        {
                            //eventLoadD +=
                            break;
                        }
                    default: break;    
                }
                eventLoadD?.Invoke();
            }
            else
            {
                errorLabel.Visible = true;
            }
            if (!authorizationPanel.Visible)
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

        //Выход пользователя из учётной записи

        private void exitBotton_Click(object sender, EventArgs e)
        {
            login = false;
            position = "";
            authorizationPanel.Visible = true;
            exitBotton.Visible = false;
            nameApplication.Visible = false;
            bProf = false;
            bProgress = false;
            bManag = false;
            eventCloseD();
        }

        //Закрытие базы данных при закрытие формы

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // заркываем соединение с БД
            myConnection.Close();
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
            if (login && bProf)
            {
                if (Convert.ToInt32(StId) > 0)
                    loadDataProfileStud();
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
            if (login && bProgress)
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
            if (login)
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
            if (login && bManag)
            {
                mainPanel.Hide();
                profilePanel.Hide();
                progressPanel.Hide();
                ratingPanel.Hide();
                managementPanel.Show();
                managementPanel.Dock = DockStyle.Fill;
            }
        }


    }
}
