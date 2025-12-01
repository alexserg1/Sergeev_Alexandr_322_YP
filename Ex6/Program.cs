using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex6
{
    public class TaxiCar
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Status { get; set; }
        public string Driver { get; set; }

        public TaxiCar(string brand, string model, int year, int mileage, string driver)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Mileage = mileage;
            Driver = driver;
            Status = "в работе";
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Марка: {Brand}");
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Год выпуска: {Year}");
            Console.WriteLine($"Пробег: {Mileage} км");
            Console.WriteLine($"Состояние: {Status}");
            Console.WriteLine($"Водитель: {Driver}");
        }

        public void SetStatus(string newStatus)
        {
            if (newStatus == "в работе" || newStatus == "на ремонте")
            {
                Status = newStatus;
                Console.WriteLine($"Статус изменен на: {newStatus}");
            }
            else
            {
                Console.WriteLine("Некорректный статус!");
            }
        }

        public void UpdateMileage(int newMileage)
        {
            if (newMileage >= Mileage)
            {
                Mileage = newMileage;
                Console.WriteLine($"Пробег обновлен: {newMileage} км");
            }
            else
            {
                Console.WriteLine("Новый пробег не может быть меньше текущего!");
            }
        }
    }

    public class TaxiPark
    {
        private List<TaxiCar> cars;

        public TaxiPark()
        {
            cars = new List<TaxiCar>();
        }

        public void AddCar(TaxiCar car)
        {
            cars.Add(car);
            Console.WriteLine("Автомобиль добавлен!");
        }

        public void DisplayAllCars()
        {
            Console.WriteLine("Список всех автомобилей:");
            foreach (var car in cars)
            {
                car.DisplayInfo();
                Console.WriteLine("---");
            }
        }

        public TaxiCar FindByDriver(string driverName)
        {
            return cars.FirstOrDefault(c => c.Driver == driverName);
        }

        public List<TaxiCar> GetCarsByStatus(string status)
        {
            return cars.Where(c => c.Status == status).ToList();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАДАЧА 6: ТАКСОПАРК ===\n");

            TaxiPark park = new TaxiPark();



            park.AddCar(new TaxiCar("Toyota", "Camry", 2020, 75000, "Иванов А.С."));
            park.AddCar(new TaxiCar("Hyundai", "Solaris", 2021, 45000, "Петров В.И."));
            park.AddCar(new TaxiCar("Kia", "Rio", 2019, 90000, "Сидоров М.П."));
            park.AddCar(new TaxiCar("Lada", "Vesta", 2022, 30000, "Козлова О.Л."));

            Console.WriteLine("\n" + new string('=', 50));


            park.DisplayAllCars();

            Console.WriteLine("\n" + new string('=', 50));


            var foundCar = park.FindByDriver("Петров В.И.");
            if (foundCar != null)
            {
                Console.WriteLine("Найденный автомобиль:");
                foundCar.DisplayInfo();
            }

            Console.WriteLine("\n" + new string('=', 50));


            Console.WriteLine("Изменяем статусы автомобилей:");
            foundCar?.SetStatus("на ремонте");

            var kia = park.FindByDriver("Сидоров М.П.");
            kia?.SetStatus("на ремонте");

            Console.WriteLine("\n" + new string('=', 50));


            Console.WriteLine("Обновляем пробег:");
            kia?.UpdateMileage(92000);

            Console.WriteLine("\n" + new string('=', 50));


            var workingCars = park.GetCarsByStatus("в работе");
            Console.WriteLine("Автомобили в работе:");
            foreach (var car in workingCars)
            {
                car.DisplayInfo();
                Console.WriteLine("---");
            }

            Console.WriteLine("\n" + new string('=', 50));


            var repairCars = park.GetCarsByStatus("на ремонте");
            Console.WriteLine("Автомобили на ремонте:");
            foreach (var car in repairCars)
            {
                car.DisplayInfo();
                Console.WriteLine("---");
            }

            Console.WriteLine("\n=== КОНЕЦ ЗАДАЧИ 6 ===\n");
        }
    }
}

