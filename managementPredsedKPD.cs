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
        private void commonPredsedKPDLabel_Click(object sender, EventArgs e)
        {
            commonPredsedKPDPanel.Show();
            personalPredsedKPDPanel.Hide();
            panelListKPD.Hide();
        }

        private void personalPredsedKPDLabel_Click(object sender, EventArgs e)
        {
            commonPredsedKPDPanel.Hide();
            personalPredsedKPDPanel.Show();
            panelListKPD.Hide();
        }
        
        private void KPDLabel_Click(object sender, EventArgs e)
        {
            commonPredsedKPDPanel.Hide();
            personalPredsedKPDPanel.Hide();
            panelListKPD.Show();
        }
        

        private void loadDatePredsedKPD()
        {
            panelPredsedKPD.Show();
            panelPredsedKPD.Dock = DockStyle.Fill;
            

            commonPredsedKPDPanel.Show();

            personalPredsedKPDPanel.Hide();
            panelListKPD.Hide();


            //инициализация панели меню
            menuPanelPredsedKPD.Location = new Point(3, 3);
            menuPanelPredsedKPD.Size = new Size(660, 45);

            //инициализация панели для просмотра баллов всех студентов
            commonPredsedKPDPanel.Location = new Point(3, 50);
            commonPredsedKPDPanel.Size = new Size(660, 320);

            //инициализация таблицы для просмотра баллов всех студентов
            commonPredsedKPDTable.Location = new Point(100, 5);
            commonPredsedKPDTable.Size = new Size(460, 120);

            //инициализация панели для просмотра баллов выбранного студентов
            personalPredsedKPDPanel.Location = new Point(3, 50);
            personalPredsedKPDPanel.Size = new Size(660, 320);

            //инициализация таблицы для просмотра баллов выбранного студентов
            personalPredsedKPDTable.Location = new Point(50, 55);
            personalPredsedKPDTable.Size = new Size(560, commonPredsedKPDTable.ColumnHeadersHeight);

            //инициализация панели для просмотра списка КПД
            panelListKPD.Location= new Point(3, 50);
            panelListKPD.Size = new Size(660, 320);

            //инициализация таблицы для просмотра списка КПД
            PredsedKPDTable.Location = new Point(50, 20);
            PredsedKPDTable.Size = new Size(560, commonPredsedKPDTable.ColumnHeadersHeight);


            commonDatePredsedKPD(); 
            listKPD();
        }

        private void listKPD()
        {
            PredsedKPDTable.Rows.Clear();
            string querry = "Select Distinct [kindKPD], [numberKPD], [scoreKPD] From [KPD]";
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
                PredsedKPDTable.Rows.Add(s);


            int sz = PredsedKPDTable.ColumnHeadersHeight + PredsedKPDTable.RowTemplate.Height * PredsedKPDTable.Rows.Count
                - Convert.ToInt32(PredsedKPDTable.Rows.Count * 2);
            if (sz <= panelListKPD.Height - PredsedKPDTable.Location.Y - 15)
                PredsedKPDTable.Height = sz;
            else PredsedKPDTable.Height = panelListKPD.Height- PredsedKPDTable.Location.Y-15;
        }


        private void commonDatePredsedKPD()
        {
            commonPredsedKPDTable.Rows.Clear();
            string querry = "Select Distinct [a.fullName], b.[allScoreKPD] From [Students] a, [ScoresStud] b Where a.[idStud]=b.[idStud]";
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
                commonPredsedKPDTable.Rows.Add(s);


            int sz = commonPredsedKPDTable.ColumnHeadersHeight + commonPredsedKPDTable.RowTemplate.Height * commonPredsedKPDTable.Rows.Count
                - Convert.ToInt32(commonPredsedKPDTable.Rows.Count * 2);
            if (sz <= commonPredsedKPDPanel.Height - commonPredsedKPDTable.Location.Y - 15)
                commonPredsedKPDTable.Height = sz;
            else commonPredsedKPDTable.Height = commonPredsedKPDPanel.Height- commonPredsedKPDTable.Location.Y-15;

        }
        
        int idKPD;
        private void personalDatePredsedKPD(int idS)
        {
            idKPD = idS;
            personalPredsedKPDTable.Rows.Clear();

            string querry = "Select b.[kindKPD], a.[dateKpd], a.[scoreKPD] From [StudKPD] a, [KPD] b " +
                "Where a.[idKindKPD]=b.[idKindKPD] And a.[idStud]=@id";
            OleDbCommand command = new OleDbCommand(querry, myConnection);
            command.Parameters.Add("id", OleDbType.Integer).Value = idKPD;
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
                personalPredsedKPDTable.Rows.Add(s);

            personalPredsedKPDTable.Rows.Add("","","");

            int count = personalPredsedKPDTable.Rows.Count;
            if (count == 0)
                count = 1;
            personalPredsedKPDTable[0, count - 1].ReadOnly = false;
            personalPredsedKPDTable[1, count - 1].ReadOnly = false;

            int sz = personalPredsedKPDTable.ColumnHeadersHeight + personalPredsedKPDTable.RowTemplate.Height * personalPredsedKPDTable.Rows.Count
                - Convert.ToInt32(personalPredsedKPDTable.Rows.Count * 2);
            if (sz <= personalPredsedKPDPanel.Height - personalPredsedKPDTable.Location.Y - 15)
                personalPredsedKPDTable.Height = sz;
            else personalPredsedKPDTable.Height = personalPredsedKPDPanel.Height - personalPredsedKPDTable.Location.Y - 15;
        }
        
        private void savePredsedKPD_Click(object sender, EventArgs e)
        {
            int k = personalPredsedKPDTable.Rows.Count;
            if (k == 0)
                k = 1;

            string kind = personalPredsedKPDTable[0, k - 1].Value.ToString();
            string date = personalPredsedKPDTable[1, k - 1].Value.ToString();

            string[] words = date.Split(new char[] { '.' });
            int day, month, year;
            int.TryParse(words[0], out day);
            int.TryParse(words[1], out month);
            int.TryParse(words[2], out year);

            DateTime dt = new DateTime();
            try
            {
                dt = new DateTime(year, month, day);
            }
            catch
            {
                personalPredsedKPDTable[1, k - 1].Value = "ошибка";
            }

            int num = 1;
            for (int i=0; i< personalPredsedKPDTable.Rows.Count-1;i++)
            {
                if (kind == personalPredsedKPDTable[0, i].Value.ToString())
                    num++;
            }
            if (num > 3) num = 3;

            string quer = "Select [idKindKPD], [scoreKPD] From [KPD] Where [kindKPD]=@kind And [numberKPD]=@num";
            OleDbCommand command = new OleDbCommand(quer, myConnection);
            command.Parameters.Add("kind", OleDbType.VarChar).Value = kind;
            command.Parameters.Add("num", OleDbType.Integer).Value = num;
            OleDbDataReader reade = command.ExecuteReader();

            int idKind=0, count=0;
            while (reade.Read())
            {
                idKind = int.Parse(reade[0].ToString());
                count = int.Parse(reade[1].ToString());
            }
            reade.Close();
            
            string query = "Insert Into [StudKPD] Values (@id, @kind, @scor, @date, @stat)";
            command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("id", OleDbType.Integer).Value = idKPD;
            command.Parameters.Add("kind", OleDbType.Integer).Value = idKind;
            command.Parameters.Add("scor", OleDbType.Integer).Value = count;
            command.Parameters.Add("date", OleDbType.DBDate).Value = dt;
            command.Parameters.Add("stat", OleDbType.VarChar).Value = "закрыто";

            try
            {
                command.ExecuteNonQuery();
                personalDatePredsedKPD(idKPD);
                eventChangeDataTable();
            }
            catch
            {
                personalPredsedKPDTable[2, k - 1].Value = "ошибка";
            }

            
        }
        
        private void findPredsedKPD_Click(object sender, EventArgs e)
        {
            if (namePredsedKPDTB.Text != "")
            {
                string querry = "Select [idStud] From [Students] Where [fullName]=@name";
                OleDbCommand command = new OleDbCommand(querry, myConnection);
                command.Parameters.Add("name", OleDbType.VarChar).Value = namePredsedKPDTB.Text;
                try
                {
                    int id = int.Parse(command.ExecuteScalar().ToString());
                    personalDatePredsedKPD(id);
                }
                catch
                {

                }
            }
        }

        private void closeDatePredsedKPD()
        {
            panelPredsedKPD.Hide();
            eventLoadD -= loadDatePredsedKPD;
            eventChangeDataTable -= commonDatePredsedKPD;
            eventCloseD -= closeDatePredsedKPD;
        }
    }
}
