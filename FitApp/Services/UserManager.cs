using FitApp.Model;
using FitApp.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public List<User> Users = new List<User>(); // Lista på alla användar


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

        public bool IsCurrentUserAdmin => CurrentUser?.Username == "admin";

        public UserManager()
        {
            //Skapa användare 
            var admin = new AdminUser("Sweden", "admin", "abcd");
            var user1 = new User { Username = "gabriella", Password = "abc123", Country = "Finland" };
            var user2 = new User { Username = "erik", Password = "abc123", Country = "Norway" };

            // Lägg till träningspass för Gabriella
            user1.Workouts.Add(new CardioWorkout
            {
                Type = "Cardio",
                Distance = 5,
                Duration = new TimeSpan(0, 20, 0),
                CaloriesBurned = 250,
                DateTime = new DateTime(2024, 10, 24, 08, 00, 0),
                Notes = "Morning run"
            });
            user1.Workouts.Add(new StrengthWorkout
            {
                Type = "Strength",
                Repetitions = 10,
                Duration = new TimeSpan(0, 45, 0),
                CaloriesBurned = 300,
                DateTime = new DateTime(2024, 10, 20, 18, 30, 0),
                Notes = "Evening Gym Session"
            });

            // Lägg till träningspass för Erik
            user2.Workouts.Add(new CardioWorkout
            {
                Type = "Cardio",
                Distance = 3,
                Duration = new TimeSpan(0, 15, 0),
                CaloriesBurned = 180,
                DateTime = new DateTime(2024, 10, 25, 16, 00, 0),
                Notes = "Evening Jog"
            });

            user2.Workouts.Add(new StrengthWorkout
            {
                Type = "Strength",
                Repetitions = 12,
                Duration = new TimeSpan(1, 0, 0),
                CaloriesBurned = 400,
                DateTime = new DateTime(2024, 10, 23, 19, 00, 0),
                Notes = "Weight Training"
            });

            // Lägg till alla användare i Users-listan
            Users = new List<User> { admin, user1, user2 };
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
                    CurrentUser = user;           // Sätt CurrentUser till den hittade användaren
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
