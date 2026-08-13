using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DiabloReader
{
    public enum DllBase
    {
        D2Client,
        D2Common,
        D2Gfx,
        D2Win,
        D2Lang,
        D2Cmp,
        D2Multi,
        BNClient,
        D2Net, // conflict with STORM.DLL
        Storm,
        Fog,
        D2Launch
    }

    public unsafe class Reader
    {
        private Memory Memory;

        public Process Process;

        public Dictionary<DllBase, uint> Offset = new Dictionary<DllBase, uint>();

        public Reader(Process process)
        {
            Process = process;
            Memory = new Memory(process);

            foreach (string name in Enum.GetNames(typeof(DllBase)))
            {
                this.Offset.Add((DllBase)Enum.Parse(typeof(DllBase), name), Memory.GetBaseAddress(name + ".dll"));
            }
        }

        public void Write(IntPtr address, byte[] data)
        {
            Memory.Write(address, data);
        }

        /// <summary>
        /// Reads the pointer value, then uses that to get the real struct
        /// </summary>
        public T ReadFromPtr<T>(void* pointerTo)
        {
            return Memory.Read<T>(ReadPtr(new IntPtr(pointerTo)));
        }
        /// <summary>
        /// Reads the pointer value, then uses that to get the real struct
        /// </summary>
        public T ReadFromPtr<T>(uint pointerTo)
        {
            return Memory.Read<T>(ReadPtr((IntPtr)pointerTo));
        }
        /// <summary>
        /// Reads the pointer value, then uses that to get the real struct
        /// </summary>
        public T ReadFromPtr<T>(IntPtr pointerTo)
        {
            return Memory.Read<T>(ReadPtr(pointerTo));
        }

        public T Read<T>(void* address)
        {
            return Memory.Read<T>(new IntPtr(address));
        }
        public T Read<T>(uint address)
        {
            return Memory.Read<T>((IntPtr)address);
        }
        public T Read<T>(IntPtr address)
        {
            return Memory.Read<T>(address);
        }

        public IntPtr ReadPtr(IntPtr address)
        {
            return (IntPtr)BitConverter.ToUInt32(Memory.Read(address, 4), 0);
        }

        public byte ReadByte(uint address)
        {
            return Memory.Read(address, 1)[0];
        }
    }
}
