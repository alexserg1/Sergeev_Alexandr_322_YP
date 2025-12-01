using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SergeevYP
{
    public class TouristUser
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Destination { get; set; }
        public DateTime TravelDate { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }

        public TouristUser(string fullName, string phoneNumber, string destination, DateTime travelDate, int duration, decimal price)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Destination = destination;
            TravelDate = travelDate;
            Duration = duration;
            Price = price;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ФИО: {FullName}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Направление: {Destination}");
            Console.WriteLine($"Дата путешествия: {TravelDate:dd.MM.yyyy}");
            Console.WriteLine($"Продолжительность: {Duration} дней");
            Console.WriteLine($"Стоимость: {Price:C}");
        }
    }

    public class TravelAgency
    {
        private List<TouristUser> users;

        public TravelAgency()
        {
            users = new List<TouristUser>();
        }

        public void AddUser(TouristUser user)
        {
            users.Add(user);
            Console.WriteLine("Пользователь добавлен!");
        }

        public void DisplayAllUsers()
        {
            Console.WriteLine("Список всех пользователей:");
            foreach (var user in users)
            {
                user.DisplayInfo();
                Console.WriteLine("---");
            }
        }

        public TouristUser FindUserByPhone(string phoneNumber)
        {
            return users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАДАЧА 1: ТУРИСТИЧЕСКОЕ АГЕНТСТВО ===\n");

            TravelAgency agency = new TravelAgency();


            agency.AddUser(new TouristUser("Сергеев Александр Андреевич", "+79262628711", "Китай", new DateTime(2025, 7, 15), 10, 45000));
            agency.AddUser(new TouristUser("Иванушкина Катя", "+79262345678", "Турция", new DateTime(2025, 8, 1), 14, 68000));
            agency.AddUser(new TouristUser("Высоких Дмитрий", "+79373456789", "Япония", new DateTime(2025, 6, 20), 7, 32000));

            Console.WriteLine("\n" + new string('=', 50));


            agency.DisplayAllUsers();

            Console.WriteLine("\n" + new string('=', 50));


            var foundUser = agency.FindUserByPhone("+79262345678");
            if (foundUser != null)
            {
                Console.WriteLine("Найденный пользователь:");
                foundUser.DisplayInfo();
            }

            Console.WriteLine("\n=== КОНЕЦ ЗАДАЧИ 1 ===\n");
        }
    }
}



