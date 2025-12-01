using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex4
{
    public class BuildingItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int MinimumStock { get; set; }

        public BuildingItem(string name, string category, decimal price, int quantity, int minimumStock)
        {
            Name = name;
            Category = category;
            Price = price;
            Quantity = quantity;
            MinimumStock = minimumStock;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Категория: {Category}");
            Console.WriteLine($"Цена: {Price:C}");
            Console.WriteLine($"Количество: {Quantity}");
            Console.WriteLine($"Минимальный остаток: {MinimumStock}");
        }

        public void DecreaseQuantity(int amount)
        {
            if (amount <= Quantity)
            {
                Quantity -= amount;
                Console.WriteLine($"Количество уменьшено на {amount}. Остаток: {Quantity}");
                CheckStock();
            }
            else
            {
                Console.WriteLine("Недостаточно товара на складе!");
            }
        }

        public void CheckStock()
        {
            if (Quantity < MinimumStock)
            {
                Console.WriteLine($"ВНИМАНИЕ! Товар '{Name}' почти закончился! Остаток: {Quantity}");
            }
        }
    }

    public class BuildingStore
    {
        private List<BuildingItem> items;

        public BuildingStore()
        {
            items = new List<BuildingItem>();
        }

        public void AddItem(BuildingItem item)
        {
            items.Add(item);
            Console.WriteLine("Товар добавлен!");
        }

        public void RemoveItem(string name)
        {
            var item = items.FirstOrDefault(i => i.Name == name);
            if (item != null)
            {
                items.Remove(item);
                Console.WriteLine("Товар удален!");
            }
            else
            {
                Console.WriteLine("Товар не найден!");
            }
        }

        public void DisplayAllItems()
        {
            Console.WriteLine("Список всех товаров:");
            foreach (var item in items)
            {
                item.DisplayInfo();
                Console.WriteLine("---");
            }
        }

        public BuildingItem FindByName(string name)
        {
            return items.FirstOrDefault(i => i.Name == name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАДАЧА 4: СТРОИТЕЛЬНЫЙ МАГАЗИН ===\n");

            BuildingStore store = new BuildingStore();


            store.AddItem(new BuildingItem("Кувалда", "инструмент", 800, 15, 5));
            store.AddItem(new BuildingItem("Краска строительная", "отделка", 450, 3, 10));
            store.AddItem(new BuildingItem("Душевой шланг", "сантехника", 2500, 8, 3));
            store.AddItem(new BuildingItem("Саморезы", "инструмент", 150, 25, 15));

            Console.WriteLine("\n" + new string('=', 50));


            store.DisplayAllItems();

            Console.WriteLine("\n" + new string('=', 50));


            var foundItem = store.FindByName("Краска белая");
            if (foundItem != null)
            {
                Console.WriteLine("Найденный товар:");
                foundItem.DisplayInfo();
            }

            Console.WriteLine("\n" + new string('=', 50));


            Console.WriteLine("Покупка товаров:");
            foundItem?.DecreaseQuantity(2);

            var nails = store.FindByName("Гвозди");
            nails?.DecreaseQuantity(10);

            Console.WriteLine("\n" + new string('=', 50));


            Console.WriteLine("Удаляем смеситель:");
            store.RemoveItem("Смеситель");

            Console.WriteLine("\n" + new string('=', 50));


            Console.WriteLine("Обновленный список товаров:");
            store.DisplayAllItems();

            Console.WriteLine("\n=== КОНЕЦ ЗАДАЧИ 4 ===\n");
        }
    }
}

