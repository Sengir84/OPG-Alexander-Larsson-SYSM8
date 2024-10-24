using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.ViewModel_Länk_logik_
{
    public class WorkoutWindowViewModel : ViewModelBase, IWorkoutsWindow
    { 

        private string activeUser;

        public string ActiveUser
        {
            get { return activeUser; }
            set 
            { 
                activeUser = value; 
                OnPropertyChanged(nameof(ActiveUser));
            }
        }
        public WorkoutWindowViewModel(string activeUser)
        {
            ActiveUser = activeUser ?? throw new ArgumentNullException(nameof(activeUser));
        }


        List<IWorkout> IWorkoutsWindow.WorkoutList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddWorkout()
        {   
            throw new NotImplementedException();
        }

        public void RemoveWorkout()
        {
            throw new NotImplementedException();
        }

        void IWorkoutsWindow.OpenDetails(IWorkout workout)
        {
            throw new NotImplementedException();
        }
    }
}
