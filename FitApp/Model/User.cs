using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Model
{
    public class User : Person
    {
        // Egenskaper
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        // Konstruktor
        public User (string Country, string Username, string Password) : base (Username, Password)
        {
            User Admin = new User("Sverige", "admin", "Abcd1234");
            User User = new User(Country, Username, Password);
                    
        }

        // Metoder
        public override void SignIn()
        {

        }

        public void ResetPassword(string SecurityAnswer)
        {

        }
    }
}
