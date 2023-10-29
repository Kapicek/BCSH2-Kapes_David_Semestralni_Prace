using System.Windows;
using TaskManager.Entities;

namespace TaskManager
{
    public partial class NewProjectWindow : Window
    {
        DbConnection dbConnection;
        Team? selectedTeam;

        public NewProjectWindow()
        {
            InitializeComponent();
            dbConnection = new DbConnection();
            dbConnection.CreateTables();

            TeamComboBox.ItemsSource = dbConnection.GetTeams();
            TeamComboBox.DisplayMemberPath = "Name";

            AddToTeamCheckBox.Checked += AddToTeamCheckBox_Checked;
            AddToTeamCheckBox.Unchecked += AddToTeamCheckBox_Unchecked;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                MessageBox.Show("Please enter a name for the project.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (AddToTeamCheckBox.IsChecked == false)
                {
                    dbConnection.InsertProject(new Project(Name.Text));
                    this.Close();
                }
                else
                {
                    Team selectedTeam = (Team)TeamComboBox.SelectedItem;
                    if (selectedTeam != null)
                    {
                        dbConnection.InsertProject(new Project(Name.Text, selectedTeam.Id));
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select a team to add the project to.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddToTeamCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            selectedTeam = null; // Reset selection before a new one
            TeamComboBox.Visibility = Visibility.Visible;
        }

        private void AddToTeamCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TeamComboBox.Visibility = Visibility.Collapsed;
        }
    }
}
