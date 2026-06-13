using Project.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace Shopping_System
{
    internal class CartItem
    {
        public Product product { get; set; }
        public int quantity { get; set; }
        public CartItem(Product product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }

        public double TotalPrice => product.Price * quantity;

    }
    internal class Cart
    {
        public static List<CartItem> Items { get; set; } = new List<CartItem>();
        private static string filePath = "C:\\Users\\AAC\\source\\repos\\Online-Shopping-System\\Shopping System\\Data\\cart.json";


        public static void LoadCartFromJson()
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

                List<CartItem> loadedItems = JsonSerializer.Deserialize<List<CartItem>>(jsonString, options);
                if (loadedItems != null)
                {
                    Items = loadedItems;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading cart from JSON");
            }
        }

        public static void SaveCartToJson()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true
                };

                string jsonString = JsonSerializer.Serialize(Items, options);
                File.WriteAllText(filePath, jsonString);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Failed to save products to JSON");
            }
        }

        public static void DisplayCart()
        {
            LoadCartFromJson();

            Console.WriteLine("========================================================================================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\t\t      YOUR SHOPPING CART \n");
            Console.ResetColor();
            Console.WriteLine("========================================================================================================================");

            Console.WriteLine($"{"Index",-8} {"ID",-10} {"Product Name",-35} {"Unit Price($)",-18} {"Quantity",-12} {"Subtotal($)",-12}");
            Console.WriteLine("========================================================================================================================");

            if (Items.Count == 0)
            {
                Console.WriteLine("\t\t\t\t\t   Your cart is currently empty!");
            }
            else
            {
                double grandTotal = 0;

                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];

                    string shortId = item.product.ProductId.Length >= 5
                        ? item.product.ProductId.Substring(0, 5)
                        : item.product.ProductId;

                    Console.WriteLine(
                        $"{i + 1,-8} " +
                        $"{shortId,-10} " +
                        $"{item.product.ProductName,-35} " +
                        $"{item.product.Price,-18:F2} " +
                        $"{item.quantity,-12} " +
                        $"{item.TotalPrice,-12:F2}"
                    );

                    grandTotal += item.TotalPrice;
                }
            }
            Console.WriteLine("========================================================================================================================\n");
        }

        public static void ShoppingUI ()
        {
            ProductManager.DisplayCategoryProducts();
            bool keepShopping = true;
            while (keepShopping)
            {
                Console.WriteLine("\n------------------------------------------------");
                Console.Write("Enter the Item ID you want to buy (or type 'checkout' to finish): ");
                string itemId = Console.ReadLine();
                itemId = itemId.Trim().ToLower();
                if (itemId == "checkout")
                {
                    DisplayCart();
                    Checkout.CalculateBill();
                    keepShopping = false;
                    break;
                }

                Product itemExists = ProductManager.GetProductById(itemId);

                if (itemExists != null)
                {
                    Console.Write($"How much '{itemExists.ProductName}' do you want? ");
                    int numItems;
                    while (!int.TryParse(Console.ReadLine(), out numItems) || numItems <= 0)
                    {
                        Console.WriteLine("Invalid quantity! Please enter a positive whole number.");
                        Console.Write("Enter Quantity: ");
                    }

                    if (itemExists.StockQuantity >= numItems)
                    {
                        Cart.AddToCart(itemExists, numItems);
                        itemExists.StockQuantity -= numItems;
                        ProductManager.SaveProductsToJson();
                        Console.WriteLine($"Successfully added {numItems}x {itemExists.ProductName} to your cart.");
                    } else
                    {
                        Console.WriteLine($"Sorry, we only have {itemExists.StockQuantity} units available.");
                    }
                } else
                {
                    Console.WriteLine($"Error: Product ID '{itemId}' not found in catalog. Try again.");
                }

            }
        }
        
        public static void AddToCart(Product p, int qnty)
        {
            CartItem existingItem = Items.Find(item => item.product.ProductId.Trim().ToLower() == p.ProductId.Trim().ToLower());

            if (existingItem != null)
            {
                existingItem.quantity += qnty;
            } else
            {
                Items.Add(new CartItem(p, qnty));
            }
            SaveCartToJson();
        }

        public static void RemoveFromCart(string id)
        {
            LoadCartFromJson();
            CartItem item = Items.Find(i => i.product.ProductId.Trim().ToLower().StartsWith(id));

            if (item != null)
            {
                Items.Remove(item);
                SaveCartToJson();
                Console.WriteLine($"{item.product.ProductName} removed from cart.");
                DisplayCart();
            } else
            {
                Console.WriteLine($"Error: Item with '{id}' ID not found in cart");
            }

        }

        public static void UpdateQuantity(string id, int newQuantity)
        {
            LoadCartFromJson();
            if (newQuantity <= 0)
            {
                RemoveFromCart(id);
                return;
            }

            CartItem item = Items.Find(i => i.product.ProductId.Trim().ToLower().StartsWith(id));
            if (item != null)
            {
                item.quantity = newQuantity;
                SaveCartToJson();
                Console.WriteLine($"Updated {item.product.ProductName} quantity to {newQuantity}.");
                DisplayCart();
            } else
            {
                Console.WriteLine($"Error: Item with '{id}' ID not found in cart");
            }
        }
    }
}
