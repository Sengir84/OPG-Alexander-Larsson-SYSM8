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
        private void DefaultUsers() 
        { 
            Users.Add(new User { Username = "Alex", Password = "Alexander!", Country = "Sverige" });
        }
        public bool UniqueUsername(string username)
        {
            return !Users.Any(u=> u.Username == username);
        }
        public bool ValidPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }
            bool checkSpecChar = password.Any(ch => !char.IsDigit(ch));
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
                MessageBox.Show($"User added! Total users: {Users.Count}");
            }
        }
    }
}
