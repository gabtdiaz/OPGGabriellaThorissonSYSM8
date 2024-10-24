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
        public string SecurityQuestion { get; set; } = "What's your pets name?";
        public string SecurityAnswer { get; set; } = "Cleo";

        public User() 
        {

        }

        // Konstruktor - för att kunna lägga till ny användare i UserManager klassen.
        public User(string Country, string Username, string Password)
        {
            this.Country = Country;
            

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
