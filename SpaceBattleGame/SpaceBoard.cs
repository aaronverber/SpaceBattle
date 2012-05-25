using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SpaceBattleGame
{
    [System.ComponentModel.DesignerCategory("")]
    class SpaceBoard : Control
    {
        int _enterpriseX;
        int _enterpriseY;
        int _enterpriseMoveLeft;
        int _enterpriseMoveRight;
        int _enterpriseMoveUp;
        int _enterpriseMoveDown;

        int _birdOfPreyX;
        int _birdOfPreyY;
        int _birdOfPreyMoveLeft;
        int _birdOfPreyMoveRight;
        int _birdOfPreyMoveUp;
        int _birdOfPreyMoveDown;

        const int XMoveDistance = 26;
        const int YMoveDistance = 24;

        bool _enterpriseTurn = true;

        public SpaceBoard()
        {
            PreviewKeyDown += HandleKeyDown;
            BackColor = Color.White;

            Resize += HandleResize;
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

            g.DrawImage(Resource1.enterprise, _enterpriseX + 15, _enterpriseY + 12, 20, 20);
            g.DrawImage(Resource1.birdofprey, _birdOfPreyX + 15 , _birdOfPreyY + 12, 20, 20); 

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
            if (_enterpriseTurn)
            {
                _enterpriseTurn = false;
                if (_enterpriseX >= 220)
                    _enterpriseMoveRight = 0;
                else
                    _enterpriseMoveRight = XMoveDistance;

                if (_enterpriseY >= 200)
                    _enterpriseMoveDown = 0;
                else
                    _enterpriseMoveDown = YMoveDistance;

                if (_enterpriseY == 0)
                    _enterpriseMoveUp = 0;
                else
                    _enterpriseMoveUp = YMoveDistance;

                if (_enterpriseX == 0)
                    _enterpriseMoveLeft = 0;
                else
                    _enterpriseMoveLeft = XMoveDistance;

                switch (args.KeyCode)
                {
                    case Keys.Left:
                        _enterpriseX -= _enterpriseMoveLeft;
                        break;
                    case Keys.Up:
                        _enterpriseY -= _enterpriseMoveUp;
                        break;
                    case Keys.Right:
                        _enterpriseX += _enterpriseMoveRight;
                        break;
                    case Keys.Down:
                        _enterpriseY += _enterpriseMoveDown;
                        break;
                }
            }
            else
            {
                _enterpriseTurn = true;
                if (_birdOfPreyX >= 220)
                    _birdOfPreyMoveRight = 0;
                else
                    _birdOfPreyMoveRight = XMoveDistance;

                if (_birdOfPreyY >= 200)
                    _birdOfPreyMoveDown = 0;
                else
                    _birdOfPreyMoveDown = YMoveDistance;

                if (_birdOfPreyY == 0)
                    _birdOfPreyMoveUp = 0;
                else
                    _birdOfPreyMoveUp = YMoveDistance;

                if (_birdOfPreyX == 0)
                    _birdOfPreyMoveLeft = 0;
                else
                    _birdOfPreyMoveLeft = XMoveDistance;

                switch (args.KeyCode)
                {
                    case Keys.Left:
                        _birdOfPreyX -= _birdOfPreyMoveLeft;
                        break;
                    case Keys.Up:
                        _birdOfPreyY -= _birdOfPreyMoveUp;
                        break;
                    case Keys.Right:
                        _birdOfPreyX += _birdOfPreyMoveRight;
                        break;
                    case Keys.Down:
                        _birdOfPreyY += _birdOfPreyMoveDown;
                        break;
                }
            }
            Invalidate();
        }
    }
}
