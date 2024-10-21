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
        public string LabelTitle1 { get; set; } = "FitTrack"; // Sätter standardvärde

        public string UsernameInput { get; set; } = string.Empty;
        public string PasswordInput { get; set; }

        public ICommand SignInCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        // Konstruktor
        public MainWindowViewModel()
        {
            SignInCommand = new RelayCommand(SignIn);
            ForgotPasswordCommand = new RelayCommand(ForgotPassword);
        }

        // Metoder som öppnar och stänger fönster
        public void SignIn() 
        {
            
            MessageBox.Show("Sign in Clicked");
            //SignInWindow signInWindow = new SignInWindow();
            //signInWindow.Show();
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
            MessageBox.Show("Forgot clicked");
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
