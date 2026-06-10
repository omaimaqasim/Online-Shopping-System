using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Project.Components
{
    internal class Product
    {
        
        private string productId;
        private string productName;
        private string description;
        private double price;
        private int stockQuantity;
        private string category;
        private double averageRating;
        
        public Product(string productName, double price, int stockQuantity, string category)
        {
            this.productId = Guid.NewGuid().ToString();
            this.productName = productName;
            this.description = "No description provided";
            this.price = price;
            this.stockQuantity = stockQuantity;
            this.category = category;
            this.averageRating = 0.0;
        }

        
        public Product(string productName, string description, double price, int stockQuantity, string category)
        {
            this.productId = Guid.NewGuid().ToString();
            this.productName = productName;
            this.description = description;
            this.price = price;
            this.stockQuantity = stockQuantity;
            this.category = category;
            this.averageRating = 0.0;
        }


        public Product()
        {
            this.productId = "";
            this.productName = "Unknown Product";
            this.description = "No description provided";
            this.category = "General";
            this.averageRating = 0.0;
        }


        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public string ProductId
        {
            get { return productId; }
            set { productId = value; } 
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0) Console.WriteLine("Price can't be less than 0");
                else price = value;
            }
        }

        public int StockQuantity
        {
            get { return stockQuantity; }
            set
            {
                if (value < 0) Console.WriteLine("Stock Quantity can't be less than 0");
                else stockQuantity = value;
            }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public double AverageRating
        {
            get { return averageRating; }
        }

        public void DisplayProducts(int index)
        {
            string shortId = productId.Substring(0, 5);

            Console.WriteLine(
                $"{index,-8} " +
                $"{shortId,-10} " +
                $"{ProductName,-35} " +
                $"{Category,-25} " +
                $"{Price,-15:F2} " +
                $"{StockQuantity,-12} " +
                $"{AverageRating,-6:0.0}"
            );
        }
    }
}