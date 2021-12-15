using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RGZ_POGI3_
{
    public class CPLayers
    {
        //string color = "";

        public CPLayers()
        {

        }

        public void createplayers(List<string> players, int[] arr, Start w1, GameWindow w2) // создание игроков, определяет по количеству введеных имен
        {
            if (w1.name1.Text != "")
            {
                players[0] = w2.player1.Text = w1.name1.Text;
                w2.score1.Content = 0;
            }

            if (w1.name2.Text != "")
            {
                players[1] = w2.player2.Text = w1.name2.Text;
                w2.score2.Content = 0;
            }

            if (w1.name3.Text != "")
            {
                players[2] = w2.player3.Text = w1.name3.Text;
                w2.score3.Content = 0;
            }
            else
            {
                players.RemoveAt(players.Count - 1);
            }

            if (w1.name4.Text != "")
            {
                players[3] = w2.player4.Text = w1.name4.Text;
                w2.score4.Content = 0;
            }
            else
            {
                players.RemoveAt(players.Count - 1);
            }

            for (int i = 0; i < players.Count; i++)
            {
                arr[i] = 0;
            }
        }

        public void updatescore(int action, int[] arr, GameWindow w) //обновляет рекорд, определяя чей ход
        {
            if (action == 0)
            {
                arr[0]++;
                w.score1.Content = arr[0];
            }
            else if (action == 1)
            {
                arr[1]++;
                w.score2.Content = arr[1];
            }
            else if (action == 2)
            {
                arr[2]++;
                w.score3.Content = arr[2];
            }
            else if (action == 3)
            {
                arr[3]++;
                w.score4.Content = arr[3];
            }
        }

        public void updatecolor(int action, GameWindow w) // обновляет цвет, определяя чей ход
        {
            if (action == 0)
            {
                w.player1.Foreground = new SolidColorBrush(Colors.Red);
                w.score1.Foreground = new SolidColorBrush(Colors.Red);
                w.player2.Foreground = new SolidColorBrush(Colors.Black);
                w.score2.Foreground = new SolidColorBrush(Colors.Black);
                w.player3.Foreground = new SolidColorBrush(Colors.Black);
                w.score3.Foreground = new SolidColorBrush(Colors.Black);
                w.player4.Foreground = new SolidColorBrush(Colors.Black);
                w.score4.Foreground = new SolidColorBrush(Colors.Black);
                //color = "Red";
            }
            else if (action == 1)
            {
                w.player1.Foreground = new SolidColorBrush(Colors.Black);
                w.score1.Foreground = new SolidColorBrush(Colors.Black);
                w.player2.Foreground = new SolidColorBrush(Colors.Goldenrod);
                w.score2.Foreground = new SolidColorBrush(Colors.Goldenrod);
                w.player3.Foreground = new SolidColorBrush(Colors.Black);
                w.score3.Foreground = new SolidColorBrush(Colors.Black);
                w.player4.Foreground = new SolidColorBrush(Colors.Black);
                w.score4.Foreground = new SolidColorBrush(Colors.Black);
                //color = "Goldenrod";
            }
            else if (action == 2)
            {
                w.player1.Foreground = new SolidColorBrush(Colors.Black);
                w.score1.Foreground = new SolidColorBrush(Colors.Black);
                w.player2.Foreground = new SolidColorBrush(Colors.Black);
                w.score2.Foreground = new SolidColorBrush(Colors.Black);
                w.player3.Foreground = new SolidColorBrush(Colors.LimeGreen);
                w.score3.Foreground = new SolidColorBrush(Colors.LimeGreen);
                w.player4.Foreground = new SolidColorBrush(Colors.Black);
                w.score4.Foreground = new SolidColorBrush(Colors.Black);
                //color = "LimeGreen";
            }
            else if (action == 3)
            {
                w.player1.Foreground = new SolidColorBrush(Colors.Black);
                w.score1.Foreground = new SolidColorBrush(Colors.Black);
                w.player2.Foreground = new SolidColorBrush(Colors.Black);
                w.score2.Foreground = new SolidColorBrush(Colors.Black);
                w.player3.Foreground = new SolidColorBrush(Colors.Black);
                w.score3.Foreground = new SolidColorBrush(Colors.Black);
                w.player4.Foreground = new SolidColorBrush(Colors.DodgerBlue);
                w.score4.Foreground = new SolidColorBrush(Colors.DodgerBlue);
                //color = "DodgerBlue";
            }

            //return color;
        }
    }
}
