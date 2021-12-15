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

            quit.Visibility = Visibility.Collapsed; //невидимость кнопки выход

            Start start = new Start();
            if (start.ShowDialog() == true)
            {
                pl.createplayers(players, score, start, this); //ссылка на класс CPlayer и передача данных в него, для создания игрового поля
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

            fNames = Directory.GetFiles("C:/RGZ(POGI3)/Cards/"); //загрузка карт из папки
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

        string path = "Data Source=C:\\RGZ(POGI3)\\test.db;Version=3;"; // путь к базе данных

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fName = "C:/RGZ(POGI3)/Resources/papertake.mp3"; // путь к звуку

            for (i = 0; i < fNames.Length; i++)
            {
                imgs[i] = new Image();
                imgs[i].Source = new BitmapImage(new Uri(fNames[i]));
            }

            StackPanel stackPnl = new StackPanel();
            i = (int)((Button)sender).Tag; //индекс открытой карточки

            if (second_click == true) // если были открыты две карточки
            {
                stackPnl.Children.Add(imgs[0]);

                if (i == card1) //закрытие первой карточки
                {
                    sound.play(fName);

                    card1 = -1;

                    click++;
                }
                if (i == card2) // закрытие второй карточки
                {
                    sound.play(fName);

                    card2 = -1;

                    click++;
                }

                ((Button)sender).Content = stackPnl; // отрисовка рубашки
            }
            else
            {
                ((Button)sender).Background = Brushes.FloralWhite;

                for (int j = 0; j < 20; j++) // по индексу определяет какая карточка открыта и рисует её
                {
                    if (gen.getcell(i % width, i / width, width, height) == j + 1)
                        stackPnl.Children.Add(imgs[j + 1]);
                }

                ((Button)sender).Content = stackPnl; // отрисовка карточки
            }

            if (first_click == false) // если ни одна карточка не открыта
            {
                sound.play(fName);

                arr[0] = i;
                card1 = i; //запоминает открытую карточку
                first_click = true;
            }
            else if (second_click == false) // если вторая карточка не открыта
            {
                sound.play(fName);

                arr[1] = i;
                card2 = i; // запоминает вторую карточку

                if (arr[0] != arr[1]) // проверяет нажаты ли разные карточки или одни и те же
                {
                    second_click = true; // если не нажаты, запоминает, что открыты две карточки

                    fName = "C:/RGZ(POGI3)/Resources/twitters.mp3";

                    foreach (Button b in grid.Children) // перебирает поле
                    {
                        if (gen.getcell(arr[1] % width, arr[1] / width, width, height) == gen.getcell(arr[0] % width, arr[0] / width, width, height))
                        {
                            click = 2;

                            sound.play(fName);

                            grid.Children[arr[0]].IsEnabled = false; // то убирает первую карту
                            if (b == grid.Children[arr[0]])
                                b.Content = "";

                            grid.Children[arr[1]].IsEnabled = false; // и вторую
                            if (b == grid.Children[arr[1]])
                                b.Content = "";
                        }
                    }

                    if (gen.getcell(arr[1] % width, arr[1] / width, width, height) == gen.getcell(arr[0] % width, arr[0] / width, width, height)) // если одинаковые
                    {
                        pl.updatescore(action, score, this); // то обновляет рекорд у игрока
                    }
                    else
                    {
                        if (action < players.Count - 1)
                            action++;
                        else action = 0;

                        pl.updatecolor(action, this); // если не одинаковые, то меняет ход и цвет игрока
                    }
                }
            }

            if (click == 2) //если обе карты закрыты
            {
                //обнуляет все значения кликов
                click = 0;
                first_click = false;
                second_click = false;

                if ((score[0] + score[1] + score[2] + score[3]) == 20) // проверяет все ли карточки собраны, если да, то
                {
                    Timer.Stop(); //останавливает время
                    quit.Visibility = Visibility.Visible; // делает кнопку "выход" видимой

                    connect.SetPath(path); // подключается к базе данных

                    SPlayer player = new SPlayer();

                    for (int j = 0; j < players.Count; j++)
                    {
                        player.name = players[j];
                        player.score = score[j];
                        player.time = t;

                        db.addPlayer(player); // добавляет игрока в базу данных
                    }

                    connect.SaveData(db); //сохраняет изменения в базе данных

                    LeaderboardWindow lb = new LeaderboardWindow(); // открывает окно лидеров
                    lb.ShowDialog();
                }
            }
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // при нажатии кнопки "Выход" закрывает окно игры
        }
    }
}
