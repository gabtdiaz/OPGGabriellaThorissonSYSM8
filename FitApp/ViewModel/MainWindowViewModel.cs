﻿using FitApp.Model;
using FitApp.MVVM;
using FitApp.Services;
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
        private readonly UserManager userManager;

        private string verifyCode;

        public string LabelTitle { get; set; } = "FitTrack";
        
        // Egenskaper som returnerar användarinmatning
        private string usernameInput; // Backing field
        public string UsernameInput
        {
            get { return usernameInput;}
            set
            {
                usernameInput = value;
                OnPropertyChanged(UsernameInput); // Binding till View
            }
        }

        private string passwordInput; // Backing field

        public string PasswordInput 
        {
            get { return passwordInput; }
            set 
            { 
                passwordInput = value;
                OnPropertyChanged(PasswordInput); // Binding till View
            }
        }

        // Commands 
        public ICommand SignInCommand { get; }
        public ICommand RegisterCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; }

        // Konstruktor
        public MainWindowViewModel(Window mainWindow, UserManager userManager)
        {
            this.userManager = userManager;
            // Commands
            SignInCommand = new RelayCommand(SignIn);
            RegisterCommand = new RelayCommand(Register);
            ForgotPasswordCommand = new RelayCommand(ForgotPassword); 
        }

        // Metoder som hittar användare och stänger MainWindow och öppnar WorkoutWindow vid lyckad inloggning.
        public void SignIn()
        {
            User user = userManager.FindUser(UsernameInput, PasswordInput); // Kontrollera inloggning

            if (user != null)
            {
                Start2FA();
                userManager.CurrentUser = user; // Sätter användaren som inloggad i UserManager
                WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager);
                workoutsWindow.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Incorrect username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metod som öppnar RegisterWindow
        public void Register() 
        {
            // Skapa OCH visa RegisterWindow
            RegisterWindow registerWindow = new RegisterWindow(userManager);
            registerWindow.Show();
            // Stäng ner nuvarande fönster
            Application.Current.MainWindow.Close();
        }

        public void ForgotPassword()
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow(userManager);
            forgotPasswordWindow.Show();
            Application.Current.MainWindow.Close();
        }

        internal void Show()
        {
            throw new NotImplementedException();
        }

        // Metod för att starta tvåfaktorautentisering
        private void Start2FA()
        {
            // Generera en slumpmässig 6-siffrig kod
            verifyCode = GenerateVerificationCode();

            // Skicka koden via "e-post" (simuleras här)
            MessageBox.Show($"Verification code sent: {verifyCode}");

            // Be användaren mata in koden via en popup
            AskForCodeInput();
        }

        // Metod för att generera en sllumpmässig 6-siffrig kod
        private string GenerateVerificationCode()
        {
            // Genererar en slumpmässig 6-siffrig kod
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        // Metod som ber användaren om inmatning av kod
        private void AskForCodeInput()
        {
            // Inmatningsdialog för verifieringskoden
            string userInput = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter verification code that was sent to your E-mail:", "Two-factor authentication","");

            // Kontrollera om koden är korrekt
            if (userInput == verifyCode)
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Wrong code, try again.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

