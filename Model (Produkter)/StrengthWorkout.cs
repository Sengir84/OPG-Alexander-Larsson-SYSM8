﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitTracker.Model__Produkter_
{
    public class StrengthWorkout : WorkoutModel, INotifyPropertyChanged
    {   public string Equipment {  get; set; }
        private int repetitions;
        public int Repetitions
        {
            get
            {
                return repetitions;
            }
            set
            {
                if (repetitions != value)
                {
                    repetitions = value;
                    OnPropertyChanged(nameof(Repetitions));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }
        public StrengthWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes, string equipment, int repetitions)
        : base(date, "Strength", duration, caloriesBurned, notes) 
        { 
            Equipment = equipment;
            Repetitions = repetitions;
        }
        

        public override int CalculateCaloriesBurned()
        {

            return Repetitions * 10;
        }
    }
}
