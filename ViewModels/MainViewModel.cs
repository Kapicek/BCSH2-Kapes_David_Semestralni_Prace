using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskManager.Entities;

namespace TaskManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private DbConnection dbConnection;
        private Category category;
        private ObservableCollection<object> items;

        public ObservableCollection<object> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public ICommand LoadProjectsCommand => new RelayCommand(LoadProjects);
        public ICommand LoadTaskItemsCommand => new RelayCommand(LoadTaskItems);
        public ICommand LoadTeamsCommand => new RelayCommand(LoadTeams);
        public ICommand LoadWorkersCommand => new RelayCommand(LoadWorkers);
        public ICommand AddNewCommand => new RelayCommand(AddNew);

        public MainViewModel()
        {
            dbConnection = new DbConnection();
            dbConnection.CreateTables();
            LoadWorkers();
        }

        public void LoadProjects()
        {
            Items = new ObservableCollection<object>(dbConnection.GetProjects());
            category = Category.Projects;
        }

        public void LoadTaskItems()
        {
            Items = new ObservableCollection<object>(dbConnection.GetTasks());
            category = Category.TaskItems;
        }

        public void LoadTeams()
        {
            Items = new ObservableCollection<object>(dbConnection.GetTeams());
            category = Category.Teams;
        }

        public void LoadWorkers()
        {
            Items = new ObservableCollection<object>(dbConnection.GetWorkers());
            category = Category.Workers;
        }
        public void AddNew()
        {
            switch (category)
            {
                case Category.Workers:
                    NewWorkerWindow newWorkerWindow = new NewWorkerWindow();
                    newWorkerWindow.ShowDialog();
                    LoadWorkers();
                    break;
                case Category.Projects:
                    NewProjectWindow newProjectWindow = new NewProjectWindow();
                    newProjectWindow.ShowDialog();
                    LoadProjects();
                    break;
                case Category.TaskItems:
                    NewTaskItemWindow newTaskItemWindow = new NewTaskItemWindow();
                    newTaskItemWindow.ShowDialog();
                    LoadTaskItems();
                    break;
                case Category.Teams:
                    NewTeamWindow newTeamWindow = new NewTeamWindow();
                    newTeamWindow.ShowDialog();
                    LoadTeams();
                    break;
            }
        }

        public void Edit()
        {

        }

        public void Delete(object SelectedItem)
        {
            switch (category)
            {
                case Category.Workers:
                    Worker selectedWorker = (Worker)SelectedItem;
                    dbConnection.RemoveWorker(selectedWorker.Id);
                    LoadWorkers();
                    break;
                case Category.Projects:
                    Project selectedProject = (Project)SelectedItem;
                    dbConnection.RemoveProject(selectedProject.Id);
                    LoadProjects();
                    break;
                case Category.TaskItems:
                    TaskItem selectedTaskItem = (TaskItem)SelectedItem;
                    dbConnection.RemoveTask(selectedTaskItem.Id);
                    LoadTaskItems();
                    break;
                case Category.Teams:
                    Team selectedTeam = (Team)SelectedItem;
                    dbConnection.RemoveTeam(selectedTeam.Id);
                    LoadTeams();
                    break;
            }
        }

    }
}
