using System.Runtime.InteropServices;
using System.Text;

namespace WorldsAdriftRebornGameServer.DLLCommunication
{
    internal class Translator
    {
        public static int Utf8CstrLen( string s )
        {
            return 1 + ((s == null) ? 0 : Encoding.UTF8.GetByteCount(s));
        }
        public static void ToUtf8Cstr(string s, byte[] buffer, int bufferIndex )
        {
            if(s != null)
            {
                Encoding.UTF8.GetBytes(s, 0, s.Length, buffer, bufferIndex);
            }
            buffer[bufferIndex + Utf8CstrLen(s) - 1] = 0;
        }
        public static byte[] ToUtf8Cstr(string s )
        {
            byte[] array = new byte[Utf8CstrLen(s)];
            ToUtf8Cstr(s, array, 0);
            return array;
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
