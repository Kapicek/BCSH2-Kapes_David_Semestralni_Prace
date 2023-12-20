using System.Windows;
using TaskManager.Entities;
using TaskManager.ViewModels;

namespace TaskManager
{
    public partial class NewTeamWindow : Window
    {
        NewTeamViewModel viewModel;
        public NewTeamWindow(Team editTeam = null)
        {
            InitializeComponent();
            viewModel = new NewTeamViewModel(editTeam);
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
    }
}
