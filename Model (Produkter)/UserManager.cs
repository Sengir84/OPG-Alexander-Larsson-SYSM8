using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTracker.Model__Produkter_
{
    public class UserManager : ViewModelBase
    {
        private static UserManager instance;
        public ObservableCollection<User> Users { get; private set; }

        public UserManager() 
        { 
            Users = new ObservableCollection<User>();
            DefaultUsers();
        }

        public static UserManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserManager();
                }
                return instance;
            }
        }
        private User activeUser;
        public User ActiveUser
        {
            get { return activeUser; }
            set
            {
                activeUser = value;
                OnPropertyChanged(nameof(ActiveUser));
            }
        }
        public bool IsAdmin()
        {
            return ActiveUser != null && ActiveUser.IsAdmin;
        }
        private void DefaultUsers() 
        {
            var admin = new AdminUser
            {
                Username = "admin",
                Password = "password",
                IsAdmin = true,
                Workouts = new ObservableCollection<IWorkout>()
            };
            admin.Workouts.Add(new StrengthWorkout(
            date: new DateTime(2024, 10, 27, 16, 32, 00),
            "Strength",
            new TimeSpan(1, 22, 30),
            500,
            "Full body workout",
            "Dumbbells",
            12));
            admin.Workouts.Add(new CardioWorkout(
            date: new DateTime(2024, 10, 27, 16, 32, 00),
            "Cardio",
            new TimeSpan(1, 0, 0),
            400,
            "Running workout",
            5));

            Users.Add(admin);

            var alex = new User
            {
                Username = "Alex",
                Password = "Alexander!",
                Country = "Sverige",
                SecurityQuestion = "I vilken stad föddes du?",
                SecurityAnswer = "Trelleborg",
                Workouts = new ObservableCollection<IWorkout>()
            };
            alex.Workouts.Add(new CardioWorkout(
            date: new DateTime(2024, 10, 27, 16, 32, 00),
            "Cardio",
            new TimeSpan(1, 0, 0),
            400,
            "Running workout",
            5));
            alex.Workouts.Add(new StrengthWorkout(
            date: new DateTime(2024, 10, 27, 16, 32, 00),
            "Strength",
            new TimeSpan(1, 22, 30),
            500,
            "Full body workout",
            "Dumbbells",
            12));

            Users.Add(alex);
        }
        public bool UniqueUsername(string username)
        {
            return !Users.Any(u=> u.Username == username);
        }
        public bool ValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                return false;
            }
            bool checkSpecChar = password.Any(ch => !char.IsLetterOrDigit(ch));
            return checkSpecChar;
        }

        public void AddUser(string username, string password, string Country, string securityQuestion, string securityAnswer)
        {
            if (!UniqueUsername(username))
            {
                MessageBox.Show("Användarnamnet finns redan");
            }
            else if (!ValidPassword(password))
            {
                MessageBox.Show("Lösenord måste bestå av minst 8 tecken och ha minst ett specialtecken");
            }
            else
            {
                Users.Add(new User { Username = username, Password = password, Country = Country,SecurityQuestion = securityQuestion, SecurityAnswer = securityAnswer});
            }
        }
    }
}
