using MasteryFlooring.Data.Mappers;
using MasteryFlooring.Models;
using MasteryFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MasteryFlooring.Data
{
    public class OrderRepository : IOrderRepository
    {
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
                string[] rows = OrderMapper.ReadByDate(orderDate);
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
            OrderMapper.SaveToFile(orderDate, orders);
        }

        public Order GetOrder(DateTime orderDate, int OrderNumber)
        {
            List<Order> orders = GetOrders(orderDate);
            return orders.FirstOrDefault(o => o.OrderNumber == OrderNumber);
        }

        public void EditOrder(Order order)
        {
            List<Order> orders = GetOrders(order.OrderDate);
            //This OrderIndex is where the old version of the Order resides
            int OrderIndex = orders.FindIndex(o => o.OrderNumber == order.OrderNumber);
            orders[OrderIndex] = order;
            SetOrders(order.OrderDate, orders);
            //editing order grab all orders for the day and store it in a List
            //find the order you are modifying in the List
            //replace the order you are modifying in the List
            //write the newly modified List to the file
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
