using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace datorium_snake_game_cs
{
    public partial class Game : Form
    {
        private int verticalVelocity = 0;
        private int horizontalVelocity = 0;
        private int snakeSpeed = 20;

        private Random rand = new Random();
        private GameZone Zone;
        private List<SnakePixel> Snake = new List<SnakePixel>();
        private Timer GameTimer;
        private Food FreshFood;

        public Game()
        {
            InitializeComponent();
            InitializeGameZone();
            InitializeGame();
            InitializeSnake();
            InitializeGameTimer();
            InitializeFood();
        }

        private void AddSnakePixel()
        {
            Snake.Add(new SnakePixel());
            Snake[Snake.Count - 1].Top = Snake[Snake.Count - 2].Top;
            Snake[Snake.Count - 1].Left = Snake[Snake.Count - 2].Left;
            this.Controls.Add(Snake[Snake.Count - 1]);
            Snake[Snake.Count - 1].BringToFront();
        }

        private int RandBetween(int a, int b)
        {
            return rand.Next(a, b + 1);
        }

        private void FoodRegenerate()
        {
            FreshFood.Left = 100 + 20 * RandBetween(0, 19);
            FreshFood.Top = 100 + 20 * RandBetween(0, 19);
        }

        private void SnakeFoodCollision()
        {
            if (Snake[0].Bounds.IntersectsWith(FreshFood.Bounds))
            {
                FoodRegenerate();
                AddSnakePixel();
                if (GameTimer.Interval > 5)
                    GameTimer.Interval -= 5;
            }
        }

        private void SnakeRectangleCollision()
        {
            if (Zone.Bounds.Contains(Snake[0].Bounds) == false)
            {
                GameOver();
            }
        }

        private void SnakeItselfCollision()
        {
            for(int i = Snake.Count - 1; i > 2; i--)
            {
                if (Snake[0].Bounds.IntersectsWith(Snake[i].Bounds))
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            GameTimer.Stop();
            Snake[0].BackColor = Color.Red;
        }

        private void InitializeFood()
        {
            FreshFood = new Food();
            FoodRegenerate();
            this.Controls.Add(FreshFood);
            FreshFood.BringToFront();
        }

        private void InitializeGameTimer()
        {
            GameTimer = new Timer();
            GameTimer.Interval = 400;
            GameTimer.Tick += new EventHandler(GameTimer_Tick);
            GameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            SnakeFoodCollision();
            SnakeRectangleCollision();
            SnakeItselfCollision();
        }

        private void MoveSnake()
        {
            for(int i = Snake.Count - 1; i > 0; i--)
            {
                Snake[i].Left = Snake[i - 1].Left;
                Snake[i].Top = Snake[i - 1].Top;
            }            
            Snake[0].Left += horizontalVelocity;
            Snake[0].Top += verticalVelocity;
        }

        private void InitializeSnake()
        {
            Snake.Add(new SnakePixel());
            Snake[0].Left = 200;
            Snake[0].Top = 200;
            this.Controls.Add(Snake[0]);
            Snake[0].BringToFront();
        }

        private void InitializeGame()
        {
            this.Width = 600;
            this.Height = 600;
            this.KeyDown += new KeyEventHandler(Game_KeyDown);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    verticalVelocity = 0;
                    horizontalVelocity = snakeSpeed;
                    break;
                case Keys.Down:
                    verticalVelocity = snakeSpeed;
                    horizontalVelocity = 0;
                    break;
                case Keys.Left:
                    verticalVelocity = 0;
                    horizontalVelocity = -snakeSpeed;
                    break;
                case Keys.Up:
                    verticalVelocity = -snakeSpeed;
                    horizontalVelocity = 0;
                    break;
            }
        }

        private void InitializeGameZone()
        {
            Zone = new GameZone();
            Zone.Left = 100;
            Zone.Top = 100;
            this.Controls.Add(Zone);
        }
    }
}
