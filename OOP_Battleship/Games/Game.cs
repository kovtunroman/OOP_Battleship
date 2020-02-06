using System;
using OOP_Battleship.Battlefield;

namespace OOP_Battleship.Games
{
    class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Game(string player1Name, string player2Name)
        {
            Player1 = new Player(player1Name);
            Player2 = new Player(player2Name);

            Player1.PlaceShips();
            Player2.PlaceShips();

            Player1.PrintBoards();
            Player2.PrintBoards();
        }

        public Coordinates GetShotCoordinates()
        {
            return new Coordinates(GetCoordinate("Row"), GetCoordinate("Column"));
        }

        public int GetCoordinate(string coordinate)
        {
            int coord;
            Console.Write($"Enter the Shot {coordinate}: ");
            coord = int.Parse(Console.ReadLine());
            Console.WriteLine(Environment.NewLine);
            if (!(coord >= 1 && coord <= 10))
            {
                Console.WriteLine("Invalid Format!");
                Console.WriteLine($"Enter the Shot {coordinate}: ");
                coord = GetCoordinate(coordinate);
            }
            return coord;
        }
        public void Round()
        {
            bool IsHit = true;

            while (IsHit)
            {
                Console.Clear();
                Player1.PrintBoards();
                Console.WriteLine(Player1.Name + " is yor turn for shooting");

                var coordinates = GetShotCoordinates();
                var result = Player2.Shot(coordinates);
                Player1.ShortResult(coordinates, result);
                Console.ReadKey();
                if (result == ShotResult.Miss)
                {
                    IsHit = false;
                    Console.Clear();
                    Player1.PrintBoards();
                    Console.WriteLine("The result of your shot is MISS");
                }
                else
                {
                    Console.Clear();
                    Player1.PrintBoards();
                    Console.WriteLine("The result of your shot is HIT");
                }
                    
                Console.ReadKey();
            }

            IsHit = true;

            if (!Player2.IsLost)
            {
                while (IsHit)
                {
                    Console.Clear();
                    Player2.PrintBoards();
                    Console.WriteLine(Player2.Name + " is yor turn for shooting");

                    var coordinates = GetShotCoordinates();
                    var result = Player1.Shot(coordinates);
                    Console.ReadKey();
                    Player2.ShortResult(coordinates, result);
                    if (result == ShotResult.Miss)
                    {
                        IsHit = false;
                        Console.Clear();
                        Player2.PrintBoards();
                        Console.WriteLine("The result of your shot is Redrawed: MISS");
                    }
                    else
                    {
                        Console.Clear();
                        Player2.PrintBoards();
                        Console.WriteLine("The result of your shot is Redrawed: HIT");
                    }
                    Console.ReadKey();
                }
            }
        }

        public void Play()
        {
            while(!Player1.IsLost && !Player2.IsLost)
            {
                Round();
            }
            Console.Clear();
            Player1.PrintBoards();
            Player2.PrintBoards();

            if (Player1.IsLost)
            {
                Console.WriteLine(Player2.Name + " has won the game!");
            }
            else if (Player2.IsLost)
            {
                Console.WriteLine(Player1.Name + " has won the game!");
            }
        }
    }
}
