using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class GameFlow
    {

        Player Player1 = new Player();
        Player Player2 = new Player();
        private bool gameOver;

        public void Run()
        {
            do
            {
                //this resets the gameOver to false so it will set up a new board for each player.
                gameOver = false; 
                Player1.ShipSetUp();
                Player2.ShipSetUp();

                while (!gameOver)
                {
                    ConsoleIO.Display(Player1, Player2);
                    if (ConsoleIO.ShotsFired(Player1, Player2))
                    {
                        gameOver = true;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{Player1.name} has won the game!! Great job!!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;

                    }
                    ConsoleIO.Continue();
                    ConsoleIO.Display(Player1, Player2);
                    Console.WriteLine("Hit any key once you have completed your turn.");
                    Console.ReadKey();

                    ConsoleIO.Display(Player2, Player1);
                    if (ConsoleIO.ShotsFired(Player2, Player1))
                    {
                        gameOver = true;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{Player2.name} has won the game!! Great job!!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;

                    }
                    ConsoleIO.Continue();
                    ConsoleIO.Display(Player2, Player1);
                    Console.WriteLine("Hit any key once you have completed your turn.");
                    Console.ReadKey();
                }
            } while (ConsoleIO.PlayAgain());
        }
    }
}
