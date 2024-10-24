using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View__UI_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.ViewModel_Länk_logik_
{
    public class WorkoutWindowViewModel : ViewModelBase, IWorkoutsWindow
    { 

        List<IWorkout> IWorkoutsWindow.WorkoutList { get; set; }

        private RelayCommand addWorkoutCommand;
        public RelayCommand AddWorkoutCommand
        {
            get
            {
                if (addWorkoutCommand == null)
                {
                    addWorkoutCommand = new RelayCommand(ExecuteAddworkout);
                }
                return addWorkoutCommand;
            }
        }
        private RelayCommand userDetailsCommand;

        public RelayCommand UserDetailsCommand
        {

            get
            {
                if (userDetailsCommand == null)
                {
                    userDetailsCommand = new RelayCommand(ExecuteUserDetails);
                }
                return userDetailsCommand;
            }
        }
        private void ExecuteUserDetails(object obj)
        {
            UserDetails();
        }

        public void UserDetails()
        {
            var userDetailsWindow = new UserDetailsWindow();
            
            userDetailsWindow.Show();
        }
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
            ActiveUser = activeUser;
        }

        private void ExecuteAddworkout(object obj)
        {
            AddWorkout();
        }

        public void AddWorkout()
        {
            var addworkoutWindow = new AddWorkoutWindow();
            addworkoutWindow = new AddWorkoutWindow{ DataContext = addworkoutWindow };
            addworkoutWindow.Show();
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
