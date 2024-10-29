using FitTracker.Model__Produkter_;
using FitTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitTracker.View__UI_
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            RegisterWindowViewModel registerWindowViewModel = new RegisterWindowViewModel(UserManager.Instance);
            DataContext = registerWindowViewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterWindowViewModel viewModel)
            {
                var passwordBox = sender as PasswordBox;
                viewModel.PasswordInput = passwordBox.Password;
            }
            
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterWindowViewModel viewModel)
            {
                var passwordBox = sender as PasswordBox;
                viewModel.ConfirmPasswordInput = passwordBox.Password;
            }
        }
    }
}
