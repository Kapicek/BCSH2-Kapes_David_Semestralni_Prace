using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TaskManager.Entities;

namespace TaskManager.ViewModels
{
    public class NewWorkerViewModel : ViewModelBase
    {
        private readonly DbConnection dbConnection;
        private string firstName;
        private string lastName;
        private bool addToTeamChecked;
        private Team selectedTeam;

        public Worker EditWorker { get; set; }
        public ObservableCollection<Team> Teams { get; set; }

        public string FirstName { get => firstName; set { firstName = value; OnPropertyChanged(nameof(FirstName)); } }
        public string LastName { get => lastName; set { lastName = value; OnPropertyChanged(nameof(LastName)); } }
        public bool AddToTeamChecked { get => addToTeamChecked; set { addToTeamChecked = value; OnPropertyChanged(nameof(AddToTeamChecked)); if (!addToTeamChecked) SelectedTeam = null; } }
        public Team SelectedTeam { get => selectedTeam; set { selectedTeam = value; OnPropertyChanged(nameof(SelectedTeam)); } }

        public NewWorkerViewModel()
        {
            dbConnection = new DbConnection();
            dbConnection.CreateTables();
            Teams = new ObservableCollection<Team>(dbConnection.GetTeams());
        }

        public NewWorkerViewModel(Worker editWorker) : this()
        {
            EditWorker = editWorker;
            if (EditWorker != null)
            {
                FirstName = EditWorker.FirstName;
                LastName = EditWorker.LastName;
                if (EditWorker?.TeamId != null) AddToTeamChecked = true;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    SelectedTeam = Teams.FirstOrDefault(team => team.Id == EditWorker.TeamId);
                });
            }
        }

        public bool OK()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                MessageBox.Show("Prosím, vyplňte jméno a příjmení.", "Chybějící údaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else
            {
                int workerId;
                if (EditWorker == null)
                {
                    workerId = dbConnection.InsertWorker(new Worker(FirstName, LastName));
                }
                else
                {
                    EditWorker.FirstName = FirstName;
                    EditWorker.LastName = LastName;
                    dbConnection.UpdateWorker(EditWorker);
                    workerId = EditWorker.Id;
                }

                if (AddToTeamChecked)
                {
                    if (SelectedTeam != null)
                    {
                        if (EditWorker != null)
                        {
                            dbConnection.RemoveWorkerFromTeam(workerId);
                        }
                        dbConnection.AddWorkerToTeam(workerId, SelectedTeam.Id);
                    }
                }
                return true;
            }
        }
    }
}
