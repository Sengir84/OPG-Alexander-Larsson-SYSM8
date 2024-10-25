using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View__UI_;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.ViewModel_Länk_logik_
{
    public class WorkoutWindowViewModel : ViewModelBase, IWorkoutsWindow
    {

        private ObservableCollection<IWorkout> workoutList;
        public ObservableCollection<IWorkout> WorkoutList 
        { 
            get {  return workoutList; }
            set
            {
                workoutList = value;
                OnPropertyChanged(nameof(WorkoutList));
            }
        }
        private IWorkout selectedWorkout;
        public IWorkout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged();
            }
        }

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
        private RelayCommand workoutDetailsCommand;
        public RelayCommand WorkoutDetailsCommand
        {
            get
            {
                if (workoutDetailsCommand == null)
                {
                    workoutDetailsCommand = new RelayCommand(ExecuteOpenDetails);
                }
                return workoutDetailsCommand;
            }
            
        }

        private void ExecuteOpenDetails(object obj)
        {
            //OpenDetails(WorkoutList.);
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
            WorkoutList = new ObservableCollection<IWorkout>();
            WorkoutList.Add(new Workout { Type = "Cardio", Date = DateTime.Now, Notes = "Bra pump" });
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
            WorkoutList.Remove(SelectedWorkout);
        }

        void OpenDetails(IWorkout workout)
        {
            throw new NotImplementedException();
        }

        void IWorkoutsWindow.OpenDetails(IWorkout workout)
        {
            throw new NotImplementedException();
        }
    }
}
