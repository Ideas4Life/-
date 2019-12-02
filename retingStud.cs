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
        //Формирование рейтинга пользователей

        private void loadDataRating()
        {
            ratingTable.Rows.Clear();
            string query = "SELECT [a.fullName], [b.allScoresStud] FROM Students a, ScoresStud b WHERE b.idStud=a.idStud ORDER BY b.allScoresStud DESC";
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
    }
}
