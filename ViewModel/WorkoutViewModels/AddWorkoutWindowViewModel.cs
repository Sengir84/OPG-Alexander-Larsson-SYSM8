﻿using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View;
using FitTracker.View__UI_;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace FitTracker.ViewModel.WorkoutViewModels
{
    public class AddWorkoutWindowViewModel : ViewModelBase, IAddWorkoutWindow
    {
        //Lista över träningstyper
        public ObservableCollection<string> WorkoutType { get; set; }
        //Konstruktor för att initiera träningstyper
        public AddWorkoutWindowViewModel()
        {
            WorkoutType = new ObservableCollection<string> { "Strength", "Cardio" };
            WorkoutTypeCombobox = WorkoutType.First();
            UpdateWorkoutTypeVisibility();
        }

        //properties
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

        private TimeSpan durationInput;
        public TimeSpan DurationInput
        {
            get
            {
                return durationInput;
            }
            set
            {
                if (durationInput != value)
                {
                    durationInput = value;
                    OnPropertyChanged(nameof(DurationInput));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }
        private int caloriesBurnedInput;
        public int CaloriesBurnedInput
        {
            get
            {
                return caloriesBurnedInput;
            }
            set
            {
                if (caloriesBurnedInput != value)
                {
                    caloriesBurnedInput = value;
                    OnPropertyChanged(nameof(CaloriesBurnedInput));
                    CommandManager.InvalidateRequerySuggested();
                }
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

        private string notesInput;
        public string NotesInput
        {
            get
            {
                return notesInput;
            }
            set
            {
                if (notesInput != value)
                {
                    notesInput = value;
                    OnPropertyChanged(nameof(NotesInput));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private int distanceInput;
        public int DistanceInput
        {
            get { return distanceInput; }
            set
            {
                if (distanceInput != value)
                {
                    distanceInput = value;
                    OnPropertyChanged(nameof(DistanceInput));
                    UpdateCaloriesBurnedInput();
                    CommandManager.InvalidateRequerySuggested();
                }
            }

        }

        private string equipmentInput;
        public string EquipmentInput
        {
            get { return equipmentInput; }
            set
            {
                if (equipmentInput != value)
                {
                    equipmentInput = value;
                    OnPropertyChanged(nameof(EquipmentInput));
                    CommandManager.InvalidateRequerySuggested();
                }
            }

        }
        private int repetitionsInput;
        public int RepetitionsInput
        {
            get { return repetitionsInput; }
            set
            {
                if (repetitionsInput != value)
                {
                    repetitionsInput = value;
                    OnPropertyChanged(nameof(RepetitionsInput));
                    UpdateCaloriesBurnedInput();
                    CommandManager.InvalidateRequerySuggested();
                }
            }

        }
        //Relaycommands

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

        private RelayCommand saveWorkoutCommand;
        public RelayCommand SaveWorkoutCommand
        {
            get
            {
                if (saveWorkoutCommand == null)
                {
                    saveWorkoutCommand ??= new RelayCommand(ExecuteSaveWorkout, CanSaveWorkout);
                }
                return saveWorkoutCommand;
            }
        }
        //Metoder
        //Metod för att välja BurnedCalories metod
        private void UpdateCaloriesBurnedInput()
        {
            if (WorkoutTypeCombobox == "Strength")
            {
                CaloriesBurnedInput = new StrengthWorkout(DateTime.Now, "Strength", DurationInput, 0, NotesInput, EquipmentInput, RepetitionsInput).CalculateCaloriesBurned();
            }
            else if (WorkoutTypeCombobox == "Cardio")
            {
                CaloriesBurnedInput = new CardioWorkout(DateTime.Now, "Cardio", DurationInput, 0, NotesInput, DistanceInput).CalculateCaloriesBurned();
            }
        }
        //Metod för att uppdatera kombobox
        private void UpdateWorkoutTypeVisibility()
        {
            IsStrengthWorkout = WorkoutTypeCombobox == "Strength";
            IsCardioWorkout = WorkoutTypeCombobox == "Cardio";
        }
        private void ExecuteSaveWorkout(object obj)
        {
            SaveWorkout();
        }
        //Metod för att kontrollera om det går att att spara workout
        private bool CanSaveWorkout(object obj)
        {
            bool isValid = DurationInput != TimeSpan.Zero &&
                           CaloriesBurnedInput > 0 &&
                           !string.IsNullOrWhiteSpace(NotesInput);

            if (WorkoutTypeCombobox == "Strength")

            {
                isValid &= !string.IsNullOrWhiteSpace(EquipmentInput) &&
                 RepetitionsInput > 0;
            }


            else if (WorkoutTypeCombobox == "Cardio")
            {
                isValid &= DistanceInput > 0;
            }

            IsValidationMessageVisible = !isValid;
            return isValid;
        }
        //Metod för att spara workout
        public void SaveWorkout()
        {
            WorkoutModel workout;
            
            if (WorkoutTypeCombobox == "Strength")
            {
                workout = new StrengthWorkout(DateTime.Now, "Strength", DurationInput, CaloriesBurnedInput, NotesInput, EquipmentInput, RepetitionsInput);
                workout.CaloriesBurned = workout.CalculateCaloriesBurned();
            }
            else if (WorkoutTypeCombobox == "Cardio")
            {
                workout = new CardioWorkout(DateTime.Now, "Cardio", DurationInput, CaloriesBurnedInput, NotesInput, DistanceInput);
                workout.CaloriesBurned = workout.CalculateCaloriesBurned();
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
        //Stänger fönstret
        private void Return()
        {
            if (App.Current.Windows.OfType<AddWorkoutWindow>().FirstOrDefault() is Window addWorkoutWindow)
            {
                addWorkoutWindow.Close();
            }
        }

        
    }
}
