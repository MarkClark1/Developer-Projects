using System;
using System.Linq;

namespace PowerBall
{
    public class Ticket
    {
        private int[] numbers = new int[5];
        public int PowerBall { get; set; }
        public string Buyer { get; set; }
        public int ID { get; set; }
        public int TicketMatch { get; set; }
        public bool PowerBallMatch { get; set; }
        

        public int One
        {
            get { return numbers[0]; }
            set { numbers[0] = value; }
        }
        public int Two
        {
            get { return numbers[1]; }
            set { numbers[1] = value; }
        }
        public int Three
        {
            get { return numbers[2]; }
            set { numbers[2] = value; }

        }
        public int Four
        {
            get { return numbers[3]; }
            set { numbers[3] = value; }
        }
        public int Five
        {
            get { return numbers[4]; }
            set { numbers[4] = value; }
        }
        public bool IsUnique()
        {
            return numbers.Distinct().Count() == 5;
        }
        public void MakeRandomTicket()
        {
            Random r = new Random();
            int nextNum;
            for (int i = 0; i < 5; i++)
            {
                nextNum = r.Next(1, 70);
                while (numbers.Contains(nextNum))
                {
                    nextNum = r.Next(1, 70);
                }
                numbers[i] = nextNum;
            }
            PowerBall = r.Next(1, 27);
        }

        public void CountMatchingNumbers(Ticket t)
        {
            TicketMatch = 0;
            if(t.One == One)
            {
                TicketMatch++;
            }
            if (t.Two == Two)
            {
                TicketMatch++;
            }
            if (t.Three == Three)
            {
                TicketMatch++;
            }
            if (t.Four == Four)
            {
                TicketMatch++;
            }
            if (t.Five == Five)
            {
                TicketMatch++;
            }
            if (t.PowerBall == PowerBall)
            {
                PowerBallMatch = true;
            }
            else
            {
                PowerBallMatch = false;
            }
        }
    }
}
