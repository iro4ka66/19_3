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
using System.Data.Entity;
using System.Windows.Shapes;

namespace _19_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        СтудентыEntities db = СтудентыEntities.GetContext();
        public MainWindow(string dostup)
        {
            InitializeComponent();
            if (dostup != "admin")
            {
                Add.IsEnabled = false;
                Change.IsEnabled = false;
                Del.IsEnabled = false;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Студенты.Load();
            dg.ItemsSource = db.Студенты.Local.ToBindingList();
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = db.Студенты.ToList();
                int id = list[list.Count - 1].НомерЗачётнойКнижки + 1;
                Window window = new Record(id);
                window.ShowDialog();
                dg.ItemsSource = db.Студенты.ToList();
            }
            catch
            {
                Window window = new Record(1);
                window.ShowDialog();
                dg.ItemsSource = db.Студенты.ToList();
            }
        }

        private void Click_Сhange(object sender, RoutedEventArgs e)
        {
            Студенты Сhange = (Студенты)dg.SelectedItem;
            if (Сhange == null)
            {
                MessageBox.Show("что ты собрался изменять?", "ВОПРОСИКИ", MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }
            Window window = new Record(Сhange.НомерЗачётнойКнижки);
            window.ShowDialog();
            dg.ItemsSource = db.Студенты.ToList();
        }

        private void Click_Del(object sender, RoutedEventArgs e)
        {
            try
            {
                Студенты del = (Студенты)dg.SelectedItem;
                MessageBoxResult messageBoxResult = MessageBox.Show("вы уверены что хотите удалить запись", "предупрегдение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                    db.Студенты.Remove(del);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("нет выделенной записи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(ex.Message);
            }
        }
        private void Click_Show(object sender, RoutedEventArgs e)
        {
            Студенты show = (Студенты)dg.SelectedItem;
            if (show == null)
            {
                MessageBox.Show("что ты собрался просматривать?", "ВОПРОСИКИ", MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }
            Window window = new Record(show.НомерЗачётнойКнижки, false);
            window.ShowDialog();
            dg.ItemsSource = db.Студенты.ToList();
        }
        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void isp_31(object sender, RoutedEventArgs e)
        {
            var selection = from p in db.Студенты
                            where p.ИндексГруппы.ToLower() == "исп-31"
                            select p;
            dg.ItemsSource = selection.ToList();
        }

        private void isp_32(object sender, RoutedEventArgs e)
        {
            var selection = from p in db.Студенты
                            where p.ИндексГруппы.ToLower() == "исп-32"
                            select p;
            dg.ItemsSource = selection.ToList();
        }

        private void liveIn(object sender, RoutedEventArgs e)
        {
            var selection = from p in db.Студенты
                            where p.Общежитие
                            select p;
            dg.ItemsSource = selection.ToList();
        }

        private void mathematic(object sender, RoutedEventArgs e)
        {
            var selection = from p in db.Студенты
                            where p.Высшая_Математика
                            select p;
            dg.ItemsSource = selection.ToList();
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            var selection = from p in db.Студенты
                            select p;
            dg.ItemsSource = selection.ToList();
        }
    }
}
