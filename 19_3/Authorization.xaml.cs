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
using System.Data.Entity;

namespace _19_3
{
    public partial class Authorization : Window
    { 
    СтудентыEntities db = СтудентыEntities.GetContext();

    public Authorization()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        db.Авторизация.Load();
    }

    private void AuthAttempt(object sender, RoutedEventArgs e)
    {
        var user = from p in db.Авторизация
                   where tbLoginInput.Text == p.Логин && pbPassInput.Password == p.Пароль
                   select p;
        if (user.Count() == 1)
        {
            MainWindow mainWindow = new MainWindow(user.ToList()[0].Доступ);
            this.Close();
            mainWindow.Show();
        }
        else
        {
            MessageBox.Show("Я такую рожу не знаю", "@ФЕЙСКОНТРОЛЬ", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
        }
    }

    private void Exit(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
}

