using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FitTracker
{
    public interface IPerson
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public abstract void SignIn();
    }

    public interface IUser
    {
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public void ResetPassword(string securityAnswer);
    }

    public interface IAdminUser
    {
        public void ManageAllWorkouts();
    }

    public interface IWorkout
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        public abstract int CalculateCaloriesBurned();
    }

    public interface ICardioWorkout
    {
        public int Distance { get; set; }

    }

    public interface IStrengthWorkout
    {
        public int Repetitions { get; set; }
        public string Equipment {  get; set; }
    }

    public interface IMainWindow
    {
        public string LabelTitle { get; set; }
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public void SignIn();
        public void Register();
    }

    public interface IWorkoutsWindow
    {
        IUser User { get;}
        public ObservableCollection<IWorkout> WorkoutList {get;}
        
        void AddWorkout();
        void RemoveWorkout();
        public void OpenDetails(IWorkout workout);
    }
    public interface IRegisterWindow
    {
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }
        public void RegisterNewUser();
    }

    public interface IWorkoutDetailsWindow
    {
        public IWorkout Workout { get; set; }

        public void EditWorkout();
        public void SaveWorkout();
    }
    public interface IAddWorkoutWindow
    {
        public TimeSpan DurationInput { get; set; }
        public int CaloriesBurnedInput { get; set; }
        public string NotesInput { get; set; }

        public void SaveWorkout();
    }
    public interface IUserDetailsWindow
    {
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }
        public void SaveUserDetails();
        public void Cancel();

    
    }
}
