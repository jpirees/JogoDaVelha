using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha
{
    internal class Game
    {
        public string[,] GameView { get; set; }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player Turn { get; set; }

        public Game()
        {
            GameView = new string[3, 3];
            Turn = null;
        }

        public void NewPlayers()
        {
            Console.Clear();

            Console.Write("Enter Player 1 name: ");
            Player1 = new Player(Console.ReadLine().Trim(' '));

            Console.Clear();

            Console.Write("Enter Player 2 name: ");
            Player2 = new Player(Console.ReadLine().Trim(' '));
        }

        public void Play()
        {
            Console.Clear();

            NewPlayers();
            Restart();

            for (int rounds = 0; rounds < 9; rounds++)
            {
                Board();
                PlayerChoice();
            }
        }

        public void Board()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"X = {Player1.ToString()}");
            Console.WriteLine($"O = {Player2.ToString()}");
            Console.WriteLine();
            Console.WriteLine("\t    0     1     2   ");
            Console.WriteLine("\t       |     |      ");
            Console.WriteLine("\t0   {0}  |  {1}  |  {2}", GameView[0, 0], GameView[0, 1], GameView[0, 2]);
            Console.WriteLine("\t  _____|_____|_____ ");
            Console.WriteLine("\t       |     |      ");
            Console.WriteLine("\t1   {0}  |  {1}  |  {2}", GameView[1, 0], GameView[1, 1], GameView[1, 2]);
            Console.WriteLine("\t  _____|_____|_____ ");
            Console.WriteLine("\t       |     |      ");
            Console.WriteLine("\t2   {0}  |  {1}  |  {2}", GameView[2, 0], GameView[2, 1], GameView[2, 2]);
            Console.WriteLine("\t       |     |      ");
        }

        public void PlayerChoice()
        {
            Console.WriteLine();
            Console.WriteLine($"Which position do you want to play ? ({Turn.ToString()})");


            for (int row = 0; row < GameView.GetLength(0); row++)
            {
                for (int column = 0; column < GameView.GetLength(1); column++)
                    if (GameView[row, column] == "X" || GameView[row, column] == "O")
                        Console.Write("\t[ ][ ]");
                    else
                        Console.Write($"\t[{row}][{column}]");

                Console.WriteLine();
            }

            Console.WriteLine();


            Console.Write("Row: ");
            int choiceRow = int.Parse(Console.ReadLine());

            Console.Write("Column: ");
            int choiceColumn = int.Parse(Console.ReadLine());


            GameView[choiceRow, choiceColumn] = (Turn == Player1) ? "X" : "O";

            Turn = (Turn == Player1) ? Player2 : Player1;
        }

        public void Restart()
        {
            Turn = Player1;

            GameView[0, 0] = " ";
            GameView[0, 1] = " ";
            GameView[0, 2] = " ";

            GameView[1, 0] = " ";
            GameView[1, 1] = " ";
            GameView[1, 2] = " ";

            GameView[2, 0] = " ";
            GameView[2, 1] = " ";
            GameView[2, 2] = " ";
        }

    }
}
