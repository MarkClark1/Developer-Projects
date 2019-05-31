using MasteryFlooring.Data.Mappers;
using MasteryFlooring.Models;
using MasteryFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MasteryFlooring.Data.Repositories
{
    public class TestRepository : IOrderRepository
    {
        public void WriteOrders(List<Order> orders)
        {
            StreamWriter writer = null;
            try
            {
                DateTime targetDate = orders[0].OrderDate;
                writer = new StreamWriter($"C:\\Users\\nthny\\Documents\\Bitbucket\\anthony-dahl-individual-work\\MasteryFlooring\\MasteryFlooring.Data\\TestOrders\\Orders_{targetDate.ToString("MMddyyyy")}.txt");
                foreach (Order order in orders)
                {
                    writer.WriteLine(OrderMapper.OrderToString(order));
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Cannot retrieve date if there are no orders.");
            }
            catch (Exception)
            {
                Console.WriteLine("There was an error writing to the file.");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        //public TestRepository()
        //{ 
        //    File.Delete("C:\\Users\\nthny\\Documents\\Bitbucket\\anthony-dahl-individual-work\\MasteryFlooring\\MasteryFlooring.Data\\TestOrders\\Orders_01312020.txt");
        //    File.Create("C:\\Users\\nthny\\Documents\\Bitbucket\\anthony-dahl-individual-work\\MasteryFlooring\\MasteryFlooring.Data\\TestOrders\\Orders_01312020.txt");
        //}           

        public Order Add(DateTime orderDate, Order order)
        {
            List<Order> create = GetOrders(orderDate);
            if (order.OrderNumber == 0)
            {
                order.OrderNumber = GetNextOrderNumber(order.OrderDate);
            }
            create.Add(order);
            OrderMapper.SaveToFile(orderDate, create);
            return order;
        }

        public List<Order> GetOrders(DateTime orderDate)
        {
            try
            {
                List<Order> orders = new List<Order>();
                string[] rows = File.ReadAllLines($"C:\\Users\\nthny\\Documents\\Bitbucket\\anthony-dahl-individual-work\\MasteryFlooring\\MasteryFlooring.Data\\TestOrders\\Orders_{orderDate.ToString("MMddyyyy")}.txt");
                foreach (string row in rows)
                {
                    if (row != null)
                    {
                        orders.Add(OrderMapper.StringToOrder(row, orderDate));
                    }
                }
                return orders;
            }
            catch (Exception)
            {
                return new List<Order>();
            }
        }

        public void SetOrders(DateTime orderDate, List<Order> orders)
        {
            List<string> rows = new List<string>();
            foreach (Order order in orders)
            {
                rows.Add(OrderMapper.OrderToString(order));
            }
            File.WriteAllLines($"C:\\Users\\nthny\\Documents\\Bitbucket\\anthony-dahl-individual-work\\MasteryFlooring\\MasteryFlooring.Data\\TestOrders\\Orders_{orderDate.ToString("MMddyyyy")}.txt", rows);
        }

        public Order GetOrder(DateTime orderDate, int OrderNumber)
        {
            List<Order> orders = GetOrders(orderDate);
            return orders.FirstOrDefault(o => o.OrderNumber == OrderNumber);
        }

        public void EditOrder(Order order)
        {
            List<Order> orders = GetOrders(order.OrderDate);
            int OrderIndex = orders.FindIndex(o => o.OrderNumber == order.OrderNumber);
            orders[OrderIndex] = order;
            SetOrders(order.OrderDate, orders);
        }

        public void Delete(Order order)
        {
            List<Order> orders = GetOrders(order.OrderDate);
            int OrderIndex = orders.FindIndex(o => o.OrderNumber == order.OrderNumber);
            orders.RemoveAt(OrderIndex);
            SetOrders(order.OrderDate, orders);
        }

        private int GetNextOrderNumber(DateTime orderDate)
        {
            List<Order> orders = GetOrders(orderDate);
            if (orders.Count == 0)
            {
                return 1;
            }
            return orders.Max(o => o.OrderNumber) + 1;
        }
    }
}
