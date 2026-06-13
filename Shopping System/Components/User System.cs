using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_System
{
    internal class User_System
    {
        public interface IInputable
        {
            void PromptForDetails();
        }

        public abstract class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }

            public bool IsLoggedIn { get; set; } = false;

            public bool Login(string inputUsername, string inputPassword)
            {
                if (inputUsername == Username && inputPassword == Password)
                {
                    IsLoggedIn = true;
                    Console.WriteLine($"\n[Success] Logged in successfully as {Username}!");
                    return true;
                }
                else
                {
                    IsLoggedIn = false;
                    Console.WriteLine("\n[Error] Invalid username or password.");
                    return false;
                }
            }
            public abstract void DisplayDashboard();
            public abstract void PromptForDetails();
        }

        public class Customer : User, IInputable
        {
            public string ShippingAddress { get; set; }

            public override void PromptForDetails()
            {
                Console.WriteLine("\n--- CUSTOMER SIGN-UP ---");
                Console.Write("Set Username: ");
                Username = Console.ReadLine();

                Console.Write("Set Password: ");
                Password = Console.ReadLine();

                Console.Write("Enter Email: ");
                Email = Console.ReadLine();

                Console.Write("Enter Delivery Address: ");
                ShippingAddress = Console.ReadLine();

                Login(Username, Password);
                DisplayDashboard();
            }

            public override void DisplayDashboard()
            {
                if (!IsLoggedIn)
                {
                    Console.WriteLine("\n[Access Denied] You must log in first.");
                    return;
                }

                Menu_System.MainMenu();

            }
        }

        public class Admin : User, IInputable
        {
            public int AdminLevel { get; set; }

            public override void PromptForDetails()
            {
                Console.WriteLine("\n--- ADMIN PROVISIONING ---");
                Console.Write("Set Admin Username: ");
                Username = Console.ReadLine();

                Console.Write("Set Admin Password: ");
                Password = Console.ReadLine();

                Console.Write("Enter Admin Email: ");
                Email = Console.ReadLine();

                Login(Username, Password);
                DisplayDashboard();
            }

            public override void DisplayDashboard()
            {
                if (!IsLoggedIn)
                {
                    Console.WriteLine("\n[Access Denied] You must log in first.");
                    return;
                }

                Menu_System.MainMenu();

            }
        }
    }
}