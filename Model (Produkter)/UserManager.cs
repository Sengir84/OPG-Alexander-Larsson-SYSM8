using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTracker.Model__Produkter_
{
    public class UserManager
    {
        public ObservableCollection<User> Users { get; private set; }

        public UserManager() 
        { 
            Users = new ObservableCollection<User>();
        }
        
        public bool UniqueUsername(string username)
        {
            return !Users.Any(u=> u.Username == username);
        }

        public void AddUser(string username, string password, string Country)
        {
            if (!UniqueUsername(username))
            {
                MessageBox.Show("Användarnamnet finns redan");
            }
            else
            {
                Users.Add(new User { Username = username, Password = password, Country = Country });
            }
        }
    }
}
