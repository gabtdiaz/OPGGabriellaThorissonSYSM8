using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase // Måste ärva från ViewModelBase klassen för att kunna använda sig av OnPropertyChanged()
    {
        // Egenskaper
        public string LabelTitle { get; set; } = "FitTrack"; // Sätter standarDvärde
        public string UsernameInput { get; set; } = string.Empty;
        public string PasswordInput { get; set; } = string.Empty;

        public RelayCommand SignInCommand { get; }
        public RelayCommand RegisterCommand { get; }

        public RelayCommand ForgotPasswordCommand {  get; }

        // Konstruktor
        public MainWindowViewModel()
        {
            SignInCommand = new RelayCommand(obj => SignIn());
            RegisterCommand = new RelayCommand(obj => Register());
            ForgotPasswordCommand = new RelayCommand(obj => ForgotPassword());
        }

        // Metoder
        public void SignIn() 
        {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            Application.Current.MainWindow.Close();
        }

        public void Register() 
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.MainWindow.Close();
        }

        public void ForgotPassword()
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
