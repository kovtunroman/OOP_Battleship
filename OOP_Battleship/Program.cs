using System;
using OOP_Battleship.Games;


namespace OOP_Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 45);
            Console.Write("Please Enter Player1: ");
            string player1Name = Console.ReadLine();
            Console.WriteLine(Environment.NewLine);
            Console.Write("Please Enter Player2: ");
            string player2Name = Console.ReadLine();
            Game game = new Game(player1Name, player2Name);
            game.Play();
            Console.ReadLine();
        }
    }
}
