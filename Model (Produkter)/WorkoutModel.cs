using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class WorkoutModel : ViewModelBase, IWorkout
    {
        public DateTime Date {get;set;}
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        public WorkoutModel() { }
        public WorkoutModel(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes)
        {
            Date = date;
            Type = type;
            Duration = duration;
            CaloriesBurned = caloriesBurned;
            Notes = notes;
        }

        public int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
