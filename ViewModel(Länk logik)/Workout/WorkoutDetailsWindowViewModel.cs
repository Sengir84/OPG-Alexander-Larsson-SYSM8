using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.ViewModel_Länk_logik_
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase, IWorkoutDetailsWindow
    {
        public IWorkout Workout { get; set; }

        public WorkoutDetailsWindowViewModel(IWorkout workout)
        {
            Workout = workout ?? throw new ArgumentNullException(nameof(workout));
        }

        public void EditWorkout()
        {
            throw new NotImplementedException();
        }

        public void SaveWorkout()
        {
            throw new NotImplementedException();
        }
    }
}
