using FitApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.ViewModel
{
    public class UserDetailsWindowViewModel : RegisterWindowViewModel
    {
        // Konstruktor
        public UserDetailsWindowViewModel(string UsernameInput, string PasswordInput, string ConfirmPasswordInput, string CountryComboBox) : base (UsernameInput, PasswordInput, ConfirmPasswordInput, CountryComboBox) { }

        // Metoder

        public void SaveUserDetails() { }

        public void Cancel() { }
    }
}
