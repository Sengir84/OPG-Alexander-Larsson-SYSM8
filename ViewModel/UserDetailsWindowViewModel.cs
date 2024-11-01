using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View;
using FitTracker.View__UI_;
using System.Collections.ObjectModel;
using System.Windows;

namespace FitTracker.ViewModel
{
    public class UserDetailsWindowViewModel : ViewModelBase, IUserDetailsWindow
    {
        #region Constructor
        //initierar usermanager
        private UserManager userManager;
        
        public UserDetailsWindowViewModel(UserManager usermanager)
        {
            this.userManager = usermanager;
            ActiveUser = usermanager.ActiveUser;
            Countries = new ObservableCollection<string> { "Sverige", "Norge", "Danmark" };
            UsernameInput = ActiveUser.Username;
            CountryComboBox = ActiveUser.Country;
            SaveCommand = new RelayCommand(ExecuteSaveUserDetails);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }
        #endregion

        #region Properties
        //Fields

        public ObservableCollection<string> Countries { get; set; }
        public User ActiveUser { get; set; }

        private string usernameInput = string.Empty;
        public string UsernameInput
        {
            get => usernameInput;
            set
            {
                usernameInput = value;
                OnPropertyChanged();
                ValidateUsername();
            }
        }

        private string passwordWarningMessage = string.Empty;
        public string PasswordWarningMessage
        {
            get => passwordWarningMessage;
            set
            {
                passwordWarningMessage = value;
                OnPropertyChanged();
            }
        }

        private string usernameWarningMessage = string.Empty;
        public string UsernameWarningMessage
        {
            get => usernameWarningMessage;
            set
            {
                usernameWarningMessage = value;
                OnPropertyChanged();
            }
        }

        private string passwordInput = string.Empty;
        public string PasswordInput
        {
            get => passwordInput;
            set
            {
                passwordInput = value;
                OnPropertyChanged();
                ValidatePassword();
            }
        }

        private string confirmPasswordInput = string.Empty;
        public string ConfirmPasswordInput
        {
            get => confirmPasswordInput;
            set
            {
                confirmPasswordInput = value;
                OnPropertyChanged();
                ValidatePassword();
            }
        }

        private string countryComboBox = string.Empty;
        public string CountryComboBox
        {
            get => countryComboBox;
            set
            {
                countryComboBox = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region RelayCommand
        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }
        #endregion
        #region Methods
        //kontroll för användarnamn
        private void ValidateUsername()
        {
            if (string.IsNullOrEmpty(UsernameInput) || UsernameInput.Length < 3)
            {
                UsernameWarningMessage = "Username must be at least 3 characters long.";
            }
            else if (!userManager.UniqueUsername(UsernameInput))
            {
                UsernameWarningMessage = "Username is already taken.";
            }
            else
            {
                UsernameWarningMessage = string.Empty;
            }
        }
        //Kontroll av lösenord (G nivås varianten, VG ligger i UserManager)
        private void ValidatePassword()
        {
            if (!string.IsNullOrEmpty(PasswordInput) && !string.IsNullOrEmpty(ConfirmPasswordInput))
            {
                if (PasswordInput.Length < 5)
                {
                    PasswordWarningMessage = "Password must be at least 5 characters long.";
                }
                else if (PasswordInput != ConfirmPasswordInput)
                {
                    PasswordWarningMessage = "Passwords do not match.";
                }
                else
                {
                    PasswordWarningMessage = string.Empty;
                }
            }
        }
        //Sparar nya värden till användaren och stänger fönstret
        public void ExecuteSaveUserDetails(object obj)
        {
            SaveUserDetails();
        }
        public void SaveUserDetails()
        {
            ValidateUsername();
            ValidatePassword();

            if (string.IsNullOrEmpty(UsernameWarningMessage) && string.IsNullOrEmpty(PasswordWarningMessage))
            {
                ActiveUser.Username = UsernameInput;
                if (!string.IsNullOrEmpty(PasswordInput))
                {
                    ActiveUser.ResetPassword(PasswordInput);
                }
                ActiveUser.Country = CountryComboBox;

                if (App.Current.Windows.OfType<UserDetailsWindow>().FirstOrDefault() is Window userDetailsWindow)
                {
                    userDetailsWindow.Close();
                }
            }
        }
        
        //Stänger fönstret
        private void ExecuteCancel(object obj) 
        {
            Cancel();
        }
        public void Cancel()
        {
            if (App.Current.Windows.OfType<UserDetailsWindow>().FirstOrDefault() is Window userDetailsWindow)
            {
                userDetailsWindow.Close();
            }
        }
    }
    #endregion


}
    

