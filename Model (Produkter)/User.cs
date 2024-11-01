using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class User : Person ,IUser
    {
        //Fields
        public string Country { get; set; }
        public string SecurityQuestion {  get; set; }
        public string SecurityAnswer { get; set; }
        public bool IsAdmin { get; set; }
        public ObservableCollection<IWorkout> Workouts { get; set; } = new ObservableCollection<IWorkout>();
        public User() { }
        
        //Konstruktor
        public User (string username, string password, string country, string securityQuestion, string securityAnswer) : base ()
        {
            Username = username;
            Password = password;
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
        }
        //Metod för att kontrollera att svaret på säkerhetsfråga stämmer
        public bool VerifySecurityAnswer(string securityAnswer)
        {
            return securityAnswer == SecurityAnswer;
        }
        //Metod för att sätta ett nytt lösenord
        public void ResetPassword(string newPassword)
        {
            Password = newPassword;
        }
        //Metod för att sätta usern som active user när den loggar in
        public override void SignIn()
        {
            UserManager.Instance.ActiveUser = this;
        }
    }
}
