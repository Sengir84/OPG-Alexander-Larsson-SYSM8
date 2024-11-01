using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class WorkoutManager
    {
        //Skapa singleton av Workoutmanager så att den kan användas överallt
        private static WorkoutManager instance;
        //Returnerar Workoutmanagern
        public static WorkoutManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WorkoutManager();
                    instance.PopulateAllWorkouts();
                }
                return instance;
            }
        }
        //Lista som samlar alla workouts till admin
        public ObservableCollection<IWorkout> AllWorkouts { get; private set; } = new ObservableCollection<IWorkout>();
        
        //Metod för att lägga till workouts i Allworkouts
        public void PopulateAllWorkouts()
        {
            AllWorkouts.Clear();
            foreach (var user in UserManager.Instance.Users)
            {
                foreach (var workout in user.Workouts)
                {
                    AllWorkouts.Add(workout);
                }
            }
        }
        //En action som triggas för att uppdatera listan när man sparar en ny workout
        public event Action OnWorkoutAdded;
        //Metod för att lägga till workout i både workouts och Allworkouts
        public void AddWorkout(IWorkout workout)
        {
            if (UserManager.Instance.ActiveUser != null)
            {
                UserManager.Instance.ActiveUser.Workouts.Add(workout);
                AllWorkouts.Add(workout);
                OnWorkoutAdded?.Invoke();
                
            }
        }
        //Metod för att ta bort workouts där admins tar bort både för sig själv och 
        //andra users genom att loopa igenom alla workoutslistor
        public void RemoveWorkout(IWorkout workout)
        {
            var activeUser = UserManager.Instance.ActiveUser;
            if (activeUser is AdminUser)
            {
                
                foreach (var user in UserManager.Instance.Users)
                {
                    if (user.Workouts.Contains(workout))
                    {
                        user.Workouts.Remove(workout);
                        break;
                    }
                }
               //Tar bort från alla workouts
                AllWorkouts.Remove(workout);
            }
            else if (activeUser != null)
            {
                //Tar bort från nuvarande inloggade user
                activeUser.Workouts.Remove(workout);
            }
            else
            {
                Debug.WriteLine("No active user to remove workout.");
            }
        }




    }
}
