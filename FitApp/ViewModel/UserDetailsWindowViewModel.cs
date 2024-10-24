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
        // Egenskaper

        public Window userDetailsWindow;
        public UserManager userManager;
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public List<string> Countries { get; set; }
        public string CountryComboBox { get; set; }

        public ICommand SaveUserDetailsCommand { get; }
        public ICommand CancelCommand { get; }

        // Konstruktor
        public UserDetailsWindowViewModel(Window userDetailsWindow, UserManager userManager) // vilka egenskaper ska jag skicka med konstruktorn?
        {
            this.userManager = userManager;
            userDetailsWindow = userDetailsWindow;
            SaveUserDetailsCommand = new RelayCommand(SaveUserDetails);
            CancelCommand = new RelayCommand(Cancel);
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
