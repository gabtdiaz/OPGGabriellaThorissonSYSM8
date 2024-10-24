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
        Window forgotPasswordWindow;
        // Egenskaper 
        private readonly UserManager userManager;

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
        public ForgotPasswordWindowViewModel(UserManager userManager)
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
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Försök att återställa lösenordet via UserManager
            bool success = userManager.ResetPassword(Username, SecurityAnswer, NewPassword);

            if (success)
            {
                MessageBox.Show("Password has been reset successfully.");
            }
            else
            {
                MessageBox.Show("Invalid username or security answer.");
            }
        }

    }
}
