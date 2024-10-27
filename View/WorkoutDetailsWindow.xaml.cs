using FitTracker.Model__Produkter_;
using FitTracker.ViewModel_Länk_logik_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitTracker.View__UI_
{
    /// <summary>
    /// Interaction logic for WorkoutDetailsWindow.xaml
    /// </summary>
    public partial class WorkoutDetailsWindow : Window
    {
        public WorkoutDetailsWindow(IWorkout workout)
        {
            InitializeComponent();

            DataContext = new WorkoutDetailsWindowViewModel(workout, WorkoutManager.Instance);
        }
    }
}
