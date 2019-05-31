using PowerBall.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerBall
{
    public class ViewPowers
    {
        private object TicketFile;
        private TicketService service;
        private TicketRepository trepo;

        public ViewPowers(TicketService srv)
        {
            service = srv;
            trepo = new TicketRepository();
        }

        private void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("==============================");
            Console.WriteLine("==============================");
            Console.WriteLine("|                            |");
            Console.WriteLine("|         POWER BALL         |");
            Console.WriteLine("|                            |");
            Console.WriteLine("==============================");
            Console.WriteLine("==============================");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }

        public int MenuSelect()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("===========================================================");
            Console.WriteLine("");
            Console.WriteLine(" Please select an option on how you would like to proceed.");
            Console.WriteLine("");
            Console.WriteLine("===========================================================");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("");
            Console.WriteLine("  1. Select your own ticket number.");
            Console.WriteLine("  2. Create a randomly generated ticket number.");
            Console.WriteLine("  3. View previously selected tickets.");
            Console.WriteLine("  4. Draw the Powerball!");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("===========================================================");
            Console.ResetColor();
            Console.WriteLine("");
            return ReadIntInRange("Select 1-4:", 1, 4);
        }

        private string ReadString(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue..");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }            
        }

        private int ReadIntInRange(string prompt, int min, int max)
        {
            int result = 0;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                int.TryParse(input, out result);
                if (result < min || result > max)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Please enter a number between {min} & {max}.");
                    Console.ResetColor();
                }
            } while (result < min || result > max);
            return result;
        }

        public void Show()
        {
            Welcome();

            int selection = 0;
            do
            {
                selection = MenuSelect();
                switch (selection)
                {
                    case 1:
                        ManualPick();
                        break;
                    case 2:
                        RandomPick();
                        break;
                    case 3:
                        ViewTicket();
                        break;
                    case 4:
                        DrawTicket();
                        break;
                }
            } while (selection > 0 && selection < 4);

            Console.WriteLine("Complete");
        }

        public void ManualPick()
        {
            Console.WriteLine("Manual Pick");
            Ticket t = new Ticket();
            t.Buyer = ReadString("Enter your name.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("For the first five numbers please select a number between 1 and 69.");
            Console.ResetColor();
            do
            {
                t.One = ReadIntInRange("Please select the first number:", 1, 69);
                t.Two = ReadIntInRange("Please select the second number:", 1, 69);
                t.Three = ReadIntInRange("Please select the third number:", 1, 69);
                t.Four = ReadIntInRange("Please select the fourth number:", 1, 69);
                t.Five = ReadIntInRange("Please select the fifth number:", 1, 69);
                if (!t.IsUnique())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Each selection needs to be a unique number. Please enter your chosen numbers again.");
                    Console.ResetColor();
                }
            }
            while (!t.IsUnique());
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("For the Power Ball number please select a number between 1 and 26.");
            Console.ResetColor();
            t.PowerBall = ReadIntInRange("Please select the PowerBall number:", 1, 26);
            service.Create(t);
        }

        public void RandomPick()
        {
            Console.WriteLine("Randomly Generated Pick:");
            Ticket t = new Ticket();
            t.MakeRandomTicket();
            Console.WriteLine($"{t.One},{t.Two},{ t.Three},{ t.Four},{ t.Five},{t.PowerBall}");
            t.Buyer = ReadString("Enter your name.");
            service.Create(t);
            //return if there is a system error with a null
        }

        private void ViewTicket()
        {
            Console.WriteLine("Previously selected tickets:");
            Console.WriteLine("Please type the ID number you would like to view:");
            List<Ticket> display = trepo.GetAll();
            string readId = Console.ReadLine();
            int id;
            int.TryParse(readId, out id);
            Ticket t = display.Where(x => x.ID == id).FirstOrDefault();
            Console.WriteLine($"{t.One} { t.Two} { t.Three} { t.Four} { t.Five} { t.PowerBall} { t.Buyer} { t.ID}");
            Console.WriteLine();
        }

        private void PrintListOfTickets(List<Ticket> ticketList)
        {
            string powerballMatches;
            foreach (Ticket t in ticketList)
            {
                powerballMatches = t.PowerBallMatch ? "True" : "False";
                Console.WriteLine($"{t.One} { t.Two} { t.Three} { t.Four} { t.Five} { t.PowerBall} { t.Buyer} { t.ID}");
                Console.WriteLine($"Matching Numbers: {t.TicketMatch} || PowerBall Match: {powerballMatches}");
                Console.WriteLine("");
            }
            Console.WriteLine();
        }

        private void DrawTicket()
        {
            Console.WriteLine("Winning Power Ball ticket number is:");
            Ticket t = new Ticket();
            Console.ForegroundColor = ConsoleColor.Green;
            t.MakeRandomTicket();
            List<Ticket> closestTickets = trepo.ScoreMatch(t);
            Console.WriteLine($"{t.One},{t.Two},{ t.Three},{ t.Four},{ t.Five},{t.PowerBall}");
            Console.ResetColor();
            PrintListOfTickets(closestTickets);
            Console.Read();
        }
    }
}
