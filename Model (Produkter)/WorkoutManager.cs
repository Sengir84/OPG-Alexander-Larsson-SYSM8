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
        private static WorkoutManager instance;
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
        public ObservableCollection<IWorkout> AllWorkouts { get; private set; } = new ObservableCollection<IWorkout>();

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

        public event Action OnWorkoutAdded;

        public void AddWorkout(IWorkout workout)
        {
            if (UserManager.Instance.ActiveUser != null)
            {
                UserManager.Instance.ActiveUser.Workouts.Add(workout);
                AllWorkouts.Add(workout);
                OnWorkoutAdded?.Invoke();
                
            }
        }

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
               
                AllWorkouts.Remove(workout);
            }
            else if (activeUser != null)
            {
                
                activeUser.Workouts.Remove(workout);
            }
            else
            {
                Debug.WriteLine("No active user to remove workout.");
            }
        }




    }
}
