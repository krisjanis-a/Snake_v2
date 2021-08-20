using System;
using System.Collections.Generic;
using static Snake.AppleModel;
using static Snake.SnakeModel;
using System.Diagnostics;


namespace Snake
{
    public class GameModel
    {
        public static int Score { get; set; } = 0;

        public static bool endGame;

        public static bool wallEnabled = false;

        public static int gameSpeed = 100; // Time for user input in miliseconds

        public static void RunGame()
        {
            int boardHeight = 20;
            int boardWidth = 30;

            endGame = false;

            InitializeSnakeCoordinates(boardHeight, boardWidth);

            direction = initial_direction;

            appleCoordinates = GenerateAppleCoordinates(boardHeight, boardWidth);

            while (endGame == false)
            {
                string[,] board = CreateBoard(boardHeight, boardWidth);

                board[appleCoordinates[1], appleCoordinates[0]] = "apple";

                board[snakeCoordinates[0][1], snakeCoordinates[0][0]] = "head";

                for(int i = 1; i < snakeCoordinates.Count;)
                {
                    board[snakeCoordinates[i][1], snakeCoordinates[i][0]] = "body";
                    i++;
                }

                // Check if apple is eaten: if yes generate new apple, if no retain previous coordinates

                int[] newAppleCoordinates = EatApple(boardHeight, boardWidth);

                appleCoordinates = newAppleCoordinates;

                // Print current board

                RenderBoard(board);

                // Get user input and update snake coordinates

                int[] newDirection = GetUserInput(direction);

                direction = newDirection;

                List<int[]> newSnakeCoordinates = UpdateSnakeCoordinates(snakeCoordinates, boardHeight, boardWidth);

                // If apple is eaten, grow snake body

                if (appleEaten == false)
                {
                    snakeCoordinates = newSnakeCoordinates;
                }

                if (appleEaten == true)
                {
                    List<int[]> grownSnakeCoordinates = GrowSnake(snakeCoordinates, newSnakeCoordinates);
                    snakeCoordinates = grownSnakeCoordinates;
                    appleEaten = false;
                }

                CheckIfBodyHit();

                Console.Clear();
            }

            snakeCoordinates = new List<int[]>(0);
            Score = 0;
        }

        public static string[,] CreateBoard(int height, int width)
        {
            string[,] newBoard = new string[height, width];

            return newBoard;
        }

        public static void RenderBoard(string[,] board)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);

            string headerMidSection = new string('-', boardWidth);

            string header = "+" + headerMidSection + "+";

            Console.WriteLine(header);

            for (int i = 0; i < boardHeight;)
            {
                string currentLine = "|";

                for (int j = 0; j < boardWidth;)
                {
                   if (board[i, j] == null)
                    {
                        currentLine += " ";
                    }

                    if (board[i, j] == "apple")
                    {
                        currentLine += "@";
                    }

                    if (board[i, j] == "head")
                    {
                        currentLine += "X";
                    }

                    if (board[i, j] == "body")
                    {
                        currentLine += "O";
                    }

                    j++;
                }

                currentLine += "|";
                Console.WriteLine(currentLine);

                i++;
            }

            Console.WriteLine(header);

            Console.WriteLine("\n" + "Current score: " + Score);
        }

        public static int[] GetUserInput(int[] currentDirection)
        {
            int[] nextMove = currentDirection; // { x , y }

            ConsoleKeyInfo input = new ConsoleKeyInfo();

            long timeElapsed = 0;
            int waitTime = gameSpeed;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while(timeElapsed <= waitTime)
            {
                if (Console.KeyAvailable)
                {
                    input = Console.ReadKey();
                    break;
                }

                timeElapsed = stopwatch.ElapsedMilliseconds;
            }

            if(input.Key == ConsoleKey.UpArrow)
            {
                nextMove = up;

                if (currentDirection == down)
                {
                    nextMove = down;
                }
            }

            if (input.Key == ConsoleKey.DownArrow)
            {
                nextMove = down;

                if (currentDirection == up)
                {
                    nextMove = up;
                }
            }

            if (input.Key == ConsoleKey.LeftArrow)
            {
                nextMove = left;

                if (currentDirection == right)
                {
                    nextMove = right;
                }
            }

            if (input.Key == ConsoleKey.RightArrow)
            {
                nextMove = right;

                if (currentDirection == left)
                {
                    nextMove = left;
                }
            }

            while (true)
            {
                timeElapsed = stopwatch.ElapsedMilliseconds;

                if (timeElapsed > waitTime)
                {
                    stopwatch.Stop();
                    return nextMove;
                }
            }
        }
    }
}
