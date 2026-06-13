using System;
using System.Collections.Generic;
using Project.Components;
using Shopping_System.Components; 

namespace Shopping_System
{
    internal class Checkout
    {
        public static void CalculateBill()
        {
            Cart.LoadCartFromJson();

            if (Cart.Items.Count <= 0)
            {
                Console.WriteLine("Your cart is empty. Cannot checkout!");
                return;
            }

            double totalBill = 0;
            foreach (CartItem item in Cart.Items)
            {
                totalBill += item.TotalPrice;
            }

            Console.WriteLine($"\nYour total bill is: ${totalBill:F2}");
            Console.WriteLine("Select Payment Option:\n1. Credit Card\n2. PayPal\n3. Bank Transfer");
            Console.Write("Enter option choice (1-3): ");
            string choice = Console.ReadLine();

            string paymentMethod = "Credit Card";
            if (choice == "2") paymentMethod = "PayPal";
            else if (choice == "3") paymentMethod = "Bank Transfer";

            Payment orderPayment = new Payment(totalBill, paymentMethod);
            bool isPaymentCleared = orderPayment.Transaction();

            if (isPaymentCleared)
            {
                Cart.Items.Clear();
                Cart.SaveCartToJson();
                Console.WriteLine("Cart cleared. Order loop finalized successfully.");
            }
        }
    }
}