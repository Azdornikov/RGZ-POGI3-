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
using System.Windows.Shapes;

namespace RGZ_POGI3_
{
    System.Windows.Threading.DispatcherTimer Timer;

    public Start()
    {
        InitializeComponent();

        Timer = new System.Windows.Threading.DispatcherTimer();
        Timer.Tick += new EventHandler(check);
        Timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        Timer.Start();
    }

    private void check(object sender, EventArgs e)
    {
        if ((name1.Text != "") && (name2.Text != ""))
        {
            next.Visibility = Visibility.Visible;
            player3.Visibility = Visibility.Visible;
            name3.Visibility = Visibility.Visible;
        }
        else
        {
            next.Visibility = Visibility.Collapsed;
            player3.Visibility = Visibility.Collapsed;
            name3.Visibility = Visibility.Collapsed;
        }

        if ((name2.Text != "") && (name3.Text != ""))
        {
            player4.Visibility = Visibility.Visible;
            name4.Visibility = Visibility.Visible;
        }
        else
        {
            player4.Visibility = Visibility.Collapsed;
            name4.Visibility = Visibility.Collapsed;
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
    }
}
