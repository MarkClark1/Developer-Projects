using System.Collections.Generic;

namespace PowerBall.Data
{

    public interface ITicketRepository
    {
        List<Ticket> GetAll();
        Ticket FindById(int id);
        Ticket Add(Ticket ticket);
    }
}
