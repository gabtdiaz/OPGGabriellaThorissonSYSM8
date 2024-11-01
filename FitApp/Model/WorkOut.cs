using System;

namespace FitApp.Model
{
    public class Workout
    {
        // Egenskaper
        public DateTime DateTime {  get; set; } = DateTime.Now;
        public string Type { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public int CaloriesBurned { get; set; } = 0;
        public string Notes {  get; set; } = string.Empty;

        public int Distance { get; set; } = 0;
        public int Repetitions { get; set; } = 0;
        public int Sets { get; set; } = 0;

        // Konstruktor    
        public Workout() { }    

        // Metod
        public virtual int CalculateCaloriesBurned()
        {
            return CaloriesBurned;
        }
    }
}
