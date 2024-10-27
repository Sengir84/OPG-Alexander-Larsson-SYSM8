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
        
        public ObservableCollection<IWorkout> WorkoutList
        {
            get { return WorkoutManager.Instance.WorkoutList; }

            set
            {
                WorkoutManager.Instance.WorkoutList = value;
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
    
        public WorkoutWindowViewModel(string activeUser)
        {
            ActiveUser = activeUser;
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
                    workoutDetailsCommand = new RelayCommand(ExecuteOpenWorkoutDetails);
                }
                return workoutDetailsCommand;
            }
            
        }
        private RelayCommand removeWorkoutCommand;
        public RelayCommand RemoveWorkoutCommand
        {
            get
            {
                if (removeWorkoutCommand == null)
                {
                    removeWorkoutCommand = new RelayCommand(ExecuteRemoveWorkout);
                }
                return removeWorkoutCommand;
            }
        }
        private RelayCommand signOutCommand;
        public RelayCommand SignOutCommand
        {
            get
            {
                if(signOutCommand == null)
                {
                    signOutCommand = new RelayCommand(ExecuteSignout);
                }
                return signOutCommand;

            }
        }

       

        private void ExecuteOpenWorkoutDetails(object obj)
        {
            if (SelectedWorkout != null)
            {
                OpenDetails(SelectedWorkout);
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
        public void ExecuteRemoveWorkout(object obj) 
        {  
            RemoveWorkout(); 
        }
        public void RemoveWorkout()
        {
            if (selectedWorkout != null)
            {
                WorkoutManager.Instance.RemoveWorkout(selectedWorkout);
            }
        }

        public void OpenDetails(IWorkout workout)
        {
            if (SelectedWorkout != null)
            {
                var workoutDetailsWindow = new WorkoutDetailsWindow(SelectedWorkout);
                workoutDetailsWindow.Show();
            }
        }
        private void ExecuteSignout(object obj)
        {
            SignOut();
        }

        public void SignOut()
        {
            ActiveUser = null;
            
            var mainWindowViewModel = new MainWindowViewModel();
            var mainWindow = new MainWindow { DataContext = mainWindowViewModel };
            mainWindow.Show();
            App.Current.Windows[0].Close();
        }

    }
}
