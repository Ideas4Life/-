using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections.Generic;

namespace BRS_Hostel
{
    public partial class HomeForm : Form
    {


        private void commonScienceOrgLabel_Click(object sender, EventArgs e)
        {
            commonScienceOrgPanel.Show();
            personalScienceOrgPanel.Hide();
            stipendScienceOrgPanel.Hide();
            studyScienceOrgPanel.Hide();
        }

        private void personalScienceOrgLabel_Click(object sender, EventArgs e)
        {
            commonScienceOrgPanel.Hide();
            personalScienceOrgPanel.Show();
            stipendScienceOrgPanel.Hide();
            studyScienceOrgPanel.Hide();
        }

        private void stipendScienceOrgLabel_Click(object sender, EventArgs e)
        {
            commonScienceOrgPanel.Hide();
            personalScienceOrgPanel.Hide();
            stipendScienceOrgPanel.Show();
            studyScienceOrgPanel.Hide();
        }

        private void studyScienceOrgLabel_Click(object sender, EventArgs e)
        {
            commonScienceOrgPanel.Hide();
            personalScienceOrgPanel.Hide();
            stipendScienceOrgPanel.Hide();
            studyScienceOrgPanel.Show();
        }

        

        private void loadDateScienceOrg()
        {
            panelScienceOrg.Dock = DockStyle.Fill;
            panelScienceOrg.Show();

            commonScienceOrgPanel.Show();
            personalScienceOrgPanel.Hide();
            stipendScienceOrgPanel.Hide();
            studyScienceOrgPanel.Hide();


            //инициализация панели меню
            menuPanelScienceOrg.Location = new Point(3, 3);
            menuPanelScienceOrg.Size = new Size(660, 60);

            //инициализация панели для просмотра баллов всех студентов
            commonScienceOrgPanel.Location = new Point(3, 63);
            commonScienceOrgPanel.Size = new Size(660, 305);

            //инициализация таблицы для просмотра баллов всех студентов
            commonScienceOrgTable.Location = new Point(100, 5);
            commonScienceOrgTable.Size = new Size(460, personalCultOrgTable.ColumnHeadersHeight);

            //инициализация панели для просмотра баллов выбранного студентов
            personalScienceOrgPanel.Location = new Point(3, 63);
            personalScienceOrgPanel.Size = new Size(660, 305);

            //инициализация таблицы для просмотра баллов выбранного студентов
            personalScienceOrgTable.Location = new Point(10, 55);
            personalScienceOrgTable.Size = new Size(640, commonCultOrgTable.ColumnHeadersHeight);


            //инициализация панели для просмотра баллов всех студентов
            stipendScienceOrgPanel.Location = new Point(3, 63);
            stipendScienceOrgPanel.Size = new Size(660, 305);

            //инициализация таблицы для просмотра баллов всех студентов
            stipendScienceOrgTable.Location = new Point(10, 55);
            stipendScienceOrgTable.Size = new Size(640, stipendScienceOrgTable.ColumnHeadersHeight);

            //инициализация панели для просмотра баллов выбранного студентов
            studyScienceOrgPanel.Location = new Point(3, 63);
            studyScienceOrgPanel.Size = new Size(660, 305);

            //инициализация таблицы для просмотра баллов выбранного студентов
            studyScienceOrgTable.Location = new Point(10, 55);
            studyScienceOrgTable.Size = new Size(560, studyScienceOrgTable.ColumnHeadersHeight);

            //расположение кнопки на панели именных стипендий студентов
            addStipendScienceOrg.Location = new Point(550, 250);

            commonDateScienceOrg();
            stipendDateScienceOrg();
            studyDateScienceOrg();
        }
        
