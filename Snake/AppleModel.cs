using System;
using System.Collections.Generic;
using static Snake.SnakeModel;
using static Snake.GameModel;

namespace Snake
{
    public class AppleModel
    {
        public static int[] appleCoordinates = { 0, 0 };

        public static int points = 1;

        public static bool appleEaten;

        public static int[] EatApple (int boardHeight, int boardWidth)
        {
            int[] newCoordinates = null;

            if (appleCoordinates[0] == snakeCoordinates[0][0] & appleCoordinates[1] == snakeCoordinates[0][1])
            {
                newCoordinates = GenerateAppleCoordinates(boardHeight, boardWidth);
                Score += points;
                appleEaten = true;
            }

            else
            {
                newCoordinates = appleCoordinates;
            }

            return newCoordinates;
        }

        public static int[] GenerateAppleCoordinates(int boardHeight, int boardWidth)
        {
            int[] newCoordinates = {0, 0};
            bool coordinatesValid = false;

            while (coordinatesValid == false)
            {
                Random appleX = new Random();
                int appleCoordX = appleX.Next(0, boardWidth - 1);

                Random appleY = new Random();
                int appleCoordY = appleY.Next(0, boardHeight - 1);

                newCoordinates[0] = appleCoordX;
                newCoordinates[1] = appleCoordY;

                coordinatesValid = ValidateCoordinates(newCoordinates);
            }

            return newCoordinates;
        }

        private static bool ValidateCoordinates(int[] newCoordinates)
        {
            bool coordinatesValid = true;

            for (int i = 0; i < snakeCoordinates.Count;)
            {
                if (newCoordinates[0] == snakeCoordinates[i][0] & newCoordinates[1] == snakeCoordinates[i][1])
                {
                    coordinatesValid = false;
                    break;
                }

                i++;
            }

            return coordinatesValid;
        }
    }
}
