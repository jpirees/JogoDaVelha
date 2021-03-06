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

        public int Rounds { get; set; }
        public int Status { get; set; }

        public Game()
        {
            GameView = new string[3, 3];
            Turn = null;
            Rounds = 0;
            Status = 0;
        }

        

        public void Play()
        {
            Console.Clear();

            NewPlayers();
            NewGame();

            while (Rounds < 9)
            {
                PlayerChoice();

                Rounds++;

                Status = (Rounds >= 5) ? CheckStatus() : 0;

                if (Status == 0)
                    Turn = (Turn == Player1) ? Player2 : Player1;
                else
                    break;
            }

            Console.Clear();

            if (Status == 0)
                Console.WriteLine($"\nNo winner!");
            else
            {
                Board();
                Console.WriteLine();
                Console.WriteLine($"\nPlayer ({Turn}) is the winner!");
            }
        }

        public void NewPlayers()
        {
            Console.Clear();

            Console.Write("Enter Player 1 name: ");
            Player1 = new Player(Console.ReadLine().ToUpper().Trim(' '));

            Console.Clear();

            Console.Write("Enter Player 2 name: ");
            Player2 = new Player(Console.ReadLine().ToUpper().Trim(' '));
        }

        public void Board()
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write($"\tX = {Player1.ToString()}");
            Console.Write($"\tO = {Player2.ToString()}");
            Console.WriteLine();
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

        public void BoardStatus()
        {
            Console.WriteLine($"\nWhich position do you want to play ? ({Turn.ToString()})");

            for (int row = 0; row < GameView.GetLength(0); row++)
            {
                for (int column = 0; column < GameView.GetLength(1); column++)
                    if (GameView[row, column] == "X" || GameView[row, column] == "O")
                        Console.Write("\t[ ][ ]");
                    else
                        Console.Write($"\t[{row}][{column}]");

                Console.WriteLine();
            }

        }

        public void PlayerChoice()
        {
            bool isEmpty;
            int choiceRow, choiceColumn;

            do
            {
                Board();

                BoardStatus();

                Console.WriteLine();

                Console.Write("Row: ");
                choiceRow = int.Parse(Console.ReadLine());

                Console.Write("Column: ");
                choiceColumn = int.Parse(Console.ReadLine());

                isEmpty = CheckPosition(choiceRow, choiceColumn);

                if(isEmpty)
                {
                    Console.WriteLine();
                    Console.WriteLine("Incorrect position");
                    Console.ReadKey();
                }

            } while (isEmpty);

            GameView[choiceRow, choiceColumn] = (Turn == Player1) ? "X" : "O";
        }

        public bool CheckPosition(int row, int column)
        {
            if (row < 0 || row > 2 || column < 0 || column > 2) return true;
            
            if (GameView[row, column] == "X" || GameView[row, column] == "O")
                return true;
            else
                return false;
        }

        public int CheckStatus()
        {
            if (CheckRows() == 1 || CheckColumns() == 1 || CheckDiagonal1() == 1 || CheckDiagonal2() == 1) return 1;
            if (CheckRows() == 2 || CheckColumns() == 2 || CheckDiagonal1() == 2 || CheckDiagonal2() == 2) return 2;

            return 0;
        }

        #region Check Rows|Columns|Diagonals
        public int CheckRows()
        {
            int countX = 0;
            int countO = 0;

            for (int row = 0; row < GameView.GetLength(0); row++)
            {
                for (int column = 0; column < GameView.GetLength(1); column++)
                    if (GameView[row, column] == "X")
                    {
                        countO = 0;
                        countX++;
                    }
                    else if (GameView[row, column] == "O")
                    {
                        countX = 0;
                        countO++;
                    }

                if (countX == 3)
                    return 1;
                else if (countO == 3)
                    return 2;

                countX = 0;
                countO = 0;
            }

            return 0;
        }

        public int CheckColumns()
        {
            int countX = 0;
            int countO = 0;

            for (int column = 0; column < GameView.GetLength(1); column++)
            {
                for (int row = 0; row < GameView.GetLength(0); row++)
                    if (GameView[row, column] == "X")
                    {
                        countO = 0;
                        countX++;
                    }
                    else if (GameView[row, column] == "O")
                    {
                        countX = 0;
                        countO++;
                    }

                if (countX == 3)
                    return 1;
                else if (countO == 3)
                    return 2;

                countX = 0;
                countO = 0;
            }

            return 0;
        }

        public int CheckDiagonal1()
        {
            int countX = 0;
            int countO = 0;

            for (int index = 0; index < GameView.GetLength(0); index++)
                if (GameView[index, index] == "X")
                {
                    countO = 0;
                    countX++;
                }
                else if (GameView[index, index] == "O")
                {
                    countX = 0;
                    countO++;
                }

            if (countX == 3)
                return 1;
            else if (countO == 3)
                return 2;

            return 0;
        }

        public int CheckDiagonal2()
        {
            int countX = 0;
            int countO = 0;

            for (int row = 0; row < GameView.GetLength(0); row++)
                if (GameView[row, GameView.GetLength(1) - 1 - row] == "X")
                {
                    countO = 0;
                    countX++;
                }
                else if (GameView[row, GameView.GetLength(1) - 1 - row] == "O")
                {
                    countX = 0;
                    countO++;
                }

            if (countX == 3)
                return 1;
            else if (countO == 3)
                return 2;

            return 0;
        }
        #endregion
    
        public void NewGame()
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
