using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitApp.ViewModel
{
    public class RegisterWindowViewModel : MainWindowViewModel
    {
        // Egenskaper
        public string ConfirmPasswordInput { get; set; }

        public ObservableCollection<string> CountryComboBox { get; set; }
        public RelayCommand RegisterCommand { get; }

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
        // Konstruktor
        public RegisterWindowViewModel() 
        {
            CountryComboBox = new ObservableCollection<string> { "Sweden", "Norway", "Denmark", "Finland" };
            RegisterCommand = new RelayCommand(obj => Register());
        }
        // Metoder
        public void Register() 
        {
            if (UsernameInput == "admin")
            {
                MessageBox.Show("Username already taken.");
            }
            else
            {
                User user = new User(); // Måste skapa en ny användare med lösenord och användarnamn
                MessageBox.Show($"User {UsernameInput} created successfully.");
                Application.Current.MainWindow.Close();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
