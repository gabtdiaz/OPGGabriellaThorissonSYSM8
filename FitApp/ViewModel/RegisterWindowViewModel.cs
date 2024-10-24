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
    public class RegisterWindowViewModel : ViewModelBase
    {
        // Egenskaper
        public UserManager userManager;
        public Window registerWindow;
        public ObservableCollection<string> CountryComboBox { get; set; }

        // Command för att skapa ny användare
        public ICommand RegisterNewUserCommand { get; }

        // Egenskaper för användarinmatning

        private string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get
            { return confirmPasswordInput; }
            set
            {
                confirmPasswordInput = value;
                OnPropertyChanged(nameof(ConfirmPasswordInput));
            }
        }

        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private string usernameInput;
        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged(nameof(UsernameInput));
            }
        }

        private string passwordInput;
        

        public string PasswordInput
        {
            get { return passwordInput; }
            set
            {
                passwordInput = value;
                OnPropertyChanged(nameof(PasswordInput));
            }
        }

        // Konstruktor

        public RegisterWindowViewModel(Window registerWindow, UserManager userManager)
        {
            this.registerWindow = registerWindow;
            this.userManager = userManager; // ändrade!! 100000
            CountryComboBox = new ObservableCollection<string> { "Sweden", "Norway", "Denmark", "Finland" };
            // Initiera RegisterNewUserCommand
            RegisterNewUserCommand = new RelayCommand(RegisterNewUser);
        }

        // Metod för att lägga till nya användare.
        public void RegisterNewUser()
        {
            if (string.IsNullOrWhiteSpace(UsernameInput) || string.IsNullOrWhiteSpace(PasswordInput) || // Kontrollerar ifall textboxarna är tomma
            string.IsNullOrWhiteSpace(ConfirmPasswordInput) || SelectedCountry == null)
            {
                MessageBox.Show("Please enter all the information correctly.","Error", MessageBoxButton.OK);
                return;
            }
            if (PasswordInput != ConfirmPasswordInput)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            if (PasswordInput.Length < 8 || !PasswordInput.Any(char.IsDigit) || !PasswordInput.Any(char.IsPunctuation))
            {
                MessageBox.Show("Password must follow these requirements: \n - Minimum of 8 characters \n - At least one digit \n - At least one special character", "Felmeddelande", MessageBoxButton.OK);
                return;
            }

            User newUser = new User { Country = SelectedCountry, Username = UsernameInput, Password = PasswordInput };
            userManager.Users.Add(newUser);
            userManager.CurrentUser = newUser; // Ska sätta den nya användaren till currentUser - alt bara spara till Users listan.

            MessageBox.Show("Account created successfully. Logging in..");

            WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager); // TOG BORT PARAMETER
            workoutsWindow.Show();
            registerWindow.Close();
            }
        }
    }
