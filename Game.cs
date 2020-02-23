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
        GameZone Zone;

        public Game()
        {
            InitializeComponent();
            InitializeGameZone();
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
