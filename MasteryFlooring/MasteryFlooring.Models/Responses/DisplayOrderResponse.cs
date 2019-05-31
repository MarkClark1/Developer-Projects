using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryFlooring.Models.Responses
{
    public class DisplayOrderResponse : Response
    {
        public List<Order> Order { get; set; }
    }
}
