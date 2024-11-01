using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FitApp.Model
{
    class StrengthWorkout : Workout
    {
        public new int Repetitions { get; set; }
        public new int Sets { get; set; }   

        // Metod som räknar ut kaloriförbränning baserat på reps och sets
        public override int CalculateCaloriesBurned()
        {
            if (Duration.TotalMinutes <= 0) return 0;

            // Ungefär 5 kalorier per rep
            double durationInHours = Duration.TotalHours;
            return (int)(Repetitions * Sets * 5);
        }
        public override string ToString() 
        {
            return $"Strength";
        }
    }
}
