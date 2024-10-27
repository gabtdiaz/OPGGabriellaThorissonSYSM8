using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        // Egenskaper
        public Window workoutDetailsWindow;
        public Workout Workout { get; set; }

        // Egenskaper som kontrollerar redigeringsläge
        private bool isEditing;
        public bool IsEditing
        {
            get => isEditing;
            private set
            {
                isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }

        // Commands
        public ICommand EditWorkoutCommand { get; }
        public ICommand SaveWorkoutCommand { get; }

        // Konstruktor
        public WorkoutDetailsWindowViewModel(Workout selectedWorkout, Window workoutDetailsWindow)
        {
            Workout = selectedWorkout;
            this.workoutDetailsWindow = workoutDetailsWindow;

            // Initialisera commands
            EditWorkoutCommand = new RelayCommand(EditWorkout);
            SaveWorkoutCommand = new RelayCommand(SaveWorkout);


            IsEditing = false; // Fälten är låsta i början
        }

        private void EditWorkout()
        {
            IsEditing = true; // Låser upp fälten för att kunna redigera
        }

        private void SaveWorkout()
        {

        }
    }
}
