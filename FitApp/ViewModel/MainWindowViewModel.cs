using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.ViewModel
{
    public class MainWindowViewModel
    {
        // Egenskaper
        public string LabelTitle { get; set; } = "FitTrack";
        public string UsernameInput { get; set; }
        public string PasswordInput { get; set; }

        // Konstruktor
        public MainWindowViewModel(string UsernameInput, string PasswordInput) 
        {
            this.UsernameInput = UsernameInput;
            this.PasswordInput = PasswordInput;
        }

        // Metoder
        public void SignIn() {}

        public void Register() {}
    }
}
