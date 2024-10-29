using FitTracker.Model__Produkter_;
using FitTracker.ViewModel.WorkoutViewModels;
using System.Windows;

namespace FitTracker.View__UI_
{
    /// <summary>
    /// Interaction logic for WorkoutWindow.xaml
    /// </summary>
    public partial class WorkoutWindow : Window
    {
        public WorkoutWindow()
        {
            InitializeComponent();
            DataContext = new WorkoutWindowViewModel(WorkoutManager.Instance);
        }
    }
}
