using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.ViewModel
{
    public class RegisterWindowViewModel : MainWindowViewModel
    {
        // Egenskaper
        public string ConfirmPasswordInput { get; set; }
        public string CountryComboBox { get; set; }
        // Konstruktor
        public RegisterWindowViewModel(string ConfirmPasswordInput, string CountryComboBox, string PasswordInput, string UsernameInput) : base(PasswordInput, UsernameInput)
        {
            this.ConfirmPasswordInput = ConfirmPasswordInput;
            this.CountryComboBox = CountryComboBox;
        }
        // Metoder
        public void RegisterNewUser() { }
    }
}
