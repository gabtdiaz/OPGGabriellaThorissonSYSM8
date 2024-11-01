using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        // Egenskaper

        public ObservableCollection<Workout> Workouts => workoutsWindow.Workouts;
        public Window workoutDetailsWindow;
        private Workout originalWorkout;
        public WorkoutsWindowViewModel workoutsWindow; // Referens
        public DateTime WorkoutDateTime { get; set; }
        public TimeSpan WorkoutDuration { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int Distance {  get; set; } 
        public int Repetitions { get; set; }
        public int Sets { get; set; }
        public ObservableCollection<string> WorkoutTypeOptions { get; } = new ObservableCollection<string> { "Cardio", "Strength" };

        // Egenskaper för binding i UI
        private string selectedWorkoutType = string.Empty;
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

        // Kommandon som anropar metoder
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

        // Metod för att kopiera träningspasset till binding egenskaper
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
            else
            {
                MessageBox.Show("Please select workout to edit!", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
        }

        // Sparar ändringar och återgår till WorkoutsWindow
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
                    CalculateCalories(); // Beräknar CaloriesBurned
                }
                else if (SelectedWorkout is StrengthWorkout strength)
                {
                    Sets = strength.Sets;
                    Repetitions = strength.Repetitions;
                    CalculateCalories(); // Beräknar CaloriesBurned
                }

                MessageBox.Show("Workout saved!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Fill in the correct information", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return; // återgå ifall informationen inte fylls i korrekt
            }

            // Öppnar WorkoutsWindow och stänger nuvarande fönster
            WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(workoutsWindow.userManager);
            newWorkoutsWindow.Show();
            workoutDetailsWindow?.Close();
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

        // Uppdaterar UI-egenskaper baserat på vald träningstyp
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