        private void commonDateScienceOrg()
        {
            commonScienceOrgTable.Rows.Clear();
            string querry = "Select Distinct [a.fullName], b.[allOlympConf] From [Students] a, [ScoresStud] b Where a.[idStud]=b.[idStud]";
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
                commonScienceOrgTable.Rows.Add(s);


            int sz = commonScienceOrgTable.ColumnHeadersHeight + commonScienceOrgTable.RowTemplate.Height * commonScienceOrgTable.Rows.Count
                - Convert.ToInt32(commonScienceOrgTable.Rows.Count * 2);
            if (sz <= 290)
                commonScienceOrgTable.Height = sz;
            else commonScienceOrgTable.Height = 290;

        }
        private void findScienceOrgButton_Click(object sender, EventArgs e)
        {
            if (nameScienceOrgTB.Text != "")
            {
                string querry = "Select [idStud] From [Students] Where [fullName]=@name";
                OleDbCommand command = new OleDbCommand(querry, myConnection);
                command.Parameters.Add("name", OleDbType.VarChar).Value = nameScienceOrgTB.Text;
                try
                {
                    int id = int.Parse(command.ExecuteScalar().ToString());
                    personalDateScienceOrg(id);
                }
                catch
                {

                }
            }
        }

        int idSO;
        private void personalDateScienceOrg(int idS)
        {
            idSO = idS;
            personalScienceOrgTable.Rows.Clear();
            string querry = "Select [nameOlympConf], [levelOlympConf], [resultOlympConf], [scoreOlympConf] From [OlympConf] " +
                "Where [idStud]=@id";
            OleDbCommand command = new OleDbCommand(querry, myConnection);
            command.Parameters.Add("id", OleDbType.Integer).Value = idSO;
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
                personalScienceOrgTable.Rows.Add(s);

            personalScienceOrgTable.Rows.Add("", "", "", "");

            int count = personalScienceOrgTable.Rows.Count;
            if (count == 0)
                count = 1;
            personalScienceOrgTable[0, count - 1].ReadOnly = false;
            personalScienceOrgTable[1, count - 1].ReadOnly = false;
            personalScienceOrgTable[2, count - 1].ReadOnly = false;
            personalScienceOrgTable[3, count - 1].ReadOnly = false;

            int sz = personalScienceOrgTable.ColumnHeadersHeight + personalScienceOrgTable.RowTemplate.Height * personalScienceOrgTable.Rows.Count
                - Convert.ToInt32(personalScienceOrgTable.Rows.Count * 2);
            if (sz <= 250)
                personalScienceOrgTable.Height = sz;
            else personalScienceOrgTable.Height = 250;
        }
        
        private void saveScienceOrgButton_Click(object sender, EventArgs e)
        {
            int k = personalScienceOrgTable.Rows.Count;
            if (k <= 0)
                k = 1;

            string names = personalScienceOrgTable[0, k - 1].Value.ToString();
            string level = personalScienceOrgTable[1, k - 1].Value.ToString();
            string result = personalScienceOrgTable[2, k - 1].Value.ToString();
            int count = int.Parse(personalScienceOrgTable[3, k - 1].Value.ToString());

            string query = "Insert Into [OlympConf] ([idStud],[nameOlympConf]," +
                "[levelOlympConf], [resultOlympConf], [scoreOlympConf]) Values (@id, @name, @lev,@res, @scor)";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("id", OleDbType.Integer).Value = idSO;
            command.Parameters.Add("name", OleDbType.VarChar).Value = names;
            command.Parameters.Add("lev", OleDbType.VarChar).Value = level;
            command.Parameters.Add("res", OleDbType.VarChar).Value = result;
            command.Parameters.Add("scor", OleDbType.Integer).Value = count;

            command.ExecuteNonQuery();

            personalScienceOrgTable.Rows.Clear();

            personalDateScienceOrg(idSO);
        }

        private void stipendDateScienceOrg()
        {
            stipendScienceOrgTable.Rows.Clear();
            string querry = "Select Distinct [a.fullName], b.[nameStipendia], b.[levelStipendia], b.[scoreStipendia] " +
                "From [Students] a, [Stipendia] b Where b.[idStud]=a.[idStud] And a.[idStud] in (Select [idStud] From [Students])";
            OleDbCommand command = new OleDbCommand(querry, myConnection);

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
                stipendScienceOrgTable.Rows.Add(s);

            stipendScienceOrgTable.Rows.Add("", "", "", "");

            int count = stipendScienceOrgTable.Rows.Count;
            if (count == 0)
                count = 1;
            stipendScienceOrgTable[0, count - 1].ReadOnly = false;
            stipendScienceOrgTable[1, count - 1].ReadOnly = false;
            stipendScienceOrgTable[2, count - 1].ReadOnly = false;
            stipendScienceOrgTable[3, count - 1].ReadOnly = false;

            int sz = stipendScienceOrgTable.ColumnHeadersHeight + stipendScienceOrgTable.RowTemplate.Height * stipendScienceOrgTable.Rows.Count
                - Convert.ToInt32(stipendScienceOrgTable.Rows.Count * 2);
            if (sz <= 270)
                stipendScienceOrgTable.Height = sz;
            else stipendScienceOrgTable.Height = 270;
        }

