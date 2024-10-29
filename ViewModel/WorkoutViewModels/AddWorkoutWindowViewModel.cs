using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View__UI_;
using System.Collections.ObjectModel;

namespace FitTracker.ViewModel.WorkoutViewModels
{
    public class AddWorkoutWindowViewModel : ViewModelBase, IAddWorkoutWindow
    {
        public ObservableCollection<string> WorkoutType { get; set; }
        
        private string workoutTypeCombobox;
        public string WorkoutTypeCombobox
        {
            get {  return workoutTypeCombobox; }
            set
            {
                if (workoutTypeCombobox != value)
                {
                    workoutTypeCombobox = value;
                    OnPropertyChanged(nameof(WorkoutTypeCombobox));
                }
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
        private RelayCommand returnCommand;

        public RelayCommand ReturnCommand
        {
            get
            {
                if (returnCommand == null)
                {
                    returnCommand = new RelayCommand(ExecuteReturn);
                }
                return returnCommand;
            }
        }

        public AddWorkoutWindowViewModel()
        {
            WorkoutType = new ObservableCollection<string>()
            {
                "Strength",
                "Cardio"
            };
        }
        
        public TimeSpan DurationInput { get; set; }
        public int CaloriesBurnedInput { get; set; }
        public string NotesInput { get; set; }

        private void ExecuteSaveWorkout(object obj)
        {
            SaveWorkout();
        }

        public void SaveWorkout()
        {
            Console.WriteLine($"Selected Type: {WorkoutTypeCombobox}");
            var workout = new WorkoutModel
            {
                Date = DateTime.Now, 
                Type = WorkoutTypeCombobox, 
                Duration = DurationInput, 
                CaloriesBurned = CaloriesBurnedInput,
                Notes = NotesInput 
            };
            
            WorkoutManager.Instance.AddWorkout(workout);
        }
        private void ExecuteReturn(object obj)
        {
            Return();
        }

        private void Return()
        {

            var workoutWindowViewModel = new WorkoutWindowViewModel(WorkoutManager.Instance);
            var workoutWindow = new WorkoutWindow { DataContext = workoutWindowViewModel };
            workoutWindow.Show();
            App.Current.Windows[0].Close(); 
        }

        
    }
}
