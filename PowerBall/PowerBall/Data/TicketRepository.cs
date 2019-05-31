using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerBall.Data
{
    public class TicketRepository : ITicketRepository
    {
        public const string TicketFile = @"C:\Users\nthny\Documents\Bitbucket\anthony-dahl-individual-work\PBFile\PBWordDoc.txt";

        public Ticket Add(Ticket ticket)
        {
            ticket.ID = NewTicketId();
            List <Ticket> tic = GetAll();
            List<string> lines = new List<string>();
            tic.Add(ticket);
            foreach (Ticket t in tic)
            {
                lines.Add($"{t.One} {t.Two} { t.Three} { t.Four} { t.Five} {t.PowerBall} {t.Buyer} {t.ID}");
            }
            File.WriteAllLines(TicketFile, lines.ToArray());
            return ticket;
        }

        public int NewTicketId()
        {
            List<Ticket> recentTicket = GetAll().OrderByDescending(t => t.ID).ToList();
            if (recentTicket == null)
            {
                return 0;
            }
            else
            {
                return recentTicket.Count;
            }
        }

        public List<Ticket> ScoreMatch(Ticket drawnticket)
        {
            List<Ticket> scoreKeeper = GetAll();
            foreach(Ticket sTicket in scoreKeeper)
            {
                sTicket.CountMatchingNumbers(drawnticket);
            }
            return scoreKeeper.OrderByDescending(t => t.TicketMatch).ThenByDescending(p => p.PowerBallMatch).ToList();
        }

        public Ticket FindById(int id)
        {
            return GetAll().FirstOrDefault(t => t.ID == id);
        }

        public List<Ticket> GetAll()
        {            
            List<Ticket> ticketList = new List<Ticket>();
            using (StreamReader sr = new StreamReader(TicketFile))
            {
                string line;
                Ticket newTicketReader;

                while ((line = sr.ReadLine()) != null)
                {
                    newTicketReader = new Ticket();                    

                    string[] columns = line.Split(' ');

                    newTicketReader.One = int.Parse(columns[0]);
                    newTicketReader.Two = int.Parse(columns[1]);
                    newTicketReader.Three = int.Parse(columns[2]);
                    newTicketReader.Four = int.Parse(columns[3]);
                    newTicketReader.Five = int.Parse(columns[4]);
                    newTicketReader.PowerBall = int.Parse(columns[5]);
                    newTicketReader.Buyer = columns[6];
                    newTicketReader.ID = int.Parse(columns[7]);
                    ticketList.Add(newTicketReader);
                }
            }
            return ticketList;
        }
    }
}
