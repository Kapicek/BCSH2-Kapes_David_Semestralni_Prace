using System.Windows;
using TaskManager.ViewModels;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for WorkerInfoWindow.xaml
    /// </summary>
    public partial class WorkerInfoWindow : Window
    {
        public WorkerInfoWindow()
        {
            InitializeComponent();
            DataContext = new WorkerInfoViewModel();
        }
    }
}
