using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class WorkoutManager
    {
        private static WorkoutManager instance;

        public ObservableCollection<IWorkout> WorkoutList { get; set; }

        private WorkoutManager() 
        { 
            WorkoutList = new ObservableCollection<IWorkout>();
            AddDefaultWorkout();
        }

        public static WorkoutManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WorkoutManager();
                }
                return instance;
            }
        }

        public void AddWorkout(WorkoutModel workout)
        {
            WorkoutList.Add(workout);
        }

        public void RemoveWorkout(IWorkout workout)
        {
            WorkoutList.Remove(workout);
        }
        
        private void AddDefaultWorkout()
        {
            WorkoutList.Add(new StrengthWorkout(
            date: new DateTime(2024, 10, 27, 16, 32, 00), 
            "Strength",                             
            new TimeSpan(1, 22, 30),            
            500,                          
            "Full body workout",                    
            "Dumbbells",                       
            12                               
));
        }
        
        
    }
}
