using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using System;

namespace BattleShip.UI
{
    public class Player
    {
        public Board board = new Board();
        public string name { get; set; }
        public Player EnemyPlayer { get; set; }
        public Player()
        {
            name = ConsoleIO.SetName();            
        }

        public Board Playerboard
        {
            get
            {
                return board;
            }
        }

        public void ShipSetUp()
        {
            ResetBoard();
            Console.WriteLine($"{name}, it is time to set up your ships.");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Place your {Enum.GetName(typeof(ShipType), (ShipType)i)}.");

                PlaceShipRequest request = new PlaceShipRequest()
                {
                    Coordinate = ConsoleIO.GetCoord(),
                    Direction = ConsoleIO.GetDirection(),
                    ShipType = (ShipType)i
                };

                ShipPlacement spotValidity = board.PlaceShip(request);
                switch (spotValidity)
                {
                    case ShipPlacement.NotEnoughSpace:
                        i--;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Not enough space to place a ship there!");
                        Console.ResetColor();
                        continue;
                    case ShipPlacement.Overlap:
                        i--;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("This spot overlaps with another ship!");
                        Console.ResetColor();
                        break;
                    case ShipPlacement.Ok:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Your {Enum.GetName(typeof(ShipType), (ShipType)i)} has been placed on the board!");
                        Console.ResetColor();
                        break;
                    default:
                        break;
                }
            }
            ConsoleIO.Continue();
        }

        private void ResetBoard()
        {
            board = new Board();
        }
    }
}

