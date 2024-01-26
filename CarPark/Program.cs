using CarPark.Cars;
using System.Globalization;
using System.Threading;

namespace CarPark
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            culture.DateTimeFormat.LongTimePattern = "";
            Thread.CurrentThread.CurrentCulture = culture;

            FileManager fileManager = new(args);
                
            UserInterface userInterface = new();
            CarsRepo carsRepo = new();
            carsRepo.SetCars(fileManager.ReadFromBinaryFile());

            while (true)
            {
                carsRepo.SetAvaibleCars();
                carsRepo.SetRentedCars();
                userInterface.Startup();
                userInterface.SetUserChoice();
                userInterface.ValidateUserInput();
                fileManager.WriteToBinaryFile(carsRepo.GetCars());
            }
        }
    }
}
