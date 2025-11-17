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

namespace WPFtriggers2
{
    /// <summary>
    /// Логика взаимодействия для NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        private static Random random = new Random();

        private DateTime GenerateRandomDate(DateTime start, DateTime end)
        {
            TimeSpan range = end - start;
            double randTicks = random.NextDouble() * range.TotalDays;
            return start + TimeSpan.FromDays(randTicks);
        }
        public NewTaskWindow()
        {
            InitializeComponent();
            DateTime minDate = new DateTime(2023, 1, 1); 
            DateTime maxDate = new DateTime(2025, 12, 31); 
            DueDatePicker.SelectedDate = GenerateRandomDate(minDate, maxDate);
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text.Trim();
            DateTime? dueDate = DueDatePicker.SelectedDate;
            string description = DescriptionTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Нужно ввести название задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ToDo newItem = new ToDo(title, dueDate.GetValueOrDefault(), false, description);
            MainWindow.TodoList.Add(newItem);

            Close();
        }
    }    
}

