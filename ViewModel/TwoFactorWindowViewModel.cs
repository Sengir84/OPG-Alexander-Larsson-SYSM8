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
        public event Action UserLoggedIn;
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

        public RelayCommand SubmitCommand { get; }

        public TwoFactorWindowViewModel(string expectedCode)
        {
            this.expectedCode = expectedCode;
            SubmitCommand = new RelayCommand(ExecuteSubmit);
        }

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
    }
}
