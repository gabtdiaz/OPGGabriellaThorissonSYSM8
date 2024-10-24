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
            MessageBox.Show("Clicked");
            // Kontrollera om lösenorden matchar
            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Försök att återställa lösenordet via UserManager
            bool success = userManager.ResetPassword(Username, NewPassword, SecurityAnswer);

            if (success)
            {
                MessageBox.Show("Password has been reset successfully. Navigating to Workout Window");
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
