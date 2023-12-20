using System.Collections.ObjectModel;
using System.Linq;
using TaskManager.Entities;

namespace TaskManager.ViewModels
{
    public class WorkerInfoViewModel : ViewModelBase
    {
        private readonly DbConnection dbConnection;
        private Worker selectedWorker;

        public ObservableCollection<Worker> Workers { get; set; }
        public ObservableCollection<Team> Teams { get; set; }
        public ObservableCollection<Project> TeamProjects { get; set; } // Nová kolekce pro projekty týmu

        public Worker SelectedWorker
        {
            get { return selectedWorker; }
            set
            {
                if (selectedWorker != value)
                {
                    selectedWorker = value;
                    OnPropertyChanged(nameof(SelectedWorker));
                    OnPropertyChanged(nameof(TeamId));
                    OnPropertyChanged(nameof(TeamName));
                    OnPropertyChanged(nameof(SelectedTeamProjects));
                    OnPropertyChanged(nameof(SelectedTasks));
                }
            }
        }


        public int TeamId
        {
            get { return SelectedWorker?.TeamId ?? 0; }
        }

        public string TeamName
        {
            get
            {
                if (SelectedWorker != null)
                {
                    Team team = Teams.FirstOrDefault(t => t.Id == SelectedWorker.TeamId);
                    return team?.Name ?? "Není přiřazen tým";
                }
                return string.Empty;
            }
        }

        public ObservableCollection<Project> SelectedTeamProjects
        {
            get
            {
                if (SelectedWorker != null)
                {
                    return new ObservableCollection<Project>(dbConnection.GetProjects().Where(project => project.TeamId == TeamId));
                }
                return new ObservableCollection<Project>();
            }
        }

        public ObservableCollection<TaskItem> SelectedTasks
        {
            get
            {
                if (SelectedWorker != null)
                {
                    return new ObservableCollection<TaskItem>(dbConnection.GetTasks().Where(task => task.WorkerId == SelectedWorker.Id));
                }
                return new ObservableCollection<TaskItem>();
            }
        }

        public WorkerInfoViewModel()
        {
            dbConnection = new DbConnection();
            Workers = new ObservableCollection<Worker>(dbConnection.GetWorkers());
            Teams = new ObservableCollection<Team>(dbConnection.GetTeams());

            // Defaultně vybereme prvního pracovníka
            SelectedWorker = Workers.Count > 0 ? Workers[0] : null;

        }
    }
}
