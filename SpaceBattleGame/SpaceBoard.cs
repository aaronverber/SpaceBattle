using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SpaceBattleGame
{

    class Ship
    {
        public int X;
        public int Y;
    }

    [System.ComponentModel.DesignerCategory("")]
    class SpaceBoard : Control
    {
        Ship _enterprise;
        Ship _birdOfPrey;

        const int XMoveDistance = 26;
        const int YMoveDistance = 24;

        bool _enterpriseTurn = true;

        public SpaceBoard()
        {
            PreviewKeyDown += HandleKeyDown;
            BackColor = Color.White;

            Resize += HandleResize;

            _enterprise = new Ship();
            _birdOfPrey = new Ship();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var bitmap = new Bitmap(Width, Height);
            var bitmapGraphics = Graphics.FromImage(bitmap);
            var g = bitmapGraphics;
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
            g.DrawLine(Pens.Black, new Point(10, 10), new Point(10, this.Height - 10));
            g.DrawLine(Pens.Black, new Point(10, 10), new Point(this.Width - 10, 10));

            var margin = (this.Height - 20) / 10;
            for (var i = 0; i <= 10; i++)
            {
                g.DrawLine(Pens.Black, new Point(10, margin * i + 10), new Point(this.Width - 10, margin * i + 10));
            }

            margin = (this.Width - 20) / 10;
            for (var i = 0; i <= 10; i++)
            {
                g.DrawLine(Pens.Black, new Point(margin * i + 10, 10), new Point(margin * i + 10, this.Height - 10));
            }

            g.DrawImage(Resource1.enterprise, _enterprise.X + 15, _enterprise.Y + 12, 20, 20);
            g.DrawImage(Resource1.birdofprey, _birdOfPrey.X + 15, _birdOfPrey.Y + 12, 20, 20);

            e.Graphics.DrawImage(bitmap, new Point(0, 0));

            bitmap.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }



        void HandleResize(object sender, EventArgs args)
        {
            Invalidate();
        }

        void HandleKeyDown(object sender, PreviewKeyDownEventArgs args)
        {
            int moveRight, moveLeft, moveDown, moveUp;
            Ship ship = null;
            if (_enterpriseTurn)
            {
                ship = _enterprise;
            }
            else
            {
                ship = _birdOfPrey;
            }
            if (ship.X >= 220)
                moveRight = 0;
            else
                moveRight = XMoveDistance;

            if (ship.Y >= 200)
                moveDown = 0;
            else
                moveDown = YMoveDistance;

            if (ship.Y == 0)
                moveUp = 0;
            else
                moveUp = YMoveDistance;

            if (ship.X == 0)
                moveLeft = 0;
            else
                moveLeft = XMoveDistance;

            switch (args.KeyCode)
            {
                case Keys.Left:
                    ship.X -= moveLeft;
                    break;
                case Keys.Up:
                    ship.Y -= moveUp;
                    break;
                case Keys.Right:
                    ship.X += moveRight;
                    break;
                case Keys.Down:
                    ship.Y += moveDown;
                    break;
            }
            _enterpriseTurn = !_enterpriseTurn;
            Invalidate();
        }
    }
}
