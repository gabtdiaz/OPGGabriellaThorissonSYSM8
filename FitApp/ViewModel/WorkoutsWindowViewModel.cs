using FitApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FitApp.ViewModel
{
    public class WorkoutsWindowViewModel : MainWindowViewModel
    {
        // Egenskaper
        public User User {  get; set; }
        new List<Workout> WorkoutList = new List<Workout>();
        public Workout selectedWorkout { get; set; }
 
        

        // Konstruktor
        public WorkoutsWindowViewModel(User currentUser) 
        {
            User = currentUser;
            WorkoutList = new List<Workout>
        {
            new CardioWorkout { Type = "Cardio", Distance = 5, Duration = new TimeSpan(0, 30, 0), CaloriesBurned = 300 },
            new StrengthWorkout { Type = "Strength", Repetitions = 10, Duration = new TimeSpan(0, 45, 0), CaloriesBurned = 400 }
        }; // tilldela direkt?
        }

        // Metoder
        public void AddWorkout()
        {

        }
        public void RemoveWorkout()
        {

        }
        public void OpenDetails (Workout workout) 
        {

        }
    }
}
