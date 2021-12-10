using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RGZ_POGI3_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            //sound.repeat(fName);
        }

        private void tutorial_Click(object sender, RoutedEventArgs e)
        {
            TutorialWindow tutorial = new TutorialWindow();
            tutorial.ShowDialog();
        }

        private void newgame_Click(object sender, RoutedEventArgs e)
        {
            GameWindow ng = new GameWindow();
            ng.ShowDialog();
        }

        private void leaderboard_Click(object sender, RoutedEventArgs e)
        {
            LeaderboardWindow lb = new LeaderboardWindow();
            lb.ShowDialog();
        }

        private void options_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow options = new OptionsWindow();
            options.ShowDialog();
        }

        private void quitgame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
