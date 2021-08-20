using System;
using System.Collections.Generic;
using static Snake.GameModel;

namespace Snake
{
    public class SnakeModel
    {
        public static List<int[]> snakeCoordinates = new List<int[]>(0); // Tuple<x,y>

        public static readonly int[] initial_direction = { 0, -1 };

        public static int[] direction = { 0, 0 };

        public static readonly int[] up = { 0, -1 };
        public static readonly int[] down = { 0, 1 };
        public static readonly int[] left = { -1, 0 };
        public static readonly int[] right = { 1, 0 };

        public static void InitializeSnakeCoordinates(int boardHeight, int boardWidth)
        {
            snakeCoordinates.Add(new int[]{ boardWidth / 2, boardHeight / 2});

            for (int i = 1; i < 2;)
            {
                snakeCoordinates.Add(new int[] { snakeCoordinates[0][0], snakeCoordinates[0][1] + i });
                i++;
            }
        }

        public static List<int[]> UpdateSnakeCoordinates(List<int[]> SnakeCoordinates, int boardHeight, int boardWidth)
        {
            List<int[]> newSnakeCoordinates = new List<int[]> { new int[] { SnakeCoordinates[0][0] + direction[0], SnakeCoordinates[0][1] + direction[1] }};

            // If wall enabled

            if (wallEnabled == true)
            {
                if (newSnakeCoordinates[0][0] < 0 | newSnakeCoordinates[0][0] == boardWidth | newSnakeCoordinates[0][1] < 0 | newSnakeCoordinates[0][1] == boardHeight)
                {
                    endGame = true;
                }
            }

            // If wall disabled

            if (wallEnabled == false)
            {
                if (newSnakeCoordinates[0][0] == -1)
                {
                    newSnakeCoordinates[0][0] = boardWidth - 1;
                }

                if (newSnakeCoordinates[0][0] == boardWidth)
                {
                    newSnakeCoordinates[0][0] = 0;
                }

                if (newSnakeCoordinates[0][1] == -1)
                {
                    newSnakeCoordinates[0][1] = boardHeight - 1;
                }

                if (newSnakeCoordinates[0][1] == boardHeight)
                {
                    newSnakeCoordinates[0][1] = 0;
                }
            }

            for (int i = 1; i < SnakeCoordinates.Count;)
            {
                newSnakeCoordinates.Add(SnakeCoordinates[i - 1]);
                i++;
            }

            return newSnakeCoordinates;
        }

        public static List<int[]> GrowSnake (List<int[]> SnakeCoordinates, List<int[]> newSnakeCoordinates)
        {
            List<int[]> grownSnakeCoordinates = new List<int[]> { new int[] { newSnakeCoordinates[0][0], newSnakeCoordinates[0][1] } };

            for (int i = 1; i < newSnakeCoordinates.Count;)
            {
                grownSnakeCoordinates.Add(newSnakeCoordinates[i]);
                i++;
            }

            grownSnakeCoordinates.Add(SnakeCoordinates[SnakeCoordinates.Count-1]);
            
            return grownSnakeCoordinates;
        }

        public static void CheckIfBodyHit()
        {
            int[] snakeHead = snakeCoordinates[0];

            for (int i = 1; i < snakeCoordinates.Count;)
            {
                if (snakeHead[0]==snakeCoordinates[i][0] & snakeHead[1]==snakeCoordinates[i][1])
                {
                    endGame = true;
                }
                i++;
            }
        }
    }
}
