using System.IO;

namespace SystemIconViewer
{
    public interface IFileSystemInspector
    {
        bool IsDirectory(string? path);

        bool IsDrive(string? path);

        bool IsFile(string? path);
    }

    public class FileSystemInspector : IFileSystemInspector
    {
        public bool IsDirectory(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            try
            {
                var attributes = File.GetAttributes(path);
                return attributes.HasFlag(FileAttributes.Directory);
            }
            catch
            {
                return false;
            }
        }

        public bool IsDrive(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            try
            {
                var di = new DirectoryInfo(path);

                // This sucks, but meh...
                // Examples of drive paths:
                //   C:
                //   D:\
                return di.Parent == null
                    && di.FullName.Length >= 2
                    && di.FullName.Length <= 3
                    && char.IsLetter(di.FullName[0])
                    && di.FullName[1] == ':'
                    && (di.FullName.Length != 3 || di.FullName[2] == '\\');
            }
            catch
            {
                return false;
            }
        }

        public bool IsFile(string? path)
        {
            return !IsDirectory(path);
        }
    }
}
