using System;

namespace FitApp.Model
{
    class CardioWorkout : Workout
    {
        public new int Distance { get; set; }

        // Metod som beräknar brända kalorier på distans och varaktighet.
        public override int CalculateCaloriesBurned()
        {
            if (Duration.TotalMinutes <= 0) return 0;

            // Ca 70 kalorier per kilometer per timme 
            double durationInHours = Duration.TotalHours;
            return (int)(Distance * 70 * durationInHours);
        }

        public override string ToString()
        {
            return $"Cardio";
        }
    }
}
