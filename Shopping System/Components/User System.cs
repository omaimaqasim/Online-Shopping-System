using System;
using System.Collections.Generic;

namespace Project.Components
{
    internal class User_System
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public User_System(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }
    }

    internal class Customer : User_System
    {
        public string Password { get; set; }

        public Customer(string name, string email, string phone, string password)
            : base(name, email, phone)
        {
            Password = password;
        }
    }

    internal class Admin : User_System
    {
        public string Password { get; set; }

        public Admin() : base("Admin", "admin@gmail.com", "0000000000")
        {
            Password = "admin123";
        }
    }

    internal static class UserManager
    {
        private static List<Customer> _customers = new List<Customer>();
        private static Admin _admin = new Admin();

        public static Customer CurrentCustomer { get; private set; }

        public static bool CustomerLogin()
        {
            Console.WriteLine("\n=== CUSTOMER LOGIN ===");
            Console.Write("Email: ");
            string email = Console.ReadLine().Trim();

            Customer found = null;

            foreach (Customer c in _customers)
            {
                if (c.Email.ToLower() == email.ToLower())
                {
                    found = c;
                    break;
                }
            }

            if (found == null)
            {
                Console.WriteLine("No account found.");
                Console.Write("Sign up? (y/n): ");

                if (Console.ReadLine().Trim().ToLower() != "y")
                    return false;

                found = SignUp();
            }

            Console.Write("Password: ");
            string pass = Console.ReadLine();

            if (found.Password != pass)
            {
                Console.WriteLine("Incorrect password!");
                Console.ReadKey();
                return false;
            }

            CurrentCustomer = found;
            Console.WriteLine("Welcome, " + found.Name + "!");
            Console.ReadKey();
            return true;
        }

        public static bool AdminLogin()
        {
            Console.WriteLine("\n=== ADMIN LOGIN ===");
            Console.Write("Email: ");
            string email = Console.ReadLine().Trim();

            Console.Write("Password: ");
            string pass = Console.ReadLine();

            if (email.ToLower() != _admin.Email.ToLower() || pass != _admin.Password)
            {
                Console.WriteLine("Invalid credentials!");
                Console.ReadKey();
                return false;
            }

            Console.WriteLine("Welcome, Admin!");
            Console.ReadKey();
            return true;
        }

        private static Customer SignUp()
        {
            Console.WriteLine("\n=== SIGN UP ===");

            Console.Write("Name: ");
            string name = Console.ReadLine().Trim();

            Console.Write("Email: ");
            string email = Console.ReadLine().Trim();

            Console.Write("Phone: ");
            string phone = Console.ReadLine().Trim();

            Console.Write("Password: ");
            string pass = Console.ReadLine();

            Customer customer = new Customer(name, email, phone, pass);
            _customers.Add(customer);

            Console.WriteLine("Account created!");
            return customer;
        }

        public static void LogoutCustomer()
        {
            CurrentCustomer = null;
        }
    }
}