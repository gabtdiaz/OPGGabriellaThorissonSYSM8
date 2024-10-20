using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase // Måste ärva från ViewModelBase klassen för att kunna använda sig av OnPropertyChanged()
    {
        // Egenskaper
        public string LabelTitle { get; set; } = "FitTrack"; // Sätter standarDvärde
        public string UsernameInput { get; set; } = string.Empty;
        public string PasswordInput { get; set; } = string.Empty;

        public RelayCommand SignInCommand { get; }
        public RelayCommand RegisterCommand { get; }

        public RelayCommand ForgotPassword {  get; }

        // Konstruktor
        public MainWindowViewModel()
        {
            SignInCommand = new RelayCommand(obj => SignIn());
            RegisterCommand = new RelayCommand(obj => Register());
        }

        // Metoder
        public void SignIn() 
        {
            if (UsernameInput == "admin" && PasswordInput == "password")
            {
                WorkoutsWindow workouts = new WorkoutsWindow();
                workouts.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        public void Register() 
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
