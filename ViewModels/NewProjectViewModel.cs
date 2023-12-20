using System.Collections.ObjectModel;
using System.Linq;
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

        private Project EditProject;

        public NewProjectViewModel() { }
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

        public NewProjectViewModel(Project editProject)
        {
            dbConnection = new DbConnection();
            dbConnection.CreateTables();

            EditProject = editProject;

            Teams = new ObservableCollection<Team>(dbConnection.GetTeams());

            if (editProject != null)
            {
                projectName = editProject.Name;
                if (EditProject?.TeamId != null) addToTeamChecked = true;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    SelectedTeam = Teams.FirstOrDefault(team => team.Id == EditProject.TeamId);
                });
            }
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
                int projectId;
                if (EditProject == null)
                {
                    projectId = dbConnection.InsertProject(new Project(ProjectName));
                }
                else
                {
                    EditProject.Name = ProjectName;
                    dbConnection.UpdateProject(EditProject);
                    projectId = EditProject.Id;
                }

                if (AddToTeamChecked)
                {
                    if (SelectedTeam != null)
                    {
                        if (EditProject != null)
                        {
                            dbConnection.RemoveProjectFromTeam(projectId);
                        }
                        dbConnection.AddProjectToTeam(projectId, SelectedTeam.Id);
                    }
                    else
                    {
                        MessageBox.Show("Please select a team to add the project to.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
