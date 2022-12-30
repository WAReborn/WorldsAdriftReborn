using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldsAdriftServer.Handlers
{
    internal class DataParser
    {
        static bool CompareByteArrays( byte[] array1, byte[] array2)
        {
            for (int I = 0; I < array1.Length; I++)
            {
                if (array1[I] != array2[I])
                {
                    return false;
                }
            }
            return true;
        }

        static bool CheckIfInArray( byte[] array1, byte[] array2 )
        {
            for (int I = 0; I < array2.Length - array1.Length; I++)
            {
                if (CompareByteArrays(array1, array2[I.. (I + array1.Length) ]) )
                {
                    return true;
                }
            }
            return false;
        }

        static long CheckIfInArrayLocation( byte[] array1, byte[] array2)
        {
            for (int I = 0; I < array2.Length - array1.Length; I++)
            {
                if (CompareByteArrays(array1, array2[I..(I + array1.Length)]))
                {
                    return I + array1.Length;
                }
            }
            return -1;
        }

        static char[] ConvertBinToChar( byte[] array)
        {
            char[] outputStr = new char[array.Length];
            for (int I = 0; I < outputStr.Length; I++)
            {
                outputStr[I] = (char)array[I];
            }
            return outputStr;
        }

        static byte[] ConvertStrToBin( string array )
        {
            byte[] outputArr = new byte[array.Length];
            for (int I = 0; I < outputArr.Length; I++)
            {
                outputArr[I] = (byte)array[I];
            }
            return outputArr;
        }

        static void GetDataStr( byte[] buffer, string key, string text, ref int startLocation)
        {
            startLocation = (int)CheckIfInArrayLocation(ConvertStrToBin(key), buffer[startLocation..buffer.Length]) + startLocation;
            int endLocation = (int)CheckIfInArrayLocation(ConvertStrToBin("\""), buffer[startLocation..buffer.Length]) + startLocation - 1;
            Console.WriteLine(text + new string(ConvertBinToChar(buffer[startLocation..endLocation])));
        }
        static void GetDataChar( byte[] buffer, string key, string text, ref int startLocation )
        {
            startLocation = (int)CheckIfInArrayLocation(ConvertStrToBin(key), buffer[startLocation..buffer.Length]) + startLocation;
            Console.WriteLine(text + (char)buffer[startLocation]);
        }
        static void GetDataBool( byte[] buffer, string key, string text, string option1, string option2, ref int startLocation )
        {
            startLocation = (int)CheckIfInArrayLocation(ConvertStrToBin(key), buffer[startLocation..buffer.Length]) + startLocation;
            if (CompareByteArrays(buffer[startLocation..(startLocation + 4)], ConvertStrToBin("true")))
            { Console.WriteLine(text + option1); }
            else
            { Console.WriteLine(text + option2); }
        }


        public static void ParseIncomingData( byte[] buffer, long offset, long size )
        {
            Console.WriteLine("\n");
                
            if (CheckIfInArray(ConvertStrToBin("{\"Id\":"), buffer[0..(int)size]))
            {
                int bufferLocation = 0;
                Console.WriteLine("Properties:");
                GetDataChar(buffer, "{\"Id\":", "\tId:", ref bufferLocation);
                GetDataStr(buffer, "\"characterUid\":\"", "\tCharacter UID:", ref bufferLocation);
                GetDataStr(buffer, "\"Name\":\"", "\tName:", ref bufferLocation);
                GetDataStr(buffer, "\"Server\":\"", "\tServer:", ref bufferLocation);
                GetDataStr(buffer, "\"serverIdentifier\":\"", "\tServer Identifier:", ref bufferLocation);

                Console.WriteLine("\nHead:");
                GetDataChar(buffer, "\":{\"Id\":\"", "\tId:", ref bufferLocation);
                GetDataStr(buffer, "\"Prefab\":\"", "\tPrefab:", ref bufferLocation);

                Console.WriteLine("\nBody:");
                GetDataChar(buffer, "\":{\"Id\":\"", "\tId:", ref bufferLocation);
                GetDataStr(buffer, "\"Prefab\":\"", "\tPrefab:", ref bufferLocation);

                Console.WriteLine("\nFeet:");
                GetDataChar(buffer, "\":{\"Id\":\"", "\tId:", ref bufferLocation);
                GetDataStr(buffer, "\"Prefab\":\"", "\tPrefab:", ref bufferLocation);

                Console.WriteLine("\nFace:");
                GetDataChar(buffer, "\":{\"Id\":\"", "\tId:", ref bufferLocation);
                GetDataStr(buffer, "\"Prefab\":\"", "\tPrefab:", ref bufferLocation);

                Console.WriteLine("\nFacial Hair:");
                GetDataChar(buffer, "\":{\"Id\":\"", "\tId:", ref bufferLocation);
                GetDataStr(buffer, "\"Prefab\":\"", "\tPrefab:", ref bufferLocation);

                Console.WriteLine("\nMisc:");
                GetDataBool(buffer, "\"isMale\":", "\tGender:", "Male", "Female", ref bufferLocation);
                GetDataBool(buffer, "\"seenIntro\":", "\tSeen Intro:", "Yes", "No", ref bufferLocation);
                GetDataBool(buffer, "\"skippedTutorial\":", "\tSkipped Tutorial:", "Yes", "No", ref bufferLocation);

            }
            else //Display raw data if not handled by custom handler
            {
                for (int ByteIndex = 0; ByteIndex < size; ++ByteIndex)
                {
                    Console.Write((char)buffer[ByteIndex]);
                }
            }
            
            


            return;

        }
    }
}
