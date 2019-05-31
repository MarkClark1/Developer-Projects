using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryFlooring.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order Add(DateTime dateTime, Order order);
        List<Order> GetOrders(DateTime orderDate);
        void Delete(Order order);
        void EditOrder(Order order);
    }
}
