using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFtriggers2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<ToDo> TodoList { get; set; } = new ObservableCollection<ToDo>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            TodoList.Add(new ToDo("Приготовить покушать", new DateTime(2024, 1, 15), true, "Нет описания"));
            TodoList.Add(new ToDo("Поработать", new DateTime(2024, 1, 20), false, "Съездить на совещание в Москву"));
            TodoList.Add(new ToDo("Отдохнуть", new DateTime(2024, 2, 1), false, "Съездить в отпуск в Сочи"));

            TaskListListBox.ItemsSource = TodoList;
            UpdateProgress();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var addToDoWindow = new NewTaskWindow();
            addToDoWindow.Owner = this;
            addToDoWindow.ShowDialog();

            UpdateProgress(); 
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var taskToRemove = button.DataContext as ToDo;
            if (taskToRemove != null)
            {
                TodoList.Remove(taskToRemove);
                UpdateProgress(); 
            }
        }

        private void OnCheckboxChecked(object sender, RoutedEventArgs e)
        {
            UpdateProgress(); 
        }

        private void OnCheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            int totalTasks = TodoList.Count;
            int completedTasks = TodoList.Count(x => x.IsCompleted);

            TaskProgressBar.Minimum = 0;
            TaskProgressBar.Maximum = totalTasks;
            TaskProgressBar.Value = completedTasks;

            ProgressText.Text = $"{completedTasks} / {totalTasks}";
        }
    }
}