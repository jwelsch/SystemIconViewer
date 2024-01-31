namespace SystemIconViewer.Win32
{
    public static class Constants
    {
        public const uint S_OK = 0x00000000;

        public const uint FILE_ATTRIBUTE_READONLY = 0x00000001;
        public const uint FILE_ATTRIBUTE_HIDDEN = 0x00000002;
        public const uint FILE_ATTRIBUTE_SYSTEM = 0x00000004;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        public const uint FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
        public const uint FILE_ATTRIBUTE_DEVICE = 0x00000040;
        public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;
        public const uint FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
        public const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
        public const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
        public const uint FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
        public const uint FILE_ATTRIBUTE_OFFLINE = 0x00001000;
        public const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
        public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;

        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        public const uint SHGFI_ICON = 0x000000100;
        public const uint SHGFI_SMALLICON = 0x000000001;
        public const uint SHGFI_LARGEICON = 0x000000000;
        public const uint SHGFI_SYSICONINDEX = 0x00004000;

        public const uint SHIL_LARGE = 0x00000000;
        public const uint SHIL_SMALL = 0x00000001;
        public const uint SHIL_EXTRALARGE = 0x000000002;
        public const uint SHIL_JUMBO = 0x000000004;

        public const uint ILD_NORMAL = 0x00000000;
        public const uint ILD_TRANSPARENT = 0x00000001;
        public const uint ILD_BLEND25 = 0x00000002;
        public const uint ILD_SELECTED = 0x00000004;
        public const uint ILD_MASK = 0x00000010;
        public const uint ILD_IMAGE = 0x00000020;
        public const uint ILD_ROP = 0x00000040;
        public const uint ILD_PRESERVEALPHA = 0x00001000;
        public const uint ILD_SCALE = 0x00002000;
        public const uint ILD_DPISCALE = 0x00004000;

        public static bool IsSet(uint bitFlags, uint isSetFlag)
        {
            return (bitFlags & isSetFlag) != isSetFlag;
        }
    }
}
