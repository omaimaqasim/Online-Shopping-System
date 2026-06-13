using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Security.Cryptography;
namespace Project.Components
{
    internal class ProductManager
    {
        public static List<Product> products = new List<Product>();
        private static string filePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Data", "products.json");//this will give products.json from the current project directory, and it will work regardless of where the project is located on the user's computer

        public static void LoadProductsFromJson()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Warning JSON file not found at: {filePath}. Starting with an empty catalog.");
                    return;
                }

                string jsonString = File.ReadAllText(filePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true
                };

                List<Product> loadedProducts = JsonSerializer.Deserialize<List<Product>>(jsonString, options);

                if (loadedProducts != null)
                {
                    products = loadedProducts;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Failed to load products from JSON: {ex}");
            }
        }

        public static void SaveProductsToJson()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                };

                string jsonString = JsonSerializer.Serialize(products, options);
                File.WriteAllText(filePath, jsonString);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Failed to save products to JSON: {ex}");
            }
        }

        public static void AddProduct(Product p)
        {
            ProductManager.LoadProductsFromJson();
            products.Add(p);
            SaveProductsToJson();
            ProductManager.DisplayCategoryProducts();
        }

        public static void AddProduct()
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("\t   ADD NEW PRODUCT");
            Console.WriteLine("=========================================");

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Description: ");
            string description = Console.ReadLine() ?? "";

            double price = 0;
            bool isPriceValid = false;

            while (isPriceValid == false)
            {
                Console.Write("Enter Price ($): ");
                string priceInput = Console.ReadLine() ?? "";

                if (double.TryParse(priceInput, out price) && price >= 0)
                {
                    isPriceValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid price! Please enter a positive number.");
                }
            }

            int stock = 0;
            bool isStockValid = false;

            while (isStockValid == false)
            {
                Console.Write("Enter Stock Quantity: ");
                string stockInput = Console.ReadLine() ?? "";

                if (int.TryParse(stockInput, out stock) && stock >= 0)
                {
                    isStockValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid quantity! Please enter a whole positive number.");
                }
            }

            Console.Write("Enter Category: ");
            string category = Console.ReadLine() ?? "";

            Product newProduct = new Product(name, description, price, stock, category);
            AddProduct(newProduct);

            try
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("Product added to the catalog successfully!");
                Console.WriteLine("=========================================\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("Product creation canceled due to duplicate entry.", ex);
                Console.WriteLine("=========================================\n");
            }
        }

        public static void RemoveProductById(string id)
        {
            LoadProductsFromJson();
            bool found = false;
            foreach (Product p in products)
            {
                if (p.ProductId.StartsWith(id, StringComparison.OrdinalIgnoreCase))
                {
                    products.Remove(p);
                    SaveProductsToJson();
                    found = true;
                    Console.WriteLine("Product Successfully removed");
                    DisplayCategoryProducts();
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Product with ID '{id}' not found!");
            }
        }

        public static Product GetProductById(string id)
        {
            LoadProductsFromJson();
            foreach (Product p in products)
            {
                if (p.ProductId.StartsWith(id, StringComparison.OrdinalIgnoreCase))
                {
                    return p;
                }
            }

            return null;
        }

        public static void Message(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void DisplayCategoryProducts()
        {
            LoadProductsFromJson();
            Console.WriteLine("========================================================================================================================");
            Message("\t\t\t\t\t\t\tPRODUCTS \n");
            Message("\t\t\t\t\t(Enter the product's index to add to cart.) ");
            Console.WriteLine("========================================================================================================================");

            Console.WriteLine($"{"Index",-8} {"ID",-10} {"Product Name",-35} {"Category",-25} {"Price($)",-15} {"Stock",-12} {"Rating",-6}");
            Console.WriteLine("========================================================================================================================");

            for (int i = 0; i < products.Count; i++)
            {
                products[i].DisplayProducts(i + 1);
            }

            Console.WriteLine("========================================================================================================================\n");
        }
    }
}
