using FitApp.Model;
using FitApp.MVVM;
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

        public Window _userDetailsWindow;
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }
        public string ConfirmPasswordInput { get; set; }
        public List<string> Countries { get; set; }
        public string CountryComboBox { get; set; }

        public ICommand SaveUserDetailsCommand { get; }
        public ICommand CancelCommand { get; }

        // Konstruktor
        public UserDetailsWindowViewModel(Window userDetailsWindow) // vilka egenskaper ska jag skicka med konstruktorn?
        {

            SaveUserDetailsCommand = new RelayCommand(SaveUserDetails);
            CancelCommand = new RelayCommand(Cancel);
            _userDetailsWindow = userDetailsWindow;
        }

        // Metoder

        public void SaveUserDetails() 
        {
            if (PasswordInput == ConfirmPasswordInput) 
            {
                // Hur ska jag spara usernameinput och passwordInput till användaren?
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
