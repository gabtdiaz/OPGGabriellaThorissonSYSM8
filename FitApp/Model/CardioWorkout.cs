using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Model
{
    class CardioWorkout : Workout
    {
        public int Distance { get; set; }

        public override int CalculateCaloriesBurned()
        {
            return 0;
        }
    }
}
