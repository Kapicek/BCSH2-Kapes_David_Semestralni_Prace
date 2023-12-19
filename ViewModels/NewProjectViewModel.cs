using System.Collections.ObjectModel;
using System.Windows;
using TaskManager.Entities;

namespace TaskManager.ViewModels
{
    public class NewProjectViewModel : ViewModelBase
    {
        private readonly DbConnection dbConnection;
        private string projectName;
        private bool addToTeamChecked;
        private Team selectedTeam;

        public ObservableCollection<Team> Teams { get; set; }
        //public ICommand OKCommand => new RelayCommand(OK);

        public string ProjectName
        {
            get => projectName;
            set
            {
                projectName = value;
                OnPropertyChanged(nameof(ProjectName));
            }
        }

        public bool AddToTeamChecked
        {
            get => addToTeamChecked;
            set
            {
                addToTeamChecked = value;
                OnPropertyChanged(nameof(AddToTeamChecked));
            }
        }

        public Team SelectedTeam
        {
            get => selectedTeam;
            set
            {
                selectedTeam = value;
                OnPropertyChanged(nameof(SelectedTeam));
            }
        }

        public NewProjectViewModel()
        {
            dbConnection = new DbConnection();
            dbConnection.CreateTables();

            Teams = new ObservableCollection<Team>(dbConnection.GetTeams());
        }

        public bool OK()
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                MessageBox.Show("Please enter a name for the project.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else
            {
                int projectId = dbConnection.InsertProject(new Project(ProjectName));

                if (AddToTeamChecked)
                {
                    if (SelectedTeam != null)
                    {
                        dbConnection.AddProjectToTeam(projectId, SelectedTeam.Id);
                    }
                    else
                    {
                        MessageBox.Show("Please select a team to add the project to.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return false;
                    }
                }

                MessageBox.Show("Project added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
        }
    }
}
