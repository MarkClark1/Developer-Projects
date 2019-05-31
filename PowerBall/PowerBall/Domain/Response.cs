using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBall.Domain
{
    public class Response
    {
        public object ticket { get; set; }
        public double Ticket { get; set; }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
