using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View__UI_;
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
        private UserManager userManager;
        public ObservableCollection<string> Countries { get; set; }
        

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
        public RegisterWindowViewModel(UserManager userManager)
        {
            this.userManager = userManager;

            AddUserCommand = new RelayCommand(ExecuteRegisterNewUser, CanRegisterNewUser);
            Countries = new ObservableCollection<string>
            {
                "Sverige",
                "Danmark",
                "Norge",
                "Finland",
                "Nordpolen"
            };
            
        }

       
        private bool CanRegisterNewUser(object arg)
        {
            return !string.IsNullOrWhiteSpace(UsernameInput) && !string.IsNullOrWhiteSpace(PasswordInput) &&!string.IsNullOrEmpty(ConfirmPasswordInput) && CountryComboBox != null;
        }
        
        private void ExecuteRegisterNewUser(object obj)
        {
            RegisterNewUser();
        }
        public void RegisterNewUser()
        {
           
            if (passwordInput == ConfirmPasswordInput)
            {
                userManager.AddUser(UsernameInput, PasswordInput, CountryComboBox);
                MessageBox.Show("Användare registrerad, du kan nu logga in");
                
                var Mainwindow = new MainWindow();
                Mainwindow.Show();
                App.Current.Windows[0].Close();
                
            }
            else
            {
                MessageBox.Show("Lösenord stämmer inte överens");
            }
        }

    }
    
}
