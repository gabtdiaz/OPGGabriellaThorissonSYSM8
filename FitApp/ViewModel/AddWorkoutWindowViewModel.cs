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

        public Window addWorkoutWindow;
        public ObservableCollection<string> WorkoutTypeComboBox { get; set; }

        // Referenser

        private WorkoutsWindowViewModel workoutsWindow;
        public ObservableCollection<Workout> Workouts
        {
            get => workoutsWindow.Workouts;
        }

        // Synlighets-egenskaper

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

        // Egenskaper för binding till UI

        private string selectedWorkout;

        public string SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
                UpdateVisibility();
            }
        }

        private DateTime dateinput;

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
                OnPropertyChanged(NotesInput);
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
            }
        }

        // Initierar kommando för att spara träningspass
        public ICommand SaveCommand => new RelayCommand(SaveWorkout);

        // Konstruktor - Anropar både WorkoutsWindowViewModel och MainWindowViewModels konstruktorer
        public AddWorkoutWindowViewModel(Window addWorkoutWindow, WorkoutsWindowViewModel workoutsWindow) 
        {
            this.addWorkoutWindow = addWorkoutWindow;
            this.workoutsWindow = workoutsWindow;
            WorkoutTypeComboBox = new ObservableCollection<string> { "Cardio", "Strength" };
        } 

        // Metod som sparar träningspass förutsatt att varje fält är ifyllt.
        public void SaveWorkout() 
        {
            // Kontrollerar ifall fälten är tomma
            if (string.IsNullOrWhiteSpace(DateInput.ToString()) ||
                string.IsNullOrWhiteSpace(DurationInput.ToString()) ||
                string.IsNullOrWhiteSpace(CaloriesBurnedInput.ToString()) ||
                string.IsNullOrWhiteSpace(NotesInput) ||
                WorkoutTypeComboBox == null)
            {
                MessageBox.Show("Please enter all the information correctly.", "Error", MessageBoxButton.OK);
                return;
            }

            // Kontrollerar fälten utefter träningstyp
            if (SelectedWorkout == "Cardio")
            {
                if (Distance == 0)
                {
                    MessageBox.Show("Please enter distance", "Error", MessageBoxButton.OK);
                    return;
                }
            }
            else if (SelectedWorkout == "Strength")
            {
                if (Sets == 0 || Repetitions == 0)
                {
                    MessageBox.Show("Please enter sets & reps", "Error", MessageBoxButton.OK);
                    return;
                }
            }

            // Spara träningspasset i Workouts listan från WorkoutsWindowViewModel
            Workout newWorkout = SelectedWorkout == "Cardio" ? new CardioWorkout() : new StrengthWorkout();
            newWorkout.Type = SelectedWorkout;
            newWorkout.DateTime = DateInput;
            newWorkout.Duration = DurationInput;
            newWorkout.CaloriesBurned = CaloriesBurnedInput;
            newWorkout.Notes = NotesInput;
            newWorkout.Distance = Distance;
            newWorkout.Repetitions = Repetitions;
            newWorkout.Sets = Sets;

            workoutsWindow.Workouts.Add(newWorkout);

            // Återställer fälten
            SelectedWorkout = string.Empty;
            DateInput = DateTime.Now;
            DurationInput = TimeSpan.Zero;
            CaloriesBurnedInput = 0;
            NotesInput = string.Empty;
            Distance = 0;
            Repetitions = 0;
            Sets = 0;

            // Öppna WorkoutsWindow igen och stäng AddWorkoutWindow
            WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(workoutsWindow.userManager, workoutsWindow);
            newWorkoutsWindow.Show();
            addWorkoutWindow.Close();
        }

        // Metod som gör att Cardio eller Strength "egenskaper" uppdateras varje gång SelectedWorkout ändras.
        private void UpdateVisibility()
        {
            CardioVisibility = selectedWorkout == "Cardio" ? Visibility.Visible : Visibility.Collapsed;
            StrengthVisibility = selectedWorkout == "Strength" ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
