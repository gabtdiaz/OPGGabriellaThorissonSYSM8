using FitApp.Model;
using FitApp.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
        // Egenskaper
        public string WorkoutTypeComboBox {  get; set; }
        public TimeSpan DurationInput { get; set; }
        public int CaloriesBurnedInput { get; set; }
        public string NotesInput { get; set; }

        // Konstruktor - Anropar både WorkoutsWindowViewModel och MainWindowViewModels konstruktorer
        public AddWorkoutWindowViewModel() { } // Skicka med listan

        public void SaveWorkout() 
        {

        }

    }
}
