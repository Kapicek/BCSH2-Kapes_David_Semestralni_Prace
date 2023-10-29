using System.Windows;
using TaskManager.Entities;

namespace TaskManager
{
    public partial class NewWorkerWindow : Window
    {
        DbConnection dbConnection;
        Team? selectedTeam;
        public NewWorkerWindow()
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
            if (string.IsNullOrWhiteSpace(FirstName.Text) || string.IsNullOrWhiteSpace(LastName.Text))
            {
                MessageBox.Show("Prosím, vyplňte jméno a příjmení.", "Chybějící údaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (AddToTeamCheckBox.IsChecked == false)
                {
                    dbConnection.InsertWorker(new Worker(FirstName.Text, LastName.Text));
                    this.Close();
                }
                else
                {
                    Team selectedTeam = (Team)TeamComboBox.SelectedItem;
                    if (selectedTeam != null)
                    {
                        dbConnection.InsertWorker(new Worker(FirstName.Text, LastName.Text, selectedTeam.Id));
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Prosím, vyberte si kam chcete pracovníka přiřadit", "Chybějící údaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            selectedTeam = null; // Nulování před novým výběrem
            TeamComboBox.Visibility = Visibility.Visible;
        }

        private void AddToTeamCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TeamComboBox.Visibility = Visibility.Collapsed;
        }
    }
}
