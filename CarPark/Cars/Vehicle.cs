using System;

namespace CarPark.Cars
{
    [Serializable]
    internal class Vehicle
    {
        internal int id { get; set; }
        internal string? name { get; set; }
        internal double? balance { get; set; }
        internal int? term { get; set; }
        internal DateTime? dataWyp { get; set; }
        internal DateTime? dataZwr { get; set; }
        internal string marka { get; set; }
        internal string model { get; set; }
        internal DateTime data_prod { get; set; }
        internal double value { get; set; }
        internal double? amortyzacja { get; set; }
        internal double odometer { get; set; }

        public Vehicle(string marka, string model, DateTime data_prod, double value, double odometer, int id) : this(data_prod, value, odometer, id)
        {
            this.marka = marka;
            this.model = model;
        }

        public Vehicle(DateTime data_prod, double value, double odometer, int id)
        {
            this.data_prod = data_prod;
            this.value = value;
            this.odometer = odometer;
            this.id = id;
            name = null;
            balance = null;
            term = null;
            dataWyp = null;
            dataZwr = null;
            amortyzacja = null;
        }
    }
}
