using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace FitTracker.ViewModel_Länk_logik_
{
    public class RegisterWindowViewModel : ViewModelBase, IRegisterWindow
    {
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<User> Users { get; set; }
        private string usernameInput;
        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged(nameof(UsernameInput));
            }
        }
        

        private string countryComboBox;
        public string CountryComboBox
        {
            get { return countryComboBox; }
            set
            {
                if (countryComboBox != value)
                {
                    countryComboBox = value;
                    OnPropertyChanged(nameof(CountryComboBox));
                }
            }
        }
        
        private string passwordInput;
        public string PasswordInput
        {
            get { return passwordInput; }
            set
            {
                if (passwordInput != value)
                {
                    passwordInput = value;
                    OnPropertyChanged(nameof(PasswordInput));
                    AddUserCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get { return confirmPasswordInput; }
            set
            {
                if (confirmPasswordInput != value)
                {
                    confirmPasswordInput = value;
                    OnPropertyChanged(nameof(ConfirmPasswordInput));
                    AddUserCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public RelayCommand AddUserCommand { get; }
        public RegisterWindowViewModel()
        {
            Users = new ObservableCollection<User>();
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
            Countries = new ObservableCollection<string>
            {
                "Sverige",
                "Danmark",
                "Norge",
                "Finland",
                "Nordpolen"
            };
        }

        private bool CanAddUser(object arg)
        {
            return !string.IsNullOrWhiteSpace(UsernameInput) && !string.IsNullOrWhiteSpace(PasswordInput) &&!string.IsNullOrEmpty(ConfirmPasswordInput) && PasswordInput == ConfirmPasswordInput;
        }

        private void AddUser(object obj)
        {
            Users.Add(new User { Username = UsernameInput, Password = PasswordInput });
            UsernameInput = string.Empty;
            PasswordInput = string.Empty;
        }
            public void RegisterNewUser()
        {
            throw new NotImplementedException();
        }

    }
    
}
