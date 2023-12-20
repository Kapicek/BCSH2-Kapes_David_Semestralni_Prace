using System.Collections.ObjectModel;
using System.Windows;
using TaskManager.Entities;

namespace TaskManager.ViewModels
{
    public class NewTaskItemViewModel : ViewModelBase
    {
        private readonly DbConnection dbConnection;
        private string name;
        private string description;
        private string priority;
        private string status;
        private bool addToProjectChecked;
        private Project selectedProject;

        private TaskItem EditTaskItem { get; set; }

        public NewTaskItemViewModel() { }

        public ObservableCollection<Project> Projects { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Priority
        {
            get => priority;
            set
            {
                priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public bool AddToProjectChecked
        {
            get => addToProjectChecked;
            set
            {
                addToProjectChecked = value;
                OnPropertyChanged(nameof(AddToProjectChecked));
            }
        }

        public Project SelectedProject
        {
            get => selectedProject;
            set
            {
                selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
            }
        }

        public NewTaskItemViewModel(TaskItem editTaskItem)
        {
            dbConnection = new DbConnection();
            dbConnection.CreateTables();

            Projects = new ObservableCollection<Project>(dbConnection.GetProjects());

            EditTaskItem = editTaskItem;

            if (EditTaskItem != null)
            {
                name = editTaskItem.Name;
                description = editTaskItem.Description;
                priority = editTaskItem.Priority;
                status = editTaskItem.Status;
            }
        }

        public bool OK()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(Priority) || string.IsNullOrWhiteSpace(Status))
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else
            {
                int taskItemId;
                if (EditTaskItem == null)
                {
                    taskItemId = dbConnection.InsertTaskItem(new TaskItem(Name, Description, Priority, Status));
                }
                else
                {
                    EditTaskItem.Name = Name;
                    EditTaskItem.Description = Description;
                    EditTaskItem.Priority = Priority;
                    EditTaskItem.Status = Status;
                    dbConnection.UpdateTaskItem(EditTaskItem);
                    taskItemId = EditTaskItem.Id;
                }
                if (addToProjectChecked)
                {
                    if (selectedProject != null)
                    {
                        dbConnection.AddTaskToProject(taskItemId, selectedProject.Id);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Please select a project to associate the task with.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            return false;
        }
    }
}
