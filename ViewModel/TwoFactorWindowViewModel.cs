using FitTracker.MVVM;
using FitTracker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTracker
{
    public class TwoFactorWindowViewModel : ViewModelBase
    {
        //ett event för att trigga login på mainwindow om 2fa är godkänd
        public event Action UserLoggedIn;
        #region Constructor
        //konstruktor
        public TwoFactorWindowViewModel(string expectedCode)
        {
            this.expectedCode = expectedCode;
            SubmitCommand = new RelayCommand(ExecuteSubmit);
        }
        #endregion
        #region Properties
        //Fields
        private string twoFactorInput;
        public string TwoFactorInput
        {
            get { return twoFactorInput; }
            set
            {
                twoFactorInput = value;
                OnPropertyChanged(nameof(TwoFactorInput));
            }
        }
        private readonly string expectedCode;
        #endregion
        #region Relay Commands
        //Relay Commands
        public RelayCommand SubmitCommand { get; }
        #endregion
        #region Method
        //Triggar Userloggedin eventet som login metoden i mainwindow prenumererar på
        //om rätt 2fa kod har blivit inmatad
        private void ExecuteSubmit(object parameter)
        {
            if (TwoFactorInput == expectedCode)
            {
                UserLoggedIn?.Invoke();
                
                if (App.Current.Windows.OfType<TwoFactorWindow>().FirstOrDefault() is Window twoFactorWindow)
                {
                    twoFactorWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid 2FA code.");
            }
        }
        #endregion
    }
}
