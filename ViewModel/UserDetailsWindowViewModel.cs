using FitTracker.Model__Produkter_;
using FitTracker.View;
using FitTracker.View__UI_;
using System.Windows;

namespace FitTracker.ViewModel
{
    public class UserDetailsWindowViewModel : IUserDetailsWindow
    {
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }

        public UserDetailsWindowViewModel()
        {
            var user = UserManager.Instance.ActiveUser;
            UsernameInput = user.Username;
            CountryComboBox = user.Country;
        }

        public void Cancel()
        {
            if (App.Current.Windows.OfType<UserDetailsWindow>().FirstOrDefault() is Window userDetailsWindow)
            {
                userDetailsWindow.Close();
            }
        }

        public void SaveUserDetails()
        {
            var user = UserManager.Instance.ActiveUser;
            
            user.Username = UsernameInput;
            //kontrollera att lösenorden matchar.
            // PasswordInput == ConfirmPasswordInput
            user.Password = PasswordInput;

            user.Country = CountryComboBox;

            
        }
    }
}
