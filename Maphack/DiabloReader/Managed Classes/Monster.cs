using System;
using System.Collections.Generic;

using D2Data;

namespace DiabloReader
{
    public unsafe class Monster
    {
        private Reader Reader;
        public IntPtr Pointer;

        /// <summary>
        /// Always the latest value
        /// </summary>
        public int X
        {
            get
            {
                Unmanaged.Path path = Reader.Read<Unmanaged.Path>(UnitAny.Path);

                return path.X;
            }
        }

        /// <summary>
        /// Always the latest value
        /// </summary>
        public int Y
        {
            get
            {
                Unmanaged.Path path = Reader.Read<Unmanaged.Path>(UnitAny.Path);

                return path.Y;
            }
        }

        private Unmanaged.UnitAny UnitAny
        {
            get
            {
                return Reader.Read<Unmanaged.UnitAny>(Pointer);
            }
        }

        public Monster(Reader reader)
        {
            Reader = reader;
        }

        private Monster(Reader reader, IntPtr unitAny)
        {
            Reader = reader;
            Pointer = unitAny;
        }

        /// <summary>
        /// Gets monsters near the hero
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static List<Monster> GetMonstersInArea(Reader reader)
        {
            Unmanaged.UnitAny unitAny = reader.ReadFromPtr<Unmanaged.UnitAny>(reader.Offset[DllBase.D2Client] + 0x11C1E0);
            Unmanaged.Path path = reader.Read<Unmanaged.Path>(unitAny.Path);
            Unmanaged.Room1 room1 = reader.Read<Unmanaged.Room1>(path.Room1);
            Unmanaged.Room2 room2 = reader.Read<Unmanaged.Room2>(room1.Room2);
            Unmanaged.Level level = reader.Read<Unmanaged.Level>(room2.Level);

            List<Monster> monsters = new List<Monster>();

            if (level.Room2 != null)
            {
                for (Unmanaged.Room2 currentRoom2 = reader.Read<Unmanaged.Room2>(level.Room2); ; currentRoom2 = reader.Read<Unmanaged.Room2>(currentRoom2.Next))
                {
                    if ((IntPtr)currentRoom2.Room1 == IntPtr.Zero)
                        break;

                    Unmanaged.Room1 currentRoom1 = reader.Read<Unmanaged.Room1>(currentRoom2.Room1);

                    IntPtr monsterAddress = currentRoom1.pUnitFirst;

                    for (Unmanaged.UnitAny monster = reader.Read<Unmanaged.UnitAny>(currentRoom1.pUnitFirst); ; monster = reader.Read<Unmanaged.UnitAny>(monster.Next))
                    {
                        if(monsterAddress != IntPtr.Zero)
                            monsters.Add(new Monster(reader, monsterAddress));

                        monsterAddress = monster.Next;

                        if (monsterAddress == IntPtr.Zero)
                            break;
                    }

                    if ((IntPtr)currentRoom2.Next == IntPtr.Zero)
                        break;
                }
            }

            return monsters;
        }
    }
}
