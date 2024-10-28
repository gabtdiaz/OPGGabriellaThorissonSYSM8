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
        private readonly Window workoutDetailsWindow;
        private readonly WorkoutsWindowViewModel workoutsWindow;
        private Workout originalWorkout;

        public ObservableCollection<Workout> Workouts => workoutsWindow.Workouts;

        // Binding-egenskaper för UI
        public DateTime WorkoutDateTime { get; set; }
        public TimeSpan WorkoutDuration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }
        public ObservableCollection<string> WorkoutTypeOptions { get; } = new ObservableCollection<string> { "Cardio", "Strength" };

        private string selectedWorkoutType;
        public string SelectedWorkoutType
        {
            get => selectedWorkoutType;
            set
            {
                selectedWorkoutType = value;
                OnPropertyChanged(nameof(SelectedWorkoutType));
            }
        }

        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get => selectedWorkout;
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
                UpdateFieldsFromSelectedWorkout();
            }
        }

        // Kommandon
        public ICommand EditWorkoutCommand => new RelayCommand(EditWorkout);
        public ICommand SaveWorkoutCommand => new RelayCommand(SaveWorkout);

        // Konstruktor
        public WorkoutDetailsWindowViewModel(Workout workout, Window workoutDetailsWindow, WorkoutsWindowViewModel workoutsWindow)
        {
            this.originalWorkout = workout;
            this.workoutDetailsWindow = workoutDetailsWindow;
            this.workoutsWindow = workoutsWindow;

            SelectedWorkout = workout;
            IsEditing = false;

            CopyWorkout();
        }

        // Metod för att kopiera träningspasset till bindbara egenskaper
        private void CopyWorkout()
        {
            if (originalWorkout != null)
            {
                SelectedWorkoutType = originalWorkout.Type;
                WorkoutDateTime = originalWorkout.DateTime;
                WorkoutDuration = originalWorkout.Duration;
                CaloriesBurned = originalWorkout.CaloriesBurned;
                Notes = originalWorkout.Notes;
            }
        }

        // Startar redigeringsläge
        private void EditWorkout()
        {
            if (SelectedWorkout != null)
            {
                IsEditing = true;
                CopyWorkout();
            }
        }

        // Sparar ändringar och återgår till huvudfönstret
        private void SaveWorkout()
        {
            IsEditing = false;

            Workout newWorkout;
            if (SelectedWorkoutType == "Cardio")
            {
                newWorkout = new CardioWorkout();
            }
            else
            {
                newWorkout = new StrengthWorkout();
            }

            // Tilldela egenskaper till det nya träningspasset
            newWorkout.Type = SelectedWorkoutType;
            newWorkout.DateTime = WorkoutDateTime;
            newWorkout.Duration = WorkoutDuration;
            newWorkout.CaloriesBurned = CaloriesBurned;
            newWorkout.Notes = Notes;

            workoutsWindow.Workouts.Add(newWorkout);

            // Stänger fönstret
            WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(workoutsWindow.userManager, workoutsWindow);
            newWorkoutsWindow.Show();
            workoutDetailsWindow.Close();
            // okej så när jag sparar så skapas en ny workout istället
        }

        // Uppdaterar UI-egenskaper baserat på vald workout
        private void UpdateFieldsFromSelectedWorkout()
        {
            OnPropertyChanged(nameof(CardioVisibility));
            OnPropertyChanged(nameof(StrengthVisibility));
        }

        public Visibility CardioVisibility => SelectedWorkout is CardioWorkout ? Visibility.Visible : Visibility.Collapsed;
        public Visibility StrengthVisibility => SelectedWorkout is StrengthWorkout ? Visibility.Visible : Visibility.Collapsed;

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
    }
}