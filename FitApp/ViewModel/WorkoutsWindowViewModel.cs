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
        new User User = new User();
        new List<Workout> WorkoutList = new List<Workout>();

        // Konstruktor
        public WorkoutsWindowViewModel(User User, List<Workout> Workoutlist, string UsernameInput, string PasswordInput) : base(UsernameInput, PasswordInput)
        {
            this.User = User;
            this.WorkoutList = this.WorkoutList; // tilldela direkt.
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
