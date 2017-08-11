using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        //Create a new Snake Object. The Snake is a set of circles so declare a Circle. Do the same for the Snake Food.
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        public Form1()
        {
            InitializeComponent();


            //Setting up the Speed and Timer.   WHY DOES THE SETTINGS.SPEED SHOW A VALUE OF 0. 
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //Start of a new game
            StartGame();
        }

        private void StartGame()
        {
            //Making sure the winning label is disabled while in play
            lblGameOver.Visible = false;
            
            //Set the default Settings
            new Settings();

            //Clears previous Snake from Game, Sets the GameUp.
            Snake.Clear();
            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);
            
            //Populates the Score and creates the Food
            lblScore.Text = Settings.Score.ToString();
            GenerateFood();
        }

        //Logic behind how the Food is populated Randomly
        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            //New instance of Random. X and Y show up randomly from 0 to their Max positions
            Random random = new Random();
            food = new Circle();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }

        //Checks and Updates the Screen
        private void UpdateScreen(object sender, EventArgs e)
        {
            //Checking if the Game is over
            if(Settings.GameOver == true)
            {
                //Checks if you haev pressed Enter, if you did restart the game
                if(Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
                else
                {
                    //Here I am checking to make sure the direction the Snake wants to go is not the direction its already going. 
                    if (Input.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                        Settings.direction = Direction.Right;
                    else if (Input.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                        Settings.direction = Direction.Left;
                    else if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                        Settings.direction = Direction.Up;
                    else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                        Settings.direction = Direction.Down;

                    MovePlayer();
                }
                //Logic behind refreshing the Canvas
                pbCanvas.Invalidate();
            }
        }

        //Logic for the Picture Box
        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            //If the game is not over
            if (!Settings.GameOver)
            {
                //Add the Snakes color
                Brush snakeColor;

                //Snake Settings
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                        snakeColor = Brushes.Black;
                    else
                        snakeColor = Brushes.Green;


                    //Snake Creation
                    canvas.FillEllipse(snakeColor,
                        new Rectangle(Snake[i].X * Settings.Width, 
                        Snake[i].Y * Settings.Height, 
                        Settings.Width, Settings.Height));

                    //Food Creation
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Width,
                        food.Y * Settings.Height,
                        Settings.Width, Settings.Height));
                }
            }
            else
            {   //If the game is over, populate this message.
                string gameOver = "Game over \nYour final score is: " + Settings.Score + "\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        //Logic for Moving the Player
        private void MovePlayer()
        {
            for(int i=Snake.Count -1; i>= 0; i--)
            {
                //Snake Head Movement Logic. If it goes in a Direction, add 1 circle in the opposite direction AKA Tail
                if(i ==0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }

                    //Gives the Borders of the Game
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Width / Settings.Height;

                    //How to detect when the Snake hits the walls. If the X and Y are on the Same X Y as the border, Die.
                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos) ;
                    {
                        Die();
                    }

                    //How to detect when the Snake hits its body. If the X and Y are on the Same X Y as the body, Die.
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if(Snake[i].X == Snake[j].X &&
                            Snake[i].Y ==Snake[j].Y)
                        {
                            Die();
                        }
                    }

                    //How to detect when the Snake eats the food. If the X and Y are on the Same X Y as the food, eat it!
                    if (Snake[0].X == food.X && Snake[0] .Y == food.Y)
                    {
                        Eat();
                    }

                }
                else
                {
                    //Snake Body Movement Logic
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        //The Eat function which serves as adding the food as the Snakes back Body each time. Also converting food to points in the score too.
        private void Eat()
        {
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;

            Snake.Add(food);

            //Logic Behind updating the score
            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();

            GenerateFood();
        }


        //The Die function which calls GameOver.
        private void Die()
        {
            Settings.GameOver = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }
    }
}
