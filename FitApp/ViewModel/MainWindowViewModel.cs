using FitApp.Model;
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
                userManager.CurrentUser = user; // Sätter användaren som inloggad i UserManager
                WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager);
                workoutsWindow.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Incorrect username or password.", "Login Failed", MessageBoxButton.OK);
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
            ForgotPasswordWindowVievModel forgotPasswordWindow = new ForgotPasswordWindowVievModel(userManager);
            forgotPasswordWindow.Show();
            Application.Current.MainWindow.Close();
        }

        internal void Show()
        {
            throw new NotImplementedException();
        }
    }
}
