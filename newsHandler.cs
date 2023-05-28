using System;
using System.IO;

namespace IKEAserver
{
    // This script is written only for prototype purposes to demonstrate main mechanics of the software
    // The upcoming product might be able to manage with whole HTTPS or FTP or other server to transfer files (animations, logos, descriptions)
    class newsHandler
    {
        private static string newsDirectory = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\news";

        public static string GetDirectory()
        {
            return newsDirectory;
        }

        public static int GetNumberOfDirectories()
        {
            int directoryCount = System.IO.Directory.GetDirectories(newsDirectory).Length;
            return directoryCount;
        }
    }
}