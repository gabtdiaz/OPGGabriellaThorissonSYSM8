using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
        // Egenskaper

        public Window addWorkoutWindow;

        // Referens till workoutsWindow
        public WorkoutsWindowViewModel workoutsWindow;

        // Lista med träningstyper 
        public ObservableCollection<string> WorkoutTypeComboBox { get; set; } 

        // Referens till träningslistan från WorkoutsWindow
        public ObservableCollection<Workout> Workouts
        {
            get => workoutsWindow.Workouts;
        }

        // Synlighetsegenskaper
        private Visibility cardioVisibility;
        public Visibility CardioVisibility
        {
            get => cardioVisibility;
            set
            {
                cardioVisibility = value;
                OnPropertyChanged(nameof(CardioVisibility));
            }
        }

        private Visibility strengthVisibility;
        public Visibility StrengthVisibility
        {
            get => strengthVisibility;
            set
            {
                strengthVisibility = value;
                OnPropertyChanged(nameof(StrengthVisibility));
            }
        }

        // Träningsegenskaper för binding i UI
        private string selectedWorkout = string.Empty;
        public string SelectedWorkout
        {
            get => selectedWorkout;
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
                UpdateVisibility();
                CalculateCalories(); // För automatisk kalorieberäkning
            }
        }

        private DateTime dateinput = DateTime.Now; // Sätter startvärde på datum
        public DateTime DateInput
        {
            get => dateinput; 
            set
            {
                dateinput = value;
                OnPropertyChanged(nameof(DateInput));
            }
        }

        private TimeSpan durationInput;
        public TimeSpan DurationInput
        {
            get => durationInput;
            set
            {
                durationInput = value;
                OnPropertyChanged(nameof(DurationInput));
                CalculateCalories(); // För automatisk kalorieberäkning
            }
        }

        private int caloriesBurnedInput;
        public int CaloriesBurnedInput
        {
            get => caloriesBurnedInput;
            set
            {
                caloriesBurnedInput = value;
                OnPropertyChanged(nameof(CaloriesBurnedInput));
            }
        }

        private string notesInput = string.Empty;
        public string NotesInput
        {
            get => notesInput;
            set
            {
                notesInput = value;
                OnPropertyChanged(nameof(NotesInput));
            }
        }

        private int distance;
        public int Distance
        {
            get => distance;
            set
            {
                distance = value;
                OnPropertyChanged(nameof(Distance));
                CalculateCalories(); // För automatisk kalorieberäkning
            }
        }

        private int repetitions;
        public int Repetitions
        {
            get => repetitions; 
            set
            {
                repetitions = value;
                OnPropertyChanged(nameof(Repetitions));
                CalculateCalories(); // För automatisk kalorieberäkning
            }
        }

        private int sets;
        public int Sets
        {
            get => sets;
            set
            {
                sets = value;
                OnPropertyChanged(nameof(Sets));
                CalculateCalories(); // För automatisk kalorieberäkning
            }
        }

        // Kommando som anropa metod
        public ICommand SaveCommand => new RelayCommand(SaveWorkout);

        public AddWorkoutWindowViewModel(Window addWorkoutWindow, WorkoutsWindowViewModel workoutsWindow)
        {
            this.addWorkoutWindow = addWorkoutWindow;
            this.workoutsWindow = workoutsWindow;
            WorkoutTypeComboBox = new ObservableCollection<string> { "Cardio", "Strength" };
            DateInput = DateTime.Now; // Sätt dagens datum som default
        }

        // Beräknar kalorier - anropar metoder från StrengthWorkout & CardioWorkout
        private void CalculateCalories()
        {
            if (SelectedWorkout == "Strength" && Sets > 0 && Repetitions > 0)
            {
                Workout workout = new StrengthWorkout
                {
                    Sets = Sets,
                    Repetitions = Repetitions,
                    Duration = DurationInput
                };
                CaloriesBurnedInput = workout.CalculateCaloriesBurned();
            }
            else if (SelectedWorkout == "Cardio" && Distance > 0)
            {
                Workout workout = new CardioWorkout
                {
                    Distance = Distance,
                    Duration = DurationInput
                };
                CaloriesBurnedInput = workout.CalculateCaloriesBurned();
            }
        }

        // Metod som uppdaterar synlighet av fält baserat på vald träningstyp
        private void UpdateVisibility()
        {
            CardioVisibility = selectedWorkout == "Cardio" ? Visibility.Visible : Visibility.Collapsed;
            StrengthVisibility = selectedWorkout == "Strength" ? Visibility.Visible : Visibility.Collapsed;
        }


        // Metod som sparar träningspasset och kontrollerar inmatning
        public void SaveWorkout()
        {
            if (SelectedWorkout == null)
            {
                MessageBox.Show("Unable to save workout. Please select a workout type!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (DurationInput.TotalMinutes <= 0)
            {
                MessageBox.Show("Duration must be greater than 0!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Workout newWorkout;

            if (SelectedWorkout == "Cardio")
            {
                if (Distance <= 0)
                {
                    MessageBox.Show("Distance must be greater than 0!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                newWorkout = new CardioWorkout
                {
                    Distance = Distance,
                    Duration = DurationInput,
                    DateTime = DateInput,
                    Notes = NotesInput,
                    Type = SelectedWorkout,
                    CaloriesBurned = CaloriesBurnedInput  // Använd det redan uträknade värdet
                };

                MessageBox.Show("Cardio workout added! ", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else // Strength
            {
                if (Sets <= 0 || Repetitions <= 0)
                {
                    MessageBox.Show("Sets and Repetitions must be greater than 0!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                newWorkout = new StrengthWorkout
                {
                    Sets = Sets,
                    Repetitions = Repetitions,
                    Duration = DurationInput,
                    DateTime = DateInput,
                    Notes = NotesInput,
                    Type = SelectedWorkout,
                    CaloriesBurned = CaloriesBurnedInput  // Använd det redan uträknade värdet
                };

                MessageBox.Show("Strength workout added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Beräknar kalorier automatiskt 
            newWorkout.CaloriesBurned = newWorkout.CalculateCaloriesBurned();

            // Lägg till träning i användarens lista
            workoutsWindow.userManager?.CurrentUser?.Workouts.Add(newWorkout);

            // Lägger till träning den temporära listan för nuvarande vwindow
            workoutsWindow.Workouts.Add(newWorkout);

            // Återställer alla fält
            ResetFields();

            // Öppnar Workoutswindow och stänger Addworkoutswindow
            WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(workoutsWindow.userManager);
            newWorkoutsWindow.Show();
            addWorkoutWindow.Close();
        }

        // Metod som återställer alla fält
        private void ResetFields()
        {
            SelectedWorkout = string.Empty;
            DateInput = DateTime.Now;
            DurationInput = TimeSpan.Zero;
            CaloriesBurnedInput = 0;
            NotesInput = string.Empty;
            Distance = 0;
            Repetitions = 0;
            Sets = 0;
        }

    }
}
