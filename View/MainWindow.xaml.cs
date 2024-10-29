using FitTracker.Model__Produkter_;
using FitTracker.ViewModel;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(UserManager.Instance);
            DataContext = mainWindowViewModel;
        }

        private void PasswordInput(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                var passwordBox = sender as PasswordBox;
                viewModel.PasswordInput = passwordBox.Password;
            }

        }
    }
}