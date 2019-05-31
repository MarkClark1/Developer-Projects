using PowerBall.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBall
{
    class Program
    {
        static void Main(string[] args)
        {
            ITicketRepository repo = new TicketRepository();
            TicketService tcksrv = new TicketService(repo);
            ViewPowers ball = new ViewPowers(tcksrv);
            ball.Show();
        }
    }
}
