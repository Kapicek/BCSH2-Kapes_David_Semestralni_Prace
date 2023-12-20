using System.Collections.ObjectModel;
using System.Linq;
using TaskManager.Entities;

namespace TaskManager.ViewModels
{
    public class AssignWorkerViewModel : ViewModelBase
    {
        private readonly DbConnection dbConnection;
        private Worker selectedWorker;
        private TaskItem selectedTaskItem;
        private bool isTaskComboBoxEnabled;

        public ObservableCollection<Worker> Workers { get; set; }
        public ObservableCollection<TaskItem> TaskItems { get; set; }

        public AssignWorkerViewModel()
        {
            dbConnection = new DbConnection();

            Workers = new ObservableCollection<Worker>(dbConnection.GetWorkers());
            TaskItems = new ObservableCollection<TaskItem>(dbConnection.GetTasks());

            IsTaskComboBoxEnabled = false;
        }

        public Worker SelectedWorker
        {
            get { return selectedWorker; }
            set
            {
                selectedWorker = value;
                TaskItems = FilterTaskItems();
                OnPropertyChanged(nameof(SelectedWorker));
            }
        }

        public TaskItem SelectedTaskItem
        {
            get { return selectedTaskItem; }
            set
            {
                selectedTaskItem = value;
                OnPropertyChanged(nameof(SelectedTaskItem));
            }
        }

        public bool IsTaskComboBoxEnabled
        {
            get { return isTaskComboBoxEnabled; }
            set
            {
                isTaskComboBoxEnabled = value;
                OnPropertyChanged(nameof(IsTaskComboBoxEnabled));
            }
        }

        public ObservableCollection<TaskItem> FilterTaskItems()
        {
            ObservableCollection<TaskItem> Items = null;
            if (selectedWorker != null)
            {
                int teamId = selectedWorker.TeamId;

                // Získáme seznam projektů pro daný tým
                var projectsForTeam = dbConnection.GetProjects().Where(project => project.TeamId == teamId).ToList();

                // Filtrujeme úkoly tak, aby byly přiřazeny projektům, které jsou přiřazeny týmu pracovníka
                Items = new ObservableCollection<TaskItem>(dbConnection.GetTasks().Where(task => projectsForTeam.Any(project => project.Id == task.ProjectId)));

                // Nastavíme stav ComboBoxu na základě počtu nalezených úkolů
                IsTaskComboBoxEnabled = TaskItems.Count > 0;
            }
            return Items;
        }

        public bool OK()
        {
            if (selectedWorker != null && selectedTaskItem != null)
            {
                dbConnection.RemoveWorkerFromTask(selectedTaskItem.Id);
                dbConnection.AddTaskToWorker(selectedTaskItem.Id, selectedWorker.Id);
                return true;
            }
            else { return false; }
        }
    }
}
