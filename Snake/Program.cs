using System;
using static Snake.GameModel;

namespace Snake
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            Console.SetWindowSize(32, 25);
            Console.SetBufferSize(34, 27);

            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine("Snake" + "\n");

                Console.WriteLine("Press any key to start the game");

                Console.ReadKey(false);

                Console.Clear();

                RunGame();

                Console.WriteLine("Game over");

                while (true)
                {
                    EntryPoint:

                    Console.WriteLine("Would you like to restart? [Y - yes, N - no]");

                    ConsoleKeyInfo result = Console.ReadKey(false);
                    Console.Clear();

                    if ((result.KeyChar == 'Y') || (result.KeyChar == 'y'))
                    {
                        break;
                    }

                    if ((result.KeyChar == 'N') || (result.KeyChar == 'n'))
                    {
                        exit = true;
                        break;
                    }
                    else
                    {
                        goto EntryPoint;
                    }
                }
            }
        }
    }
}
