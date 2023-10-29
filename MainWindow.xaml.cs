using System.Windows;
using TaskManager.Entities;

namespace TaskManager
{
    public partial class MainWindow : Window
    {
        DbConnection dbConnection;
        Category category;
        public MainWindow()
        {
            InitializeComponent();
            dbConnection = new DbConnection();
            dbConnection.CreateTables();

            dataGrid.ItemsSource = dbConnection.GetWorkers();
            category = Category.Workers;
        }

        private void LoadProjects(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbConnection.GetProjects();
            category = Category.Projects;
        }

        private void LoadTaskItems(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbConnection.GetTasks();
            category = Category.TaskItems;
        }

        private void LoadTeams(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbConnection.GetTeams();
            category = Category.Teams;
        }

        private void LoadWorkers(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbConnection.GetWorkers();
            category = Category.Workers;
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            switch (category)
            {
                case Category.Workers:
                    NewWorkerWindow newWorkerWindow = new NewWorkerWindow();
                    newWorkerWindow.ShowDialog();
                    dataGrid.ItemsSource = dbConnection.GetWorkers();
                    break;
                case Category.Projects:
                    NewProjectWindow newProjectWindow = new NewProjectWindow();
                    newProjectWindow.ShowDialog();
                    dataGrid.ItemsSource = dbConnection.GetProjects();
                    break;
                case Category.TaskItems:
                    NewTaskItemWindow newTaskItemWindow = new NewTaskItemWindow();
                    newTaskItemWindow.ShowDialog();
                    dataGrid.ItemsSource = dbConnection.GetTasks();
                    break;
                case Category.Teams:
                    NewTeamWindow newTeamWindow = new NewTeamWindow();
                    newTeamWindow.ShowDialog();
                    dataGrid.ItemsSource = dbConnection.GetTeams();
                    break;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch (category)
            {
                case Category.Workers:
                    break;
                case Category.Projects:
                    break;
                case Category.TaskItems:
                    break;
                case Category.Teams:
                    break;
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch (category)
            {
                case Category.Workers:
                    Worker selectedWorker = (Worker)dataGrid.SelectedItem;
                    dbConnection.RemoveWorker(selectedWorker.Id);
                    dataGrid.ItemsSource = dbConnection.GetWorkers();
                    break;
                case Category.Projects:
                    Project selectedProject = (Project)dataGrid.SelectedItem;
                    dbConnection.RemoveProject(selectedProject.Id);
                    dataGrid.ItemsSource = dbConnection.GetProjects();
                    break;
                case Category.TaskItems:
                    TaskItem selectedTaskItem = (TaskItem)dataGrid.SelectedItem;
                    dbConnection.RemoveTask(selectedTaskItem.Id);
                    dataGrid.ItemsSource = dbConnection.GetTasks();
                    break;
                case Category.Teams:
                    Team selectedTeam = (Team)dataGrid.SelectedItem;
                    dbConnection.RemoveTeam(selectedTeam.Id);
                    dataGrid.ItemsSource = dbConnection.GetTeams();
                    break;
            }

        }

    }

    public enum Category
    {
        Projects,
        TaskItems,
        Teams,
        Workers
    }
}
