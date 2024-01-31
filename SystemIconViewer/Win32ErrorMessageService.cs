using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SystemIconViewer
{
    public interface IWin32ErrorMessageService
    {
        string GetLastErrorMessage();

        string GetErrorMessage(int errorCode);
    }

    public class Win32ErrorMessageService : IWin32ErrorMessageService
    {
        public string GetLastErrorMessage()
        {
            return GetErrorMessage(Marshal.GetLastWin32Error());
        }

        public string GetErrorMessage(int errorCode)
        {
            return new Win32Exception(errorCode).Message;
        }
    }
}