        private void addStipendScienceOrg_Click(object sender, EventArgs e)
        {
            int k = stipendScienceOrgTable.Rows.Count;
            if (k <= 0)
                k = 1;

            string fio = stipendScienceOrgTable[0, k - 1].Value.ToString();
            string name = stipendScienceOrgTable[1, k - 1].Value.ToString();
            string level = stipendScienceOrgTable[2, k - 1].Value.ToString();
            int count = int.Parse(stipendScienceOrgTable[3, k - 1].Value.ToString());

            string quer = "Select [idStud] From [Students] Where [fullName] =@fio";
            OleDbCommand command = new OleDbCommand(quer, myConnection);
            command.Parameters.Add("fio", OleDbType.VarChar).Value = fio;
            int idS = int.Parse(command.ExecuteScalar().ToString());


            string query = "Insert Into [Stipendia] ([idStud],[nameStipendia]," +
                "[levelStipendia], [scoreStipendia]) Values (@id, @name, @lev, @scor)";
            OleDbCommand command1 = new OleDbCommand(query, myConnection);
            command1.Parameters.Add("id", OleDbType.Integer).Value = idS;
            command1.Parameters.Add("name", OleDbType.VarChar).Value = name;
            command1.Parameters.Add("lev", OleDbType.VarChar).Value = level;
            command1.Parameters.Add("scor", OleDbType.Integer).Value = count;

            
            command1.ExecuteNonQuery();

            stipendDateScienceOrg();
        }
        
        private void studyDateScienceOrg()
        {
            studyScienceOrgTable.Rows.Clear();
            string querry = "Select a.[fullName], b.[markStudy] From [Students] a, [ScoresStud] b Where a.[idStud]=b.[idStud]";
            OleDbCommand command = new OleDbCommand(querry, myConnection);
            OleDbDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = (double.Parse(reader[1].ToString())*20).ToString();
            }
            reader.Close();

            foreach (string[] s in data)
                studyScienceOrgTable.Rows.Add(s);

            int sz = studyScienceOrgTable.ColumnHeadersHeight + studyScienceOrgTable.RowTemplate.Height * studyScienceOrgTable.Rows.Count
                - Convert.ToInt32(studyScienceOrgTable.Rows.Count * 2);
            if (sz <= 270)
                studyScienceOrgTable.Height = sz;
            else studyScienceOrgTable.Height = 270;
        }
        
        private void addStudyScienceOrgButton_Click(object sender, EventArgs e)
        {
            int k = studyScienceOrgTable.Rows.Count;

            for (int i = 0; i < k; i++)
            {
                string fio = studyScienceOrgTable[0, i].Value.ToString();
                double avg = double.Parse(studyScienceOrgTable[1, i].Value.ToString());

                string quer = "Select [idStud] From [Students] Where [fullName]=@fio";
                OleDbCommand command = new OleDbCommand(quer, myConnection);
                command.Parameters.Add("fio", OleDbType.VarChar).Value = fio;
                var idT = int.Parse(command.ExecuteScalar().ToString());

                string query = "Update [ScoresStud] SET [markStudy]=@mark Where [idStud]=@id";
                OleDbCommand command1 = new OleDbCommand(query, myConnection);
                command1.Parameters.Add("mark", OleDbType.Double).Value = avg;
                command1.Parameters.Add("id", OleDbType.Integer).Value = idT;

                command1.ExecuteNonQuery();
            }
            studyDateScienceOrg();
            eventChangeDateTable();
        }
        
        private void closeDateScienceOrg()
        {
            panelScienceOrg.Hide();
            eventChangeDateTable -= commonDateScienceOrg;
        }
        
    }
}
