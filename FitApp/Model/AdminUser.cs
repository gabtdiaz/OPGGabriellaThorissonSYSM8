using FitApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Model
{
    class AdminUser : User
    {
        //    public AdminUser(string Country, string Username, string Password ) : base ( Country, Username, Password )
        //    {
        //        User admin = new AdminUser( "Sverige", "admin", "ABCD1234" );
        //    }
        private UserManager userManager;
        private User User;
        private Workout Workout;


        public AdminUser(UserManager userManager)
        {
            this.userManager = userManager;
        }
    }
}
        // Metod för att hämta alla träningspass
//        public ObservableCollection<Workout> GetAllWorkouts()
//        {
//            ObservableCollection<Workout> allWorkouts = new ObservableCollection<Workout>();

//            foreach (User user in userManager.Users) // Anta att UserManager har en lista över användare
//            {
//                foreach (Workout workout in user.Workouts) // Anta att varje User har en lista av Workouts
//                {
//                    allWorkouts.Add(workout);
//                }
//            }

//            return allWorkouts;
//        }

//        // Metod för att ta bort ett träningspass
//        public void RemoveWorkout(Workout workout)
//        {
//            foreach (var user in userManager.Users)
//            {
//                if (user.Workouts.Contains(workout))
//                {
//                    user.Workouts.Remove(workout);
//                    break; // Avbryt när workout är borttagen
//                }
//            }
//        }

//        // Metod för att redigera ett träningspass
//        public void EditWorkout(Workout workout, Workout updatedWorkout)
//        {
//            foreach (var user in userManager.Users)
//            {
//                if (user.Workouts.Contains(workout))
//                {
//                    int index = user.Workouts.IndexOf(workout);
//                    user.Workouts[index] = updatedWorkout; // Ersätt med uppdaterad workout
//                    break; // Avbryt när workout är redigerad
//                }
//            }
//        }
//    }
//}
//}
