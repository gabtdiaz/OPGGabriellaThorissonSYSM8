using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Model
{
    class StrengthWorkout : Workout
    {
        public int Repetitions { get; set; }
        public override int CalculateCaloriesBurned()
        {
            // Beräknar kalorier baserat på reps, sets och duration
            double durationInHours = Duration.TotalHours;
            return (int)(Repetitions * Sets * 5 * durationInHours);
        }
    public override string ToString() 
        {
            return $"Strength";
        }
    }
}
