using System.Runtime.InteropServices;
using System.Text;

namespace WorldsAdriftRebornGameServer.DLLCommunication
{
    internal class Translator
    {
        public unsafe static string FromUtf8Cstr(byte* buffer )
        {
            if(buffer == null)
            {
                return "";
            }

            int len = 0;
            while (buffer[len] != 0)
            {
                len++;
            }

            byte[] array = new byte[len];
            for(int i = 0; i < len; i++)
            {
                array[i] = buffer[i];
            }

            return Encoding.UTF8.GetString(array);
        }
        public unsafe static string FromUtf8Cstr( byte* buffer, long len )
        {
            if (buffer == null)
            {
                return "";
            }

            Console.WriteLine("buffer seems to be of size " + len);

            byte[] array = new byte[len];
            for (long i = 0; i < len; i++)
            {
                array[i] = buffer[i];
            }

            return Encoding.UTF8.GetString(array);
        }

        /**
         need to call Marshal.FreeHGlobal() on the ptr when done
         can do ptr.ToPointer() to get a void* ptr
         */
        public unsafe static IntPtr stringToIntPtr(string data )
        {
            return Marshal.StringToHGlobalAnsi(data);
        }
    }
}
