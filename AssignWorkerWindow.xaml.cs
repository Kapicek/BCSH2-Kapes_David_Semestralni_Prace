using System.Windows;
using TaskManager.ViewModels;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for AssignWorkerWindow.xaml
    /// </summary>
    public partial class AssignWorkerWindow : Window
    {
        AssignWorkerViewModel viewModel;
        public AssignWorkerWindow()
        {
            InitializeComponent();
            viewModel = new AssignWorkerViewModel();
            DataContext = viewModel;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WorkerComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TaskComboBox.ItemsSource = viewModel.FilterTaskItems();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.OK())
            {
                this.Close();
            }
        }
    }
}
