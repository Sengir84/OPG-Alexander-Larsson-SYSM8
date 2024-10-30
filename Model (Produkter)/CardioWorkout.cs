using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class CardioWorkout : WorkoutModel
    {
        private double distance;
        public double Distance { get; set; }
     


                public CardioWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes, double distance)
        : base(date, "Cardio", duration, caloriesBurned, notes) 
        { 
            Distance = distance;
        }

        public override int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
