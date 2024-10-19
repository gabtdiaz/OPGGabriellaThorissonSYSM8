using FitApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.ViewModel
{
    public class AddWorkoutWindowViewModel : WorkoutsWindowViewModel
    {
        // Egenskaper
        public string WorkoutTypeComboBox {  get; set; }
        public TimeSpan DurationInput { get; set; }
        public int CaloriesBurnedInput { get; set; }
        public string NotesInput { get; set; }

        // Konstruktor - Anropar både WorkoutsWindowViewModel och MainWindowViewModels konstruktorer
        public AddWorkoutWindowViewModel(User User, List<Workout> WorkoutList, string WorkoutTypeComboBox, TimeSpan DurationInput, int CaloriesBurnedInput, string NotesInput, string UsernameInput, string PasswordInput)
            : base(User, WorkoutList, UsernameInput, PasswordInput)
        {
            this.WorkoutTypeComboBox = WorkoutTypeComboBox;
            this.DurationInput = DurationInput;
            this.CaloriesBurnedInput = CaloriesBurnedInput;
            this.NotesInput = NotesInput;
        }

        public void SaveWorkout() { }

    }
}
