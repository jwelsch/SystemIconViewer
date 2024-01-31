using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SystemIconViewer
{
    public interface IBitmapConverter
    {
        Avalonia.Media.Imaging.Bitmap? ConvertToAvalonia(Bitmap? systemBitmap);

        Avalonia.Media.Imaging.Bitmap? ConvertToAvalonia(IntPtr hIcon);
    }

    public class BitmapConverter : IBitmapConverter
    {
        public Avalonia.Media.Imaging.Bitmap? ConvertToAvalonia(Bitmap? systemBitmap)
        {
            if (systemBitmap == null)
            {
                return null;
            }
#pragma warning disable CA1416 // Validate platform compatibility

            //var systemBitmapTemp = new System.Drawing.Bitmap(systemBitmap.Instance);
            //var bitmapData = systemBitmapTemp.LockBits(new System.Drawing.Rectangle(0, 0, systemBitmapTemp.Width, systemBitmapTemp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            //var avaloniaBitmap = new Avalonia.Media.Imaging.Bitmap(Avalonia.Platform.PixelFormat.Bgra8888, Avalonia.Platform.AlphaFormat.Premul,
            //                                                       bitmapData.Scan0,
            //                                                       new Avalonia.PixelSize(bitmapData.Width, bitmapData.Height),
            //                                                       new Avalonia.Vector(96, 96),
            //                                                       bitmapData.Stride);
            //systemBitmapTemp.UnlockBits(bitmapData);
            //systemBitmapTemp.Dispose();

            var bitmapData = systemBitmap.LockBits(new System.Drawing.Rectangle(0, 0, systemBitmap.Width, systemBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            var avaloniaBitmap = new Avalonia.Media.Imaging.Bitmap(Avalonia.Platform.PixelFormat.Bgra8888, Avalonia.Platform.AlphaFormat.Premul,
                                                                   bitmapData.Scan0,
                                                                   new Avalonia.PixelSize(bitmapData.Width, bitmapData.Height),
                                                                   new Avalonia.Vector(96, 96),
                                                                   bitmapData.Stride);
            systemBitmap.UnlockBits(bitmapData);

            return avaloniaBitmap;
#pragma warning restore CA1416 // Validate platform compatibility
        }

        public Avalonia.Media.Imaging.Bitmap? ConvertToAvalonia(IntPtr hIcon)
        {
            if (hIcon == IntPtr.Zero)
            {
                return null;
            }

#pragma warning disable CA1416 // Validate platform compatibility

            var icon = Icon.FromHandle(hIcon);

            return ConvertToAvalonia(icon.ToBitmap());
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
