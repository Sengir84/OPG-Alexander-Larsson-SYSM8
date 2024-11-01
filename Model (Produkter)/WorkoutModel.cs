using FitTracker.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public abstract class WorkoutModel : ViewModelBase, IWorkout
    {
        //Fields
    public DateTime Date { get; set; } 
    public string Type { get; set; }
    public TimeSpan Duration { get; set; } 
    public int CaloriesBurned { get; set; }
    public string Notes { get; set; }
        //Konstruktor
    public WorkoutModel() { }
    public WorkoutModel(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes)
    {
        Date = date;
        Type = type;
        Duration = duration;
        CaloriesBurned = caloriesBurned;
        Notes = notes;
    }
        //Metod som ska ärvas
        public abstract int CalculateCaloriesBurned();
        
    }
}
