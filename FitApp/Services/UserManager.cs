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
        public List<User> Users = new List<User>(); // Spara alla användare
        //kan jag skita i och ha denna klassen? Eller måste jag ha alla user-relaterade metoder här?

        public UserManager()
        {
            // Lägga till en user
            Users.Add(new User { Country = "Sweden", Username = "admin", Password = "abcd1234" });
        }
    }
}
