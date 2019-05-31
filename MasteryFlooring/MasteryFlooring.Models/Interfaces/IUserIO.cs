using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryFlooring.Models.Interfaces
{
    public interface IUserIO
    {
        void DisplayOrder(Order order);
        void DisplayOrders(List<Order> orders);
        void ReadKey();
        string ReadLine();
        void WriteLine(string line);
        void Clear();
    }
}
