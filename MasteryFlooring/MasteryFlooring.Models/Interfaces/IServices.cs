using MasteryFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryFlooring.Models.Interfaces
{
    public interface IServices
    {
        List<Tax> GetTaxList();
        List<Product> GetProductsList();
        AddOrderResponse Add(Order order);
        List<Order> GetOrdersByDate(DateTime dateTime);
        Order GetOrderByOrderNumber(DateTime dateTime, int OrderNumber);
        Order CalculateOrder(Order order);
        DeleteOrderResponse Delete(Order order);
    }
}
