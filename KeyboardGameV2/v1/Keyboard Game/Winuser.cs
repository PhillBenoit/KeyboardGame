using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace User32
{
    public static class Winuser
    {
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
            public IntPtr hwndTarget;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTHEADER
        {
            public uint dwType;
            public uint dwSize;
            public IntPtr hDevice;
            public IntPtr wParam;
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
        public static extern uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

        [DllImport("User32.dll")]
        public static extern bool BlockInput(bool fBlockIt);

        //keyboard characters
        public enum ANSI : byte
        {
            ZERO,
            VK_LBUTTON,
            VK_RBUTTON,
            VK_CANCEL,
            VK_MBUTTON,
            VK_XBUTTON1,
            VK_XBUTTON2,
            RESERVED07,
            VK_BACKSPACE,
            VK_TAB,
            RESERVED0A,
            RESERVED0B,
            VK_CLEAR,
            VK_RETURN,
            UNASSIGNED0E,
            UNASSIGNED0F,
            VK_SHIFT,
            VK_CONTROL,
            VK_MENU,
            VK_PAUSE,
            VK_CAPITAL,
            VK_KANA,
            VK_IME_ON,
            VK_JUNJA,
            VK_FINAL,
            VK_KANJI,
            VK_IME_OFF,
            VK_ESCAPE,
            VK_IME_CONVERT,
            VK_IME_NONCONVERT,
            VK_IME_ACCEPT,
            VK_IME_MODECHANGE,
            VK_SPACE,
            VK_PG_UP,
            VK_PG_DN,
            VK_END,
            VK_HOME,
            VK_LEFT,
            VK_UP,
            VK_RIGHT,
            VK_DOWN,
            VK_SELECT,
            VK_PRINT,
            VK_EXECUTE,
            VK_SNAPSHOT,
            VK_INSERT,
            VK_DELETE,
            VK_HELP,
            VK_ZERO,
            VK_ONE,
            VK_TWO,
            VK_THREE,
            VK_FOUR,
            VK_FIVE,
            VK_SIX,
            VK_SEVEN,
            VK_EIGHT,
            VK_NINE,
            UNDEFINED3A,
            UNDEFINED3B,
            UNDEFINED3C,
            UNDEFINED3D,
            UNDEFINED3E,
            UNDEFINED3F,
            UNDEFINED40,
            VK_A,
            VK_B,
            VK_C,
            VK_D,
            VK_E,
            VK_F,
            VK_G,
            VK_H,
            VK_I,
            VK_J,
            VK_K,
            VK_L,
            VK_M,
            VK_N,
            VK_O,
            VK_P,
            VK_Q,
            VK_R,
            VK_S,
            VK_T,
            VK_U,
            VK_V,
            VK_W,
            VK_X,
            VK_Y,
            VK_Z,
            VK_LWIN,
            VK_RWIN,
            VK_APPS,
            RESERVED5E,
            VK_SLEEP,
            VK_NUMPAD0,
            VK_NUMPAD1,
            VK_NUMPAD2,
            VK_NUMPAD3,
            VK_NUMPAD4,
            VK_NUMPAD5,
            VK_NUMPAD6,
            VK_NUMPAD7,
            VK_NUMPAD8,
            VK_NUMPAD9,
            VK_MULTIPLY,
            VK_ADD,
            VK_SEPARATOR,
            VK_SUBTRACT,
            VK_DECIMAL,
            VK_DIVIDE,
            VK_F1,
            VK_F2,
            VK_F3,
            VK_F4,
            VK_F5,
            VK_F6,
            VK_F7,
            VK_F8,
            VK_F9,
            VK_F10,
            VK_F11,
            VK_F12,
            VK_F13,
            VK_F14,
            VK_F15,
            VK_F16,
            VK_F17,
            VK_F18,
            VK_F19,
            VK_F20,
            VK_F21,
            VK_F22,
            VK_F23,
            VK_F24,
            RESERVED88,
            RESERVED89,
            RESERVED8A,
            RESERVED8B,
            RESERVED8C,
            RESERVED8D,
            RESERVED8E,
            RESERVED8F,
            VK_NUMLOCK,
            VK_SCROLL,
            OEM92,
            OEM93,
            OEM94,
            OEM95,
            OEM96,
            UNASSIGNED97,
            UNASSIGNED98,
            UNASSIGNED99,
            UNASSIGNED9A,
            UNASSIGNED9B,
            UNASSIGNED9C,
            UNASSIGNED9D,
            UNASSIGNED9E,
            UNASSIGNED9F,
            VK_LSHIFT,
            VK_RSHIFT,
            VK_LCONTROL,
            VK_RCONTROL,
            VK_LMENU,
            VK_RMENU,
            VK_BROWSER_BACK,
            VK_BROWSER_FORWARD,
            VK_BROWSER_REFRESH,
            VK_BROWSER_STOP,
            VK_BROWSER_SEARCH,
            VK_BROWSER_FAVORITES,
            VK_BROWSER_HOME,
            VK_VOLUME_MUTE,
            VK_VOLUME_DOWN,
            VK_VOLUME_UP,
            VK_MEDIA_NEXT_TRACK,
            VK_MEDIA_PREV_TRACK,
            VK_MEDIA_STOP,
            VK_MEDIA_PLAY_PAUSE,
            VK_LAUNCH_MAIL,
            VK_LAUNCH_MEDIA_SELECT,
            VK_LAUNCH_APP1,
            VK_LAUNCH_APP2,
            RESERVEDB8,
            RESERVEDB9,
            VK_SEMICOLON,
            VK_OEM_PLUS,
            VK_OEM_COMMA,
            VK_OEM_MINUS,
            VK_OEM_PERIOD,
            VK_FORWARD_SLASH,
            VK_TILDE,
            RESERVEDC1,
            RESERVEDC2,
            RESERVEDC3,
            RESERVEDC4,
            RESERVEDC5,
            RESERVEDC6,
            RESERVEDC7,
            RESERVEDC8,
            RESERVEDC9,
            RESERVEDCA,
            RESERVEDCB,
            RESERVEDCC,
            RESERVEDCD,
            RESERVEDCE,
            RESERVEDCF,
            RESERVEDD0,
            RESERVEDD1,
            RESERVEDD2,
            RESERVEDD3,
            RESERVEDD4,
            RESERVEDD5,
            RESERVEDD6,
            RESERVEDD7,
            RESERVEDD8,
            RESERVEDD9,
            RESERVEDDA,
            VK_OPEN_BRACE,
            VK_PIPE,
            VK_CLOSE_BRACE,
            VK_APOSTROPHE,
            VK_OEM_8,
            RESERVEDE0,
            OEME1,
            VK_OEM_102,
            OEME3,
            OEME4,
            VK_IME_PROCESS,
            OEME6,
            VK_PACKET,
            UNASSIGNEDE8,
            OEME9,
            OEMEA,
            OEMEB,
            OEMEC,
            OEMED,
            OEMEE,
            OEMEF,
            OEMF0,
            OEMF1,
            OEMF2,
            OEMF3,
            OEMF4,
            OEMF5,
            VK_ATTN,
            VK_CRSEL,
            VK_EXSEL,
            VK_EREOF,
            VK_PLAY,
            VK_ZOOM,
            VK_NONAME,
            VK_PA1,
            VK_OEM_CLEAR
        }
        public static readonly byte MAX_ANSI = Winuser.ANSI.GetValues(
            typeof(Winuser.ANSI)).Cast<byte>().Max();
    }
}