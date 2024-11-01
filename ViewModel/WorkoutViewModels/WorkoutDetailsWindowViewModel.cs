using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View__UI_;
using System.ComponentModel;
using System.Windows;

namespace FitTracker.ViewModel.WorkoutViewModels
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase, IWorkoutDetailsWindow
    {
        //Initierar WorkoutManager
        private readonly WorkoutManager workoutManager;

        #region Constructors
        //Konstruktor
        public WorkoutDetailsWindowViewModel(IWorkout workout, WorkoutManager workoutManager)
        {
            Workout = workout ?? throw new ArgumentNullException(nameof(workout));
            this.workoutManager = workoutManager ?? throw new ArgumentNullException(nameof(workoutManager));

            IsTextBoxReadOnly = true;
            UpdateWorkoutTypeVisibility();
            IsValidationMessageVisible = false;
        }
        #endregion

        #region Properties
        //Properties
        public IWorkout Workout { get; set; }

        private bool isStrengthWorkoutVisible;

        public bool IsStrengthWorkoutVisible
        {
            get => isStrengthWorkoutVisible;
            set
            {
                isStrengthWorkoutVisible = value;
                OnPropertyChanged(nameof(IsStrengthWorkoutVisible));
            }
        }

        private bool isCardioWorkoutVisible;

        public bool IsCardioWorkoutVisible
        {
            get => isCardioWorkoutVisible;
            set
            {
                isCardioWorkoutVisible = value;
                OnPropertyChanged(nameof(IsCardioWorkoutVisible));
            }
        }

        public User ActiveUser
        {
            get
            {
                return UserManager.Instance.ActiveUser;
            }
        }

        private bool isTextBoxReadOnly;

        public bool IsTextBoxReadOnly
        {
            get
            {
                return isTextBoxReadOnly;
            }
            set
            {
                isTextBoxReadOnly = value;
                OnPropertyChanged(nameof(IsTextBoxReadOnly));
            }
        }

        private bool isValidationMessageVisible;
        public bool IsValidationMessageVisible
        {
            get => isValidationMessageVisible;
            set
            {
                if (isValidationMessageVisible != value)
                {
                    isValidationMessageVisible = value;
                    OnPropertyChanged(nameof(IsValidationMessageVisible));
                }
            }
        }

        #endregion

        #region RelayCommands
        //RelayCommands
        private RelayCommand editWorkoutCommand;
        public RelayCommand EditWorkoutCommand
        {
            get
            {
                if (editWorkoutCommand == null)
                {
                    editWorkoutCommand = new RelayCommand(ExecuteEditWorkout);
                }
                return editWorkoutCommand;
            }
        }

        private RelayCommand saveWorkoutCommand;
        public RelayCommand SaveWorkoutCommand
        {
            get
            {
                if (saveWorkoutCommand == null)
                {
                    saveWorkoutCommand = new RelayCommand(ExecuteSaveWorkout, CanSaveWorkout);
                }
                return saveWorkoutCommand;
            }
        }
        private RelayCommand copyWorkoutCommand;
        public RelayCommand CopyWorkoutCommand
        {
            get
            {
                if (copyWorkoutCommand == null)
                {
                    copyWorkoutCommand = new RelayCommand(ExecuteCopyWorkout);
                }
                return copyWorkoutCommand;
            }

        }

        #endregion

        #region Methods
        //Uppdaterar synlighet baserat på träningspass
        private void UpdateWorkoutTypeVisibility()
        {
            IsStrengthWorkoutVisible = false;
            IsCardioWorkoutVisible = false;

            if (Workout.Type == "Cardio")
            {
                IsCardioWorkoutVisible = true;
            }
            else if (Workout.Type == "Strength")
            {
                IsStrengthWorkoutVisible= true;
            }
        }
        //Metod för att kopiera träningspass
        private void ExecuteCopyWorkout(object obj)
        {
            IWorkout copiedWorkout;

            if (Workout is StrengthWorkout strengthWorkout)
            {
                copiedWorkout = new StrengthWorkout(
                    strengthWorkout.Date,
                    strengthWorkout.Type,
                    strengthWorkout.Duration,
                    strengthWorkout.CaloriesBurned,
                    strengthWorkout.Notes,
                    strengthWorkout.Equipment,
                    strengthWorkout.Repetitions);
            }
            else if (Workout is CardioWorkout cardioWorkout)
            {
                copiedWorkout = new CardioWorkout(
                    cardioWorkout.Date,
                    cardioWorkout.Type,
                    cardioWorkout.Duration,
                    cardioWorkout.CaloriesBurned,
                    cardioWorkout.Notes,
                    cardioWorkout.Distance);
            }
            else
            {
                MessageBox.Show("Unknown workout type");
                return;
            }

            WorkoutManager.Instance.AddWorkout(copiedWorkout);
        }

        //Kontroll för att se om det går att spara en workout
        private bool CanSaveWorkout(object obj)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Workout.Type) ||
                Workout.Date == default(DateTime) ||
                (Workout.Duration is TimeSpan timespan && timespan == TimeSpan.Zero) ||
                Workout.CaloriesBurned <= 0 ||
                string.IsNullOrWhiteSpace(Workout.Notes))
            {
                isValid = false;
            }

            else if (Workout is StrengthWorkout strengthWorkout)
            {
                if (string.IsNullOrWhiteSpace(strengthWorkout.Equipment) || strengthWorkout.Repetitions <= 0) 
                {
                    isValid = false;
                }
            }
            else if (Workout is CardioWorkout cardioWorkout)
            {
                if (cardioWorkout.Distance <= 0) 
                {
                    isValid = false;
                }
            }
            IsValidationMessageVisible = !isValid;
            return isValid;
            
        }
        //Sparar träningspass
        private void ExecuteSaveWorkout(object obj)
        {
            if (CanSaveWorkout(obj))
            { 
                SaveWorkout();
                IsValidationMessageVisible = false;
            }
            else { IsValidationMessageVisible = true; }
        }

        private void ExecuteEditWorkout(object obj)
        {
            EditWorkout();
        }
        //Metod för att göra fält redigerbara
        public void EditWorkout()
        {
            IsTextBoxReadOnly = false;
        }
        //Metod för att spara en workout
        public void SaveWorkout()
        {
            var userWorkouts = UserManager.Instance.ActiveUser?.Workouts;
            foreach (var workout in userWorkouts)
            {
                if (workout == Workout)
                {
                    workout.Type = Workout.Type;
                    workout.Date = Workout.Date;
                    workout.Duration = Workout.Duration;
                    workout.CaloriesBurned = Workout.CaloriesBurned;
                    workout.Notes = Workout.Notes;
                }
            }
            IsTextBoxReadOnly = true;

            var workoutWindowViewModel = new WorkoutWindowViewModel(workoutManager);
            var workoutWindow = new WorkoutWindow { DataContext = workoutWindowViewModel };
            workoutWindow.Show();
            App.Current.Windows[0].Close();

        }
       
        #endregion
    }
}
