using FitApp.Model;
using FitApp.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitApp.Services
{
    public class UserManager : ViewModelBase
    {
        // Egenskaper

        public List<User> Users = new List<User>(); // Lista på alla användare

        private User currentUser; // Hanterar inloggade användare
        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged(nameof(CurrentUser)); // Detta meddelar att CurrentUser har ändrats
                OnPropertyChanged(nameof(CurrentUserName)); // Meddelar att CurrentUserName har ändrats

            }
        }
        
        public UserManager()
        {
            // Lägger till användare direkt i listan.
            Users = new List<User>
            {
                new User { Username = "admin", Password = "abcd", Country = "Sweden" },
                new User { Username = "gabriella", Password = "abc123", Country = "Finland" }
            };
    }


        // Metod för att matcha användare från listan, och sätta den som CurrentUser
        public User FindUser(string username, string password = null)
        {
            foreach (User user in Users)
            {
                if (user.Username == username)
                {
                    // Om password är null, returnera användaren utan att kolla lösenordet
                    if (password == null || user.Password == password)
                    {
                        return user; // Returnerar användaren om användarnamnet matchar
                    }
                }
            }
            return null; // Om ingen matchning finns
        }

        // Metod för att återställa lösenordet.
        public bool ResetPassword(string username, string newPassword, string securityAnswer)
        {
            // Hitta användaren baserat på användarnamn
            //User user = null; // Skapa en variabel med ev. tillfälligt värde
            User user = Users.FirstOrDefault(u => u.Username == username && u.SecurityAnswer == securityAnswer);

            foreach (User u in Users) // Iterera över alla användare i listan Users
            {
                if (u.Username == username) // Kontrollera om användarnamnet matchar
                {
                    user = u; // Tilldela användaren till variabeln om matchning hittas
                    break;    // Avsluta loopen när vi har hittat den första matchningen
                }
            }

            if (user != null)
            {
                // Kontrollera om säkerhetssvaret matchar 
                if (user.SecurityAnswer.ToLower() == securityAnswer.ToLower())
                {
                    user.Password = newPassword;  // Återställ lösenordet
                    CurrentUser = user;           // Sätt CurrentUser till den funna användaren
                    return true;
                }
            }
                return false; // Om säkerhetssvaret var fel eller användaren inte hittades
        }
        public string CurrentUserName => CurrentUser?.Username ?? "No User";

        // Metod för att logga ut
        public void SignOut()
        {
            CurrentUser = null; // Nollställ inloggad användare
        }
    }
    
}
