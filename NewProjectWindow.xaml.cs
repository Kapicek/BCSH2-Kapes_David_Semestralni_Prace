using System.Windows;
using TaskManager.Entities;
using TaskManager.ViewModels;

namespace TaskManager
{
    public partial class NewProjectWindow : Window
    {
        NewProjectViewModel viewModel;
        public NewProjectWindow(Project editProject = null)
        {
            InitializeComponent();
            viewModel = new NewProjectViewModel(editProject);
            DataContext = viewModel;

            //if (editProject != null && editProject.TeamId != 0)
            //{
            //    AddToTeamCheckBox.Visibility = Visibility.Collapsed;
            //}
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
