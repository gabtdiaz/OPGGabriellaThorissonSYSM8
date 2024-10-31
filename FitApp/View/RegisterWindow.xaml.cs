using FitApp.Services;
using FitApp.ViewModel;
using System.Windows;

namespace FitApp.View
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow(UserManager userManager)
        {
            InitializeComponent();
            this.DataContext = new RegisterWindowViewModel(this, userManager);
        }
    }
}


