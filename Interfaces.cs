using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker
{
    interface IPerson
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public abstract void SignIn();
    }

    interface IUser
    {
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public void ResetPassword(string securityAnswer);
    }

    interface IAdminUser
    {
        public void ManageAllWorkouts();
    }

    interface IWorkout
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }
        public abstract int CalculateCaloriesBurned();
    }

    interface ICardioWorkout
    {
        public int Distance { get; set; }

    }

    interface IStrengthWorkout
    {
        public int Repetitions { get; set; }

    }

    interface IMainWindow
    {
        public string LabelTitle { get; set; }
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public void SignIn();
        public void Register();
    }

    interface IWorkoutsWindow
    {
        public IUser user { get; set; }
        public List<IWorkout> WorkoutList { get; set; }
        public void AddWorkout();
        public void RemoveWorkout();
        public void OpenDetails(IWorkout workout);
    }
    interface IRegisterWindow
    {
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }
        public void RegisterNewUser();
    }

    interface IWorkoutDetailsWindow
    {
        public IWorkout Workout { get; set; }

        public void EditWorkout();
        public void SaveWorkout();
    }
    interface IAddWorkoutWindow
    {
        public string WorkoutTypeComboBox { get; set; }
        public TimeSpan DurationInput { get; set; }
        public int CaloriesBurnedInput { get; set; }
        public string NotesInput { get; set; }

        public void SaveWorkout();
    }
    interface IUserDetailsWindow
    {
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }
        public void SaveUserDetails();
        public void Cancel();

    
    }
}
