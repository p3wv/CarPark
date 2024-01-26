using CarPark.Cars;
using System;

namespace CarPark
{
    class UserInterface
    {
        const ConsoleKey RentCarOption = ConsoleKey.D1;
        const ConsoleKey ReturnCarOption = ConsoleKey.D2;
        const ConsoleKey AddNewCarOption = ConsoleKey.D3;
        const ConsoleKey DeleteExistingCarOption = ConsoleKey.D4;
        const ConsoleKey GetCarsInfoOption = ConsoleKey.D5;
        const ConsoleKey EscapeOption = ConsoleKey.Escape;
        ConsoleKey UserChoice { get; set; }
        CarsRepo carsRepo = new();

        public void Startup()
        {
            Console.WriteLine("Witaj użytkowniku! Co chcesz zrobić?");
            Console.WriteLine("1. Wypożycz samochód.");
            Console.WriteLine("2. Zwróć samochód.");
            Console.WriteLine("3. Dodaj nowy samochód.");
            Console.WriteLine("4. Usuń istniejący samochód.");
            Console.WriteLine("5. Sprawdź informacje o samochodach.");
            Console.WriteLine("ESC Zapisanie i wyjście z aplikacji.");
        }

        public void SetUserChoice()
        {
            UserChoice = (ConsoleKey)Console.ReadKey().KeyChar;
            Console.Clear();
        }

        public void ValidateUserInput()
        {
            switch (UserChoice)
            {
                case RentCarOption:
                    RentCar();
                    break;
                case ReturnCarOption:
                    ReturnCar();
                    break;
                case AddNewCarOption:
                    AddNewCar();
                    break;
                case DeleteExistingCarOption:
                    DeleteExistingCar();
                    break;
                case GetCarsInfoOption:
                    GetCarsInfo();
                    break;
                case EscapeOption:
                    Console.Clear();
                    Console.WriteLine("Stan aplikacji został zapisany pomyślnie.");
                    Console.WriteLine("Wciśnij dowolny przycisk aby zakończyć.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wprowadzono błędną wartość. Proszę wybrać jedną z opcji 1-4");
                    break;
            }
        }

        public void AddNewCar()
        {
            Console.Clear();
            Console.WriteLine("Dodawanie nowego pojazdu");
            Console.WriteLine("1. Fiat Panda");
            Console.WriteLine("2. Ford Mustang");
            Console.WriteLine("3. Ford Ranger");
            Console.WriteLine("4. Niezdefiniowany pojazd");

            SetUserChoice();
            AddCarBasedOnUserChoice();
        }

        public void GetCarsInfo()
        {
            Console.Clear();
            Console.WriteLine(carsRepo.ToString());
        }

        public void AddCarBasedOnUserChoice()
        {
            if (UserChoice.Equals(ConsoleKey.D1) || UserChoice.Equals(ConsoleKey.D2) || UserChoice.Equals(ConsoleKey.D3) || UserChoice.Equals(ConsoleKey.D4))
            {
                string marka = string.Empty;
                string model = string.Empty;

                if (UserChoice.Equals(ConsoleKey.D4))
                {
                    Console.WriteLine("Podaj markę:");
                    marka = Console.ReadLine();
                    Console.WriteLine("Podaj model:");
                    model = Console.ReadLine();
                }

                Console.WriteLine("Podaj datę produkcji (DD/MM/RRRR):");
                DateTime data_prod = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Podaj wartość pojazdu:");
                double value = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Podaj przebieg pojazdu:");
                double odometer = Convert.ToDouble(Console.ReadLine());
                int id = carsRepo.GetLastCarId() + 1;

                switch (UserChoice)
                {
                    case ConsoleKey.D1:
                        carsRepo.AddCar(new Car(data_prod, value, odometer, id));
                        break;
                    case ConsoleKey.D2:
                        carsRepo.AddCar(new Muscle(data_prod, value, odometer, id));
                        break;
                    case ConsoleKey.D3:
                        carsRepo.AddCar(new Truck(data_prod, value, odometer, id));
                        break;
                    case ConsoleKey.D4:
                        carsRepo.AddCar(new Vehicle(marka, model, data_prod, value, odometer, id));
                        break;
                }

                Console.Clear();
                Console.WriteLine("Samochód został dodany prawidłowo");
            }
            else
            {
                Console.WriteLine("Wprowadzono błędną wartość!");
            }
        }

