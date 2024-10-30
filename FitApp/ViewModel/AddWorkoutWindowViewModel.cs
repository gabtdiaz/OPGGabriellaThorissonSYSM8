using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
        // Egenskaper

        // Fönster och Referenser
        public Window addWorkoutWindow;
        private WorkoutsWindowViewModel workoutsWindow;

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
            get { return cardioVisibility; }
            set
            {
                cardioVisibility = value;
                OnPropertyChanged(nameof(CardioVisibility));
            }
        }

        private Visibility strengthVisibility;
        public Visibility StrengthVisibility
        {
            get { return strengthVisibility; }
            set
            {
                strengthVisibility = value;
                OnPropertyChanged(nameof(StrengthVisibility));
            }
        }

        // Träningsegenskaper
        private string selectedWorkout;
        public string SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
                UpdateVisibility();
                CalculateCalories(); // Lägg till denna för automatisk kalorieberäkning
            }
        }

        private DateTime dateinput = DateTime.Now; // Sätt default värde
        public DateTime DateInput
        {
            get { return dateinput; }
            set
            {
                dateinput = value;
                OnPropertyChanged(nameof(DateInput));
            }
        }

        private TimeSpan durationInput;
        public TimeSpan DurationInput
        {
            get { return durationInput; }
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
            get { return caloriesBurnedInput; }
            set
            {
                caloriesBurnedInput = value;
                OnPropertyChanged(nameof(CaloriesBurnedInput));
            }
        }

        private string notesInput;
        public string NotesInput
        {
            get { return notesInput; }
            set
            {
                notesInput = value;
                OnPropertyChanged(nameof(NotesInput));
            }
        }

        private int distance;
        public int Distance
        {
            get { return distance; }
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
            get { return repetitions; }
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
            get { return sets; }
            set
            {
                sets = value;
                OnPropertyChanged(nameof(Sets));
                CalculateCalories(); // För automatisk kalorieberäkning
            }
        }

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

        // Uppdaterar synlighet av fält baserat på vald träningstyp
        private void UpdateVisibility()
        {
            CardioVisibility = selectedWorkout == "Cardio" ? Visibility.Visible : Visibility.Collapsed;
            StrengthVisibility = selectedWorkout == "Strength" ? Visibility.Visible : Visibility.Collapsed;
        }

        // Sparar träningspasset och kontrollerar inmatning
        public void SaveWorkout()
        {
            // Skapar nytt träningspass baserat på typ
            Workout newWorkout;
            if (SelectedWorkout == "Cardio")
            {
                newWorkout = new CardioWorkout
                {
                    Distance = Distance,
                    Duration = DurationInput,
                    DateTime = DateInput,
                    Notes = NotesInput,
                    Type = SelectedWorkout
                };
            }
            else // Strength
            {
                newWorkout = new StrengthWorkout
                {
                    Sets = Sets,
                    Repetitions = Repetitions,
                    Duration = DurationInput,
                    DateTime = DateInput,
                    Notes = NotesInput,
                    Type = SelectedWorkout
                };
            }

            // Beräknar kalorier automatiskt 
            newWorkout.CaloriesBurned = newWorkout.CalculateCaloriesBurned();

            // Lägger till i listan
            workoutsWindow.Workouts.Add(newWorkout);

            // Återställer alla fält
            ResetFields();

            // Öppnar WorkoutsWindow och stänger nuvarande fönster
            WorkoutsWindowViewModel viewModel = new WorkoutsWindowViewModel(workoutsWindow.userManager, null);
            WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(workoutsWindow.userManager, viewModel);
            newWorkoutsWindow.Show();
            addWorkoutWindow.Close();
        }

        // Återställer alla fält till default-värden
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
