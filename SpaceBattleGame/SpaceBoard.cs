﻿using System;
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
        public int MaxShields;
        public int CurShields;

        public Ship(int maxshields)
        {
            MaxShields = maxshields;
            CurShields = maxshields;
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

            _enterprise = new Ship(5);
            _birdOfPrey = new Ship(5);
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

            var actualEnterpriseLocation = ActualLocation(new Point(_enterprise.X, _enterprise.Y));

            g.DrawImage(Resource1.enterprise,
                        actualEnterpriseLocation.X + (int)(0.2 * squareWidth),
                        actualEnterpriseLocation.Y + (int)(0.2 * squareHeight),
                        squareWidth - (int)(0.4* squareWidth),
                        squareHeight - (int)(0.4 * squareHeight));
            g.DrawImage(Resource1.birdofprey, _birdOfPrey.X * squareWidth + 15, _birdOfPrey.Y * squareHeight + 12, squareWidth - 10, squareHeight - 5);

            g.DrawEllipse(Pens.Blue, new Rectangle(actualEnterpriseLocation.X + (int)(squareWidth * 0.1), actualEnterpriseLocation.Y + (int)(squareHeight * 0.1), squareWidth - (int)(squareWidth * 0.2), squareHeight - (int)(squareHeight * 0.2)));

            e.Graphics.DrawImage(bitmap, new Point(0, 0));
            bitmap.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        Point ActualLocation(Point shipLocation)
        {
            var squareWidth = (this.Width - 20) / 10;
            var squareHeight = (this.Height - 20) / 10;
            return new Point(shipLocation.X * squareWidth + 11, shipLocation.Y * squareHeight + 11);
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
            if (ship.X >= this.Width - 20)
                moveRight = 0;
            else
                moveRight = XMoveDistance;

            if (ship.Y >= this.Height - 20)
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
