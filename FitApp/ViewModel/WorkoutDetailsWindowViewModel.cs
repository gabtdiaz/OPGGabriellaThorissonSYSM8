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
        public string Notes { get; set; }
        public int Distance {  get; set; }
        public int Repetitions { get; set; }
        public int Sets { get; set; }
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

        private int caloriesBurned;
        public int CaloriesBurned
        {
            get => caloriesBurned;
            set
            {
                caloriesBurned = value;
                OnPropertyChanged(nameof(CaloriesBurned));
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
            if (SelectedWorkout != null)
            {
                WorkoutDateTime = SelectedWorkout.DateTime;
                WorkoutDuration = SelectedWorkout.Duration;
                CaloriesBurned = SelectedWorkout.CaloriesBurned;
                Notes = SelectedWorkout.Notes;

                // Uppdatera fält baserat på workout typ
                if (SelectedWorkout is CardioWorkout cardio)
                {
                    Distance = cardio.Distance;
                    CalculateCalories(); // Beräkna CaloriesBurned
                }
                else if (SelectedWorkout is StrengthWorkout strength)
                {
                    Sets = strength.Sets;
                    Repetitions = strength.Repetitions;
                    CalculateCalories(); // Beräkna CaloriesBurned
                }

                MessageBox.Show("Workout saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBox.Show("Fill in the correct information", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Öppna och stäng fönster
            WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(workoutsWindow.userManager, workoutsWindow);
            newWorkoutsWindow.Show();
            workoutDetailsWindow.Close();
        }


        // Metod för att berkäkna kalorier
        private void CalculateCalories()
        {
            if (SelectedWorkoutType == "Strength" && Sets > 0 && Repetitions > 0)
            {
                var workout = new StrengthWorkout
                {
                    Sets = Sets,
                    Repetitions = Repetitions,
                    Duration = WorkoutDuration
                };
                CaloriesBurned = workout.CalculateCaloriesBurned();
            }
            else if (SelectedWorkoutType == "Cardio" && Distance > 0)
            {
                var workout = new CardioWorkout
                {
                    Distance = Distance,
                    Duration = WorkoutDuration
                };
                CaloriesBurned = workout.CalculateCaloriesBurned();
            }
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