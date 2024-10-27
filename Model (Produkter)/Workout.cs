using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class Workout : ViewModelBase, IWorkout
    {
        public int Id { get; set; }
        public DateTime Date {get;set;}
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        public int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
