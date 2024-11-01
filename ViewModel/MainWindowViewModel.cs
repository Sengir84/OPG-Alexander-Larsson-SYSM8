using FitTracker.Model__Produkter_;
using FitTracker.MVVM;
using FitTracker.View;
using FitTracker.View__UI_;
using FitTracker.ViewModel.WorkoutViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FitTracker.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IMainWindow
    {
        private UserManager userManager;
        private User currentUser;
        public MainWindowViewModel() : this(UserManager.Instance) { }
        public MainWindowViewModel(UserManager userManager)
        {
            this.userManager = userManager;
            FontFamily = "Arial";

        }
        public RelayCommand AddUserCommand { get; }
        public string LabelTitle { get; set; }

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
        private string fontFamily;

        public string FontFamily
        {
            get { return fontFamily; }
            set
            {
                fontFamily = value;
                OnPropertyChanged(nameof(FontFamily));
            }
        }

        
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
        private string securityAnswerInput;
        public string SecurityAnswerInput
        {
            get { return securityAnswerInput; }
            set
            {
                securityAnswerInput = value;
                OnPropertyChanged(nameof(SecurityAnswerInput));
            }
        }
        private string securityQuestion;
        public string SecurityQuestion
        {
            get { return securityQuestion; }
            set
            {
                securityQuestion = value;
                OnPropertyChanged(nameof(SecurityQuestion));
            }
        }


        private string newPasswordInput;
        public string NewPasswordInput
        {
            get { return newPasswordInput; }
            set
            {
                newPasswordInput = value;
                OnPropertyChanged(nameof(NewPasswordInput));
            }
        }

        private string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get => confirmPasswordInput;
            set { confirmPasswordInput = value; OnPropertyChanged(); }
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            NewPasswordInput = passwordBox.Password;  
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            ConfirmPasswordInput = passwordBox.Password;  
        }
        
        

        private bool isQuestionVisible;
        public bool IsQuestionVisible
        {
            get => isQuestionVisible;
            set { isQuestionVisible = value; OnPropertyChanged(); }
        }

        private bool isPasswordResetVisible;
        public bool IsPasswordResetVisible
        {
            get => isPasswordResetVisible;
            set { isPasswordResetVisible = value; OnPropertyChanged(); }
        }


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
        private RelayCommand resetPasswordCommand;
        public RelayCommand ResetPasswordCommand
        {
            get
            {
                if (resetPasswordCommand == null)
                {
                    resetPasswordCommand = new RelayCommand(ExecuteResetPassword);
                }
                return resetPasswordCommand;
            }
        }
        public RelayCommand ForgotPasswordCommand => new RelayCommand(ExecuteForgotPassword);
        public RelayCommand ChangePasswordCommand => new RelayCommand(ExecuteChangePassword);
        private void ExecuteForgotPassword(object obj)
        {
            currentUser = userManager.Users.FirstOrDefault(u => u.Username == UsernameInput);

            if (currentUser != null)
            {
                SecurityQuestion = currentUser.SecurityQuestion;
                IsQuestionVisible = true;  
            }
            else
            {
                MessageBox.Show("Enter username in user field before pushing the button");
            }
        }
        
        private void ExecuteChangePassword(object obj)
        {
            var currentUser = userManager.Users.FirstOrDefault(u => u.Username == UsernameInput);

            if (currentUser != null)
            {
                
                if (currentUser.VerifySecurityAnswer(SecurityAnswerInput))
                {
                    
                    IsPasswordResetVisible = true;
                    IsQuestionVisible = false;  
                }
                else
                {
                    MessageBox.Show("Incorrect security answer. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("User not found.");
            }
        }



        private void ExecuteRegister(object parameter)
        {
            Register();
        }
        
        public void Register()
        {

            var registerWindowViewModel = new RegisterWindowViewModel(UserManager.Instance);
            var registerWindow = new RegisterWindow { DataContext = registerWindowViewModel };
            registerWindow.Show();

            App.Current.Windows[0].Close();
        }

        private void ExecuteResetPassword(object obj)
        {
            ResetPassword(); 
        }

        private void ResetPassword()
        {
            if (NewPasswordInput == ConfirmPasswordInput)
            {

                //var user = userManager.Users.FirstOrDefault(u => u.Username == UsernameInput);

                bool isValidPassword = userManager.ValidPassword(NewPasswordInput);
                if (isValidPassword)
                {
                    var user = userManager.Users.FirstOrDefault(u => u.Username == UsernameInput);

                    if (user != null)
                    {
                        user.ResetPassword(NewPasswordInput);
                        MessageBox.Show("Password reset successful. Please log in with your new password.");

                        NewPasswordInput = string.Empty;
                        ConfirmPasswordInput = string.Empty;
                        IsPasswordResetVisible = false;
                        IsQuestionVisible = false;

                    }
                    else
                    {
                        MessageBox.Show("Username not found.");
                    }
                }
                else
                {
                    MessageBox.Show("Password must be at least 8 characters long and contain at least one special character.");
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match. Please try again.");
            }
        }

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
        
        
        public void SignIn()
        {
            var user = userManager.Users.FirstOrDefault(u => u.Username == UsernameInput && u.Password == PasswordInput);

            if (user != null)
            {
                Random twofa = new Random();
                string doublecheck = twofa.Next(100000, 999999).ToString();
                
                MessageBox.Show($"You´ve got mail! Your 2FA code is {doublecheck}");
                var twoFactorViewModel = new TwoFactorWindowViewModel(doublecheck);
                var twoFactorWindow = new TwoFactorWindow(doublecheck) { DataContext = twoFactorViewModel };


                twoFactorViewModel.UserLoggedIn += () =>
                {
                    user.SignIn();
                    var workoutWindowViewModel = new WorkoutWindowViewModel(WorkoutManager.Instance);
                    var workoutWindow = new WorkoutWindow { DataContext = workoutWindowViewModel };
                    workoutWindow.Show();
                    App.Current.Windows[0].Close();
                };

                twoFactorWindow.ShowDialog();
            }

            else
            {
                MessageBox.Show("Wrong user or password");
            }
        }
            
            public void ExecuteSignIn(object parameter)
        {
            SignIn();
        }
    }
}
