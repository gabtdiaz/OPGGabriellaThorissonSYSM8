using FitApp.Model;
using FitApp.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitApp.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
        // Egenskaper
        Window _addWorkoutWindow;
        public string WorkoutTypeComboBox {  get; set; }
        public TimeSpan DurationInput { get; set; }
        public int CaloriesBurnedInput { get; set; }
        public string NotesInput { get; set; }

        // Konstruktor - Anropar både WorkoutsWindowViewModel och MainWindowViewModels konstruktorer
        public AddWorkoutWindowViewModel(Window addWorkoutWindow) 
        {
            _addWorkoutWindow = addWorkoutWindow;
        } 

        public void SaveWorkout() 
        {

        }

    }
}
