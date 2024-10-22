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
        public string ConfirmPasswordInput { get; set; }
        public ObservableCollection<string> CountryComboBox { get; set; }
        public ICommand RegisterNewUserCommand { get; }

        public UserManager UserManager;

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

        // Konstruktor
        public RegisterWindowViewModel(UserManager UserManager) 
        {
            this.UserManager = UserManager;
            CountryComboBox = new ObservableCollection<string> { "Sweden", "Norway", "Denmark", "Finland" };
            RegisterNewUserCommand = new RelayCommand(RegisterNewUser);
        }
        // Metoder
        public void RegisterNewUser() 
        {
            User newUser = new User()
            {
                Country = SelectedCountry,
                Username = UsernameInput,
                Password = PasswordInput,
            };   

            
        }
    }
}
