using System.Windows;
using TaskManager.Entities;

namespace TaskManager.ViewModels
{
    public class NewTeamViewModel : ViewModelBase
    {
        private readonly DbConnection dbConnection;
        private string teamName;
        private Team EditTeam { get; set; }
        public NewTeamViewModel() { }

        public string TeamName
        {
            get => teamName;
            set
            {
                teamName = value;
                OnPropertyChanged(nameof(TeamName));
            }
        }

        public NewTeamViewModel(Team editTeam)
        {
            dbConnection = new DbConnection();
            dbConnection.CreateTables();
            EditTeam = editTeam;
            if (EditTeam != null)
            {
                teamName = EditTeam.Name;
            }
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
                if (EditTeam != null)
                {
                    EditTeam.Name = teamName;
                    dbConnection.UpdateTeam(EditTeam);
                }
                else
                {
                    dbConnection.InsertTeam(new Team(TeamName));
                }
                return true;
            }
        }
    }
}
