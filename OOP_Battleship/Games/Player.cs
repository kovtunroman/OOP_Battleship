using OOP_Battleship.Battlefield;
using OOP_Battleship.Ships;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OOP_Battleship.Games
{
    public class Player
    {
        public string Name { get; set; }
        public Ocean Ocean { get; set; }
        public Ocean ShootingBoard { get; set; }
        public List<Ship> Ships { get; set; }
        
        public bool IsLost
        {
            get
            {
                return Ships.All(ship => ship.IsDestroyed);
            }
        }

        public Player(string name)
        {
            Name = name;
            Ocean = new Ocean();
            ShootingBoard = new Ocean();
            Ships = new List<Ship>()
            {
                new Carrier(),
                new Battlship(),
                new Crusier(),
                new Submarine(),
                new Destroyer()
            };
        }

        public void PlaceShips()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach(var ship in Ships)
            {
                bool isOpen = true;
                while (isOpen)
                {
                    var startColumn = rand.Next(1, 11);
                    var startRow = rand.Next(1, 11);
                    int endRow = startRow;
                    int endColumn = startColumn;
                    var orientation = rand.Next(1, 101) % 2;
                    
                    if(orientation == 0)
                    {
                        for(int i = 1; i < ship.Length; i++)
                        {
                            endRow++;
                        }
                    }
                    else
                    {
                        for(int i = 1; i < ship.Length; i++)
                        {
                            endColumn++;
                        }
                    }

                    if(endRow > 10 || endColumn > 10)
                    {
                        isOpen = true;
                        continue;
                    }

                    var affectedCells = Ocean.CellsInRange(startRow, startColumn, endRow, endColumn);
                    if (affectedCells.Any(cell => cell.IsOccupied))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach(var cell in affectedCells)
                    {
                        cell.OccupationType = ship.OccupationType;
                    }
                    isOpen = false;

                }
            }
        }

        public void PrintBoards()
        {
            Console.WriteLine("Player: " + Name);                
            Console.WriteLine("Own SeaBoard:                                           Firing Board:\n");
            Console.WriteLine("    1   2   3   4   5   6   7   8   9   10                      1   2   3   4   5   6   7   8   9   10\n");
            for (int row = 1; row <= 10; row++)
            {
                if(row < 10)
                {
                    Console.Write(row + "   ");
                } 
                else
                {
                    Console.Write(row + "  ");
                }
                
                for(int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    Console.Write(Ocean.CellAtCoordinate(row, ownColumn).Status + "   ");
                }
                Console.Write("                ");
                if (row < 10)
                {
                    Console.Write(row + "   ");
                }
                else
                {
                    Console.Write(row + "  ");
                }
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    Console.Write(ShootingBoard.CellAtCoordinate(row, firingColumn).Status + "   ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

        //Shot is the shot result on the PCs Ocean
        public ShotResult Shot(Coordinates coordinates)
        {
            var cell = Ocean.CellAtCoordinate(coordinates.Row, coordinates.Column);
            if (!cell.IsOccupied)
            {
                Console.WriteLine(Name + " says: \"Miss!\"");
                cell.OccupationType = OccupationType.Miss;
                return ShotResult.Miss;
            }
            var ship = Ships.First(x => x.OccupationType == cell.OccupationType);
            ship.Hits++;
            Console.WriteLine(Name + " says: \"Hit!\"");
            if (ship.IsDestroyed)
            {
                Console.WriteLine(Name + " says: \"You Destroy my " + ship.Name + "!\"");
            }
            cell.OccupationType = OccupationType.Hit;
            return ShotResult.Hit;
        }

        //ShotResult is shot result on the PCs ShootingBoard
        public void ShortResult(Coordinates coordinates, ShotResult result)
        {
            var cell = ShootingBoard.CellAtCoordinate(coordinates.Row, coordinates.Column);
            switch (result)
            {
                case ShotResult.Hit:
                    cell.OccupationType = OccupationType.Hit;
                    break;

                case ShotResult.Miss:
                    cell.OccupationType = OccupationType.Miss;
                    break;
            }
        }
    }
}
