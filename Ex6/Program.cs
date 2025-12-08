using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex6
{
    public class TaxiCar
    {
        private string _brand;
        private string _model;
        private int _year;
        private int _mileage;

        public string Brand
        {
            get => _brand;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Марка не может быть пустой", nameof(value));
                _brand = value;
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Модель не может быть пустой", nameof(value));
                _model = value;
            }
        }

        public int Year
        {
            get => _year;
            set
            {
                if (value < 1900 || value > DateTime.Now.Year + 1)
                    throw new ArgumentOutOfRangeException(nameof(value),
                        $"Год должен быть между 1900 и {DateTime.Now.Year + 1}");
                _year = value;
            }
        }

        public int Mileage
        {
            get => _mileage;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Пробег не может быть отрицательным");
                _mileage = value;
            }
        }

        public string Status { get; private set; }
        public string Driver { get; set; }

        public TaxiCar(string brand, string model, int year, int mileage, string driver)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Mileage = mileage;

            if (string.IsNullOrWhiteSpace(driver))
                throw new ArgumentException("Водитель не может быть пустым", nameof(driver));
            Driver = driver;

            Status = "в работе";
        }

        public void DisplayInfo()
        {
            if (string.IsNullOrWhiteSpace(Brand) || string.IsNullOrWhiteSpace(Model))
                throw new InvalidOperationException("Данные автомобиля неполны");

            Console.WriteLine($"Марка: {Brand}");
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Год выпуска: {Year}");
            Console.WriteLine($"Пробег: {Mileage} км");
            Console.WriteLine($"Состояние: {Status}");
            Console.WriteLine($"Водитель: {Driver}");
        }

        public void SetStatus(string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                throw new ArgumentException("Статус не может быть пустым", nameof(newStatus));

            if (newStatus == "в работе" || newStatus == "на ремонте")
            {
                Status = newStatus;
                Console.WriteLine($"Статус изменен на: {newStatus}");
            }
            else
            {
                throw new ArgumentException($"Некорректный статус: {newStatus}. " +
                    $"Допустимые значения: 'в работе', 'на ремонте'", nameof(newStatus));
            }
        }

        public void UpdateMileage(int newMileage)
        {
            if (newMileage < 0)
                throw new ArgumentOutOfRangeException(nameof(newMileage),
                    "Пробег не может быть отрицательным");

            if (newMileage < Mileage)
                throw new ArgumentException($"Новый пробег ({newMileage}) не может быть меньше текущего ({Mileage})",
                    nameof(newMileage));

            int oldMileage = Mileage;
            Mileage = newMileage;
            Console.WriteLine($"Пробег обновлен с {oldMileage} км на {newMileage} км");
        }
    }

    public class TaxiPark
    {
        private List<TaxiCar> cars;

        public int CarCount => cars.Count;

        public IEnumerable<TaxiCar> AllCars => cars.AsReadOnly();

        public TaxiPark()
        {
            cars = new List<TaxiCar>();
        }

        public void AddCar(TaxiCar car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car), "Автомобиль не может быть null");
            if (cars.Any(c => c.Driver == car.Driver))
                throw new InvalidOperationException($"Водитель {car.Driver} уже закреплен за другим автомобилем");

            cars.Add(car);
            Console.WriteLine($"Автомобиль {car.Brand} {car.Model} добавлен!");
        }

        public bool RemoveCar(TaxiCar car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            return cars.Remove(car);
        }

        public void DisplayAllCars()
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("Таксопарк пуст");
                return;
            }

            Console.WriteLine($"Список всех автомобилей ({cars.Count} шт.):");
            foreach (var car in cars)
            {
                try
                {
                    car.DisplayInfo();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка отображения автомобиля: {ex.Message}");
                }
                Console.WriteLine("---");
            }
        }

        public TaxiCar FindByDriver(string driverName)
        {
            if (string.IsNullOrWhiteSpace(driverName))
                throw new ArgumentException("Имя водителя не может быть пустым", nameof(driverName));

            return cars.FirstOrDefault(c =>
                c.Driver.Equals(driverName, StringComparison.OrdinalIgnoreCase));
        }

        public List<TaxiCar> GetCarsByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Статус не может быть пустым", nameof(status));

            if (status != "в работе" && status != "на ремонте")
                throw new ArgumentException($"Некорректный статус: {status}", nameof(status));

            return cars.Where(c => c.Status == status).ToList();
        }

        public List<TaxiCar> GetCarsByBrand(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Марка не может быть пустой", nameof(brand));

            return cars.Where(c =>
                c.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<TaxiCar> GetCarsOlderThan(int years)
        {
            if (years < 0)
                throw new ArgumentOutOfRangeException(nameof(years),
                    "Количество лет не может быть отрицательным");

            int currentYear = DateTime.Now.Year;
            return cars.Where(c => (currentYear - c.Year) > years).ToList();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАДАЧА 6: ТАКСОПАРК ===\n");

            try
            {
                TaxiPark park = new TaxiPark();
                AddCarsSafely(park);

                Console.WriteLine("\n" + new string('=', 50));

                park.DisplayAllCars();

                Console.WriteLine("\n" + new string('=', 50));
                try
                {
                    var foundCar = park.FindByDriver("Петров В.И.");
                    if (foundCar != null)
                    {
                        Console.WriteLine("Найденный автомобиль:");
                        foundCar.DisplayInfo();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при поиске: {ex.Message}");
                }

                Console.WriteLine("\n" + new string('=', 50));
                try
                {
                    Console.WriteLine("Изменяем статусы автомобилей:");
                    var petrovCar = park.FindByDriver("Петров В.И.");
                    petrovCar?.SetStatus("на ремонте");

                    var kia = park.FindByDriver("Сидоров М.П.");
                    kia?.SetStatus("на ремонте");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при изменении статуса: {ex.Message}");
                }

                Console.WriteLine("\n" + new string('=', 50));

                try
                {
                    Console.WriteLine("Обновляем пробег:");
                    var kia = park.FindByDriver("Сидоров М.П.");
                    kia?.UpdateMileage(92000);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении пробега: {ex.Message}");
                }

                Console.WriteLine("\n" + new string('=', 50));

                try
                {
                    var workingCars = park.GetCarsByStatus("в работе");
                    Console.WriteLine($"Автомобили в работе ({workingCars.Count} шт.):");
                    foreach (var car in workingCars)
                    {
                        car.DisplayInfo();
                        Console.WriteLine("---");
                    }

                    Console.WriteLine("\n" + new string('=', 50));

                    var repairCars = park.GetCarsByStatus("на ремонте");
                    Console.WriteLine($"Автомобили на ремонте ({repairCars.Count} шт.):");
                    foreach (var car in repairCars)
                    {
                        car.DisplayInfo();
                        Console.WriteLine("---");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении автомобилей по статусу: {ex.Message}");
                }
                Console.WriteLine("\n" + new string('=', 50));
                try
                {
                    var toyotaCars = park.GetCarsByBrand("Toyota");
                    Console.WriteLine($"Автомобили Toyota: {toyotaCars.Count} шт.");

                    var oldCars = park.GetCarsOlderThan(3);
                    Console.WriteLine($"Автомобили старше 3 лет: {oldCars.Count} шт.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                Console.WriteLine("\n=== КОНЕЦ ЗАДАЧИ 6 ===\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка в программе: {ex.Message}");
                Console.WriteLine("Детали ошибки:");
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void AddCarsSafely(TaxiPark park)
        {
            try
            {
                park.AddCar(new TaxiCar("Toyota", "Camry", 2020, 75000, "Иванов А.С."));
                park.AddCar(new TaxiCar("Hyundai", "Solaris", 2021, 45000, "Петров В.И."));
                park.AddCar(new TaxiCar("Kia", "Rio", 2019, 90000, "Сидоров М.П."));
                park.AddCar(new TaxiCar("Lada", "Vesta", 2022, 30000, "Козлова О.Л."));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении автомобиля: {ex.Message}");
            }
        }
    }
}