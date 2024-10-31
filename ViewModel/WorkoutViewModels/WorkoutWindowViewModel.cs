using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View;
using FitTracker.View__UI_;
using System.Collections.ObjectModel;
using System.Windows;

namespace FitTracker.ViewModel.WorkoutViewModels
{
    public class WorkoutWindowViewModel : ViewModelBase, IWorkoutsWindow
    {
        private readonly UserManager userManager;
        public IUser User { get; }

        #region Constructors
        public WorkoutWindowViewModel(WorkoutManager workoutManager)
        {
            userManager = UserManager.Instance;
            User = UserManager.Instance.ActiveUser;

            UpdateWorkoutList();

            if (userManager.ActiveUser is AdminUser)
            {
                workoutManager.PopulateAllWorkouts();

            }
        }
        #endregion

        #region Properties
        private int? durationFilter { get; set; } = 0;

        public int? DurationFilter
        {
            get { return durationFilter; }
            set
            {
                if (durationFilter == value) return;

                durationFilter = value;
                OnPropertyChanged(nameof(DurationFilter));
                UpdateWorkoutList();
            }
        }

        private string typeFilter { get; set; }

        public string TypeFilter
        {
            get { return typeFilter; }
            set
            {
                if (typeFilter == value) return;

                typeFilter = value;
                OnPropertyChanged(nameof(TypeFilter));
                UpdateWorkoutList();
            }
        }


        private DateTime? dateFilter { get; set; }

        public DateTime? DateFilter
        {
            get { return dateFilter; }
            set
            {
                if (dateFilter == value) return;

                dateFilter = value;
                OnPropertyChanged(nameof(DateFilter));
                UpdateWorkoutList();
            }
        }


        private ObservableCollection<IWorkout> workoutList;
        public ObservableCollection<IWorkout> WorkoutList
        {
            get { return workoutList; }
            private set
            {
                workoutList = value;
                OnPropertyChanged(nameof(WorkoutList));
            }

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

        #endregion

        #region RelayCommands
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
                if (signOutCommand == null)
                {
                    signOutCommand = new RelayCommand(ExecuteSignout);
                }
                return signOutCommand;

            }
        }

        #endregion

        #region Methods

        private void UpdateWorkoutList()
        {
            IEnumerable<IWorkout> filteredWorkouts;
            if (userManager.ActiveUser?.IsAdmin == true)
            {
                filteredWorkouts = WorkoutManager.Instance.AllWorkouts;
            }
            else
            {
                filteredWorkouts = userManager.ActiveUser?.Workouts ?? new ObservableCollection<IWorkout>(); // Regular users see their workouts
            }

            if (DurationFilter.HasValue)
            {
                filteredWorkouts = filteredWorkouts.Where(x => x.Duration >= TimeSpan.FromMinutes(DurationFilter.Value));
            }
            if (DateFilter.HasValue)
            {
                filteredWorkouts = filteredWorkouts.Where(x => x.Date == DateFilter.Value);
            }
            if (!string.IsNullOrEmpty(TypeFilter))
            {
                filteredWorkouts = filteredWorkouts.Where(x => x.Type.StartsWith(TypeFilter, StringComparison.OrdinalIgnoreCase));
            }

            WorkoutList = new ObservableCollection<IWorkout>(filteredWorkouts);

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
                UpdateWorkoutList();
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
        #endregion
    }
}
