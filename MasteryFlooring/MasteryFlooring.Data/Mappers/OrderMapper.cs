using MasteryFlooring.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MasteryFlooring.Data.Mappers
{
    public class OrderMapper
    {
        private static string GetPathByDate(DateTime dateTime)
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Project":
                    return ($".\\Orders_{dateTime.ToString("MMddyyyy")}.txt");
                case "Test":
                    return ($"C:\\Users\\nthny\\Documents\\Bitbucket\\anthony-dahl-individual-work\\MasteryFlooring\\MasteryFlooring.Data\\TestOrders\\Orders_{dateTime.ToString("MMddyyyy")}.txt");
                default:
                    throw new System.Exception("Mode value in app is not set to a valid mode.");
            }
        }

        public static string[] ReadByDate(DateTime orderDate)
        {
            return File.ReadAllLines(GetPathByDate(orderDate));
        }

        public static Order StringToOrder(string row, DateTime orderDate)
        {
            string[] fields = row.Split(new string[] { "::" }, StringSplitOptions.None);

            if (fields[0] == "OrderNumber")
                return null;
            Order result = new Order()
            {
                OrderDate = orderDate,
                OrderNumber = int.Parse(fields[0]),
                CustomerName = fields[1],
                State = fields[2],
                TaxRate = decimal.Parse(fields[3]),
                ProductType = fields[4],
                Area = decimal.Parse(fields[5]),
                CostPerSquareFoot = decimal.Parse(fields[6]),
                LaborCostPerSquareFoot = decimal.Parse(fields[7]),
                MaterialCost = decimal.Parse(fields[8]),
                LaborCost = decimal.Parse(fields[9]),
                Tax = decimal.Parse(fields[10]),
                Total = decimal.Parse(fields[11])
            };
            return result;
        }

        public static string OrderToString(Order order)
        {
            return $"{order.OrderNumber}::{order.CustomerName}::{order.State}::{order.TaxRate}::{order.ProductType}::{order.Area}::{order.CostPerSquareFoot}::" +
                $"{order.LaborCostPerSquareFoot}::{order.MaterialCost}::{order.LaborCost}::{order.Tax}::{order.Total}";
        }

        internal static void SaveToFile(DateTime orderDate, List<Order> create)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(GetPathByDate(orderDate));
                foreach (Order storage in create)
                {
                    writer.WriteLine(OrderToString(storage));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("There was a loading error.");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
