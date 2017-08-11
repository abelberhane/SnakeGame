using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //Holds the Directions for the Game
    public enum Direction { Up, Down, Left, Right}

    //This class is where the settings are created. Just creating the variables below and allowing get, set so it can be reached from anywhere.
    public class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static bool GameOver { get; set; }
        public static Direction direction { get; set; }

        //These is where we SET the settings
        public Settings()
        {
            Width = 16;
            Height = 16;
            Speed = 16;
            Score = 0;
            Points = 10;
            GameOver = false;
            direction = Direction.Down;
        }
    }
}
