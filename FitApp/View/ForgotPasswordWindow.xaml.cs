using FitApp.Services;
using FitApp.ViewModel;
using System.Windows;

namespace FitApp.View
{
    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow(UserManager userManager)
        {
            InitializeComponent();
            this.DataContext = new ForgotPasswordWindowViewModel(this, userManager);
        }
    }
}
