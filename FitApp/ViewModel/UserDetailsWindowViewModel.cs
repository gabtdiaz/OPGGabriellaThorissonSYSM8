using FitApp.Model;
using FitApp.MVVM;
using FitApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class UserDetailsWindowViewModel : ViewModelBase
    {
        // Referenser
        public Window userDetailsWindow;
        public UserManager userManager;

        // Egenskaper
        public List<string> Countries { get; set; }

        public string CountryComboBox { get; set; }

        // Commands
        public ICommand SaveUserDetailsCommand => new RelayCommand(SaveUserDetails);
        public ICommand CancelCommand => new RelayCommand(Cancel);

        // Egenskaper för användarinmatning
        private string usernameInput;
        public string UsernameInput
        {
            get => usernameInput;
            set { usernameInput = value; OnPropertyChanged(); }
        }

        public string passwordInput;
        public string PasswordInput
        {
            get => passwordInput;
            set { passwordInput = value; OnPropertyChanged(); }
        }

        public string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get => confirmPasswordInput;
            set { confirmPasswordInput = value; OnPropertyChanged(); }
        }

        private string selectedCountry;
        public string SelectedCountry
        {
            get => selectedCountry;
            set { selectedCountry = value; OnPropertyChanged(); }
        }

        // Konstruktor
        public UserDetailsWindowViewModel(Window userDetailsWindow, UserManager userManager) // vilka egenskaper ska jag skicka med konstruktorn?
        {
            this.userManager = userManager;
            this.userDetailsWindow = userDetailsWindow;
            this.userManager = userManager;
        }

        // Metoder

        public void SaveUserDetails() 
        {
            if (PasswordInput == ConfirmPasswordInput)
            {
                // Uppdatera nuvarande användarens uppgifter
                userManager.CurrentUser.Password = PasswordInput;
                userManager.CurrentUser.Username = UsernameInput;
                userManager.CurrentUser.Country = CountryComboBox;

                MessageBox.Show("Details updated successfully.");
            }
            else
            {
                MessageBox.Show("Passwords do not match.");
            }
        }

        public void Cancel() 
        {
            // Gå tillbaka till WorkoutsWindow?
        }
    }
}
