//using System;
//using System.Linq;
//using System.Runtime.InteropServices;

namespace KeyboardGameV2.src
{
    public static class Winuser
    {
        /*
        //defined constants
        public const int RID_INPUT = 0x10000003; //raw input data classification = input
        public const ushort RIM_TYPEKEYBOARD = 0x01;//raw input message type = keyboard
        public const ushort WM_INPUT = 0xFF;//windows message type = input
        public const ushort RIC_PRIMARY_PAGE = 0x01; // HID page
        public const ushort RIC_KEYBOARD = 0x06; // HID usage
        public const ushort RIC_INPUTSINK = 0x00; //receive input even when not focused
        public const ushort RIK_KEY_DOWN_FLAG = 0x00; //raw input keyboard keypress event flag
        public const ushort RIK_KEY_UP_FLAG = 0x01; //raw input keyboard keypress event flag

        //structures for formatting data
        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICECLASS
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public uint dwFlags;
            public nint hwndTarget;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTHEADER
        {
            public uint dwType;
            public uint dwSize;
            public nint hDevice;
            public nint wParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWKEYBOARD
        {
            public ushort MakeCode;
            public ushort Flags;
            public ushort Reserved;
            public ushort VKey;
            public uint Message;
            public uint ExtraInformation;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct RAWINPUTUNION
        {
            [FieldOffset(0)]
            public RAWKEYBOARD keyboard;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUT
        {
            public RAWINPUTHEADER header;
            public RAWINPUTUNION data;
        }


        //methods to improt from user32.dll
        [DllImport("User32.dll")]
        public static extern bool RegisterRawInputDevices(RAWINPUTDEVICECLASS[] pRawInputDevices, uint uiNumDevices, uint cbSize);

        [DllImport("User32.dll")]
        public static extern uint GetRawInputData(nint hRawInput, uint uiCommand, nint pData, ref uint pcbSize, uint cbSizeHeader);

        [DllImport("User32.dll")]
        public static extern bool BlockInput(bool fBlockIt);
        */
        //keyboard characters
    }
}