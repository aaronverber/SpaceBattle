using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SpaceBattleGame
{

    interface ISprite
    {
        void Draw(Graphics g, int width, int height);
    }

    class Ship : ISprite
    {
        public int X;
        public int Y;
        public int MaxShields;
        public int CurShields;
        public int MaxHull;
        public int CurHull;

        Image _image;

        public Ship(Image image, int maxShields, int maxHull)
        {
            _image = image;
            MaxShields = maxShields;
            CurShields = maxShields;
            MaxHull = maxHull;
            CurHull = maxHull;
        }

        public void Draw(Graphics g, int width, int height)
        {
            var actualLocation = ActualLocation(new Point(X, Y), width, height);
            var squareWidth = (width - 20) / 10;
            var squareHeight = (height - 20) / 10;
            g.DrawImage(_image,
                        actualLocation.X + (int)(0.2 * squareWidth),
                        actualLocation.Y + (int)(0.2 * squareHeight),
                        squareWidth - (int)(0.4 * squareWidth),
                        squareHeight - (int)(0.4 * squareHeight));

            var shieldPen = new Pen(Brushes.Blue, 2.0f);
            shieldPen.Color = Color.FromArgb(122, 0, 0, 255);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (CurShields == MaxShields)
            {
                g.DrawEllipse(shieldPen, new Rectangle(actualLocation.X + (int)(squareWidth * 0.1), actualLocation.Y + (int)(squareHeight * 0.1), squareWidth - (int)(squareWidth * 0.2), squareHeight - (int)(squareHeight * 0.2)));
            }
            if (CurShields > MaxShields * 0.5)
            {
                g.DrawEllipse(shieldPen, new Rectangle(actualLocation.X + (int)(squareWidth * 0.2), actualLocation.Y + (int)(squareHeight * 0.2), squareWidth - (int)(squareWidth * 0.4), squareHeight - (int)(squareHeight * 0.4)));
            }
            if (CurShields > 0)
            {
                g.DrawEllipse(shieldPen, new Rectangle(actualLocation.X + (int)(squareWidth * 0.3), actualLocation.Y + (int)(squareHeight * 0.3), squareWidth - (int)(squareWidth * 0.6), squareHeight - (int)(squareHeight * 0.6)));
            }

            g.FillRectangle(Brushes.Red, 
                            actualLocation.X, 
                            actualLocation.Y + (int)(squareHeight * 0.9), 
                            (int)(squareWidth * (this.CurHull / (this.MaxHull * 1.0))), 
                            (int)(squareHeight * 0.1));

            g.DrawString("Shield Strength: " + CurShields, SystemFonts.DefaultFont, Brushes.Blue, actualLocation.X, actualLocation.Y);
            g.DrawString("Hull Strength: " + CurHull, SystemFonts.DefaultFont, Brushes.Red, (float)(actualLocation.X), (float)(actualLocation.Y + 10));
        }


        Point ActualLocation(Point shipLocation, int width, int height)
        {
            var squareWidth = (width - 20) / 10;
            var squareHeight = (height - 20) / 10;
            return new Point(shipLocation.X * squareWidth + 11, shipLocation.Y * squareHeight + 11);
        }

    }

    [System.ComponentModel.DesignerCategory("")]
    class SpaceBoard : Control
    {
        Ship _enterprise;
        Ship _birdOfPrey;

        bool _enterpriseTurn = true;

        public SpaceBoard()
        {
            PreviewKeyDown += HandleKeyDown;
            BackColor = Color.White;

            Resize += HandleResize;

            _enterprise = new Ship(Resource1.enterprise, 5, 10);
            _enterprise.CurShields = 1;
            _enterprise.CurHull = 5;
            _birdOfPrey = new Ship(Resource1.birdofprey, 5, 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var bitmap = new Bitmap(Width, Height);
            var bitmapGraphics = Graphics.FromImage(bitmap);
            var g = bitmapGraphics;
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
            g.DrawLine(Pens.Black, new Point(10, 10), new Point(10, this.Height - 10));
            g.DrawLine(Pens.Black, new Point(10, 10), new Point(this.Width - 10, 10));

            var squareHeight = (this.Height - 20) / 10;
            for (var i = 0; i <= 10; i++)
            {
                g.DrawLine(Pens.Black, new Point(10, squareHeight * i + 10), new Point(this.Width - 10, squareHeight * i + 10));
            }

            var squareWidth = (this.Width - 20) / 10;
            for (var i = 0; i <= 10; i++)
            {
                g.DrawLine(Pens.Black, new Point(squareWidth * i + 10, 10), new Point(squareWidth * i + 10, this.Height - 10));
            }

            _enterprise.Draw(g, this.Width, this.Height);
            _birdOfPrey.Draw(g, this.Width, this.Height);

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
            if (ship.X >= 9)
                moveRight = 0;
            else
                moveRight = XMoveDistance;

            if (ship.Y >= 9)
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

        int XMoveDistance
        {
            get
            {
                return 1;
            }
        }

        int YMoveDistance
        {
            get
            {
                return 1;
            }
        }
    }
}
