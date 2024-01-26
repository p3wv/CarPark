using System.Collections.Generic;
using System.Text;

namespace CarPark.Cars
{
    internal class CarsRepo
    {
        static List<Vehicle> VehiclesList { get; set; } = new();
        static List<Vehicle> AvaibleVehiclesList { get; set; } = new();
        static List<Vehicle> RentedVehiclesList { get; set; } = new();

        public void AddCar(Vehicle vehicle)
        {
            VehiclesList.Add(vehicle);
        }

        public List<Vehicle> GetCars()
        {
            return VehiclesList;
        }

        public List<Vehicle> GetAvaibleCars()
        {
            return AvaibleVehiclesList;
        }

        public List<Vehicle> GetRentedCars()
        {
            return RentedVehiclesList;
        }

        public void SetCars(List<Vehicle> cars)
        {
            VehiclesList = cars;
        }

        public Vehicle GetCarById(int id)
        {
            return VehiclesList.Find(car => car.id == id);
        }

        public void EditCar(Vehicle car)
        {
            int id = VehiclesList.FindIndex(x => x.id == car.id);
            VehiclesList[id] = car;
        }

        public void DeleteCarById(int id)
        {
            VehiclesList.Remove(GetCarById(id));
        }

        public int GetLastCarId()
        {
            if(VehiclesList.Count == 0)
            {
                return 0;
            }
            else
            {
                return VehiclesList[^1].id;
            }
            
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            
            foreach (var vehicle in VehiclesList)
            {
                sb.AppendLine($"{ vehicle.id}. {vehicle.marka} {vehicle.model} ");
                sb.AppendLine($"Wynajęty przez: {vehicle.name}");
                sb.AppendLine($"Na okres: {vehicle.term} dni");
                sb.AppendLine($"Cena wynajmu: {vehicle.balance}");
                sb.AppendLine($"Od: {vehicle.dataWyp} do {vehicle.dataZwr}");
                sb.AppendLine($"Data produkcji: {vehicle.data_prod}");
                sb.AppendLine($"Wartość pojazdu: {vehicle.value}");
                sb.AppendLine($"Koszty amortyzacji: {vehicle.value}");
                sb.AppendLine($"Przebieg: {vehicle.odometer}");
                sb.AppendLine("");
            }
            
            return sb.ToString();
        }

        public void SetAvaibleCars()
        {
            AvaibleVehiclesList = new();
            foreach (var car in VehiclesList)
            {
                if (car.name == null)
                {
                    AvaibleVehiclesList.Add(car);
                }
            }
        }

        public void SetRentedCars()
        {
            RentedVehiclesList = new();
            foreach (var car in VehiclesList)
            {
                if (car.name != null)
                {
                    RentedVehiclesList.Add(car);
                }
            }
        }

        public string GetAvaibleCarsInfo()
        {
            StringBuilder sb = new();

            foreach (var car in AvaibleVehiclesList)
            {
                sb.AppendLine($"{ car.id}. {car.marka} {car.model} Data prod.: {car.data_prod}  Wartość: {car.value} Przebieg: {car.odometer}");
            }
            
            return sb.ToString();
        }

        public string GetRentedCarsInfo()
        {
            StringBuilder sb = new();

            foreach (var car in RentedVehiclesList)
            {
                sb.AppendLine($"{ car.id}. {car.marka} {car.model} Imie i nazwisko: {car.name} Koszt wyp.: {car.balance + car.amortyzacja}  Od: {car.dataWyp} Do: {car.dataZwr}");
            }

            return sb.ToString();
        }
    }
}
