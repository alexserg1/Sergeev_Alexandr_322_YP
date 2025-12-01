using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3
{
    public class SportCustomer
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Product { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethod { get; set; }

        public SportCustomer(string fullName, int age, string product, string size, decimal price, string paymentMethod)
        {
            FullName = fullName;
            Age = age;
            Product = product;
            Size = size;
            Price = price;
            PaymentMethod = paymentMethod;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"ФИО: {FullName}");
            Console.WriteLine($"Возраст: {Age}");
            Console.WriteLine($"Товар: {Product}");
            Console.WriteLine($"Размер: {Size}");
            Console.WriteLine($"Цена: {Price:C}");
            Console.WriteLine($"Способ оплаты: {PaymentMethod}");
        }
    }

    public class SportStore
    {
        private List<SportCustomer> customers;

        public SportStore()
        {
            customers = new List<SportCustomer>();
        }

        public void AddCustomer(SportCustomer customer)
        {
            customers.Add(customer);
            Console.WriteLine("Покупатель добавлен!");
        }

        public List<SportCustomer> FindByAge(int age)
        {
            return customers.Where(c => c.Age == age).ToList();
        }

        public void SortByPrice()
        {
            customers = customers.OrderBy(c => c.Price).ToList();
        }

        public void DisplayAllCustomers()
        {
            Console.WriteLine("Список всех покупателей:");
            foreach (var customer in customers)
            {
                customer.DisplayInfo();
                Console.WriteLine("---");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАДАЧА 3: МАГАЗИН СПОРТИВНОЙ ОДЕЖДЫ ===\n");

            SportStore store = new SportStore();


            store.AddCustomer(new SportCustomer("Синабонов Марк", 25, "кроссовки", "42", 5000, "карта"));
            store.AddCustomer(new SportCustomer("Черцов Василий", 19, "футболка", "M", 1500, "наличные"));
            store.AddCustomer(new SportCustomer("Варфаломеев Николай", 30, "спортивные штаны", "L", 3000, "карта"));
            store.AddCustomer(new SportCustomer("Морозова Инна", 19, "кеды", "38", 2500, "наличные"));

            Console.WriteLine("\n" + new string('=', 50));


            Console.WriteLine("Все покупатели:");
            store.DisplayAllCustomers();

            Console.WriteLine("\n" + new string('=', 50));


            var age19Customers = store.FindByAge(19);
            Console.WriteLine("Покупатели в возрасте 19 лет:");
            foreach (var customer in age19Customers)
            {
                customer.DisplayInfo();
                Console.WriteLine("---");
            }

            Console.WriteLine("\n" + new string('=', 50));


            store.SortByPrice();
            Console.WriteLine("Покупатели отсортированные по стоимости:");
            store.DisplayAllCustomers();

            Console.WriteLine("\n=== КОНЕЦ ЗАДАЧИ 3 ===\n");
        }
    }
}

