using System;

namespace JogoDaVelha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            string action;

            do
            {
                Console.Clear();

                switch (action = Menu())
                {
                    case "0": break;

                    case "1":
                        game.Play();
                        break;

                    default:
                        Console.WriteLine("Action not found");
                        break;
                }

                Console.ReadKey();

            } while (action != "0");
            
        }

        public static string Menu()
        {
            Console.WriteLine("=============== MENU ===============");
            Console.WriteLine("[1]\tPlay");
            Console.WriteLine("====================================");
            Console.WriteLine("[0] Exit");

            Console.Write("\n\n:: ");

            return Console.ReadLine();
        }
    }
}
