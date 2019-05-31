using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using System;

namespace BattleShip.UI
{
    public static class ConsoleIO
    {
        public static void Display(Player currentPlayer, Player enemyPlayer)
        {
            Console.Clear();
            const string LETTERS = "@ABCDEFGHIJ";
            Console.WriteLine($"{currentPlayer.name}'s turn:\n");
            Console.WriteLine("    1   2   3   4   5   6   7   8   9  10");
            Console.WriteLine("  |***|***|***|***|***|***|***|***|***|***|");
            for (int column = 1; column < 11; column++)
            {
                Console.Write(String.Format($"{LETTERS[column]} | "));
                for (int row = 1; row <= 10; row++)
                {
                    Coordinate boardCoord = new Coordinate(column, row);
                    ShotHistory shotTaken = enemyPlayer.board.CheckCoordinate(boardCoord);
                    if (shotTaken == ShotHistory.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("H");
                        Console.ResetColor();
                        Console.Write(" | ");

                    }
                    else if (shotTaken == ShotHistory.Miss)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("M");
                        Console.ResetColor();
                        Console.Write(" | ");
                    }
                    else
                    {
                        Console.Write("  | ");
                    }
                }
                Console.Write("\n");
                Console.WriteLine("  |***|***|***|***|***|***|***|***|***|***|");
            }
        }

        public static void Continue()
        {
            Console.WriteLine("Please type any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        public static string SetName()
        {
            Console.WriteLine("Please enter your name: ");
            return Console.ReadLine();
        }

        public static Coordinate GetCoord()
        {
            Console.Write("Input a cordinate:");
            string cordInput;
            char letterCoord;
            string numberCoord;
            int yCoord;
            int xCoord;

            do
            {
                cordInput = Console.ReadLine();
                if (cordInput.Length > 3 || cordInput.Length <= 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input!");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    letterCoord = cordInput.ToUpper().ToCharArray()[0];
                    yCoord = letterCoord - 'A' + 1;
                    if (yCoord > 10 || yCoord < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid input!");
                        Console.ResetColor();
                    }

                    numberCoord = cordInput.Substring(1);
                    if (!int.TryParse(numberCoord, out xCoord))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid input!");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (xCoord > 10 || xCoord < 1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Invalid input!");
                            Console.ResetColor();
                            continue;
                        }
                    }
                }
                break;
            } while (true);
            return new Coordinate(yCoord, xCoord);
        }

        public static bool ShotsFired(Player player, Player enemyPlayer)
        {
            ConsoleIO.Display(player, enemyPlayer);
            while (true)
            {
                FireShotResponse firingplayershotresponse = enemyPlayer.board.FireShot(GetCoord());
                switch (firingplayershotresponse.ShotStatus)
                {
                    case ShotStatus.Invalid:
                        continue;
                    case ShotStatus.Duplicate:
                        continue;
                    case ShotStatus.Miss:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Missed shot!");
                        Console.ResetColor();
                        return false;
                    case ShotStatus.Hit:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Direct hit!");
                        Console.ResetColor();
                        return false;
                    case ShotStatus.HitAndSunk:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("You've sunk an enemie's ship!");
                        Console.ResetColor();
                        return false;
                    case ShotStatus.Victory:
                        return true;
                }
            }
        }

        public static bool PlayAgain()
        {
            Console.WriteLine("Would you like to play another game: y/n");
            char userInput;
            do
            {
                userInput = Console.ReadKey().KeyChar;
                if (userInput == 'y')
                {
                    return true;
                }
                if (userInput == 'n')
                {
                    return false;
                }
                Console.WriteLine("Invalid input.");
            } while (true);
        }

        public static ShipDirection GetDirection()
        {
            ConsoleKeyInfo input;
            char direction;
            do
            {
                Console.WriteLine("Input direction(W,A,S,D)");
                input = Console.ReadKey();
                Console.WriteLine();
                direction = input.KeyChar;
                switch (direction)
                {
                    case 'w':
                    case 'W':
                        return ShipDirection.Up;
                    case 'd':
                    case 'D':
                        return ShipDirection.Right;
                    case 's':
                    case 'S':
                        return ShipDirection.Down;
                    case 'a':
                    case 'A':
                        return ShipDirection.Left;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid input.");
                        Console.ResetColor();
                        continue;
                }
            } while (true);
        }

        public static void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("                                 Welcome to Battleship");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");
            Console.WriteLine("                                            ____________________||_____");
            Console.WriteLine("                                    _______| _______________________   |____");
            Console.WriteLine("                                    \\        _______________________       |");
            Console.WriteLine("                     _               \\_____________________        |     __|");
            Console.WriteLine("                 ===| \\                  |                               |");
            Console.WriteLine("        ____________|__\\_________________|__                           __|____________");
            Console.WriteLine("        \\                                                                           /");
            Console.WriteLine("         \\                                                                         /");
            Console.WriteLine("          \\                                                                       /");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("______________________________________________________________________________________________");
            Console.ResetColor();
            Console.WriteLine("");
            Console.ResetColor();
            ConsoleIO.Continue();
        }
    }
}
