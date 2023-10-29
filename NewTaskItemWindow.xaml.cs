using System.Windows;
using TaskManager.Entities;

namespace TaskManager
{
    public partial class NewTaskItemWindow : Window
    {
        DbConnection dbConnection;
        public NewTaskItemWindow()
        {
            InitializeComponent();
            dbConnection = new DbConnection();
            dbConnection.CreateTables();

            ProjectComboBox.ItemsSource = dbConnection.GetProjects();
            ProjectComboBox.DisplayMemberPath = "Name";

            AddToProjectCheckBox.Checked += AddToProjectCheckBox_Checked;
            AddToProjectCheckBox.Unchecked += AddToProjectCheckBox_Unchecked;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Description.Text) ||
                string.IsNullOrWhiteSpace(Priority.Text) || string.IsNullOrWhiteSpace(Status.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (AddToProjectCheckBox.IsChecked == false)
                {
                    dbConnection.InsertTaskItem(new TaskItem(Name.Text, Description.Text, Priority.Text, Status.Text));
                    this.Close();
                }
                else
                {
                    Project selectedProject = (Project)ProjectComboBox.SelectedItem;
                    if (selectedProject != null)
                    {
                        int id = dbConnection.InsertTaskItem(new TaskItem(Name.Text, Description.Text, Priority.Text, Status.Text));
                        dbConnection.AddTaskToProject(id, selectedProject.Id);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select a project to associate the task with.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddToProjectCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ProjectComboBox.Visibility = Visibility.Visible;
        }

        private void AddToProjectCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ProjectComboBox.Visibility = Visibility.Collapsed;
        }
    }
}
