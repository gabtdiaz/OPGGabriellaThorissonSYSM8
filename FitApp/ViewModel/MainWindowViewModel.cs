﻿using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace FitApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase // Måste ärva från ViewModelBase klassen för att kunna använda sig av OnPropertyChanged()
    {
        // Egenskaper
        public string LabelTitle1 { get; set; } = "FitTrack"; // Sätter standardvärde

        public ICommand SignInCommand { get; }
        public ICommand RegisterCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; }

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
                OnPropertyChanged(UsernameInput);
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
                OnPropertyChanged(PasswordInput);
            }
        }

        // Konstruktor
        public MainWindowViewModel()
        {
            SignInCommand = new RelayCommand(SignIn);
            RegisterCommand = new RelayCommand(Register);
            ForgotPasswordCommand = new RelayCommand(ForgotPassword);
        }

        // Metoder som öppnar och stänger fönster
        public void SignIn() 
        {
            if (UsernameInput != null)
            {
                if (usernameInput == "admin" && passwordInput == "abcd1234")
                {
                    WorkoutsWindow workoutsWindow = new WorkoutsWindow();
                    workoutsWindow.Show();
                    Application.Current.MainWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("Kontot existerar inte. Registrera nytt konto.", "Felmeddelande",MessageBoxButton.OK);
            } 
        }

        public void Register() 
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.MainWindow.Close();
        }

        public void ForgotPassword()
        {
            MessageBox.Show("Forgot clicked");
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
