using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace FitTracker.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase, IRegisterWindow
    {
        private UserManager userManager;
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> SecurityQuestionList { get; set; }

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
        private string securityQuestion;
        public string SecurityQuestion
        {
            get { return securityQuestion; }
            set
            {
                if (securityQuestion != value)
                {
                    securityQuestion = value;
                    OnPropertyChanged(nameof(SecurityQuestion));
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
        private string securityAnswer;
        public string SecurityAnswer
        {
            get { return securityAnswer; }
            set
            {
                securityAnswer = value;
                OnPropertyChanged(nameof(SecurityAnswer));
            }
        }
        public RelayCommand AddUserCommand { get; }

        public RegisterWindowViewModel() : this(UserManager.Instance) { }
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
            SecurityQuestionList = new ObservableCollection<string>
            {
                "Vad hette ditt första husdjur?",
                "I vilken stad föddes du?",
                "Vem är din favoritkaraktär i Bolibumpa"
            };
        }

       
        private bool CanRegisterNewUser(object arg)
        {
            return !string.IsNullOrWhiteSpace(UsernameInput) && !string.IsNullOrWhiteSpace(PasswordInput) 
                && !string.IsNullOrEmpty(ConfirmPasswordInput) && CountryComboBox != null ;
        }
        
        private void ExecuteRegisterNewUser(object obj)
        {
            RegisterNewUser();
        }
        public void RegisterNewUser()
        {
            if (PasswordInput == ConfirmPasswordInput)
            {
                userManager.AddUser(UsernameInput, passwordInput, CountryComboBox, SecurityQuestion, SecurityAnswer);
                var mainWindowViewModel = new MainWindowViewModel(UserManager.Instance);
                var mainWindow = new MainWindow { DataContext = mainWindowViewModel };
                mainWindow.Show();

                App.Current.Windows[0].Close();
            }
            else
            {
                MessageBox.Show("Lösenord och kontroll stämmer inte överens");
            }
        }
        

    }
    
}
