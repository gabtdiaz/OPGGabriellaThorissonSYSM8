﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Model
{
    public abstract class Person
    { 
        // Egenskaper
        public string Username { get; set; }
        protected string Password { get; set; }

        // Konstruktor?

        // Metod
        public abstract void SignIn();

    }
}