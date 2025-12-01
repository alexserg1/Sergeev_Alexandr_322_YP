using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2
{
    public class JewelryCustomer
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string JewelryType { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }

        public JewelryCustomer(string fullName, string phoneNumber, string jewelryType, string material, decimal price, decimal discountPercent)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            JewelryType = jewelryType;
            Material = material;
            Price = price;
            DiscountPercent = discountPercent;
        }

        public decimal CalculateFinalPrice()
        {
            return Price * (1 - DiscountPercent / 100);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"ФИО: {FullName}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Тип украшения: {JewelryType}");
            Console.WriteLine($"Материал: {Material}");
            Console.WriteLine($"Цена: {Price:C}");
            Console.WriteLine($"Скидка: {DiscountPercent}%");
            Console.WriteLine($"Итоговая стоимость: {CalculateFinalPrice():C}");
        }
    }

    public class JewelryStore
    {
        private List<JewelryCustomer> customers;

        public JewelryStore()
        {
            customers = new List<JewelryCustomer>();
        }

        public void AddCustomer(JewelryCustomer customer)
        {
            customers.Add(customer);
            Console.WriteLine("Покупатель добавлен!");
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

        public JewelryCustomer FindByPhone(string phoneNumber)
        {
            return customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        }

        public decimal CalculateTotalProfit()
        {
            return customers.Sum(c => c.CalculateFinalPrice());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАДАЧА 2: МАГАЗИН ЮВЕЛИРНЫХ УКРАШЕНИЙ ===\n");

            JewelryStore store = new JewelryStore();


            store.AddCustomer(new JewelryCustomer("Сергеева Анастасия", "+79161112233", "ожерелье", "золото", 25000, 5));
            store.AddCustomer(new JewelryCustomer("Семен Медведев", "+79262223344", "браслет", "серебро", 12000, 10));
            store.AddCustomer(new JewelryCustomer("Сухарева Татьяна", "+79363334455", "серьги", "белое золото", 45000, 15));

            Console.WriteLine("\n" + new string('=', 50));


            store.DisplayAllCustomers();

            Console.WriteLine("\n" + new string('=', 50));

            var foundCustomer = store.FindByPhone("+79262223344");
            if (foundCustomer != null)
            {
                Console.WriteLine("Найденный покупатель:");
                foundCustomer.DisplayInfo();
            }

            Console.WriteLine("\n" + new string('=', 50));


            Console.WriteLine($"Общая прибыль магазина: {store.CalculateTotalProfit():C}");

            Console.WriteLine("\n=== КОНЕЦ ЗАДАЧИ 2 ===\n");
        }
    }
}

