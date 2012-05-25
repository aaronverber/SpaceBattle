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
        int _x;
        int _y;

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
            g.FillRectangle(Brushes.White, new Rectangle(0,0,Width, Height));
            g.DrawLine(Pens.Black, new Point(10,10), new Point(10, this.Height - 10));
            g.DrawLine(Pens.Black, new Point(10, 10), new Point(this.Width - 10, 10));

            var margin = this.Height / 10;
            for (var i = 0; i < 10; i++)
            {
                g.DrawLine(Pens.Black, new Point(10, margin * i + 10), new Point(this.Width - 10, margin * i + 10));
            }

            margin = this.Width / 10;
            for (var i = 0; i < 10; i++)
            {
                g.DrawLine(Pens.Black, new Point(margin * i + 10, 10), new Point(margin * i + 10, this.Height - 10));
            }

            g.DrawImage(Resource1.enterprise, _x + 15, _y + 12 ,20,20);

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
            switch (args.KeyCode)
            {
                case Keys.Left:
                    _x -= 28;
                    break;
                case Keys.Up:
                    _y -= 26;
                    break;
                case Keys.Right:
                    _x += 28;
                    break;
                case Keys.Down:
                    _y += 26;
                    break;

            }
            Invalidate();
        }
    }
}
