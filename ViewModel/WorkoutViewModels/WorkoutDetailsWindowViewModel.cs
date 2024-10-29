using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View__UI_;

namespace FitTracker.ViewModel.WorkoutViewModels
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase, IWorkoutDetailsWindow
    {
        private readonly WorkoutManager workoutManager;
        public IWorkout Workout { get; set; }
        
        public User ActiveUser
        {
            get
            {
                return UserManager.Instance.ActiveUser;
            }
        }

        public WorkoutDetailsWindowViewModel(IWorkout workout, WorkoutManager workoutManager)
        {
            Workout = workout ?? throw new ArgumentNullException(nameof(workout));
            this.workoutManager = workoutManager ?? throw new ArgumentNullException(nameof(workoutManager));
            IsTextBoxReadOnly = true;
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
                    saveWorkoutCommand = new RelayCommand(ExecuteSaveWorkout);
                }
                return saveWorkoutCommand;
            }
        }
        private void ExecuteSaveWorkout(object obj)
        {
            SaveWorkout();
        }

        private void ExecuteEditWorkout(object obj)
        {
            EditWorkout();
        }

        public void EditWorkout()
        {
            IsTextBoxReadOnly = false;
        }

        public void SaveWorkout()
        {
            foreach (var workout in workoutManager.WorkoutList)
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
        
    }
}
