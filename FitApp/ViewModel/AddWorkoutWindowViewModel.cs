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
        private WorkoutsWindowViewModel workoutsViewModel; // Använd WorkoutsWindowViewModel
        public Window addWorkoutWindow;

        public ObservableCollection<string> WorkoutTypeComboBox { get; set; }
       
        // Synlighets-egenskaper
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
        private string selectedWorkout;

        public string SelectedWorkout
        {
            get => selectedWorkout;
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
                OnPropertyChanged(DateInput.ToString());
            }
        }

        private TimeSpan durationInput;

        public TimeSpan DurationInput
        {
            get { return durationInput; }
            set 
            { 
                durationInput = value;
                OnPropertyChanged(DurationInput.ToString());
            }
        }
        private int caloriesBurnedInput;

        public int CaloriesBurnedInput
        {
            get { return caloriesBurnedInput; }
            set 
            { 
                caloriesBurnedInput = value; 
                OnPropertyChanged(CaloriesBurnedInput.ToString());
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

        public ICommand SaveCommand { get; }

        // Konstruktor - Anropar både WorkoutsWindowViewModel och MainWindowViewModels konstruktorer
        public AddWorkoutWindowViewModel(Window addWorkoutWindow, WorkoutsWindowViewModel workoutsViewModel) 
        {
            this.workoutsViewModel = workoutsViewModel;
            WorkoutTypeComboBox = new ObservableCollection<string> { "Cardio", "Strength" };
            this.addWorkoutWindow = addWorkoutWindow;
            SaveCommand = new RelayCommand(SaveWorkout);
        } 

        // Metod som sparar träningspass förutsatt att varje fält är ifyllt.
        public void SaveWorkout() 
        {
            if (string.IsNullOrWhiteSpace(DateInput.ToString()) || string.IsNullOrWhiteSpace(DurationInput.ToString()) || // Kontrollerar ifall textboxarna är tomma
           string.IsNullOrWhiteSpace(CaloriesBurnedInput.ToString()) || string.IsNullOrWhiteSpace(NotesInput) || WorkoutTypeComboBox == null)
            {
                MessageBox.Show("Please enter all the information correctly.", "Error", MessageBoxButton.OK);
                return;
            }

            // Spara träningspasset i Workouts listan från WorkoutsWindowViewModel
            Workout newWorkout = SelectedWorkout == "Cardio" ? new CardioWorkout() : new StrengthWorkout();
            newWorkout.Type = SelectedWorkout;
            newWorkout.DateTime = DateInput;
            newWorkout.Duration = DurationInput;
            newWorkout.CaloriesBurned = CaloriesBurnedInput;
            newWorkout.Notes = NotesInput;

            workoutsViewModel.Workouts.Add(newWorkout);

            // Återställer fälten
            SelectedWorkout = string.Empty;
            DateInput = DateTime.Now;
            DurationInput = TimeSpan.Zero;
            CaloriesBurnedInput = 0;
            NotesInput = string.Empty;
        }

        // Metod som gör att Cardio eller Strength "egenskaper" uppdateras varje gång SelectedWorkout ändras.
        private void UpdateVisibility()
        {
            CardioVisibility = selectedWorkout == "Cardio" ? Visibility.Visible : Visibility.Collapsed;
            StrengthVisibility = selectedWorkout == "Strength" ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
