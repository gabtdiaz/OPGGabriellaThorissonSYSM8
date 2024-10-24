using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Model
{
    public class Workout
    {
        // Egenskaper
        public DateTime DateTime {  get; set; } = DateTime.Now;
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes {  get; set; }

        // Konstruktor

        // Metod
        public virtual int CalculateCaloriesBurned()
        {
            return 0;
        }
    }
}
