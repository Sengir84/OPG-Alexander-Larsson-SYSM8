using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public abstract class Person : ViewModelBase
    {
        //Fields
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password { get; set; }
        //Metod som ska ärvas
        public abstract void SignIn();
    }
    
}
