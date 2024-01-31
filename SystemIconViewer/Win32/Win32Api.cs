using System;
using System.Runtime.InteropServices;
using static SystemIconViewer.Win32.Interfaces;

namespace SystemIconViewer.Win32
{
    public interface IWin32Api
    {
        int SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint flags);

        bool DestroyIcon(IntPtr hIcon);

        uint SHGetImageList(int iImageList, ref Guid riid, out IImageList ppv);

        /// <summary>
        /// Not an actual Win32 API.
        /// </summary>
        /// <param name="o"></param>
        void Marshal_ReleaseComObject(object o);
    }

    public partial class Win32Api : IWin32Api
    {
        private static partial class Win32Methods
        {
            [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint flags);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool DestroyIcon(IntPtr hIcon);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool DeleteObject(IntPtr hObject);

            [DllImport("shell32.dll", SetLastError = true)]
            public static extern uint SHGetImageList(int iImageList, ref Guid riid, out IImageList ppv);
        }

        public int SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint flags)
        {
            return Win32Methods.SHGetFileInfo(pszPath, dwFileAttributes, out psfi, cbFileInfo, flags);
        }

        public bool DestroyIcon(IntPtr hIcon)
        {
            return Win32Methods.DestroyIcon(hIcon);
        }

        public bool DeleteObject(IntPtr hObject)
        {
            return Win32Methods.DeleteObject(hObject);
        }

        public uint SHGetImageList(int iImageList, ref Guid riid, out IImageList ppv)
        {
            return Win32Methods.SHGetImageList(iImageList, ref riid, out ppv);
        }

        public void Marshal_ReleaseComObject(object o)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            Marshal.ReleaseComObject(o);
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
