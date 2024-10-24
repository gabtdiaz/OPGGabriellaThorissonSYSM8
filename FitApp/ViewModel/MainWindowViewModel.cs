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
        //public Window _mainWindow;
        //public Window _registerWindow;
        //public Window _workoutsWindow;
        UserManager userManager = new UserManager();

        public string LabelTitle { get; set; } = "FitTrack"; // Sätter standardvärde

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
        public MainWindowViewModel(Window mainWindow)
        {
            SignInCommand = new RelayCommand(SignIn);
            RegisterCommand = new RelayCommand(Register);
            ForgotPasswordCommand = new RelayCommand(ForgotPassword);
            //_mainWindow = mainWindow;   
        }

        // Metoder som hittar användare och stänger MainWindow och öppnar WorkoutWindow vid lyckad inloggning.
        public void SignIn() 
        {
            User user = userManager.FindUser(UsernameInput, PasswordInput); // Anropar metoden i UserManager

            if (user != null)
            {
                // Användare hittad
                WorkoutsWindow workoutsWindow = new WorkoutsWindow(userManager.CurrentUser);
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
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            // Stäng ner nuvarande fönster
            Application.Current.MainWindow.Close();
        }

        public void ForgotPassword()
        {
            MessageBox.Show("Forgot clicked");
            ForgotPasswordWindowVievModel forgotPasswordWindow = new ForgotPasswordWindowVievModel();
            forgotPasswordWindow.Show();
            Application.Current.MainWindow.Close();
            
            
        }

        internal void Show()
        {
            throw new NotImplementedException();
        }
    }
}
