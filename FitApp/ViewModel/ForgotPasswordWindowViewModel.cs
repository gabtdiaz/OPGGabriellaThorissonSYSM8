using FitApp.MVVM;
using FitApp.Services;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class ForgotPasswordWindowViewModel : ViewModelBase
    {
        public Window forgotPasswordWindow;
        public Window workoutsWindow;
        // Egenskaper 
        private UserManager userManager;

        // Command för att återställa lösenord
        public ICommand ResetPasswordCommand { get; }

        // Egenskaper för användarinmatning
        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(Username);
            }
        }

        private string newPassword;

        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                OnPropertyChanged(NewPassword);
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged(ConfirmPassword);
            }
        }

        //private string securityQuestion;

        //public string SecurityQuestion
        //{
        //    get { return securityQuestion; }
        //    set 
        //    { 
        //        securityQuestion = value; 
        //        OnPropertyChanged(SecurityQuestion);
        //    }
        //}


        private string securityAnswer;

        public string SecurityAnswer
        {
            get { return securityAnswer; }
            set
            {
                securityAnswer = value;
                OnPropertyChanged(SecurityAnswer);
            }
        }

        // Konstruktor
        public ForgotPasswordWindowViewModel(Window forgotPasswordWindow, UserManager userManager) // ändra från UserManager till Window
        {

            this.userManager = userManager;
            this.forgotPasswordWindow = forgotPasswordWindow;

            // Initiera ResetPasswordCommand
            ResetPasswordCommand = new RelayCommand(ResetPassword);
            
        }

        // Metod som körs när kommandot anropas
        private void ResetPassword()
        {
            // Kontrollera om lösenorden matchar
            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match","Error",MessageBoxButton.OK);
                return;
            }
        
            if (NewPassword.Length < 8 || !NewPassword.Any(char.IsDigit) || !NewPassword.Any(char.IsPunctuation))
            {
                MessageBox.Show("Password must follow these requirements: \n - Minimun of 8 characters \n - At least one digit \n - At least one special character", "Error", MessageBoxButton.OK);
                return;
            }
            // Försök att återställa lösenordet via UserManager
            bool success = userManager.ResetPassword(Username, NewPassword, SecurityAnswer);

            if (success)
            {
                MessageBox.Show("Password has been reset successfully! Logging in..");
                WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager);
                workoutsWindow.Show();
                Application.Current.MainWindow.Close(); // fönstret stängs ej.
            }
            else
            {
                MessageBox.Show("Invalid username or security answer.");
            }
        }
    }
}
