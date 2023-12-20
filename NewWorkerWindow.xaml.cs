using System.Windows;
using TaskManager.Entities;
using TaskManager.ViewModels;

namespace TaskManager
{
    public partial class NewWorkerWindow : Window
    {
        NewWorkerViewModel viewModel;
        public NewWorkerWindow(Worker editWorker = null)
        {
            InitializeComponent();
            viewModel = new NewWorkerViewModel(editWorker);
            DataContext = viewModel;
            if (editWorker != null && editWorker.TeamId != 0)
            {
                AddToTeamCheckBox.Visibility = Visibility.Collapsed;
            }
        }


        private void OK_Click(object sender, RoutedEventArgs e)
        {
            bool result = viewModel.OK();
            if (result)
            {
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddToWorkerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            TeamComboBox.Visibility = Visibility.Visible;
        }

        private void AddToWorkerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TeamComboBox.Visibility = Visibility.Collapsed;
        }

    }
}
