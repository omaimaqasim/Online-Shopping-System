using System;
using System.Collections.Generic;
using System.IO;        
using System.Text.Json; 

namespace Shopping_System.Components
{
    internal class Payment
    {
        public string PaymentId { get; set; }
        public double Amount { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public string TimeStamp { get; set; }

        private static string filePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Data", "payments.json");//this will give payment.json from the current project directory,

        public Payment() { }

        public Payment(double amount, string method)
        {
            this.PaymentId = "PAY-" + Guid.NewGuid().ToString().Substring(0, 5);
            this.Amount = amount;
            this.Method = method;
            this.Status = "Pending";
            this.TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public bool Transaction()
        {

            if (Amount <= 0)
            {
                this.Status = "Failed"; 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Transaction Failed: Total charge amount must be greater than $0.00.");
                Console.ResetColor();

                string failedJson = JsonSerializer.Serialize(this);
                File.AppendAllText(filePath, failedJson + Environment.NewLine);

                return false;
            }
            else
            {
                this.Status = "Success"; 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[SUCCESS] Captured ${Amount:F2} successfully.");
                Console.ResetColor();

                string successJson = JsonSerializer.Serialize(this);
                File.AppendAllText(filePath, successJson + Environment.NewLine);

                PrintReceipt();
                return true;
            }
        }

        public void PrintReceipt()
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("\t   TRANSACTION RECEIPT");
            Console.WriteLine("=========================================");
            Console.WriteLine($"Transaction ID: {PaymentId}");
            Console.WriteLine($"Date/Time:      {TimeStamp}"); 
            Console.WriteLine($"Method:         {Method}");
            Console.WriteLine($"Status:         {Status}");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"TOTAL BILLED:   ${Amount:F2}");
            Console.WriteLine("=========================================\n");
        }
    }
}