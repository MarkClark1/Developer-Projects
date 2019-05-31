using PowerBall.Data;
using System;
using System.Configuration;

namespace PowerBall.Domain
{
    public static class TicketManagerSetUp
    {
        public static TicketService Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "FreeTest":
                    return new TicketService(new TicketRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
