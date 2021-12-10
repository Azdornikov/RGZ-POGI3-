using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RGZ_POGI3_
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    ///
    public partial class GameWindow : Window
    {
        System.Windows.Threading.DispatcherTimer Timer;

        int s = 0;
        int i = 0;

        int height = 5;
        int width = 8;
        int[] arr = new int[2];

        int card1;
        int card2;

        int click = 0;

        List<string> players = new List<string>() { "1", "2", "3", "4" };
        int[] score = new int[4];

        int action = 0;

        string[] fNames;
        Image[] imgs;

        CGenerator gen;
        CDBConnector connect = new CDBConnector();
        CDBDataBase db = new CDBDataBase();
        CSound sound = new CSound();
        CPLayers pl = new CPLayers();

        string fName = "";

        public GameWindow()
        {
            InitializeComponent();

            quit.Visibility = Visibility.Collapsed;

            Start start = new Start();
            if (start.ShowDialog() == true)
            {
                pl.createplayers(players, score, start, this);
            }

            grid.Rows = height;
            grid.Columns = width;
            //коэффициент 1,4525
            grid.Width = width * 105;
            grid.Height = height * 152.5125;

            gen = new CGenerator(width, height);
            gen.generate();

            for (int i = 0; i < width * height; i++)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("pack://application:,,,/Cards/0.png"));

                Button button = new Button();
                button.Tag = i;
                button.Width = 105;
                button.Height = 152.5125;
                button.Content = img;
                button.Background = Brushes.FloralWhite;
                button.BorderThickness = new Thickness(0);
                button.Click += Button_Click;
                grid.Children.Add(button);
            }

            fNames = Directory.GetFiles("C:/RGZ(POGI3)/Memo/Cards/");
            imgs = new Image[fNames.Length];

            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(dispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 1);
            Timer.Start();
        }

        string t = "";
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            s++;
            DateTime dt = new DateTime();
            dt = dt.AddSeconds(s);
            time.Content = "Время: " + dt.ToString("mm:ss");
            t = dt.ToString("mm:ss");
        }

        bool first_click = false;
        bool second_click = false;

        string path = "Data Source=C:\\RGZ(POGI3)\\test.db;Version=3;";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fName = "C:/RGZ(POGI3)/Memo/Resources/papertake.mp3";

            for (i = 0; i < fNames.Length; i++)
            {
                imgs[i] = new Image();
                imgs[i].Source = new BitmapImage(new Uri(fNames[i]));
            }

            StackPanel stackPnl = new StackPanel();
            i = (int)((Button)sender).Tag;

            if (second_click == true)
            {
                stackPnl.Children.Add(imgs[0]);

                if (i == card1)
                {
                    sound.play(fName);

                    card1 = -1;

                    click++;
                }
                if (i == card2)
                {
                    sound.play(fName);

                    card2 = -1;

                    click++;
                }

                ((Button)sender).Content = stackPnl;
            }
            else
            {
                ((Button)sender).Background = Brushes.FloralWhite;

                for (int j = 0; j < 20; j++)
                {
                    if (gen.getcell(i % width, i / width, width, height) == j + 1)
                        stackPnl.Children.Add(imgs[j + 1]);
                }

                ((Button)sender).Content = stackPnl;
            }

            if (first_click == false)
            {
                sound.play(fName);

                arr[0] = i;
                card1 = i;
                first_click = true;
            }
            else if (second_click == false)
            {
                sound.play(fName);

                arr[1] = i;
                card2 = i;

                if (arr[0] != arr[1])
                {
                    second_click = true;

                    fName = "C:/RGZ(POGI3)/Memo/Resources/twitters.mp3";

                    foreach (Button b in grid.Children)
                    {
                        if (gen.getcell(arr[1] % width, arr[1] / width, width, height) == gen.getcell(arr[0] % width, arr[0] / width, width, height))
                        {
                            click = 2;

                            sound.play(fName);

                            grid.Children[arr[0]].IsEnabled = false;
                            if (b == grid.Children[arr[0]])
                                b.Content = "";

                            grid.Children[arr[1]].IsEnabled = false;
                            if (b == grid.Children[arr[1]])
                                b.Content = "";
                        }
                    }

                    if (gen.getcell(arr[1] % width, arr[1] / width, width, height) == gen.getcell(arr[0] % width, arr[0] / width, width, height))
                    {
                        pl.updatescore(action, score, this);
                    }
                    else
                    {
                        if (action < players.Count - 1)
                            action++;
                        else action = 0;

                        pl.updatecolor(action, this);
                    }
                }
            }

            if (click == 2)
            {
                click = 0;
                first_click = false;
                second_click = false;

                if ((score[0] + score[1] + score[2] + score[3]) == 20)
                {
                    Timer.Stop();
                    quit.Visibility = Visibility.Visible;

                    connect.SetPath(path);

                    SPlayer player = new SPlayer();

                    for (int j = 0; j < players.Count; j++)
                    {
                        player.name = players[j];
                        player.score = score[j];
                        player.time = t;

                        db.addPlayer(player);
                    }

                    connect.SaveData(db);

                    LeaderboardWindow lb = new LeaderboardWindow();
                    lb.ShowDialog();
                }
            }
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
