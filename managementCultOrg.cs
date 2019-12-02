using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections.Generic;

namespace BRS_Hostel
{
    public partial class HomeForm : Form
    {

        private void personalCultOrgLabel_Click(object sender, EventArgs e)
        {
            commonCultOrgPanel.Hide();
            personalCultOrgPanel.Show();
        }

        private void commonCultOrgLabel_Click(object sender, EventArgs e)
        {
            commonCultOrgPanel.Show();
            personalCultOrgPanel.Hide();
        }

        private void loadDateCultOrg()
        {
            panelCultOrg.Dock = DockStyle.Fill;
            panelCultOrg.Show();

            commonCultOrgPanel.Show();
            personalCultOrgPanel.Hide();


            //инициализация панели меню
            menuPanelCultOrg.Location = new Point(3, 3);
            menuPanelCultOrg.Size = new Size(660, 45);

            //инициализация панели для просмотра баллов всех студентов
            commonCultOrgPanel.Location = new Point(3, 50);
            commonCultOrgPanel.Size = new Size(660, 320);

            //инициализация таблицы для просмотра баллов всех студентов
            commonCultOrgTable.Location = new Point(100, 5);
            commonCultOrgTable.Size = new Size(460, 120);

            //инициализация панели для просмотра баллов выбранного студентов
            personalCultOrgPanel.Location = new Point(3, 50);
            personalCultOrgPanel.Size = new Size(660, 320);

            //инициализация таблицы для просмотра баллов выбранного студентов
            personalCultOrgTable.Location = new Point(50, 55);
            personalCultOrgTable.Size = new Size(560, commonCultOrgTable.ColumnHeadersHeight);

            commonDateCultOrg();
        }

        private void commonDateCultOrg()
        {
            commonCultOrgTable.Rows.Clear();
            string querry = "Select Distinct [a.fullName], b.[allSportCultVolont] From [Students] a, [ScoresStud] b Where a.[idStud]=b.[idStud]";
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
                commonCultOrgTable.Rows.Add(s);


            int sz = commonCultOrgTable.ColumnHeadersHeight + commonCultOrgTable.RowTemplate.Height * commonCultOrgTable.Rows.Count
                - Convert.ToInt32(commonCultOrgTable.Rows.Count * 2);
            if (sz <= commonCultOrgPanel.Height - 20)
                commonCultOrgTable.Height = sz;
            else commonCultOrgTable.Height = commonCultOrgPanel.Height-20;

        }
        int idCO;
        private void personalDateCultOrg(int idS)
        {
            idCO = idS;
            personalCultOrgTable.Rows.Clear();
            string querry = "Select [nameCultSportVolont], [levelCultSportVolont], [scoreCultSportVolont]  From [CultSportVolont] " +
                "Where [idStud]=@id";
            OleDbCommand command = new OleDbCommand(querry, myConnection);
            command.Parameters.Add("id", OleDbType.Integer).Value = idCO;
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
                personalCultOrgTable.Rows.Add(s);

            personalCultOrgTable.Rows.Add("","","");

            int count = personalCultOrgTable.Rows.Count;
            personalCultOrgTable[0, count - 1].ReadOnly = false;
            personalCultOrgTable[1, count - 1].ReadOnly = false;
            personalCultOrgTable[2, count - 1].ReadOnly = false;

            int sz = personalCultOrgTable.ColumnHeadersHeight + personalCultOrgTable.RowTemplate.Height * personalCultOrgTable.Rows.Count
                - Convert.ToInt32(personalCultOrgTable.Rows.Count * 2);
            if (sz <= personalCultOrgPanel.Height - panel5.Height - 20)
                personalCultOrgTable.Height = sz;
            else personalCultOrgTable.Height = personalCultOrgPanel.Height- panel5.Height-20;
        }

        private void saveCultOrgButton_Click(object sender, EventArgs e)
        {
            int k = personalCultOrgTable.Rows.Count;
            if (k <= 0)
                k = 1;

            string names = personalCultOrgTable[0, k - 1].Value.ToString();
            string level = personalCultOrgTable[1, k - 1].Value.ToString();
            int count = int.Parse(personalCultOrgTable[2, k - 1].Value.ToString());

            string query = "Insert Into [CultSportVolont] ([idStud],[nameCultSportVolont]," +
                "[levelCultSportVolont],[scoreCultSportVolont]) Values (@id, @name, @lev, @scor)";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.Parameters.Add("id", OleDbType.Integer).Value = idCO;
            command.Parameters.Add("name", OleDbType.VarChar).Value = names;
            command.Parameters.Add("lev", OleDbType.VarChar).Value = level;
            command.Parameters.Add("scor", OleDbType.Integer).Value = count;

            command.ExecuteNonQuery();

            personalCultOrgTable.Rows.Clear();

            personalDateCultOrg(idCO);
            eventChangeDataTable();
        }

        private void findCultOrgButton_Click(object sender, EventArgs e)
        {
            if (nameCultOrgTB.Text != "")
            {
                string querry = "Select [idStud]  From [Students] Where [fullName]=@name";
                OleDbCommand command = new OleDbCommand(querry, myConnection);
                command.Parameters.Add("name", OleDbType.VarChar).Value = nameCultOrgTB.Text;
                try
                {
                    int id = int.Parse(command.ExecuteScalar().ToString());
                    personalDateCultOrg(id);
                }
                catch
                {

                }
            }
        }

        private void closeDateCultOrg()
        {
            panelCultOrg.Hide();
            eventLoadD -= loadDateCultOrg;
            eventCloseD -= closeDateCultOrg;
            eventChangeDataTable -= commonDateCultOrg;
        }
    }
}
