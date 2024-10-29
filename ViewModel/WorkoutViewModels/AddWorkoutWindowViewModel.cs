using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View;
using FitTracker.View__UI_;
using System.Collections.ObjectModel;
using System.Windows;

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
                    UpdateWorkoutTypeVisibility();
                }
            }
        }
        private bool isStrengthWorkout;
        public bool IsStrengthWorkout
        {
            get { return isStrengthWorkout; }
            set
            {
                isStrengthWorkout = value;
                OnPropertyChanged(nameof(IsStrengthWorkout));
            }
        }
        private bool isCardioWorkout;
        public bool IsCardioWorkout
        {
            get { return isCardioWorkout; }
            set
            {
                isCardioWorkout = value;
                OnPropertyChanged(nameof(IsCardioWorkout));
            }
        }
        private void UpdateWorkoutTypeVisibility()
        {
            IsStrengthWorkout = WorkoutTypeCombobox == "Strength";
            IsCardioWorkout = WorkoutTypeCombobox == "Cardio";
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
            WorkoutType = new ObservableCollection<string>{"Strength","Cardio"};
            WorkoutTypeCombobox = WorkoutType.First();
            UpdateWorkoutTypeVisibility();
        }
        
        public TimeSpan DurationInput { get; set; }
        public int CaloriesBurnedInput { get; set; }
        public string NotesInput { get; set; }
        public double DistanceInput {  get; set; }
        public string EquipmentInput { get; set; }
        public int RepetitionsInput { get; set; }
        private void ExecuteSaveWorkout(object obj)
        {
            SaveWorkout();
        }

        public void SaveWorkout()
        {
            WorkoutModel workout;
            
            if (WorkoutTypeCombobox == "Strength")
            {
                workout = new StrengthWorkout(DateTime.Now, "Strength", DurationInput, CaloriesBurnedInput, NotesInput, EquipmentInput, RepetitionsInput);
            }
            else if (WorkoutTypeCombobox == "Cardio")
            {
                workout = new CardioWorkout(DateTime.Now, "Cardio", DurationInput, CaloriesBurnedInput, NotesInput, DistanceInput);
            }
            else
            {
                MessageBox.Show("Must choose a workout type");
                return;
            }

            WorkoutManager.Instance.AddWorkout(workout);
        }
        private void ExecuteReturn(object obj)
        {
            Return();
        }

        private void Return()
        {
            if (App.Current.Windows.OfType<AddWorkoutWindow>().FirstOrDefault() is Window addWorkoutWindow)
            {
                addWorkoutWindow.Close();
            }
        }

        
    }
}
