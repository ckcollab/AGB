using System;
using System.Runtime.InteropServices;

using D2Data;

using AGB.D2;

namespace DiabloReader.Unmanaged
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct MonsterData
    {
        public fixed byte _1[22];

        public byte Flags;

        public UInt16 _2;
        public uint _3;
        public fixed byte Enchants[9];
        public byte _4;
        public UInt16 UniqueNo;
        public byte _5;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]
        public string Name;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct BnetData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public uint[] _1;
        public UInt16 _1a;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string GameName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 86)]
        public string GameServerIp;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
        public string AccountName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string CharName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string RealmName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 291)]
        public byte[] _2;
        public byte Difficulty;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)]
        public byte[] _3;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string GamePassword;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Path
    {
        public UInt16 XOffset;
        public UInt16 X;
        public UInt16 YOffset;
        public UInt16 Y;

        public fixed uint _1[2];
        public UInt16 xTarget;
        public UInt16 yTarget;
        public fixed uint _2[2];
        public Room1* Room1;
        public IntPtr pRoomUnk;
        public fixed uint _3[3];
        public IntPtr pUnit;
        public uint dwFlags;
        public uint _4;
        public uint dwPathType;
        public uint dwPrevPathType;
        public uint dwUnitSize;
        public fixed uint _5[4];
        public IntPtr pTargetUnit;
        public uint dwTargetType;
        public uint dwTargetId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct UnitAny
    {
        public uint dwType;
        public uint dwTxtFileNo;
        public uint _1;
        public uint UID;
        public uint dwMode;

        public IntPtr UnitAnyData;

        public ActLevel ActLevel;
        public Act* Act;
        public fixed uint dwSeed[2];
        public uint _2;

        // second union
        public IntPtr Path;

        public fixed uint _3[5];
        public uint dwGfxFrame;
        public uint dwFrameRemain;
        public ushort wFrameRate;
        public ushort _4;
        public IntPtr ptrGfxUnk;
        public IntPtr ptrGfxInfo;
        public uint _5;
        public IntPtr ptrStats;
        public IntPtr ptrInventory;
        public IntPtr ptrtLight;
        public fixed uint _6[9];

        public ushort X;
        public ushort Y;

        public uint _7;

        public uint dwOwnerType;
        public uint dwOwnerId;

        public fixed uint _8[3];

        public IntPtr ptrInfo;

        public fixed uint _9[6];

        public uint dwFlags;
        public uint dwFlags2;

        public fixed uint _10[5];

        public IntPtr ptrChangedNext;
        public IntPtr ptrRoomNext;
        public IntPtr Next;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Level
    {
        public uint _1;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public AreaLevel nLevelNo;
        public fixed UInt16 _1a[240];
        public UInt16 wLevelType;
        public uint Seed;
        public fixed uint _2[1];
        public Room2* Room2;
        public IntPtr ptrActMisc;
        public fixed UInt16 _3[16];
        public Level* Next;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Act
    {
        public uint Seed;
        public fixed byte _1[0x30];        
        public Room1* Room1;    
        public ActMisc* Misc;   
        public uint _2;             
        public ActLevel ActLevel;   
        public uint pfnCallBack;    
        public fixed uint _3[0x0C]; 
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ActMisc
    {
        public uint Seed;
        public fixed uint _1[29];
        public Act* ptrAct;
        public uint nBossTombLvl;
        public fixed uint _2[248];
        public Level* LevelFirst;
        public fixed uint _3[2];
        public uint nStaffTombLvl;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Room1
    {
        public fixed uint dwSeed[2];
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public fixed byte _1[28];
        public IntPtr pRoomsNear;
        public Room2* Room2;
        public IntPtr pUnitFirst;
        public fixed byte _2[24];
        public IntPtr _1s;
        public uint _3;
        public uint _5;
        public fixed UInt16 _6[4];
        public IntPtr pAct;
        public uint _7;
        public Room1* RoomNext;
        public int nUnknown;
        public uint dwRoomsNear;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct Room2
    {
        public IntPtr pRoomTiles;
        public uint _1;
        public int PresetType;
        public int Dt1Mask;
        public uint dwRoomsNear;
        public fixed UInt16 _3[4];
        public Level* Level;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public Room2* pRoom2Near;
        public PresetUnit* PresetUnit;
        public Room2* Next;
        public fixed UInt16 _4[68];
        public IntPtr Prev;
        public IntPtr pRoom2Other;
        public uint _5;
        public fixed uint dwSeed[2];
        public uint _6;
        public uint _7;
        public fixed UInt16 _8[4];
        public Room1* Room1;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct PresetUnit
    {
        public fixed uint _1[2];
        public int Y;
        public int nTxtFileNo;
        public fixed uint _2[1];
        public PresetUnit* Next;
        public int X;
        public UnitType Type;
    }
}
