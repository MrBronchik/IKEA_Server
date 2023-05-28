using System;
using System.IO;

namespace IKEAserver
{
    // This script is written only for prototype purposes to demonstrate main mechanics of the software
    // The upcoming product might be able to manage with whole HTTPS or FTP or other server to transfer files (animations, logos, descriptions)
    class dbHandler
    {
        private static string dataBaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\database";
        public static string FindDirectory(string _dir)
        {
            string targetDir = dataBaseDirectory + "\\" + _dir;

            if (Directory.Exists(targetDir)) {
                return targetDir;
            } else return "";
        }
    }
}
