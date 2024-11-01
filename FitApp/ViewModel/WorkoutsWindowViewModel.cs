using FitApp.Model;
using FitApp.MVVM;
using FitApp.Services;
using FitApp.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class WorkoutsWindowViewModel : ViewModelBase
    {
        // Egenskaper

        public Window workoutsWindow { get; private set; } 
        public ObservableCollection<Workout> Workouts { get; set; }

        // Referenser
        public Window? workoutDetailsWindow { get; private set; }
        public UserManager userManager { get; private set; }
        public RegisterWindowViewModel registerWindow;

        // Filteregenskaper
        public DateTime? FilterDate { get; set; }
        public string? FilterType { get; set; }
        public TimeSpan? FilterDuration { get; set; }

        // Egenskap som kollar ifall currentUser är AdminUser
        public bool IsAdmin => userManager.IsCurrentUserAdmin;

        // Egenskap som returnerar användarnamnet
        public string CurrentUserName
        {
            get => userManager.CurrentUser?.Username ?? "No User"; 
            set
            {
                // Detta kommer inte direkt sätta CurrentUserName, utan det behöver hanteras via CurrentUser
                if (userManager.CurrentUser != null)
                {
                    userManager.CurrentUser.Username = value;
                    OnPropertyChanged(nameof(CurrentUserName));
                }
            }
        }

        // Egenskaper för binding i UI
        private Workout selectedWorkout;
        public Workout SelectedWorkout 
        {
            get => selectedWorkout;
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
            }
        }

        // Kommando 
        public ICommand AddWorkoutCommand { get; }
        public ICommand UserDetailsCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand WorkoutDetailsCommand { get; }
        public ICommand InfoCommand { get; }

        public ICollectionView FilteredWorkouts { get; private set; }
        public ICommand FilterCommand { get; }
        public ICommand ClearFilterCommand { get; }

        // Konstruktor 
        public WorkoutsWindowViewModel(UserManager userManager, Window workoutsWindow)
        {

            this.userManager = userManager;
            this.workoutsWindow = workoutsWindow;

        // Initiera Workouts 
        Workouts = new ObservableCollection<Workout>();

        try
        { 
            // Hämta träningspass baserat på om användaren är admin eller ej
            if (userManager.IsCurrentUserAdmin)
            {
                foreach (User user in userManager.Users)
                {
                    foreach (Workout workout in user.Workouts)
                    {
                        // Lägg till användarnamn i anteckningarna för admin
                        Workout workoutCopy = workout;
                        workoutCopy.Notes += $"(User: {user.Username})";
                        Workouts.Add(workoutCopy);
                    }
                }
            }

            else
            {   // Lägg till null-check innan foreach
                if (userManager?.CurrentUser?.Workouts != null)  // Kollar både userManager och CurrentUser
                {
                    foreach (Workout workout in userManager.CurrentUser.Workouts)
                    {
                        Workouts.Add(workout);
                    }
                }
            }
        }

        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load workouts: {ex.Message}", 
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);         
        }

        // Initiera FilteredWorkouts
        FilteredWorkouts = CollectionViewSource.GetDefaultView(Workouts);
        FilteredWorkouts.Filter = FilterWorkouts;

        // Initiera kommando
        AddWorkoutCommand = new RelayCommand(AddWorkout);
        RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
        UserDetailsCommand = new RelayCommand(UserDetails);
        WorkoutDetailsCommand = new RelayCommand(() => WorkoutDetails(SelectedWorkout));
        SignOutCommand = new RelayCommand(SignOut);
        InfoCommand = new RelayCommand(Info);
        FilterCommand = new RelayCommand(ApplyFilter);
        ClearFilterCommand = new RelayCommand(ClearFilter);
        }

        // Metod som öppnar AddWorkoutWindow - och stänger WorkoutsWindow
        public void AddWorkout()
        {
            // Skapa fönstret först
            var addWorkoutWindow = new AddWorkoutWindow();

            // Skapa ViewModel med referens till det nya fönstret och denna ViewModel
            var viewModel = new AddWorkoutWindowViewModel(addWorkoutWindow, this);

            // Sätter DataContext  - öppnar AddworkoutWindow och stänger WorkoutsWindow
            addWorkoutWindow.DataContext = viewModel;
            addWorkoutWindow.Show();
            workoutsWindow?.Close();
        }

        // Metod som tar bort valt träningspass
        public void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                Workouts.Remove(SelectedWorkout);
            }
            else
            {
                MessageBox.Show("You must choose a workout to remove.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metod som öppnar användardetaljer - och stänger nuvarande fönster
        public void UserDetails()
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(userManager, registerWindow);
            userDetailsWindow.Show();

            if (workoutsWindow != null)
            {
                workoutsWindow.Close();
            }
            else
            {
                Console.WriteLine("workoutsWindow är null!"); // Debugutskrift
            }
        }

        // Metod som öppnar träningsdetaljer för alla träningspass- och stänger nuvarande fönster
        public void WorkoutDetails(Workout workout)
        {
            WorkoutDetailsWindow detailsWindow = new WorkoutDetailsWindow(workout, this);
            detailsWindow.Show();

            if (workoutsWindow != null)
            {
                workoutsWindow.Close();
            }
            else
            {
                Console.WriteLine("WorkoutsWindow är null!"); // Debugutskrift
            }
        }

        // Metod för utloggning
        public void SignOut()
        {
            userManager.SignOut();
            MainWindow mainWindow = new MainWindow(userManager);
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            workoutsWindow.Close();
        }

        // Metod för filtrering av träningspass
        private bool FilterWorkouts(object obj)
        {
            try
            {
                if (obj is not Workout workout) return false;
                if (FilterDate.HasValue && workout.DateTime.Date != FilterDate.Value.Date) return false;
                if (!string.IsNullOrEmpty(FilterType) && !workout.Type.Equals(FilterType, StringComparison.OrdinalIgnoreCase)) return false;
                if (FilterDuration.HasValue && workout.Duration < FilterDuration.Value) return false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filter operation failed: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // Metod som använder filter
        private void ApplyFilter()
        {
            FilteredWorkouts.Refresh();
        }

        // Metod som rensar filter
        private void ClearFilter()
        {
            FilterDate = null;
            FilterType = null;
            FilterDuration = null;
            FilteredWorkouts.Refresh();
        }

        // Metod som visar hjälpinformation
        public void Info()
        {
            MessageBox.Show("Welcome to the FitTrack!\n\n" +
                "- Use the 'User' button to access your profile.\n" +
                "- Click '+ Add Workout' to add a new workout.\n" +
                "- Use the 'Remove' button to delete a workout.\n" +
                "- To view workout details click 'See Details'.\n" +
                "* Caloriecount accurate for medium intensity workouts only.\n", 
                "App Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
