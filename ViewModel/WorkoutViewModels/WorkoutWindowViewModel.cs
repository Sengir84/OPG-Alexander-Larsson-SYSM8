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
        //Initierar Usermanager
        private readonly UserManager userManager;

        #region Constructors
        //Konstruktor
        public WorkoutWindowViewModel(WorkoutManager workoutManager)
        {
            userManager = UserManager.Instance;
            User = UserManager.Instance.ActiveUser;

            UpdateWorkoutList();

            if (userManager.ActiveUser is AdminUser)
            {
                workoutManager.PopulateAllWorkouts();
            }

            WorkoutManager.Instance.OnWorkoutAdded += UpdateWorkoutList;
        }
        #endregion

        #region Properties
        public IUser User { get; }
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
        //RelayCommands
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
        private RelayCommand showInfoCommand;
        public RelayCommand ShowInfoCommand
        {
            get
            {
                if (showInfoCommand == null)
                {
                    showInfoCommand = new RelayCommand(ExecuteShowInfo);
                }
                return showInfoCommand;
            }
        }

        
            #endregion

        #region Methods
            //sök metod för workoutlistan. Baserad på Workout type, duration, och date. 
            //Om användaren är admin hämtas hela listan för filtrering om vanlig user hämtas activeusers lista
            private void UpdateWorkoutList()
        {
            IEnumerable<IWorkout> filteredWorkouts;
            if (userManager.ActiveUser?.IsAdmin == true)
            {
                filteredWorkouts = WorkoutManager.Instance.AllWorkouts;
            }
            else
            {
                filteredWorkouts = userManager.ActiveUser?.Workouts ?? new ObservableCollection<IWorkout>(); 
            }
            
            if (DurationFilter.HasValue)
            {
                
                filteredWorkouts = filteredWorkouts.Where(x => x.Duration >= TimeSpan.FromMinutes(DurationFilter.Value));
            }
            if (DateFilter.HasValue)
            {
                filteredWorkouts = filteredWorkouts.Where(x => x.Date == DateFilter.Value);
            }
                //Om type filter rutan inte är null
            if (!string.IsNullOrEmpty(TypeFilter))
            {   //Loopar igenom listan och filtrerar ut baserat på input i Typefilter inputrutan och sparar sen i den filtreradelistan
                filteredWorkouts = filteredWorkouts.Where(x => x.Type.StartsWith(TypeFilter, StringComparison.OrdinalIgnoreCase));
            }
            //Lista som håller de filtrerade workoutsen
            WorkoutList = new ObservableCollection<IWorkout>(filteredWorkouts);
        }
        
        //Öppnar fönster med användarens information
        private void ExecuteUserDetails(object obj)
        {
            UserDetails();
        }
        public void UserDetails()
        {
            var userDetailsWindow = new UserDetailsWindow(userManager);

            userDetailsWindow.Show();
        }

        //Öppnar fönstret för att lägga till en workout
        private void ExecuteAddworkout(object obj)
        {
            AddWorkout();
        }
        public void AddWorkout()
        {
            var addworkoutWindow = new AddWorkoutWindow();
            addworkoutWindow.Show();
        }

        //Metod för att ta bort en workout
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

        //Öppnar nytt fönster med detaljerad information om ett valt träningspass
        private void ExecuteOpenWorkoutDetails(object obj)
        {
            if (SelectedWorkout == null)
            {
                MessageBox.Show("You need to select a workout first");
            }
            OpenDetails(SelectedWorkout);
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

        //Loggar ut activeuser och återvänder till loginskärmen
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
        //Inforuta i form av en messagebox
        private void ExecuteShowInfo(object obj)
        {
            MessageBox.Show("FitTracker is the all in one app for keeping track of your Workouts." +
                " Fittrack was created by a gardengnome called Ragnar that´s stuck in a cellar until the app has made one billion Dollars.\n" +
                "FitTrack HQ adress: I don´t know im locked in the cellar");
        }
        #endregion
    }
}
