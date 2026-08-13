using System;

using D2Data;

namespace DiabloReader
{
    /// <summary>
    /// 
    /// </summary>
    public unsafe class Hero
    {
        private Reader Reader;

        public Hero(Reader reader)
        {
            Reader = reader;
        }

        /// <summary>
        /// Gets the current Y from memory, always the latest value
        /// </summary>
        public int X
        {
            get
            {
                Unmanaged.Path path = Reader.Read<Unmanaged.Path>(PlayerUnit.Path);

                return path.X;
            }
        }

        /// <summary>
        /// Gets the current Y from memory, always the latest value
        /// </summary>
        public int Y
        {
            get
            {

                Unmanaged.Path path = Reader.Read<Unmanaged.Path>(PlayerUnit.Path);

                return path.Y;
            }
        }

        /// <summary>
        /// Gets the current AreaLevel from memory, always the latest value
        /// </summary>
        public AreaLevel AreaLevel
        {
            get
            {
                return (AreaLevel)Reader.Read<uint>(Reader.Offset[DllBase.D2Client] + 0x12340C);
            }
        }

        /// <summary>
        /// Gets the current seed from memory, always the latest value
        /// </summary>
        public uint Seed
        {
            get
            {
                //return Reader.ReadFromPtr<uint>(Reader.Offset[DllBase.D2Client] + 0x11C020);
                return Act.Seed;
            }
        }

        /// <summary>
        /// Gets the current difficulty from memory, always the latest value
        /// </summary>
        public GameDifficulty Difficulty
        {
            get
            {
                return (GameDifficulty)Reader.Read<byte>(Reader.Offset[DllBase.D2Client] + 0x11BFF4);
            }
        }

        /// <summary>
        /// Gets the current realm from memory, always the latest value
        /// </summary>
        public AGB.D2.Realm Realm
        {
            get
            {
                return (AGB.D2.Realm)Enum.Parse(typeof(AGB.D2.Realm), BnetData.RealmName);
            }
        }

        /// <summary>
        /// Gets the current hero name from memory, always the latest value
        /// </summary>
        public string Name
        {
            get
            {
                return BnetData.CharName;
            }
        }

        private Unmanaged.Act Act
        {
            get
            {
                return Reader.Read<Unmanaged.Act>(PlayerUnit.Act);
            }
        }

        private Unmanaged.BnetData BnetData
        {
            get
            {
                return Reader.ReadFromPtr<Unmanaged.BnetData>(Reader.Offset[DllBase.D2Launch] + 0x25ACC);
            }
        }

        private Unmanaged.UnitAny PlayerUnit
        {
            get
            {
                return Reader.ReadFromPtr<Unmanaged.UnitAny>(Reader.Offset[DllBase.D2Client] + 0x11C3D0);
            }
        }
    }
}
