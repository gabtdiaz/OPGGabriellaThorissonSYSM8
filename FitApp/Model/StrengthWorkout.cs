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
            return (int)(Repetitions * Sets * 5); ;
        }
        public override string ToString() 
        {
            return $"Strength";
        }
    }
}
