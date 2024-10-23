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
