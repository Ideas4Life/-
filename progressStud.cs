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
            Загрузка данных пользователя о его достижениях
        */

        //загрузка данных пользователя о его олимпиадах, конференциях из базы данных

        private void loadDataOlympKonf()
        {
            olympСonfTable.Rows.Clear();

            string query = "SELECT [nameOlympConf], [levelOlympConf], [resultOlympConf], [scoreOlympConf]" +
                " FROM [OlympConf] WHERE idStud=@uId";

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
                //scoresStud[1] += Convert.ToDouble(reader[2].ToString());
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
            //scoresStud[2] = 0;
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
            //scoresStud[3] = 0;
            string query = "SELECT [sanKom], [starosta], [remontRoom], [studSovet], [markStudy]" +
                " FROM [ScoresStud] WHERE idStud=@uId";
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
                //scoresStud[5] -= Convert.ToDouble(reader[3].ToString());
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

            int sz = KPDTable.ColumnHeadersHeight + KPDTable.RowTemplate.Height * KPDTable.Rows.Count - Convert.ToInt32(KPDTable.Rows.Count * 2);
            if (sz <= 250)
                KPDTable.Height = sz;
            else KPDTable.Height = 250;
        }

        //загрузка данных пользователя и его общих сводных данных из базы данных

        private void loadDataAllProgress()
        {
            allProgressTable.Rows.Clear();
            string query = "SELECT * FROM [ScoresStud] Where [idStud]=@id ";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("id", OleDbType.VarChar).Value = StId;

            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                allProgressTable.Rows.Add(new string[2] { "Олимпиады и конференции", reader[6].ToString()});
                allProgressTable.Rows.Add(new string[2] { "Спортивные, культурно-массовые, гражданско-патриотические," +
                "общественные мероприятия", reader[7].ToString() });
                allProgressTable.Rows.Add(new string[2] { "Хоз часы", reader[8].ToString() });
                allProgressTable.Rows.Add(new string[2] { "Именные стипендии", reader[9].ToString() });
                allProgressTable.Rows.Add(new string[2] { "Штрафные баллы за КПД", reader[10].ToString()});
                double dopSc = (double.Parse(reader[1].ToString())-5)*5 + double.Parse(reader[2].ToString()) +
                    double.Parse(reader[3].ToString()) + double.Parse(reader[4].ToString()) +
                    double.Parse(reader[5].ToString())*20;
                allProgressTable.Rows.Add(new string[2] { "Дополнительные баллы", dopSc.ToString() });
                allProgressTable.Rows.Add(new string[2] { "Всего баллов", reader[11].ToString() });
            }
            reader.Close();

            foreach (string[] s in data)
                allProgressTable.Rows.Add(s);
             
            int sz = allProgressTable.ColumnHeadersHeight +
                allProgressTable.RowTemplate.Height * allProgressTable.Rows.Count - Convert.ToInt32(allProgressTable.Rows.Count * 2);
            if (sz <= 250)
                allProgressTable.Height = sz;
            else allProgressTable.Height = 250;
        }
    }
}
