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
    public class UserDetailsWindowViewModel : ViewModelBase
    {
        // Egenskaper 

        private readonly Window userDetailsWindow;

        // Referenser 
        private readonly WorkoutsWindowViewModel workoutsWindow;
        private readonly RegisterWindowViewModel registerWindow;
        private readonly UserManager userManager;
        public ObservableCollection<string> CountryComboBox
        {
            get { return registerWindow.CountryComboBox; }
        }

        // Egenskaper för användarinmatning

        private string currentUsername;
        public string CurrentUsername
        {
            get { return currentUsername; }
            set
            {
                if (currentUsername != value)
                {
                    currentUsername = value;
                    OnPropertyChanged(nameof(CurrentUsername));
                }
            }
        }

        private string currentPassword;
        public string CurrentPassword
        {
            get { return currentPassword; }
            set
            {
                if (currentPassword != value)
                {
                    currentPassword = value;
                    OnPropertyChanged(nameof(CurrentPassword));
                }
            }
        }

        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                if (selectedCountry != value)
                {
                    selectedCountry = value;
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        private string newUsername;
        public string NewUsername
        {
            get { return newUsername; }
            set
            {
                if (newUsername != value)
                {
                    newUsername = value;
                    OnPropertyChanged(nameof(NewUsername));
                }
            }
        }

        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    OnPropertyChanged(nameof(NewPassword));
                }
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }

        // Commands 

        public ICommand SaveUserDetailsCommand { get; }
        public ICommand CancelCommand { get; }

        public UserDetailsWindowViewModel(Window userDetailsWindow, UserManager userManager)
        {
            this.userDetailsWindow = userDetailsWindow;
            this.userManager = userManager;

            // Initiera egenskaperna från userManager
            currentUsername = userManager.CurrentUser.Username;
            currentPassword = userManager.CurrentUser.Password;
            selectedCountry = userManager.CurrentUser.Country;

            SaveUserDetailsCommand = new RelayCommand(SaveUserDetails);
            CancelCommand = new RelayCommand(Cancel);
        }


        private void SaveUserDetails()
        {

            if (newPassword == confirmPassword)
            {

                // Uppdatera användarens uppgifter
                if (currentUsername != newUsername)
                    userManager.CurrentUser.Username = newUsername;

                if (currentPassword != newPassword)
                    userManager.CurrentUser.Password = newPassword;

                if (selectedCountry != userManager.CurrentUser.Country)
                    userManager.CurrentUser.Country = selectedCountry;

                // Stäng fönstret
               // registerWindow.RegisterNewUser(); //behöver jag denna? eller ska jag lägga till i Useramanager istället
               
            }
            else
            {
                MessageBox.Show("Something went wrong, please try again", "Error", MessageBoxButton.OK);
            }


            WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager);
            workoutsWindow.Show();
            // Stäng fönstret
            userDetailsWindow.Close();

        }

        private void Cancel()
        {
            // Stäng fönstret utan att spara ändringar
            userDetailsWindow.Close();


        }
    }
}
