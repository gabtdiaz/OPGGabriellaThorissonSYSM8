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
        public ObservableCollection<string> CountryComboBox { get; set; }

        public UserManager userManager; // får den vara här?

        private string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get
            {
                return confirmPasswordInput;
            }
            set
            {
                confirmPasswordInput = value;
                OnPropertyChanged(nameof(ConfirmPasswordInput));
            }
        }

        private string selectedCountry;
        public string SelectedCountry
        {
            get
            {
                return selectedCountry;
            }
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private string usernameInput;
        public string UsernameInput
        {
            get
            {
                return usernameInput;
            }
            set
            {
                usernameInput = value;
                OnPropertyChanged(nameof(UsernameInput));
            }
        }

        private string passwordInput;
        public string PasswordInput
        {
            get
            {
                return passwordInput;
            }
            set
            {
                passwordInput = value;
                OnPropertyChanged(nameof(PasswordInput));
            }
        }

        public ICommand RegisterNewUserCommand { get; }
        // Konstruktor

        public RegisterWindowViewModel(UserManager userManager) // måste man ha denna som parameter?
        {
            this.userManager = userManager; 
            CountryComboBox = new ObservableCollection<string> { "Sweden", "Norway", "Denmark", "Finland" };
            RegisterNewUserCommand = new RelayCommand(RegisterNewUser);
        }
        // Metod för att lägga till nya användare.
        public void RegisterNewUser()
        {
            if (string.IsNullOrWhiteSpace(UsernameInput) || string.IsNullOrWhiteSpace(PasswordInput) ||
            string.IsNullOrWhiteSpace(ConfirmPasswordInput) || SelectedCountry == null)
            {
                MessageBox.Show("Please enter information correctly.","Error", MessageBoxButton.OK);
                return;
            }
            if (PasswordInput != ConfirmPasswordInput)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            if (PasswordInput.Length < 8 && !PasswordInput.Any(char.IsDigit) && !PasswordInput.Any(char.IsSymbol))
            {
                MessageBox.Show("Password must follow these requirements: \n - Minimun of 8 characters \n - At least one digit \n - At least one special character", "Felmeddelande", MessageBoxButton.OK);
                return;
            }
            else
            {
                userManager.Users.Add(new User { Country = SelectedCountry, Username = UsernameInput, Password = PasswordInput });
                
                // Tömmer textboxarna på innehåll.
                //UsernameInput = "";
                //PasswordInput = "";
                //ConfirmPasswordInput = "";
                //SelectedCountry = null;

                MessageBox.Show("Account created successfully. Navigating back to HomePage");
                
                // måste jag skapa nytt objekt varje gång jag vill öppna nytt fönster?
                MainWindowViewModel mainWindowView = new MainWindowViewModel();
                mainWindowView.Show();
            }
        }
    }
}
