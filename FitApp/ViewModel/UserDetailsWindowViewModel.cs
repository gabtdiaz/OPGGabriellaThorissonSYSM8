using FitApp.Model;
using FitApp.MVVM;
using FitApp.Services;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class UserDetailsWindowViewModel : ViewModelBase
    {
        // Egenskaper 

        private readonly Window userDetailsWindow;

        // Referenser 
        private readonly WorkoutsWindowViewModel workoutsWindow;
        private readonly RegisterWindowViewModel registerWindow;
        private readonly UserManager userManager;

        // Egenskaper för användarinmatning
        public ObservableCollection<string> Countries { get; set; }

        private string selectedCountry;
        public string SelectedCountry
        {
            get => selectedCountry;
            set
            {
                if (selectedCountry != value)
                {
                    selectedCountry = value;
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        private string currentUsername;
        public string CurrentUsername
        {
            get => currentUsername;
            set
            {
                if (currentUsername != value)
                {
                    currentUsername = value;
                    OnPropertyChanged(nameof(CurrentUsername));
                }
            }
        }

        private string currentPassword;
        public string CurrentPassword
        {
            get => currentPassword;
            set
            {
                if (currentPassword != value)
                {
                    currentPassword = value;
                    OnPropertyChanged(nameof(CurrentPassword));
                }
            }
        }

        private string newUsername;
        public string NewUsername
        {
            get => newUsername;
            set
            {
                if (newUsername != value)
                {
                    newUsername = value;
                    OnPropertyChanged(nameof(NewUsername));
                }
            }
        }

        private string newPassword;
        public string NewPassword
        {
            get => newPassword; 
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    OnPropertyChanged(nameof(NewPassword));
                }
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }

        // Kommando som anropar metoder 

        public ICommand SaveUserDetailsCommand { get; }
        public ICommand CancelCommand { get; }

        public UserDetailsWindowViewModel(Window userDetailsWindow, UserManager userManager, RegisterWindowViewModel registerWindow)
        {
            this.userDetailsWindow = userDetailsWindow;
            this.userManager = userManager;
            this.registerWindow = registerWindow;
            Countries = new ObservableCollection<string> { "Sweden", "Norway", "Denmark", "Finland" };

            // Initiera egenskaperna från userManager
            currentUsername = userManager.CurrentUser.Username;
            currentPassword = userManager.CurrentUser.Password;
            selectedCountry = userManager.CurrentUser.Country;

            // Initiera kommando
            SaveUserDetailsCommand = new RelayCommand(SaveUserDetails);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Metod som validerar användaruppgifter
        private bool ValidateInput()
        {
            // Validering av användarnamn
            if (string.IsNullOrEmpty(NewUsername) || NewUsername.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long.",
                              "Validation Error", MessageBoxButton.OK,MessageBoxImage.Warning);
                return false;
            }

            // Kollar ifall användarnamn redan existerar
            if (userManager.Users.Any(u => u.Username.Equals(NewUsername, StringComparison.OrdinalIgnoreCase))
                && !NewUsername.Equals(currentUsername, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Username already exists. Please choose another username.",
                              "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validering av lösenord
            if (!string.IsNullOrEmpty(NewPassword))
            {
                if (NewPassword.Length < 8 || !NewPassword.Any(char.IsDigit) || !NewPassword.Any(char.IsPunctuation))
                {
                    MessageBox.Show("Password must follow these requirements: " +
                        "\n - Minimun of 8 characters \n - At least one digit " +
                        "\n - At least one special character", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }

                if (NewPassword != ConfirmPassword)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            return true;
        }

        // Metod som sparar användaruppgifter
        private void SaveUserDetails()
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                // Uppdaterar användarnamn och lösenord
                if (!string.IsNullOrEmpty(NewUsername))
                {
                    userManager.CurrentUser.Username = NewUsername;
                }

                if (!string.IsNullOrEmpty(NewPassword))
                {
                    userManager.CurrentUser.Password = NewPassword;
                }

                if (selectedCountry != userManager.CurrentUser.Country)
                {
                    userManager.CurrentUser.Country = SelectedCountry;
                }

                MessageBox.Show("User details updated successfully!",
                              "Success", MessageBoxButton.OK,
                              MessageBoxImage.Information);

                // Öppnar WorkoutsWindow och stänger nuvarande fönster
                WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(userManager);
                newWorkoutsWindow.Show();
                userDetailsWindow?.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving user details: {ex.Message}",
                              "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metod som avbryter och går tillbaka till WorkoutsWindow
        private void Cancel()
        {
            WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(userManager);
            newWorkoutsWindow.Show();
            userDetailsWindow?.Close();
        }
    }
}