        public void RentCar()
        {
            Console.Clear();
            Console.WriteLine("Samochody dostępne do wypożyczenia:");
            Console.WriteLine(carsRepo.GetAvaibleCarsInfo());

            try
            {
                int id = int.Parse(Console.ReadKey().KeyChar.ToString());
                bool isCarFound = false;
                foreach (var car in carsRepo.GetAvaibleCars())
                {
                    if (id == car.id)
                    {
                        Vehicle carToRent = carsRepo.GetCarById(id);
                        Console.WriteLine("Podaj imię i nazwisko klienta:");
                        carToRent.name = Console.ReadLine();
                        Console.WriteLine("Podaj koszt wypożyczenia:");
                        carToRent.balance = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Podaj date wypożyczenia (DD/MM/RRRR):");
                        carToRent.dataWyp = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Podaj date zwrotu (DD/MM/RRRR):");
                        carToRent.dataZwr = Convert.ToDateTime(Console.ReadLine());
                        TimeSpan difference = (TimeSpan)(carToRent.dataZwr - carToRent.dataWyp);
                        carToRent.term = difference.Days;
                        carToRent.amortyzacja = 0.1 * carToRent.balance;

                        carsRepo.EditCar(carToRent);
                        isCarFound = true;
                        break;
                    }
                }
                if (!isCarFound)
                {
                    Console.WriteLine("Wprowadzono błędny numer samochodu");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Wprowadzono błędny numer samochodu");
            }

            Console.WriteLine("Wypożyczono samochód pomyślnie");
        }

        public void ReturnCar()
        {
            Console.Clear();
            Console.WriteLine("Samochody dostępne do zwrócenia:");
            Console.WriteLine(carsRepo.GetRentedCarsInfo());

            try
            {
                int id = int.Parse(Console.ReadKey().KeyChar.ToString());
                bool isCarFound = false;
                foreach (var car in carsRepo.GetRentedCars())
                {
                    if (id == car.id)
                    {
                        Vehicle carToReturn = carsRepo.GetCarById(id);
                        carToReturn.name = null;
                        carToReturn.balance = null;
                        carToReturn.dataWyp = null;
                        carToReturn.dataZwr = null;
                        carToReturn.term = null;
                        carToReturn.amortyzacja = null;

                        carsRepo.EditCar(carToReturn);
                        isCarFound = true;
                        break;
                    }
                }
                if (!isCarFound)
                {
                    Console.WriteLine("Wprowadzono błędny numer samochodu");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Wprowadzono błędny numer samochodu");
            }

            Console.WriteLine("Zwrócono samochód pomyślnie");
        }

        public void DeleteExistingCar()
        {
            Console.Clear();
            Console.WriteLine("Który samochód chcesz usunąć? (Wypożyczone samochody nie mogą być usuniętę)");
            Console.WriteLine(carsRepo.GetAvaibleCarsInfo());

            try
            {
                int id = int.Parse(Console.ReadKey().KeyChar.ToString());
                bool isCarFound = false;
                foreach (var car in carsRepo.GetAvaibleCars())
                {
                    if (id == car.id)
                    {
                        carsRepo.DeleteCarById(id);
                        isCarFound = true;
                        break;
                    }
                }
                if (!isCarFound)
                {
                    Console.WriteLine("Wprowadzono błędny numer samochodu");
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Wprowadzono błędny numer samochodu");
            }
        }
    }
}
