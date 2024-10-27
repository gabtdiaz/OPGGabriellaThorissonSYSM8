using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        // Referenser
        private readonly Window workoutDetailsWindow;
        private readonly WorkoutsWindowViewModel workoutsWindow;

        // Ursprungligt workout-objekt
        private readonly Workout originalWorkout;

        // Binding egenskaper för UI
        public string WorkoutType { get; private set; }
        public DateTime WorkoutDateTime { get; private set; }
        public TimeSpan WorkoutDuration { get; private set; }
        public int CaloriesBurned { get; private set; }
        public string Notes { get; private set; }

        // För redigering och val av workout
        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get => selectedWorkout;
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
                UpdateVisibility();
            }
        }

        // Egenskaper för Visibility (anpassade för Cardio och Strength)
        public Visibility CardioVisibility => SelectedWorkout is CardioWorkout ? Visibility.Visible : Visibility.Collapsed;
        public Visibility StrengthVisibility => SelectedWorkout is StrengthWorkout ? Visibility.Visible : Visibility.Collapsed;

        // Kontroll för redigeringsläge
        private bool isEditing;
        public bool IsEditing
        {
            get => isEditing;
            private set
            {
                isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
                if (value && SelectedWorkout != null)
                {
                    CopyWorkout();
                }
            }
        }

        // Kommandon
        public ICommand EditWorkoutCommand => new RelayCommand(EditWorkout);
        public ICommand SaveWorkoutCommand => new RelayCommand(SaveWorkout)

        // Konstruktor
        public WorkoutDetailsWindowViewModel(Workout workout, Window workoutDetailsWindow, WorkoutsWindowViewModel workoutsWindow)
        {
            originalWorkout = workout;
            this.workoutDetailsWindow = workoutDetailsWindow;
            this.workoutsWindow = workoutsWindow;
            SelectedWorkout = workout; // Sätter valt träningspass

            IsEditing = false; // Redigeringsläge avstängt från början
        }

        // Metod för att kopiera träningspasset till bindbara egenskaper
        private void CopyWorkout()
        {
            if (originalWorkout != null)
            {
                WorkoutType = originalWorkout.Type;
                WorkoutDateTime = originalWorkout.DateTime;
                WorkoutDuration = originalWorkout.Duration;
                CaloriesBurned = originalWorkout.CaloriesBurned;
                Notes = originalWorkout.Notes;

                // Uppdaterar UI-bindningar
                OnPropertyChanged(nameof(WorkoutType));
                OnPropertyChanged(nameof(WorkoutDateTime));
                OnPropertyChanged(nameof(WorkoutDuration));
                OnPropertyChanged(nameof(CaloriesBurned));
                OnPropertyChanged(nameof(Notes));
            }
        }

        // Startar redigeringsläge
        private void EditWorkout()
        {
            IsEditing = true;
        }

        // Sparar ändringar och återgår till huvudfönstret
        private void SaveWorkout()
        {
            IsEditing = false;

            // Öppnar huvudfönstret igen och stänger det aktuella fönstret
            WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(workoutsWindow.userManager, workoutsWindow);
            newWorkoutsWindow.Show();
            workoutDetailsWindow.Close();
        }

        // Uppdaterar Visibility-egenskaper beroende på typ av workout
        private void UpdateVisibility()
        {
            OnPropertyChanged(nameof(CardioVisibility));
            OnPropertyChanged(nameof(StrengthVisibility));
        }
    }
}
