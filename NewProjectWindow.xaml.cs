using System.Windows;
using TaskManager.ViewModels;

namespace TaskManager
{
    public partial class NewProjectWindow : Window
    {
        NewProjectViewModel viewModel;
        public NewProjectWindow()
        {
            InitializeComponent();
            viewModel = new NewProjectViewModel();
            DataContext = viewModel;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            bool result = viewModel.OK();
            if (result)
            {
                this.Close();
            }
        }
    }
}
