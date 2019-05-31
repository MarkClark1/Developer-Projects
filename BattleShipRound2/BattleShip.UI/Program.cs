using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleIO.Welcome();
            GameFlow game = new GameFlow();
            game.Run();
        }
    }
}
