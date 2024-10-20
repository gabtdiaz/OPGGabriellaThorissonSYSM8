using FitApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.ViewModel
{
    public class WorkoutDetailsWindowViewModel 
    {
        // Egenskaper
        public Workout Workout {  get; set; }

        // Konstruktor
        public WorkoutDetailsWindowViewModel(Workout Workout)
        {
            this.Workout = Workout; 
        }

        // Metoder
        public void EditWorkout() { }
        public void SaveWorkout() { }
    }
}
