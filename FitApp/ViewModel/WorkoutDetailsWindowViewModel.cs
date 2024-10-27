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
        // Egenskaper
        public Window workoutDetailsWindow;

        // Egenskap för att få tillgång till Workouts från WorkoutsWindowViewModel
        private WorkoutsWindowViewModel workoutsWindow;
        public ObservableCollection<Workout> Workouts
        {
            get { return workoutsWindow.Workouts; }
        }

        // Egenskap för det träningspass som redigeras
        private Workout _workout;
        public Workout _Workout
        {
            get { return _workout; }
            set
            {
                _workout = value;
                OnPropertyChanged(nameof(_Workout));
            }
        }

        // Egenskap för det valda träningspasset
        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
            }
        }

        // Egenskap som kontrollerar redigering
        private bool isEditing;
        public bool IsEditing
        {
            get { return isEditing; }
            private set
            {
                isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
                if (value == true && SelectedWorkout != null)
                {
                    CopyWorkoutProperties();
                }
            }
        }



        // Initialisera commands
        public ICommand EditWorkoutCommand => new RelayCommand(EditWorkout);
        public ICommand SaveWorkoutCommand => new RelayCommand(SaveWorkout);
        public ICommand CancelCommand => new RelayCommand(Cancel);


        // Konstruktor
        public WorkoutDetailsWindowViewModel(Workout workout, Window workoutDetailsWindow, WorkoutsWindowViewModel workoutsWindow)
        {
            this._workout = workout;
            this.workoutDetailsWindow = workoutDetailsWindow;
            this.workoutsWindow = workoutsWindow;

            IsEditing = false; // Fälten är låsta i början
        }

        // Metod för att redigera valt träningspass
        private void EditWorkout()
        {
            IsEditing = true; // Låser upp fälten för att kunna redigera

            if (SelectedWorkout != null)
            {
                CopyWorkoutProperties();
            }
        }

        // Metod för att spara redigeringar, samt återgå till WorkoutsWindow
        private void SaveWorkout()
        {
            IsEditing = false; // Lås fälten igen

            // Öppna WorkoutsWindow igen och stäng WorkoutDetailsWindow
            WorkoutsWindow newWorkoutsWindow = new WorkoutsWindow(workoutsWindow.userManager, workoutsWindow);
            newWorkoutsWindow.Show();
            workoutDetailsWindow.Close();
        }

        // Metod för att "kopiera" de valda träningspassets egenskaper
        private void CopyWorkoutProperties()
        {
            _workout.Type = SelectedWorkout.Type;
            _workout.DateTime = SelectedWorkout.DateTime;
            _workout.Duration = SelectedWorkout.Duration;
            _workout.CaloriesBurned = SelectedWorkout.CaloriesBurned;
            _workout.Notes = SelectedWorkout.Notes;
            _workout.Sets = SelectedWorkout.Sets;
            _workout.Repetitions = SelectedWorkout.Repetitions;
            _workout.Distance = SelectedWorkout.Distance;
        }
        private void Cancel()
        {
            IsEditing = false;
            Workout _Workout = new Workout(); // Återställ till tomt workout
        }
    }
}
