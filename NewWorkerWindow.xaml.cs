using System.Windows;
using TaskManager.ViewModels;

namespace TaskManager
{
    public partial class NewWorkerWindow : Window
    {
        NewWorkerViewModel viewModel;
        public NewWorkerWindow()
        {
            InitializeComponent();
            viewModel = new NewWorkerViewModel();
            DataContext = viewModel;
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
