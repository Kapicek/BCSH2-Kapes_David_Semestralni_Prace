using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskManager.Entities;

namespace TaskManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private DbConnection dbConnection;
        private Category category;
        public string categoryName = "Žádné";
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
        public ICommand AssignTask => new RelayCommand(AssignTaskToWorker);
        public MainViewModel()
        {
            dbConnection = new DbConnection();
            dbConnection.CreateTables();
            LoadWorkers();
        }

        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        public void AssignTaskToWorker() { }
        public void LoadProjects()
        {
            Items = new ObservableCollection<object>(dbConnection.GetProjects());
            category = Category.Projects;
            CategoryName = "Projekty";
        }

        public void LoadTaskItems()
        {
            Items = new ObservableCollection<object>(dbConnection.GetTasks());
            category = Category.TaskItems;
            CategoryName = "Úkoly";
        }

        public void LoadTeams()
        {
            Items = new ObservableCollection<object>(dbConnection.GetTeams());
            category = Category.Teams;
            CategoryName = "Týmy";
        }

        public void LoadWorkers()
        {
            Items = new ObservableCollection<object>(dbConnection.GetWorkers());
            category = Category.Workers;
            CategoryName = "Pracovníci";
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

        public void Edit(object SelectedItem)
        {
            if (SelectedItem != null)
            {
                switch (category)
                {
                    case Category.Workers:
                        Worker selectedWorker = (Worker)SelectedItem;
                        NewWorkerWindow newWorkerWindow = new NewWorkerWindow(selectedWorker);
                        newWorkerWindow.ShowDialog();
                        LoadWorkers();
                        break;
                    case Category.Projects:
                        Project selectedProject = (Project)SelectedItem;
                        NewProjectWindow newProjectWindow = new NewProjectWindow(selectedProject);
                        newProjectWindow.ShowDialog();
                        LoadProjects();
                        break;
                    case Category.TaskItems:
                        TaskItem selectedTaskItem = (TaskItem)SelectedItem;
                        NewTaskItemWindow newTaskItemWindow = new NewTaskItemWindow(selectedTaskItem);
                        newTaskItemWindow.ShowDialog();
                        LoadTaskItems();
                        break;
                    case Category.Teams:
                        Team selectedTeam = (Team)SelectedItem;
                        NewTeamWindow newTeamWindow = new NewTeamWindow(selectedTeam);
                        newTeamWindow.ShowDialog();
                        LoadTeams();
                        break;
                }
            }
        }

        public void Delete(object SelectedItem)
        {
            if (SelectedItem != null)
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
}
