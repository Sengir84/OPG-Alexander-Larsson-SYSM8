using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Model__Produkter_
{
    public class AdminUser : User, IAdminUser
    {
        //Metod för att admins ska få upp alla användares workouts i sin lista
        public void ManageAllWorkouts()
        {
            WorkoutManager.Instance.PopulateAllWorkouts();
        }
    }
    
}
