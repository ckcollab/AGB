/*
    This file is part of AGB.MapHack
 
    AGB.MapHack - Reveals the maps in Diablo II, clientlessly
    Copyright (C) 2008 Eric Carmichael

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using System.Threading;

namespace AGB.MapHack.Drawing
{
    internal class FastBitmap
    {
        private Bitmap image;
        private BitmapData bitmapData;
        private int height;
        private int width;
        private byte[] rgbValues;
        bool locked = false;

        public int Height
        {
            get
            {
                return this.height;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
        }

        public FastBitmap(int x, int y)
        {
            width = x;
            height = y;
            image = new Bitmap(x, y);
        }

        public byte[] GetAllPixels()
        {
            return rgbValues;
        }

        public void SetAllPixels(byte[] pixels)
        {
            rgbValues = pixels;
        }

        public Color GetPixel(int x, int y)
        {
            int blue = rgbValues[(y * image.Width + x) * 4];
            int green = rgbValues[(y * image.Width + x) * 4 + 1];
            int red = rgbValues[(y * image.Width + x) * 4 + 2];

            return Color.FromArgb(red, green, blue);
        }

        public void SetPixel(int x, int y, Color cIn)
        {
            int index = (y * image.Width + x) * 4;
            rgbValues[index] = cIn.B;
            rgbValues[index + 1] = cIn.G;
            rgbValues[index + 2] = cIn.R;
        }

        public static implicit operator Image(FastBitmap bmp)
        {
            return bmp.image;
        }

        public static implicit operator Bitmap(FastBitmap bmp)
        {
            return bmp.image;
        }

        public void LockPixels()
        {
            LockPixels(new Rectangle(0, 0, image.Width, image.Height));
        }

        private void LockPixels(Rectangle area)
        {
            if (locked)
                return;

            locked = true;

            bitmapData = image.LockBits(area, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);

            IntPtr ptr = bitmapData.Scan0;
            int stride = bitmapData.Stride;
            int numBytes = image.Width * image.Height * 4;

            rgbValues = new byte[numBytes];
            Marshal.Copy(ptr, rgbValues, 0, numBytes);
        }

        public void UnlockPixels()
        {
            if (!locked)
                return;

            locked = false;
            Marshal.Copy(rgbValues, 0, bitmapData.Scan0, image.Width * image.Height * 4);
            image.UnlockBits(bitmapData);
        }
    }
	public class DrawPanel : Panel
    {
        #region Fields
        private List<List<IDrawing>> Layers;

        public int RefreshPause = 100;

        public Color Background;

        public new float Scale;

        public int OffsetX;
        public int OffsetY;
        #endregion

        #region Constructor
        public DrawPanel()
		{
            Layers = new List<List<IDrawing>>();

            Scale = 1;

			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            Thread myThread = new Thread(new ThreadStart(DrawLoop));
            myThread.Start();
        }
        #endregion

        #region Modifying layers/drawings
        /// <summary>
        /// Adds a new list of drawings to the li
        /// </summary>
        /// <returns>Layer index</returns>
        public int AddLayer()
        {
            Layers.Add(new List<IDrawing>());

            return Layers.Count - 1;
        }

        public void AddDrawing(int layerIndex, IDrawing drawing)
        {
            if (Layers.Count == 0)
                throw new ArgumentException("You must add a layer before adding drawings");

            if (layerIndex > Layers.Count - 1)
                throw new ArgumentException("Layer index out of bounds");

            Layers[layerIndex].Add(drawing);

            base.Refresh();
        }

        public void ClearLayer(int layerIndex)
        {
            if (Layers.Count == 0)
                throw new ArgumentException("You must add a layer before clearing it!");

            if (layerIndex > Layers.Count - 1)
                throw new ArgumentException("Layer index out of bounds");

            Layers[layerIndex].Clear();
        }
        #endregion

        #region Draw loop
        private void DrawLoop()
        {
            for (; ; )
            {
                base.Invalidate();

                System.Threading.Thread.Sleep(RefreshPause);
            }
        }
        #endregion

        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
		{
            //e.Graphics.TranslateTransform(400, 0);
            //e.Graphics.RotateTransform(30);

            if (Background != null)
            {
                e.Graphics.FillRectangle(new SolidBrush(Background), e.ClipRectangle);
            }

            e.Graphics.ScaleTransform(Scale, Scale);

            e.Graphics.TranslateTransform(OffsetX, OffsetY);

            Rectangle viewingArea = new Rectangle(-OffsetX, -OffsetY, Width, Height);

            foreach (List<IDrawing> layer in Layers)
                foreach (IDrawing drawing in layer)
                    drawing.Draw(e.Graphics, viewingArea);

            base.OnPaint(e);
        }
        #endregion
    }

    #region IDrawing interface
    public interface IDrawing
    {
        int X { get; set; }
        int Y { get; set; }

        void Draw(Graphics g, Rectangle bounds);
    }
    #endregion

    #region IDrawings
    #region Unit
    public class Unit : IDrawing
    {
        #region Fields
        private int x;
        private int y;

        public Color Color;

        public uint ID;
        #endregion

        #region Properties
        public int X
        {
            get { return x; }
            set { x = value;}
        }
        public int Y
        {
            get { return y; }
            set { y = value;}
        }
        #endregion

        #region Constructor
        public Unit(Color color, int x, int y)
        {
            Color = color;

            X = x;
            Y = y;
        }
        #endregion

        #region Draw
        public void Draw(Graphics g, Rectangle bounds)
		{
            /*
            Rectangle rect2 = rect;
            rect2.X += rect.Width / 2;
            rect2.Y -= rect.Height;
            rect2.Width = rect.Height;
            rect2.Height = rect.Width;

            if (FillRectangle)
            {
                g.FillRectangle(new SolidBrush(Color), rect);
                g.FillRectangle(new SolidBrush(Color), rect2);
            }
            else
            {
                g.DrawRectangle(new Pen(Color), rect);
                g.DrawRectangle(Pens.Orange, rect2);
            }*/

            List<Point> points = new List<Point>();

            // new center
            int x = X + 2;
            int y = Y + 2;

            int distanceOut = 4;

            points.Add(new Point(x - distanceOut, y));
            points.Add(new Point(x - (distanceOut / 2), y));
            points.Add(new Point(x - (distanceOut / 2), y + (distanceOut / 2)));
            points.Add(new Point(x + (distanceOut / 2), y + (distanceOut / 2)));

            points.Add(new Point(x + (distanceOut / 2), y));
            points.Add(new Point(x + distanceOut, y));
            points.Add(new Point(x + distanceOut, y - (distanceOut / 2)));
            points.Add(new Point(x + (distanceOut / 2), y - (distanceOut / 2)));

            points.Add(new Point(x + (distanceOut / 2), y - distanceOut));
            points.Add(new Point(x - (distanceOut / 2), y - distanceOut));
            points.Add(new Point(x - (distanceOut / 2), y - (distanceOut / 2)));
            points.Add(new Point(x - distanceOut, y - (distanceOut / 2)));

            g.DrawPolygon(new Pen(Color), points.ToArray());
        }
        #endregion
    }
    #endregion

    #region Map
    public class Map : IDrawing
    {
        #region Fields
        private Image Image;

        public byte[,] Collisions;

        public Color Color;

        public int PlayerX;
        public int PlayerY;

        public int Width;
        public int Height;
        #endregion

        #region Properties
        public int X
        {
            get { return 0; }
            set {  }
        }
        public int Y
        {
            get { return 0; }
            set {  }
        }
        #endregion

        #region Constructor
        public Map(Color collisionColor, byte[,] collisions)
        {
            Color = collisionColor;

            Collisions = collisions;

            Width = Collisions.GetUpperBound(0) + 1;
            Height = Collisions.GetUpperBound(1) + 1;

            FastBitmap bitmap = new FastBitmap(Width, Height);

            bitmap.LockPixels();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Collisions[x, y] == 1)
                        bitmap.SetPixel(x, y, Color);
                }
            }

            bitmap.UnlockPixels();

            Image = bitmap;
        }
        #endregion

        #region Draw
        public void Draw(Graphics g, Rectangle bounds)
        {
            g.DrawImage(Image, new Point(0, 0));

            //Pen pen = new Pen(new SolidBrush(Color));
            /*
            Width = Collisions.GetUpperBound(0) + 1;
            Height = Collisions.GetUpperBound(1) + 1;

            if (Width == 0 || Height == 0)
                return;

            int offsetX = PlayerX - (bounds.Width / 2);
            int offsetY = PlayerY - (bounds.Height / 2);

            int offsetWidth = PlayerX + (bounds.Width / 2);
            int offsetHeight = PlayerY + (bounds.Height / 2);

            int width = offsetWidth - offsetX;
            int height = offsetHeight - offsetY;

            FastBitmap bitmap = new FastBitmap(Width, Height);

            bitmap.LockPixels();

            /*
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (!((Collisions[x, y] & 1) > 0))
                        bitmap.SetPixel(x, y, Color);
                }
            }
            //END COMMENT HERE
            
            for (int yd = 0, ys = offsetY; ys < offsetHeight; yd++, ys++)
            {
                if (yd > 300)
                {
                    int alertme = 1;
                    Console.Write("test");
                }

                if (ys < 0 || ys >= Height)
                    continue;
                for (int xd = 0, xs = offsetX; xs < offsetWidth; xd++, xs++)
                {
                    if (xs < 0 || xs >= Width)
                        continue;

                    if (!((Collisions[xs, ys] & 1) > 0))
                        bitmap.SetPixel(xd, yd, Color);
                        //g.DrawRectangle(pen, x, y, 1, 1);
                }
            }
            // END COMMENT HERE
            bitmap.UnlockPixels();

            //Bitmap mapBitmap = Bitmap.FromStream();

            g.DrawImage(bitmap, new Point(offsetX, offsetY));

            /*
            for (int y = offsetY; y < offsetHeight; y++)
            {
                if (y < 0 || y >= Height)
                    continue;
                for (int x = offsetX; x < offsetWidth; x++)
                {
                    if (x < 0 || x >= Width)
                        continue;

                    if (!((Collisions[x, y] & 1) > 0))
                        g.DrawRectangle(pen, x, y, 1, 1);
                }
            }*/
        }
        #endregion
    }
    #endregion

    #region Path
    public class Path : IDrawing
    {
        #region Fields
        private List<AGB.D2.PathNode> PathNodes;

        private Color Color;

        private int MapX;
        private int MapY;
        #endregion

        #region Properties
        public int X
        {
            get { return 0; }
            set { }
        }
        public int Y
        {
            get { return 0; }
            set { }
        }
        #endregion

        #region Constructor
        public Path(List<AGB.D2.PathNode> path, Color color, int mapX, int mapY)
        {
            PathNodes = path;

            Color = color;

            MapX = mapX;
            MapY = mapY;
        }
        #endregion

        #region Draw
        public void Draw(Graphics g, Rectangle bounds)
        {
            AGB.D2.PathNode trailingNode = new AGB.D2.PathNode(-1, -1);

            Brush brush = new SolidBrush(Color);

            foreach (AGB.D2.PathNode node in PathNodes)
            {
                if (trailingNode.X != -1 && trailingNode.Y != -1)
                {
                    g.DrawLine(
                        new Pen(brush),
                        new Point(node.X - MapX + 2, node.Y - MapY + 2),
                        new Point(trailingNode.X - MapX + 2, trailingNode.Y - MapY + 4));
                }

                //graphics.DrawRectangle(new Pen(Brushes.Orange), node.X - mapX - 45, node.Y - mapY - 45, 90, 90);

                g.DrawRectangle(new Pen(brush), node.X - MapX, node.Y - MapY, 4, 4);
                g.FillRectangle(Brushes.White, node.X - MapX + 1, node.Y - MapY + 1, 3, 3);

                trailingNode = node;
            }
        }
        #endregion
    }
    #endregion

    #region Square
    public class Square : IDrawing
    {
        #region Fields
        private Color Color;

        private int MapX;
        private int MapY;

        private int Size;
        #endregion

        #region Properties
        public int X
        {
            get { return 0; }
            set { }
        }
        public int Y
        {
            get { return 0; }
            set { }
        }
        #endregion

        #region Constructor
        public Square(Color color, int mapX, int mapY, int size)
        {
            Color = color;

            MapX = mapX;
            MapY = mapY;

            Size = size;
        }
        #endregion

        #region Draw
        public void Draw(Graphics g, Rectangle bounds)
        {
            Brush brush = new SolidBrush(Color);

            g.FillRectangle(brush, new Rectangle(MapX, MapY, Size, Size));
        }
        #endregion
    }
    #endregion

    #region Line
    public class Line : IDrawing
    {
        #region Fields
        private Color Color;

        private int EndX;
        private int EndY;

        public int StartX;
        public int StartY;
        #endregion

        #region Properties
        public int X
        {
            get { return 0; }
            set { }
        }
        public int Y
        {
            get { return 0; }
            set { }
        }
        #endregion

        #region Constructor
        public Line(Color color, int startX, int startY, int endX, int endY)
        {
            Color = color;

            StartX = startX;
            StartY = startY;


            EndX = endX;
            EndY = endY;
        }
        #endregion

        #region Draw
        public void Draw(Graphics g, Rectangle bounds)
        {
            Pen pen = new Pen(Color);
            pen.Width = 2;

            g.DrawLine(pen, new Point(StartX, StartY), new Point(EndX, EndY));
        }
        #endregion
    }
    #endregion
    #endregion
}
