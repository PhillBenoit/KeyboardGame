//Simplified calls to get key press information

using System.Runtime.InteropServices;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Windows.Win32.UI.Input
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public static class KeyEvent
    {
        //information to save from a windows message about a keypress
        public class KeyFrom(ushort key, IntPtr from, bool isUp)
        {
            public readonly ushort key = key;
            public readonly IntPtr from = from;
            public readonly bool isUp = isUp;
        }

        //read information from a windows message
        unsafe public static KeyFrom? GetKeyFrom(ref Message m)
        {
            //filter for input type windows messages
            if (!IsWmType(m, SysEnum.WINDOWS_MESSAGE_TYPE.WM_INPUT)) { return null; }
            
            //get size of input buffer
            uint dwSize = 0;
            PInvoke.GetRawInputData(
                new HRAWINPUT(m.LParam),
                RAW_INPUT_DATA_COMMAND_FLAGS.RID_INPUT,
                null,
                ref dwSize,
                (uint)sizeof(RAWINPUTHEADER));

            //allocate space to save a copy of the buffer
            RAWINPUT rawinput;
            IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);
            try
            {
                //read and copy buffer as long as it's of the expected size
                if (PInvoke.GetRawInputData(
                    (HRAWINPUT)m.LParam,
                    RAW_INPUT_DATA_COMMAND_FLAGS.RID_INPUT,
                    (void*)buffer,
                    ref dwSize,
                    (uint)sizeof(RAWINPUTHEADER))
                    == dwSize) rawinput = Marshal.PtrToStructure<RAWINPUT>(buffer);
                else return null;
                
                //filter for keyboard type messages
                if (!IsRimType(rawinput, SysEnum.RIM_TYPE.KEYBOARD)) return null;

                //return the data
                return new KeyFrom(
                        rawinput.data.keyboard.VKey,
                        rawinput.header.hDevice,
                        Convert.ToBoolean(
                            rawinput.data.keyboard.Flags & (ushort)SysEnum.RAW_KB_FLAGS.UP)
                        );
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        //type matching methods
        public static bool IsWmType(Message message,
            SysEnum.WINDOWS_MESSAGE_TYPE type)
        { return message.Msg == (int)type; }
        internal static bool IsRimType(RAWINPUT message,
            SysEnum.RIM_TYPE type)
        { return message.header.dwType == (int)type; }

        //registers keyboard devices to generate windows messages
        unsafe static public bool RegisterKBHandle(nint FormHandle)
        {
            RAWINPUTDEVICE device = new()
            {
                usUsagePage = (ushort)SysEnum.HID_USAGE_PAGE.GENERIC,
                usUsage = (ushort)SysEnum.HID_USAGE_ID.KEYBOARD,
                dwFlags = RAWINPUTDEVICE_FLAGS.RIDEV_INPUTSINK,
                hwndTarget = (Foundation.HWND)FormHandle
            };
            return PInvoke.RegisterRawInputDevices(
                &device, 1, (uint)sizeof(RAWINPUTDEVICE));
        }

    }
}