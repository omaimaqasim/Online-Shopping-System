using Project.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_System

{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ALWAYS LOAD THESE TO METHODS BEFORE CALLING ADDTOPRODUCTS OR REMOVEFROMPRODUCTS
            // otherwise all the fake data store in products.json will be overwritten
            ProductManager.LoadProductsFromJson();
            ProductManager.DisplayCategoryProducts();
        }
    }
}
