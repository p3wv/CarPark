using System;

namespace CarPark.Cars
{
    [Serializable]
    internal class Muscle : Vehicle
    {
        public Muscle(DateTime data_prod, double value, double odometer, int id) : base(data_prod, value, odometer, id)
        {
            marka = "Ford";
            model = "Mustang";
        }
    }
}
