using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //This is the "Fruit" or "Apple" class that the snake will chase. 
    class Circle
    {
        //So we can use them in other classes
        public int X { get; set; } 
        public int Y { get; set; }

        //Constructor of the "Fruit" or "Apple"
        public Circle ()
        {
            X = 0;
            Y = 0;
        }
    }
}
