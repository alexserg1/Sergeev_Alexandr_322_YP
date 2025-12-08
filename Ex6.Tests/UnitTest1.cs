using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ex6.Tests
{
    [TestClass]
    public class TaxiCarTests
    {
        [TestMethod]
        public void TaxiCar_ValidConstructor_CreatesSuccessfully()
        {
            // Arrange & Act
            var car = new TaxiCar("Toyota", "Camry", 2020, 75000, "Иванов А.С.");

            // Assert
            Assert.AreEqual("Toyota", car.Brand);
            Assert.AreEqual("Camry", car.Model);
            Assert.AreEqual(2020, car.Year);
            Assert.AreEqual(75000, car.Mileage);
            Assert.AreEqual("Иванов А.С.", car.Driver);
            Assert.AreEqual("в работе", car.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TaxiCar_EmptyBrand_ThrowsArgumentException()
        {
            // Act
            new TaxiCar("", "Camry", 2020, 75000, "Иванов А.С.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TaxiCar_NullDriver_ThrowsArgumentException()
        {
            // Act
            new TaxiCar("Toyota", "Camry", 2020, 75000, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TaxiCar_InvalidYear_ThrowsArgumentOutOfRangeException()
        {
            // Act
            new TaxiCar("Toyota", "Camry", 1800, 75000, "Иванов А.С.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TaxiCar_NegativeMileage_ThrowsArgumentOutOfRangeException()
        {
            // Act
            new TaxiCar("Toyota", "Camry", 2020, -100, "Иванов А.С.");
        }

        [TestMethod]
        public void SetStatus_ValidStatus_SetsSuccessfully()
        {
            // Arrange
            var car = new TaxiCar("Toyota", "Camry", 2020, 75000, "Иванов А.С.");

            // Act
            car.SetStatus("на ремонте");

            // Assert
            Assert.AreEqual("на ремонте", car.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetStatus_InvalidStatus_ThrowsArgumentException()
        {
            // Arrange
            var car = new TaxiCar("Toyota", "Camry", 2020, 75000, "Иванов А.С.");

            // Act
            car.SetStatus("некорректный статус");
        }

        [TestMethod]
        public void UpdateMileage_ValidMileage_UpdatesSuccessfully()
        {
            // Arrange
            var car = new TaxiCar("Toyota", "Camry", 2020, 75000, "Иванов А.С.");

            // Act
            car.UpdateMileage(80000);

            // Assert
            Assert.AreEqual(80000, car.Mileage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateMileage_LowerMileage_ThrowsArgumentException()
        {
            // Arrange
            var car = new TaxiCar("Toyota", "Camry", 2020, 75000, "Иванов А.С.");

            // Act
            car.UpdateMileage(70000);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateMileage_NegativeMileage_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var car = new TaxiCar("Toyota", "Camry", 2020, 75000, "Иванов А.С.");

            // Act
            car.UpdateMileage(-100);
        }
    }

    [TestClass]
    public class TaxiParkTests
    {
        private TaxiPark _park;
        private TaxiCar _car1;
        private TaxiCar _car2;

        [TestInitialize]
        public void Initialize()
        {
            _park = new TaxiPark();
            _car1 = new TaxiCar("Toyota", "Camry", 2020, 75000, "Иванов А.С.");
            _car2 = new TaxiCar("Hyundai", "Solaris", 2021, 45000, "Петров В.И.");
        }

        [TestMethod]
        public void AddCar_ValidCar_AddsSuccessfully()
        {
            // Act
            _park.AddCar(_car1);

            // Assert
            Assert.AreEqual(1, _park.CarCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCar_NullCar_ThrowsArgumentNullException()
        {
            // Act
            _park.AddCar(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddCar_DuplicateDriver_ThrowsInvalidOperationException()
        {
            // Arrange
            _park.AddCar(_car1);
            var duplicateCar = new TaxiCar("Kia", "Rio", 2019, 90000, "Иванов А.С.");

            // Act
            _park.AddCar(duplicateCar);
        }

        [TestMethod]
        public void RemoveCar_ExistingCar_RemovesSuccessfully()
        {
            // Arrange
            _park.AddCar(_car1);
            _park.AddCar(_car2);

            // Act
            bool result = _park.RemoveCar(_car1);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _park.CarCount);
        }

        [TestMethod]
        public void RemoveCar_NonExistingCar_ReturnsFalse()
        {
            // Arrange
            _park.AddCar(_car1);

            // Act
            bool result = _park.RemoveCar(_car2);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, _park.CarCount);
        }

        [TestMethod]
        public void FindByDriver_ExistingDriver_ReturnsCar()
        {
            // Arrange
            _park.AddCar(_car1);
            _park.AddCar(_car2);

            // Act
            var foundCar = _park.FindByDriver("Иванов А.С.");

            // Assert
            Assert.IsNotNull(foundCar);
            Assert.AreEqual("Toyota", foundCar.Brand);
        }

        [TestMethod]
        public void FindByDriver_NonExistingDriver_ReturnsNull()
        {
            // Arrange
            _park.AddCar(_car1);

            // Act
            var foundCar = _park.FindByDriver("Несуществующий Водитель");

            // Assert
            Assert.IsNull(foundCar);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindByDriver_EmptyDriverName_ThrowsArgumentException()
        {
            // Act
            _park.FindByDriver("");
        }

        [TestMethod]
        public void GetCarsByStatus_ValidStatus_ReturnsCorrectCars()
        {
            // Arrange
            _park.AddCar(_car1);
            _park.AddCar(_car2);
            _car1.SetStatus("на ремонте");

            // Act
            var workingCars = _park.GetCarsByStatus("в работе");
            var repairCars = _park.GetCarsByStatus("на ремонте");

            // Assert
            Assert.AreEqual(1, workingCars.Count);
            Assert.AreEqual(1, repairCars.Count);
            Assert.AreEqual("Hyundai", workingCars[0].Brand);
            Assert.AreEqual("Toyota", repairCars[0].Brand);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCarsByStatus_InvalidStatus_ThrowsArgumentException()
        {
            // Act
            _park.GetCarsByStatus("некорректный статус");
        }

        [TestMethod]
        public void GetCarsByBrand_ValidBrand_ReturnsCorrectCars()
        {
            // Arrange
            _park.AddCar(_car1);
            _park.AddCar(_car2);

            // Act
            var toyotaCars = _park.GetCarsByBrand("Toyota");

            // Assert
            Assert.AreEqual(1, toyotaCars.Count);
            Assert.AreEqual("Camry", toyotaCars[0].Model);
        }

        [TestMethod]
        public void GetCarsOlderThan_ValidYears_ReturnsCorrectCars()
        {
            // Arrange
            var oldCar = new TaxiCar("Lada", "2107", 2015, 150000, "Староверов С.П.");
            _park.AddCar(_car1); // 2020
            _park.AddCar(oldCar); // 2015

            // Act
            var oldCars = _park.GetCarsOlderThan(5); // Старше 5 лет

            // Assert
            Assert.AreEqual(1, oldCars.Count);
            Assert.AreEqual("Lada", oldCars[0].Brand);
        }

        [TestMethod]
        public void DisplayAllCars_EmptyPark_DisplaysEmptyMessage()
        {
            // Arrange
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _park.DisplayAllCars();

                // Assert
                var output = sw.ToString();
                StringAssert.Contains(output, "Таксопарк пуст");
            }
        }
    }
}