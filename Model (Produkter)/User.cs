using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class User : Person ,IUser
    {
        public string Country { get; set; }
        public string SecurityQuestion {  get; set; }
        public string SecurityAnswer { get; set; }

        public User() { }

        public User (string username, string password, string country, string securityQuestion, string securityAnswer) : base ()
        {
            Username = username;
            Password = password;
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
        }

        public void ResetPassword(string securityAnswer)
        {
            throw new NotImplementedException();
        }

        public override void SignIn()
        {
            throw new NotImplementedException();
        }
    }
}
