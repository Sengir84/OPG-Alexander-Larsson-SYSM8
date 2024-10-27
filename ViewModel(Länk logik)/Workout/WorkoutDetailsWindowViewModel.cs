using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.ViewModel_Länk_logik_
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase, IWorkoutDetailsWindow
    {
        public IWorkout Workout { get; set; }
        
        public WorkoutDetailsWindowViewModel(IWorkout workout)
        {
            Workout = workout ?? throw new ArgumentNullException(nameof(workout));
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

            IsTextBoxReadOnly= true;
        }
    }
}
