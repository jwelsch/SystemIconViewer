using Avalonia.Media.Imaging;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SystemIconViewer.Win32;
using static SystemIconViewer.Win32.Interfaces;

namespace SystemIconViewer
{
    public enum SystemIconSize
    {
        /// <summary>
        /// 16x16 pixels
        /// </summary>
        Small,

        /// <summary>
        /// 32x32 pixels
        /// </summary>
        Large,

        /// <summary>
        /// 48x48 pixels
        /// </summary>
        ExtraLarge,

        /// <summary>
        /// 256x256 pixels
        /// </summary>
        Jumbo
    }

    public interface IFileSystemIconProvider
    {
        Bitmap? GetFileSystemIcon(string path, SystemIconSize iconSize, bool loadFromDisk, ImageListDrawOptions imageListDrawOptions);
    }

    public class FileSystemIconProvider : IFileSystemIconProvider
    {
        private readonly IWin32Api _win32Api;
        private readonly IBitmapConverter _bitmapConverter;
        private readonly IFileSystemInspector _fileSystemInspector;

        public FileSystemIconProvider(IWin32Api win32Api, IBitmapConverter bitmapConverter, IFileSystemInspector fileSystemInspector)
        {
            _win32Api = win32Api;
            _bitmapConverter = bitmapConverter;
            _fileSystemInspector = fileSystemInspector;
        }

        private static uint SystemIconSizeToFlag(SystemIconSize iconSize)
        {
            return iconSize switch
            {
                SystemIconSize.Small => Constants.SHGFI_SMALLICON,
                SystemIconSize.Large => Constants.SHGFI_LARGEICON,
                SystemIconSize.ExtraLarge => Constants.SHIL_EXTRALARGE,
                SystemIconSize.Jumbo => Constants.SHIL_JUMBO,
                _ => 0
            };
        }

        private static uint ImageListDrawOptionsToFlag(ImageListDrawOptions imageListDrawOptions)
        {
            var flags = Constants.ILD_NORMAL;

            flags |= imageListDrawOptions.Transparent ? Constants.ILD_TRANSPARENT : 0;
            flags |= imageListDrawOptions.Blend ? Constants.ILD_BLEND25 : 0;
            flags |= imageListDrawOptions.Selected ? Constants.ILD_SELECTED : 0;

            return flags;
        }

        public Bitmap? GetFileSystemIcon(string path, SystemIconSize iconSize, bool loadFromDisk, ImageListDrawOptions imageListDrawOptions)
        {
            var attributes = Constants.FILE_ATTRIBUTE_NORMAL;

            var iconIndex = GetSysIconIndex(path, attributes, loadFromDisk);

            var hIcon = GetIconFromSystemImageList(iconIndex, iconSize, imageListDrawOptions);

            if (hIcon == IntPtr.Zero)
            {
                return null;
            }

            var bitmap = _bitmapConverter.ConvertToAvalonia(hIcon);

            CleanUp(hIcon);

            return bitmap;
        }

        private int GetSysIconIndex(string path, uint attributes, bool loadFromDisk)
        {
            var flags = Constants.SHGFI_SYSICONINDEX;

            if (!loadFromDisk)
            {
                flags |= Constants.SHGFI_USEFILEATTRIBUTES;
            }

            if (_fileSystemInspector.IsDirectory(path))
            {
                attributes |= Constants.FILE_ATTRIBUTE_DIRECTORY;
            }

            var result = _win32Api.SHGetFileInfo(path, attributes, out SHFILEINFO shfi, (uint)Marshal.SizeOf(typeof(SHFILEINFO)), flags);

            if (result == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return shfi.iIcon;
        }

        private IntPtr GetIconFromSystemImageList(int iconIndex, SystemIconSize iconSize, ImageListDrawOptions imageListDrawOptions)
        {
            var iImageListGuid = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");

            var iconSizeFlag = SystemIconSizeToFlag(iconSize);

            var hResult = _win32Api.SHGetImageList((int)iconSizeFlag, ref iImageListGuid, out IImageList iml);

            if (hResult != Constants.S_OK)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var hIcon = IntPtr.Zero;

            var flags = ImageListDrawOptionsToFlag(imageListDrawOptions);

            hResult = (uint)iml.GetIcon(iconIndex, (int)flags, ref hIcon);

            if (iml != null)
            {
                _win32Api.Marshal_ReleaseComObject(iml);
            }

            if (hResult != Constants.S_OK)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return hIcon;
        }

        private void CleanUp(IntPtr hIcon)
        {
            if (hIcon != IntPtr.Zero)
            {
                _win32Api.DestroyIcon(hIcon);
            }
        }
    }
}
