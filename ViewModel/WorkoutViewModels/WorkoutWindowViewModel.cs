using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View;
using FitTracker.View__UI_;
using System.Collections.ObjectModel;
using System.Windows;

namespace FitTracker.ViewModel.WorkoutViewModels
{
    public class WorkoutWindowViewModel : ViewModelBase//, IWorkoutsWindow
    {
        private readonly UserManager userManager;
        private ObservableCollection<IWorkout> workoutList; 
        public ObservableCollection<IWorkout> WorkoutList
        {
            get { return workoutList; }
            private set
            {
                workoutList = value;
                OnPropertyChanged(nameof(WorkoutList));
            }
            //get {
            //    if (userManager.ActiveUser?.IsAdmin == true)
            //    {
            //        return WorkoutManager.Instance.AllWorkouts;
            //    }
            //    return userManager.ActiveUser?.Workouts;
            //}
        }
        private ObservableCollection<IWorkout> allWorkouts;
        public ObservableCollection<IWorkout> AllWorkouts
        {
            get { return allWorkouts; }
            set
            {
                allWorkouts = value;
                OnPropertyChanged(nameof(AllWorkouts));
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
    
        public WorkoutWindowViewModel(WorkoutManager workoutManager)
        {
            userManager = UserManager.Instance;
            ActiveUser = UserManager.Instance.ActiveUser;

            UpdateWorkoutList();

            if (userManager.ActiveUser is AdminUser)
            {
                workoutManager.PopulateAllWorkouts();
                //adminUser.ManageAllWorkouts();
            }
        }

        private void UpdateWorkoutList()
        {
            if (userManager.ActiveUser?.IsAdmin == true)
            {
                WorkoutList = WorkoutManager.Instance.AllWorkouts; // Admin sees all workouts
            }
            else
            {
                WorkoutList = userManager.ActiveUser?.Workouts ?? new ObservableCollection<IWorkout>(); // Regular users see their workouts
            }
        }

        private User activeUser;
        public User ActiveUser
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
            if (SelectedWorkout == null)
            {
                    MessageBox.Show("You need to select a workout first");
            }
            OpenDetails(SelectedWorkout);
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
            addworkoutWindow.Show();
            
        }
        public void ExecuteRemoveWorkout(object obj) 
        {  
            RemoveWorkout(); 
        }
        public void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                WorkoutManager.Instance.RemoveWorkout(SelectedWorkout);
                UpdateWorkoutList(); // Update list after removal
            }
            else
            {
                MessageBox.Show("No workout selected to remove.");
            }
        }

        public void OpenDetails(IWorkout workout)
        {
            if (SelectedWorkout != null)
            {
                var workoutDetailsWindow = new WorkoutDetailsWindow(SelectedWorkout);
                workoutDetailsWindow.Show();
                if (App.Current.Windows.OfType<WorkoutWindow>().FirstOrDefault() is Window workoutWindow)
                {
                    workoutWindow.Close();
                }
            }
            
        }
        private void ExecuteSignout(object obj)
        {
            SignOut();
        }

        public void SignOut()
        {
            UserManager.Instance.ActiveUser = null;
            
            var mainWindowViewModel = new MainWindowViewModel();
            var mainWindow = new MainWindow { DataContext = mainWindowViewModel };
            mainWindow.Show();
            App.Current.Windows[0].Close();
        }

    }
}
