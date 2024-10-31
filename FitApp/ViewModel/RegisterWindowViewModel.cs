using FitApp.Model;
using FitApp.MVVM;
using FitApp.Services;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FitApp;

namespace FitApp.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
        // Egenskaper

        public ObservableCollection<string> CountryComboBox { get; set; }
        
        //Referenser
        public Window registerWindow;

        public UserManager userManager; // För att ha åtkomst till UserManager klassens egenskaper och metoder

        // Egenskaper för användarinmatning

        private string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get
            => confirmPasswordInput;
            set
            {
                confirmPasswordInput = value;
                OnPropertyChanged(nameof(ConfirmPasswordInput));
            }
        }

        private string selectedCountry;
        public string SelectedCountry
        {
            get => selectedCountry;
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private string usernameInput;
        public string UsernameInput
        {
            get => usernameInput;
            set
            {
                usernameInput = value;
                OnPropertyChanged(nameof(UsernameInput));
            }
        }

        private string passwordInput;
        public string PasswordInput
        {
            get => passwordInput;
            set
            {
                passwordInput = value;
                OnPropertyChanged(nameof(PasswordInput));
            }
        }

        // Kommando som anropar metoder
        public ICommand RegisterNewUserCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        // Konstruktor
        public RegisterWindowViewModel(Window registerWindow, UserManager userManager)
        {
            this.registerWindow = registerWindow;
            this.userManager = userManager;
            CountryComboBox = new ObservableCollection<string> { "Sweden", "Norway", "Denmark", "Finland" };

            // Initierar kommando
            RegisterNewUserCommand = new RelayCommand(RegisterNewUser);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Metod för att lägga till nya användare.
        public void RegisterNewUser()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UsernameInput) || 
                    string.IsNullOrWhiteSpace(PasswordInput) || // Kontrollerar ifall textboxarna är tomma
                    string.IsNullOrWhiteSpace(ConfirmPasswordInput) || 
                    SelectedCountry == null)
                {
                    MessageBox.Show("Please enter all the information correctly.", 
                        "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Kontrollera om användarnamnet redan existerar
                if (userManager.FindUser(UsernameInput, null) != null) // null för lösenord, vi bryr oss bara om användarnamnet här
                {
                    MessageBox.Show("The username is already taken. Please choose another username.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Kontrollera att lösenorden matchar
                if (PasswordInput != ConfirmPasswordInput)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }

                // Kontrollera att lösenordet uppfyller kraven
                if (PasswordInput.Length < 8 || !PasswordInput.Any(char.IsDigit) 
                    || !PasswordInput.Any(char.IsPunctuation))
                {
                    MessageBox.Show("Password must follow these requirements: " +
                        "\n - Minimum of 8 characters " +
                        "\n - At least one digit " +
                        "\n - At least one special character", 
                        "Felmeddelande", MessageBoxButton.OK, 
                        MessageBoxImage.Information);
                    return;
                }

                // Skapa ny användare - Sätt till CurrentUser
                User newUser = new User { Country = SelectedCountry, Username = UsernameInput, Password = PasswordInput };
                userManager.Users.Add(newUser);
                userManager.CurrentUser = newUser;

                MessageBox.Show("Account created successfully. Logging in..", "Success");

                WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager);
                workoutsWindow.Show();
                registerWindow.Close();
            }

            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid input: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Operation error: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metod som avbryter och går tillbaka till inloggningssidan
        public void Cancel()
        {
            MainWindow mainWindow = new MainWindow(userManager);
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            registerWindow.Close();
        }
    }
}
