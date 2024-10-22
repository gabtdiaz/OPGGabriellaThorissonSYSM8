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
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
        // Egenskaper
        public string ConfirmPasswordInput { get; set; }
        public ObservableCollection<string> CountryComboBox { get; set; }
        public ICommand RegisterNewUserCommand { get; }

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
            RegisterNewUserCommand = new RelayCommand(RegisterNewUser);
        }
        // Metoder
        public void RegisterNewUser() 
        {                
                Application.Current.MainWindow.Close();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            
        }
    }
}
