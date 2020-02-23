using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace datorium_snake_game_cs
{
    class SnakePixel: PictureBox
    {
        public SnakePixel()
        {
            InitializeSnakePixel();
        }

        private void InitializeSnakePixel()
        {
            this.BackColor = Color.Blue;
            this.Width = 20;
            this.Height = 20;
        }
    }
}
