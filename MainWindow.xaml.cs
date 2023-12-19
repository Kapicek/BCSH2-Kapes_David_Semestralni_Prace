using System.Windows;
using TaskManager.ViewModels;

namespace TaskManager
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }


        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete(dataGrid.SelectedItem);
        }

    }
}
