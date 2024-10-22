using FitApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Services
{
    public class UserManager
    {
        public List<User> Users = new List<User>(); // Spara alla användare

        public UserManager()
        {
            // Lägga till en user
            Users.Add(new User { Country = "Sweden", Username = "admin", Password = "abcd1234" });


        }
    }
}
