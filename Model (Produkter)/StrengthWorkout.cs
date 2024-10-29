using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class StrengthWorkout : WorkoutModel
    {   public string Equipment {  get; set; }
        public int Repetitions { get; set; }
        public StrengthWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes, string equipment, int repetitions)
        : base(date, "Strength", duration, caloriesBurned, notes) 
        { 
            Equipment = equipment;
            Repetitions = repetitions;
        }
        

        public override int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
