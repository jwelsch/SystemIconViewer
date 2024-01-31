using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;
using SystemIconViewer.Win32;

namespace SystemIconViewer
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IFileSystemIconProvider _fileSystemIconProvider = new FileSystemIconProvider(new Win32Api(), new BitmapConverter(), new FileSystemInspector());
        private readonly IWin32ErrorMessageService _errorMessageService = new Win32ErrorMessageService();

        [ObservableProperty]
        private string? _path;

        [ObservableProperty]
        private Bitmap? _smallIcon;

        [ObservableProperty]
        private Bitmap? _largeIcon;

        [ObservableProperty]
        private Bitmap? _extraLargeIcon;

        [ObservableProperty]
        private Bitmap? _jumboIcon;

        [ObservableProperty]
        private string? _errorMessage;

        public MainViewModel()
        {
        }

        public void FindSystemIcons(string path, ImageListDrawOptions imageListDrawOptions)
        {
            ErrorMessage = null;

            try
            {
                SmallIcon = _fileSystemIconProvider.GetFileSystemIcon(path, SystemIconSize.Small, true, imageListDrawOptions);
            }
            catch (Exception ex)
            {
                ErrorMessage = FormatErrorMessage(ex);
                return;
            }

            try
            {
                LargeIcon = _fileSystemIconProvider.GetFileSystemIcon(path, SystemIconSize.Large, true, imageListDrawOptions);
            }
            catch (Exception ex)
            {
                ErrorMessage = FormatErrorMessage(ex);
                return;
            }

            try
            {
                ExtraLargeIcon = _fileSystemIconProvider.GetFileSystemIcon(path, SystemIconSize.ExtraLarge, true, imageListDrawOptions);
            }
            catch (Exception ex)
            {
                ErrorMessage = FormatErrorMessage(ex);
                return;
            }

            try
            {
                JumboIcon = _fileSystemIconProvider.GetFileSystemIcon(path, SystemIconSize.Jumbo, true, imageListDrawOptions);
            }
            catch (Exception ex)
            {
                ErrorMessage = FormatErrorMessage(ex);
                return;
            }
        }

        private static string FormatErrorMessage(Exception ex)
        {
            return ex is Win32Exception win32Ex
                ? $"Win32 error: {win32Ex.NativeErrorCode}\r\n{win32Ex.Message}"
                : ex.Message;
        }
    }
}
