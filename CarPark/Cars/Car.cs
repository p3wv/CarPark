using System;

namespace CarPark.Cars
{
    [Serializable]
    internal class Car : Vehicle 
    {
        public Car(DateTime data_prod, double value, double odometer, int id) : base(data_prod, value, odometer, id)
        {
            marka = "Fiat";
            model = "Panda";
        }
    }
}
