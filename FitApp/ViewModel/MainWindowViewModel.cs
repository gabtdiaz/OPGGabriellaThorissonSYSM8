using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase // Måste ärva från ViewModelBase klassen för att kunna använda sig av OnPropertyChanged()
    {
        // Egenskaper
        public string LabelTitle { get; set; } = "FitTrack"; // Sätter standarDvärde
        public string UsernameInput { get; set; } = string.Empty;
        public string PasswordInput { get; set; } = string.Empty;

        public ICommand SignInCommand { get; }

        // Konstruktor
        public MainWindowViewModel()
        {
            SignInCommand = new RelayCommand(SignIn);
        }

        // Metoder som öppnar och stänger fönster
        public void SignIn() 
        {
            //User user = new User ( { UsernameInput,  PasswordInput });
            //user.SignIn();
            MessageBox.Show("Sign in Clicked");
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            //Application.Current.MainWindow.Close();
        }

        public void Register() 
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.MainWindow.Close();
        }

        public void ForgotPassword()
        {
            MessageBox.Show("Forgot clicked");
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.Show();
            //Application.Current.MainWindow.Close();
        }
    }
}
