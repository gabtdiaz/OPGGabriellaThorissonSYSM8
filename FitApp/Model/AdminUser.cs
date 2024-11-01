using System.Collections.ObjectModel;

namespace FitApp.Model
{
    public class AdminUser : User
    {
        public AdminUser(string country, string username, string password)
            : base(country, username, password)
        {}

        // Metod för att se alla workouts
        public ObservableCollection<Workout> ManageAllWorkouts(ObservableCollection<Workout> existingWorkouts)
        {
            return existingWorkouts;  // Returnerar alla workouts som finns
        }
    }
}

