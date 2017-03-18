using System;
using MyItems.DomainLogic;

namespace Training
{

    public class DistanceWorkout : Workout
    {
        public double Distance { get; }
        public double Pace { get; }
        public DistanceWorkout(double distance, DateTime datetime, TimeSpan duration, double rate, string notes) 
            : base(datetime, duration, rate, notes)
        {
            Distance = distance;
            Pace = distance / duration.TotalHours;
        }
    }

    public class BikeWorkout : DistanceWorkout
    {
        public WorkoutType Type { get; }

        public BikeWorkout(WorkoutType type, double distance, DateTime datetime, TimeSpan duration, double rate, string notes) 
            : base(distance, datetime, duration, rate, notes)
        {
            Type = type;
        }
    }
}