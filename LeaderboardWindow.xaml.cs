using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RGZ_POGI3_
{
    /// <summary>
    /// Логика взаимодействия для LeaderboardWindow.xaml
    /// </summary>
    public partial class LeaderboardWindow : Window
    {
        public string[] updates { get; set; }

        CDBConnector connect = new CDBConnector();

        public LeaderboardWindow()
        {
            InitializeComponent();
            updates = new string[] { "number", "name", "score", "time" };
            DataContext = this;

            LoadDB();
        }

        public class CTest
        {
            public int number { get; set; }
            public string name { get; set; }
            public int score { get; set; }
            public string time { get; set; }
        }

        string path = "Data Source=C:\\RGZ(POGI3)\\test.db;Version=3;";

        public void LoadDB()
        {
            DataGrid.Items.Clear();

            connect.SetPath(path);

            List<SPlayer> db = ((CDBDataBase)connect.LoadData()).GetPlayers();

            foreach (SPlayer player in db)
            {
                var str = new CTest
                {
                    name = player.name,
                    score = player.score,
                    time = player.time,
                };
                DataGrid.Items.Add(str);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
