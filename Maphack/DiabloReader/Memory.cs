using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DiabloReader
{
    /// <summary>
    /// ProcessMemoryReader is a class that enables direct reading a process memory
    /// </summary>
    class ProcessMemoryReaderApi
    {
        // constants information can be found in <winnt.h>
        [Flags]
        public enum ProcessAccessType
        {
            PROCESS_TERMINATE = (0x0001),
            PROCESS_CREATE_THREAD = (0x0002),
            PROCESS_SET_SESSIONID = (0x0004),
            PROCESS_VM_OPERATION = (0x0008),
            PROCESS_VM_READ = (0x0010),
            PROCESS_VM_WRITE = (0x0020),
            PROCESS_DUP_HANDLE = (0x0040),
            PROCESS_CREATE_PROCESS = (0x0080),
            PROCESS_SET_QUOTA = (0x0100),
            PROCESS_SET_INFORMATION = (0x0200),
            PROCESS_QUERY_INFORMATION = (0x0400)
        }

        // function declarations are found in the MSDN and in <winbase.h> 

        //		HANDLE OpenProcess(
        //			DWORD dwDesiredAccess,  // access flag
        //			BOOL bInheritHandle,    // handle inheritance option
        //			DWORD dwProcessId       // process identifier
        //			);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);

        //		BOOL CloseHandle(
        //			HANDLE hObject   // handle to object
        //			);
        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);

        //		BOOL ReadProcessMemory(
        //			HANDLE hProcess,              // handle to the process
        //			LPCVOID lpBaseAddress,        // base of memory area
        //			LPVOID lpBuffer,              // data buffer
        //			SIZE_T nSize,                 // number of bytes to read
        //			SIZE_T * lpNumberOfBytesRead  // number of bytes read
        //			);
        [DllImport("kernel32.dll")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);

        //		BOOL WriteProcessMemory(
        //			HANDLE hProcess,                // handle to process
        //			LPVOID lpBaseAddress,           // base of memory area
        //			LPCVOID lpBuffer,               // data buffer
        //			SIZE_T nSize,                   // count of bytes to write
        //			SIZE_T * lpNumberOfBytesWritten // count of bytes written
        //			);
        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);
    }

    internal class Memory
    {
        private Process m_ReadProcess = null;

        private IntPtr m_hProcess = IntPtr.Zero;

        public Dictionary<string, IntPtr> ModuleOffsets;

        public Memory(Process proc)
        {
            this.m_ReadProcess = proc;

            Process.EnterDebugMode();

            this.OpenProcess();

            this.ModuleOffsets = this.GetModuleOffsets();
        }

        private void OpenProcess()
        {
            //			m_hProcess = ProcessMemoryReaderApi.OpenProcess(ProcessMemoryReaderApi.PROCESS_VM_READ, 1, (uint)m_ReadProcess.Id);
            //ProcessMemoryReaderApi.ProcessAccessType access;
            //access = ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_READ
            //    | ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_WRITE
            //    | ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_OPERATION
            //    | ProcessMemoryReaderApi.ProcessAccessType.PROCESS_QUERY_INFORMATION 
            //    | ProcessMemoryReaderApi.ProcessAccessType.PROCESS_SET_INFORMATION;

            // All access
            m_hProcess = ProcessMemoryReaderApi.OpenProcess((uint)0x001F0FFF, 1, (uint)m_ReadProcess.Id);
        }

        public void CloseHandle()
        {
            int iRetValue;
            iRetValue = ProcessMemoryReaderApi.CloseHandle(m_hProcess);
            if (iRetValue == 0)
                throw new Exception("CloseHandle failed");
        }

        public Dictionary<string, IntPtr> GetModuleOffsets()
        {
            Dictionary<string, IntPtr> offsets = new Dictionary<string, IntPtr>();

            foreach (ProcessModule module in this.m_ReadProcess.Modules)
            {
                string moduleName = module.ModuleName.ToLower().Replace(".dll", "");

                if (!offsets.ContainsKey(moduleName))
                    offsets.Add(moduleName, module.BaseAddress);
            }

            return offsets;
        }

        public T Read<T>(IntPtr address)
        {
            byte[] data = this.Read(address, Marshal.SizeOf(typeof(T)));

            IntPtr pnt = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, pnt, data.Length);

            T obj = (T)Marshal.PtrToStructure(pnt, typeof(T));

            Marshal.FreeHGlobal(pnt);

            return obj;
        }
        public byte[] Read(uint MemoryAddress, int bytesToRead)
        {
            int notSavingThis;
            return Read((IntPtr)MemoryAddress, (uint)bytesToRead, out notSavingThis);
        }
        public byte[] Read(IntPtr MemoryAddress, int bytesToRead)
        {
            int notSavingThis;
            return Read((IntPtr)MemoryAddress, (uint)bytesToRead, out notSavingThis);
        }
        public byte[] Read(int MemoryAddress, int bytesToRead, out int bytesRead)
        {
            return Read((IntPtr)MemoryAddress, (uint)bytesToRead, out bytesRead);
        }
        public byte[] Read(IntPtr MemoryAddress, uint bytesToRead, out int bytesRead)
        {
            byte[] buffer = new byte[bytesToRead];

            IntPtr ptrBytesRead;
            ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, MemoryAddress, buffer, bytesToRead, out ptrBytesRead);

            bytesRead = ptrBytesRead.ToInt32();

            return buffer;
        }

        public void Write(IntPtr address, byte[] data)
        {
            int ptrBytesWritten;

            this.Write(address, data, out ptrBytesWritten);
        }
        public void Write(IntPtr MemoryAddress, byte[] bytesToWrite, out int bytesWritten)
        {
            IntPtr ptrBytesWritten;
            ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, MemoryAddress, bytesToWrite, (uint)bytesToWrite.Length, out ptrBytesWritten);

            bytesWritten = ptrBytesWritten.ToInt32();
        }

        public uint GetBaseAddress(string moduleName)
        {
            ProcessModuleCollection Modules = this.m_ReadProcess.Modules; //get all Modules of this Process
            ProcessModule pModule;
            uint baseAddress = 0;

            for (int i = 0; i < Modules.Count; i++)
            {
                pModule = Modules[i];
                if (pModule.ModuleName.Equals(moduleName, StringComparison.CurrentCultureIgnoreCase))
                {
                    baseAddress = (uint)pModule.BaseAddress;
                    break;
                }
            }
            return baseAddress;
        }
    }
}
