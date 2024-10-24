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
        public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan Duration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CaloriesBurned { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Notes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
