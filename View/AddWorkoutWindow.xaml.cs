using FitTracker.ViewModel.WorkoutViewModels;
using System.Diagnostics;
using System.Windows;

namespace FitTracker.View
{
    /// <summary>
    /// Interaction logic for AddWorkoutWindow.xaml
    /// </summary>
    public partial class AddWorkoutWindow : Window
    {
        public AddWorkoutWindow()
        {
            InitializeComponent();

            DataContext = new AddWorkoutWindowViewModel();
        }
    }
}

