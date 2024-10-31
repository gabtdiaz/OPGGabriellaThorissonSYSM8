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
        // Egenskaper

        public Window forgotPasswordWindow;

        // Referenser
        public Window workoutsWindow;
        private UserManager userManager;

        // Kommando för att återställa lösenord
        public ICommand ResetPasswordCommand { get; }

        // Kommando för att återgå till inloggnigssida
        public ICommand CancelCommand { get; }

        // Egenskaper för binding till UI
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
        public ForgotPasswordWindowViewModel(Window forgotPasswordWindow, UserManager userManager)
        {
            this.userManager = userManager;
            this.forgotPasswordWindow = forgotPasswordWindow;

            // Initiera kommando
            ResetPasswordCommand = new RelayCommand(ResetPassword);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Metod som körs när kommandot anropas
        private void ResetPassword()
        {
            // Första valideringar för att kunna återställa lösenord
            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match","Error",MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        
            if (NewPassword.Length < 8 || !NewPassword.Any(char.IsDigit) || !NewPassword.Any(char.IsPunctuation))
            {
                MessageBox.Show("Password must follow these requirements: \n - Minimun of 8 characters \n - At least one digit \n - At least one special character", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Återställa lösenordet via UserManager
            bool success = userManager.ResetPassword(Username, NewPassword, SecurityAnswer);

            if (success)
            {
                MessageBox.Show("Password has been reset successfully! Logging in..","Success",MessageBoxButton.OK);
                WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager);
                workoutsWindow.Show();
                forgotPasswordWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or security answer.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void Cancel()
        {
            MainWindow mainWindow = new MainWindow(userManager);
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            forgotPasswordWindow.Close();
        }

    }
}
