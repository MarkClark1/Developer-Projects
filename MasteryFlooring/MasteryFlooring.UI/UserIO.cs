using MasteryFlooring.Models;
using MasteryFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace MasteryFlooring.UI
{
    public class UserIO : IUserIO
    {
        public void DisplayOrder(Order order)
        {
            Console.WriteLine($"Order Number: {order.OrderNumber}");
            Console.WriteLine($"Customer Name: {order.CustomerName}");
            Console.WriteLine($"State:{order.State}");
            Console.WriteLine($"Tax Rate: {order.TaxRate}");
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Area: {order.Area}");
            Console.WriteLine($"Cost Per Square Foot: {order.CostPerSquareFoot}");
            Console.WriteLine($"Labor Cost Per Square Foot: {order.LaborCostPerSquareFoot}");
            Console.WriteLine($"Material Cost: {order.MaterialCost:C}");
            Console.WriteLine($"Labor Cost: {order.LaborCost:C}");
            Console.WriteLine($"Tax: {order.Tax:C}");
            Console.WriteLine($"Total: {order.Total:C}");
        }

        public void DisplayOrders(List<Order> orders)
        {
            foreach (Order order in orders)
            {
                DisplayOrder(order);
                Console.WriteLine(new string('=', 60));
            }
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void ReadKey()
        {
            Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
