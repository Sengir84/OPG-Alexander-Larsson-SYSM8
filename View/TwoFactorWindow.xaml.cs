using FitTracker.Model__Produkter_;
using FitTracker.ViewModel;
using Microsoft.VisualBasic;
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

namespace FitTracker.View
{
    /// <summary>
    /// Interaction logic for TwoFactorWindow.xaml
    /// </summary>
    public partial class TwoFactorWindow : Window
    {
        public TwoFactorWindow(string expectedCode)
        {
            InitializeComponent();
            var viewModel = new TwoFactorWindowViewModel(expectedCode);
            DataContext = viewModel;

            
            viewModel.UserLoggedIn += () =>
            {
                this.DialogResult = true; 
                Close(); 
            };
        }

    }
}
