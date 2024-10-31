using FitApp.Services;
using FitApp.ViewModel;
using System.Windows;

namespace FitApp.View
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow(UserManager userManager, RegisterWindowViewModel registerWindow)
        {
            InitializeComponent();
            this.DataContext = new UserDetailsWindowViewModel(this, userManager, registerWindow); 
        }
    }
}

