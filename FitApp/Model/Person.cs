using System;

namespace FitApp.Model
{
    public abstract class Person
    { 
        // Egenskaper
        public string? Username { get; set; }
        public string? Password { get; set; }

        // Metod
        public abstract void SignIn();

    }
}
