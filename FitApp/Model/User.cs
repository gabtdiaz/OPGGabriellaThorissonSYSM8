using System;
using System.Collections.ObjectModel;

namespace FitApp.Model
{
    public class User : Person
    {
        // Egenskaper
        public string? Country { get; set; }
        public string SecurityQuestion { get; set; } = "What's your pets name?";
        public string SecurityAnswer = "Cleo";
        public ObservableCollection<Workout> Workouts { get; set; } // Lista på alla träningar 

        public User() 
        {
            Workouts = new ObservableCollection<Workout>();
        }

        // Konstruktor - för att kunna lägga till ny användare i UserManager klassen.
        public User(string Country, string Username, string Password)
        {
            this.Country = Country;
            this.Username = Username;
            this.Password = Password;
            Workouts = new ObservableCollection<Workout>();
        }

        // Metod
        public override void SignIn()
        {
            
        }

    }
}
