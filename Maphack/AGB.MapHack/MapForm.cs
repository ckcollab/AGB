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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AGB.MapHack.Drawing;

namespace AGB.MapHack
{
    public partial class MapForm : Form
    {
        //List<Monster> Monsters = new List<Monster>();

        public MapForm()
        {
            InitializeComponent();
            /*
            entity.Location = new Point(15, 15);
            entity.Size = new Size(30, 30);
            panel.Entities.Add(entity);
            Path p = new Path(new Point[] { new Point(50, 50), new Point(90, 30), new Point(70, 90) });
            panel.Entities.Add(p);
            panel.Refresh();
            */
            /*
            int mapLayer = panel.AddLayer();
            int unitLayer = panel.AddLayer();

            for (int i = 0; i < 50; i++)
                Monsters.Add(new Monster(Color.Orange, 0, i * 10, 7, 5));

            foreach(Monster monster in Monsters)
                panel.AddDrawing(mapLayer, monster);

            System.Threading.Thread newThread = new System.Threading.Thread(new System.Threading.ThreadStart(moveLoop));
            newThread.Start();
             */


        }

        public void SetSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Panel.Width = width;
            Panel.Height = height;
        }

        /*
        private void moveLoop()
        {
            int offset = 1;

            for (int i = 0; ;i++)
            {
                if (i > 500)
                {
                    i = 0;
                    offset *= -1;
                }

                foreach (Monster monster in Monsters)
                {
                    monster.X += offset;
                    monster.Y += offset;
                }

                //panel.Entities[0].Location = new Point(entity.X + 2, entity.Y + 2);
                System.Threading.Thread.Sleep(25);
            }
        }*/
    }
}
