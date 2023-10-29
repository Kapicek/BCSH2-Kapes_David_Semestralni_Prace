using System.Windows;
using TaskManager.Entities;

namespace TaskManager
{
    public partial class NewTeamWindow : Window
    {
        DbConnection dbConnection;
        public NewTeamWindow()
        {
            InitializeComponent();
            dbConnection = new DbConnection();
            dbConnection.CreateTables();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TeamName.Text))
            {
                MessageBox.Show("Prosím doplňte název týmu", "Chybějící údaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                dbConnection.InsertTeam(new Team(TeamName.Text));
            }
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
