using FitApp.Model;
using FitApp.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void GetUser(string currentUser)
        {
            
        }
    }
}
