using System.Windows;
using TaskManager.Entities;

namespace TaskManager.ViewModels
{
    public class NewTeamViewModel : ViewModelBase
    {
        private readonly DbConnection dbConnection;
        private string teamName;

        public string TeamName
        {
            get => teamName;
            set
            {
                teamName = value;
                OnPropertyChanged(nameof(TeamName));
            }
        }

        public NewTeamViewModel()
        {
            dbConnection = new DbConnection();
            dbConnection.CreateTables();
        }

        public bool OK()
        {
            if (string.IsNullOrWhiteSpace(TeamName))
            {
                MessageBox.Show("Prosím doplňte název týmu", "Chybějící údaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else
            {
                dbConnection.InsertTeam(new Team(TeamName));
                return true;
            }
        }
    }
}
