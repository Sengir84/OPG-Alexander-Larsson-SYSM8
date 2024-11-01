using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class CardioWorkout : WorkoutModel
    {
        //Fields
        private int distance;
        public int Distance { get; set; }
     

        //Konstruktor
        public CardioWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes, int distance)
        : base(date, "Cardio", duration, caloriesBurned, notes) 
        { 
            Distance = distance;
        }
        //Metod för att räkna ut brända kalorier för kardio
        public override int CalculateCaloriesBurned()
        {
            return Distance * 60;
        }
    }
}
