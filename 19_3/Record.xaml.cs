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

namespace _19_3
{
    /// <summary>
    /// Логика взаимодействия для Record.xaml
    /// </summary>
    public partial class Record : Window
    {
        СтудентыEntities db = СтудентыEntities.GetContext();
        int _id = 0;
        bool _isCreate;
        public Record(int id, bool canSave = true)
        {
            InitializeComponent();
            BT_Save.IsEnabled = canSave;
            Студенты student = db.Студенты.Find(id);
            if (student != null)
            {
                _isCreate = false;
                _id = student.НомерЗачётнойКнижки;
                string[] fio = student.ФИО_Студента.Split(' ');
                LastName.Text = fio[0];
                FirstName.Text = fio[1];
                MiddleName.Text = fio[2];
                Dormitory.IsChecked = student.Общежитие;
                cb1.IsChecked = student.Высшая_Математика;
                cb2.IsChecked = student.Разработка_Базы_Данных;
                cb3.IsChecked = student.Компьютерные_Сети;
                cb4.IsChecked = student.Разработка_Программных_Модулей;
                cb5.IsChecked = student.Психология_Общения;
                Group.Text = student.ИндексГруппы;
            }
            else
            {
                _isCreate = true;
                _id = id;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            char[] digits = new char[11] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ' ' };

            StringBuilder sb = new StringBuilder();
            if (!(FirstName.Text != "" && FirstName.Text.IndexOfAny(digits) == -1))
                sb.AppendLine("поле имени не заполнено или содержит цифры");
            if (!(LastName.Text != "" && LastName.Text.IndexOfAny(digits) == -1))
                sb.AppendLine("поле фамилии не заполнено или содержит цифры");
            if (!(MiddleName.Text.IndexOfAny(digits) == -1))
                sb.AppendLine("поле очество содержит цифры");
            if (!(Group.Text != "" && Group.Text.IndexOf(' ') == (-1)))
                sb.AppendLine("поле группы не заполнено");

            if (sb.ToString() != "")
            {
                MessageBox.Show(sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Студенты student = new Студенты();
            student.НомерЗачётнойКнижки = _id;
            student.ФИО_Студента = LastName.Text + " " + FirstName.Text + " " + MiddleName.Text;
            student.Общежитие = (bool)Dormitory.IsChecked;
            student.Высшая_Математика = (bool)cb1.IsChecked;
            student.Разработка_Базы_Данных = (bool)cb2.IsChecked;
            student.Компьютерные_Сети = (bool)cb3.IsChecked;
            student.Разработка_Программных_Модулей = (bool)cb4.IsChecked;
            student.Психология_Общения = (bool)cb5.IsChecked;
            student.ИндексГруппы = Group.Text;

            if (!_isCreate)
                db.Студенты.Remove(db.Студенты.Find(_id));
            db.Студенты.Add(student);
            db.SaveChanges();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
