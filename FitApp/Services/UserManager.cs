using FitApp.Model;
using FitApp.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitApp.Services
{
    public class UserManager : ViewModelBase
    {
        public List<User> Users = new List<User>(); // Lista på alla användare
                                                    
        public User CurrentUser { get; private set; } // Hanterar inloggade användare

        public UserManager()
        {
            // Lägger till admin
            Users = new List<User>
        {
            new User { Username = "admin", Password = "abcd", Country = "Sweden" }
        };

        }

        // Metod för att matcha användare från listan, och sätta den som CurrentUser
        public User FindUser(string username, string password)
        {
            foreach (User user in Users)
            {
                if (user.Username == username && user.Password == password)
                {
                    CurrentUser = user; 
                    return CurrentUser;
                }
            }
            return null; // Om ingen matchning finns
        }


        // Metod för att logga ut
        public void SignOut()
        {
            CurrentUser = null; // Nollställ inloggad användare
        }
    }
    
}
