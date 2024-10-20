using FitTracker.Model__Produkter_;
using FitTracker.MVVM;

using FitTracker.View__UI_;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FitTracker.ViewModel_Länk_logik_
{
    public class MainWindowViewModel : ViewModelBase, IMainWindow
    {
        public ObservableCollection<User> Users { get; set; }

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

        private void ExecuteRegister(object parameter)
        {
            Register();
        }
        public string LabelTitle {  get; set; }

        private string usernameInput;
        public string UsernameInput
        {
            get {return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        

        public void Register()
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
        }

        public void SignIn()
        {
            throw new NotImplementedException();
        }



        
    }
}
