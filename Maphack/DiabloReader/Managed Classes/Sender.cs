using System;
using System.Runtime.InteropServices;
using System.Threading;
using MBNCSUtil;

namespace DiabloReader
{
    public class Sender
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32", SetLastError = true)]
        private static extern UInt32 WaitForSingleObject(IntPtr handle, uint milliseconds);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int flAllocationType, int flProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

        private const int MEM_COMMIT = 0x1000;
        private const int MEM_DECOMMIT = 0x4000;

        private const int PAGE_EXECUTE_READWRITE = 0x40;

        private const uint INFINITE = 0xFFFFFFFF;

        public static void SendPacket(Reader reader, byte[] data)
        {
            /*
             * [/] Get SendPacket offset
             * [/] VirtualAllocEx for the packet
             * [/] Write to that allocated space
             * [/] VirtualAllocEx for the code to send the packet
             * [/] Write to that allocated space
             * [/] Create the the remote thread calling the packet sending code
             * [/] Wait for it to finish
             * [/] Free the packet
             * [/] Free the calling code
             */

            // Make room for the packet data, and stick it there
            IntPtr packetDataPointer = VirtualAllocEx(reader.Process.Handle, IntPtr.Zero, data.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            reader.Write(packetDataPointer, data);

            // Make room for the sendPacket code and stick it there
            uint socket = reader.Read<uint>(reader.Offset[DllBase.D2Net] + 0xB248);
            uint sendPacket = reader.Offset[DllBase.D2Net] + 0x5DD4;

            DataBuffer asm = new DataBuffer();

            asm.InsertByte(0x60);                        // pushad

            asm.InsertByte(0x68);                        // push flags (0)
            asm.InsertInt32(0x00);

            asm.InsertByte(0x68);                        // push data length
            asm.InsertUInt32((uint)data.Length);

            asm.InsertByte(0x68);                        // push data pointer
            asm.InsertUInt32((uint)packetDataPointer);

            asm.InsertByte(0x68);                        // push socket handle
            asm.InsertUInt32((uint)socket);

            asm.InsertByte(0xB8);                        // mov eax, sendPacket function
            asm.InsertUInt32((uint)sendPacket);

            asm.InsertByte(0xFF);                        // call eax
            asm.InsertByte(0xD0);

            asm.InsertByte(0x61);                        // popad

            asm.InsertByte(0xC3);                        // RET

            string datsfda = DataFormatter.Format(asm.GetData());

            byte[] asmData = asm.GetData();

            IntPtr asmPointer = VirtualAllocEx(reader.Process.Handle, IntPtr.Zero, asmData.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            reader.Write(asmPointer, asmData);

            // Create the remote thread and wait for it to finish
            IntPtr threadHandle = CreateRemoteThread(reader.Process.Handle, IntPtr.Zero, 0, asmPointer, IntPtr.Zero, 0, IntPtr.Zero);
            WaitForSingleObject(threadHandle, INFINITE);

            // Free the allocated memory
            VirtualFreeEx(reader.Process.Handle, packetDataPointer, data.Length, MEM_DECOMMIT);
            VirtualFreeEx(reader.Process.Handle, asmPointer, asmData.Length, MEM_DECOMMIT);
        }

        private static uint ToBigEndian(uint source)
        {
            //return source;
            return (uint)(source >> 24) |
                         ((source << 8) & 0x00FF0000) |
                         ((source >> 8) & 0x0000FF00) |
                          (source << 24);
        }

    }
}
