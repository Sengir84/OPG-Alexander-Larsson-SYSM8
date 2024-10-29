using FitTracker.Model__Produkter_;
using FitTracker.MVVM;

using FitTracker.View__UI_;
using FitTracker.ViewModel.WorkoutViewModels;
using System.Windows;

namespace FitTracker.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IMainWindow
    {
        private UserManager userManager;
        public MainWindowViewModel() : this(UserManager.Instance) { }
        public MainWindowViewModel(UserManager userManager)
        {
            this.userManager = userManager;
        }
        public RelayCommand AddUserCommand { get; }
        public string LabelTitle { get; set; }

        //Fields till PasswordInput textbox
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
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }
        //Fields till UsernameInput textbox
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
       

        //Commands att binda till xaml
        private RelayCommand registerCommand;
        public RelayCommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                {
                    registerCommand = new RelayCommand(ExecuteRegister);
                }
                return registerCommand;
            }
        }
        //Metod till MVVM command
        private void ExecuteRegister(object parameter)
        {
            Register();
        }
        //Metod för att öppna Registerfönstret och stänga MainWindow
        public void Register()
        {

            var registerWindowViewModel = new RegisterWindowViewModel(UserManager.Instance);
            var registerWindow = new RegisterWindow { DataContext = registerWindowViewModel };
            registerWindow.Show();

            App.Current.Windows[0].Close();
        }

        
        //Sign in commands att binda till xaml
        private RelayCommand signInCommand;
        public RelayCommand SignInCommand
        {
            get
            {
                if (signInCommand == null)
                {
                    signInCommand = new RelayCommand(ExecuteSignIn);
                }
                return signInCommand;
            }
        }

        
            //Metod för att öpnna workout window och stänga main window
            public void SignIn()
        {
            var user = userManager.Users.FirstOrDefault(u => u.Username == UsernameInput && u.Password == PasswordInput);

            if (user != null)
               
            {
                userManager.ActiveUser = user;
                var workoutWindowViewModel = new WorkoutWindowViewModel(WorkoutManager.Instance);
                var workoutWindow = new WorkoutWindow { DataContext = workoutWindowViewModel };
                workoutWindow.Show();
                App.Current.Windows[0].Close();
            }  
            else
            {
                MessageBox.Show("Fel user eller lösen");
            }
        }
            //Metod till MVVM command
            public void ExecuteSignIn(object parameter)
        {
            SignIn();
        }
    }
}
