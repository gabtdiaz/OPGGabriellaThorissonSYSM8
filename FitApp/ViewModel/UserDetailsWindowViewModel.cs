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
        public ObservableCollection<string> Countries // Egenskap för att få tillgång till länder i RegisterWindow
        {
            get { return registerWindow.CountryComboBox; }
        }

        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
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
            get { return currentUsername; }
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
            get { return currentPassword; }
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
            get { return newUsername; }
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
            get { return newPassword; }
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
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }

        // Commands 

        public ICommand SaveUserDetailsCommand { get; }
        public ICommand CancelCommand { get; }

        public UserDetailsWindowViewModel(Window userDetailsWindow, UserManager userManager, RegisterWindowViewModel registerWindow)
        {
            this.userDetailsWindow = userDetailsWindow;
            this.userManager = userManager;
            this.registerWindow = registerWindow;
            // Initiera egenskaperna från userManager
            currentUsername = userManager.CurrentUser.Username;
            currentPassword = userManager.CurrentUser.Password;
            selectedCountry = userManager.CurrentUser.Country;

            SaveUserDetailsCommand = new RelayCommand(SaveUserDetails);
            CancelCommand = new RelayCommand(Cancel);
        }


        private bool ValidateInput()
        {
            // Validate username
            if (string.IsNullOrEmpty(NewUsername) || NewUsername.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long.",
                              "Validation Error", MessageBoxButton.OK,MessageBoxImage.Warning);
                return false;
            }

            // Check if username already exists
            if (userManager.Users.Any(u => u.Username.Equals(NewUsername, StringComparison.OrdinalIgnoreCase))
                && !NewUsername.Equals(currentUsername, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Username already exists. Please choose another username.",
                              "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate password
            if (!string.IsNullOrEmpty(NewPassword))
            {
                if (NewPassword.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters long.",
                                  "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (!NewPassword.Any(char.IsLetter) || !NewPassword.Any(char.IsDigit))
                {
                    MessageBox.Show("Password must contain at least one letter and one number.",
                                  "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void SaveUserDetails()
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                // Uppdatera användarnamn och lösenord
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
                    userManager.CurrentUser.Country = selectedCountry;
                }

                MessageBox.Show("User details updated successfully!",
                              "Success",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);

                // Open WorkoutsWindow and close current window
                WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager);
                workoutsWindow.Show();
                userDetailsWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving user details: {ex.Message}",
                              "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel()
        {
            // Open WorkoutsWindow and close current window
            WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager);
            workoutsWindow.Show();
            userDetailsWindow.Close();

        }
    }
}