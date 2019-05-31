using MasteryFlooring.Data;
using MasteryFlooring.Models;
using MasteryFlooring.Models.Interfaces;
using MasteryFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasteryFlooring.BLL
{
    public class FlooringManager
    {
        private IServices _serv;

        public FlooringManager(IServices serv)
        {
            _serv = serv;
        }

        public List<Order> GetOrdersByDate(DateTime dateTime)
        {
            List<Order> orders =_serv.GetOrdersByDate(dateTime);
            return orders;
        }

        public Order CalculateOrder(Order order)
        {
            return _serv.CalculateOrder(order);
        }

        public AddOrderResponse AddOrder(Order order)
        {
            AddOrderResponse response = _serv.Add(order);
            return response;
        }

        public Order GetOrderByOrderNumber(DateTime dateTime, int OrderNumber)
        {
            return _serv.GetOrderByOrderNumber(dateTime, OrderNumber);
        }

        public DeleteOrderResponse DeleteOrder(Order order)
        {
            DeleteOrderResponse response = _serv.Delete(order);
            return response;
        }
    }
}
