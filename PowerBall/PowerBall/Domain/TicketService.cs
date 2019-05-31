using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PowerBall.Data
{
    public class TicketService
    {
        private ITicketRepository irepo;
        
        public TicketService(ITicketRepository repo)
        {
            irepo = repo;
        }

        public Ticket Create (Ticket card)
        {
            if (IsValid(card))
            {
                irepo.Add(card);
                return card;
            }
            return null;
        }
        private bool IsValid(Ticket card)
        {
            return card.IsUnique();
        }

        //need to notify the user if there is any error within the system, ie not being able to process the number.
    }
}
