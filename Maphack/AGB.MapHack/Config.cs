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

using D2Data;

namespace AGB.MapHack
{
    public class PathHighlight
    {
        public string Name;
        public AreaLevel AreaLevel;
        public AGB.D2.PresetUnit[] Exits;
        public uint Color;
        public bool UseTeleport;

        public PathHighlight()
        {

        }

        public PathHighlight(string name, AreaLevel areaLevel, AGB.D2.PresetUnit[] exits, uint color, bool useTeleport)
        {
            Name = name;
            AreaLevel = areaLevel;
            Exits = exits;
            Color = color;
            UseTeleport = useTeleport;
        }
    }

    public class Config
    {
        public string AgbUsername;
        public string AgbPassword;

        public int WindowSizeX;
        public int WindowSizeY;

        public int RefreshPause;

        public List<PathHighlight> PathHighlights;

        public System.Windows.Forms.Keys Key;

        public Config()
        {

        }

        public Config(string fileName)
        {
            //string configString = AGB.Util.FileRead(fileName);

            if (System.IO.File.Exists(fileName))
            {
                Config config = AGB.Util.XmlDeserialize<Config>(fileName);

                if (config != null)
                {
                    AgbUsername = config.AgbUsername;
                    AgbPassword = config.AgbPassword;
                    PathHighlights = config.PathHighlights;

                    WindowSizeX = config.WindowSizeX;
                    WindowSizeY = config.WindowSizeY;

                    RefreshPause = config.RefreshPause;

                    Key = config.Key;
                }
            }
        }

        public void Save(string fileName)
        {
            //AGB.Util.FileWrite(fileName, JavaScriptConvert.SerializeObject(this));
            AGB.Util.XmlSerialize<Config>(this, fileName);
        }
    }
}
