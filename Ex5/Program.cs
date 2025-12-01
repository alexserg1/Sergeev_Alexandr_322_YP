using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex5
{
    public class ShelterAnimal
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public bool HasVaccinations { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string Status { get; set; }

        public ShelterAnimal(string name, string species, int age, bool hasVaccinations, DateTime admissionDate)
        {
            Name = name;
            Species = species;
            Age = age;
            HasVaccinations = hasVaccinations;
            AdmissionDate = admissionDate;
            Status = "в приюте";
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Кличка: {Name}");
            Console.WriteLine($"Вид: {Species}");
            Console.WriteLine($"Возраст: {Age} лет");
            Console.WriteLine($"Прививки: {(HasVaccinations ? "Да" : "Нет")}");
            Console.WriteLine($"Дата поступления: {AdmissionDate:dd.MM.yyyy}");
            Console.WriteLine($"Статус: {Status}");
        }

        public void MarkAsAdopted()
        {
            Status = "забрали домой";
            Console.WriteLine($"Животное {Name} отмечено как забранное домой!");
        }
    }

    public class AnimalShelter
    {
        private List<ShelterAnimal> animals;

        public AnimalShelter()
        {
            animals = new List<ShelterAnimal>();
        }

        public void AddAnimal(ShelterAnimal animal)
        {
            animals.Add(animal);
            Console.WriteLine("Животное добавлено!");
        }

        public List<ShelterAnimal> GetAnimalsWithoutVaccinations()
        {
            return animals.Where(a => !a.HasVaccinations).ToList();
        }

        public ShelterAnimal FindByName(string name)
        {
            return animals.FirstOrDefault(a => a.Name == name);
        }

        public void DisplayAllAnimals()
        {
            Console.WriteLine("Список всех животных:");
            foreach (var animal in animals)
            {
                animal.DisplayInfo();
                Console.WriteLine("---");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАДАЧА 5: ПИТОМНИК ДЛЯ БЕЗДОМНЫХ ЖИВОТНЫХ ===\n");

            AnimalShelter shelter = new AnimalShelter();


            shelter.AddAnimal(new ShelterAnimal("Cнежок", "кот", 2, true, new DateTime(2024, 1, 15)));
            shelter.AddAnimal(new ShelterAnimal("Друг", "собака", 3, false, new DateTime(2024, 2, 20)));
            shelter.AddAnimal(new ShelterAnimal("Баобаб", "кот", 1, true, new DateTime(2024, 3, 10)));
            shelter.AddAnimal(new ShelterAnimal("Кекс", "собака", 4, false, new DateTime(2024, 1, 5)));

            Console.WriteLine("\n" + new string('=', 50));


            shelter.DisplayAllAnimals();

            Console.WriteLine("\n" + new string('=', 50));


            var foundAnimal = shelter.FindByName("Cнежок");
            if (foundAnimal != null)
            {
                Console.WriteLine("Найденное животное:");
                foundAnimal.DisplayInfo();
            }

            Console.WriteLine("\n" + new string('=', 50));


            var unvaccinatedAnimals = shelter.GetAnimalsWithoutVaccinations();
            Console.WriteLine("Животные без прививок:");
            foreach (var animal in unvaccinatedAnimals)
            {
                animal.DisplayInfo();
                Console.WriteLine("---");
            }

            Console.WriteLine("\n" + new string('=', 50));


            Console.WriteLine("Отмечаем животных как забранных домой:");
            foundAnimal?.MarkAsAdopted();

            var rex = shelter.FindByName("Кекс");
            rex?.MarkAsAdopted();

            Console.WriteLine("\n" + new string('=', 50));


            Console.WriteLine("Обновленный список животных:");
            shelter.DisplayAllAnimals();

            Console.WriteLine("\n=== КОНЕЦ ЗАДАЧИ 5 ===\n");
        }
    }
}

