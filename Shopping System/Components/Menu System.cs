using System;
using System.Threading;
using Project.Components;

namespace Shopping_System
{
    // Parent Class
    internal abstract class Menu_System
    {
        public abstract void ShowMenu();

        protected abstract bool HandleChoice(int choice);

        public void Display()
        {
            bool logout = false;

            do
            {
                Console.Clear();
                ShowMenu();

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid choice!");
                    Console.ReadKey();
                    continue;
                }

                logout = HandleChoice(choice);

                if (!logout)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }

            } while (!logout);
        }

        // Main Menu
        public static void MainMenu()
        {
            bool exit = false;

            do
            {
                Console.Clear();

                Console.WriteLine("=================================================");
                Console.WriteLine("              ONLINE SHOPPING SYSTEM");
                Console.WriteLine("=================================================");
                Console.WriteLine("1. Customer Login");
                Console.WriteLine("2. Admin Login");
                Console.WriteLine("3. Exit");
                Console.WriteLine("=================================================");
                Console.Write("Enter Choice: ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid choice!");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Menu_System customer = new CustomerMenu_System();
                        customer.Display();
                        break;

                    case 2:
                        Menu_System admin = new AdminMenu_System();
                        admin.Display();
                        break;

                    case 3:
                        exit = true;
                        Console.WriteLine("Exiting system...");
                        break;

                    default:
                        Console.WriteLine("Enter valid choice (1-3)");
                        Console.ReadKey();
                        break;
                }

            } while (!exit);
        }
    }

    // Child Class - Customer
    internal class CustomerMenu_System : Menu_System
    {
        public override void ShowMenu()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("                 CUSTOMER PANEL");
            Console.WriteLine("=================================================");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Add Product To Cart");
            Console.WriteLine("3. View Cart");
            Console.WriteLine("4. Remove Item From Cart");
            Console.WriteLine("5. Update Cart Quantity");
            Console.WriteLine("6. Checkout");
            Console.WriteLine("7. Logout");
            Console.WriteLine("=================================================");
            Console.Write("Enter Choice: ");
        }

        protected override bool HandleChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    ProductManager.DisplayCategoryProducts();
                    break;

                case 2:
                    Cart.ShoppingUI();
                    break;

                case 3:
                    Cart.DisplayCart();
                    break;

                case 4:
                    Console.Write("Enter Product ID to remove: ");
                    string removeId = Console.ReadLine() ?? "";

                    if (!string.IsNullOrWhiteSpace(removeId))
                        Cart.RemoveFromCart(removeId);
                    else
                        Console.WriteLine("Invalid Product ID!");
                    break;

                case 5:
                    Cart.DisplayCart();

                    Console.Write("Enter Product ID: ");
                    string updateId = Console.ReadLine() ?? "";

                    Console.Write("Enter New Quantity: ");

                    int qty;
                    while (!int.TryParse(Console.ReadLine(), out qty))
                    {
                        Console.Write("Enter a valid quantity: ");
                    }

                    Cart.UpdateQuantity(updateId, qty);
                    break;

                case 6:
                    Checkout.CalculateBill();
                    break;

                case 7:
                    Console.WriteLine("Logging out...");
                    Thread.Sleep(1000);
                    return true;

                default:
                    Console.WriteLine("Enter valid choice (1-7)");
                    break;
            }

            return false;
        }
    }

    // Child Class - Admin
    internal class AdminMenu_System : Menu_System
    {
        public override void ShowMenu()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("                   ADMIN PANEL");
            Console.WriteLine("=================================================");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. Remove Product");
            Console.WriteLine("4. Logout");
            Console.WriteLine("=================================================");
            Console.Write("Enter Choice: ");
        }

        protected override bool HandleChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    ProductManager.DisplayCategoryProducts();
                    break;

                case 2:
                    ProductManager.AddProduct();
                    break;

                case 3:
                    Console.Write("Enter Product ID to remove: ");
                    string id = Console.ReadLine() ?? "";

                    if (!string.IsNullOrWhiteSpace(id))
                        ProductManager.RemoveProductById(id);
                    else
                        Console.WriteLine("Invalid Product ID!");
                    break;

                case 4:
                    Console.WriteLine("Logging out...");
                    Thread.Sleep(1000);
                    return true;

                default:
                    Console.WriteLine("Enter valid choice (1-4)");
                    break;
            }

            return false;
        }
    }
}