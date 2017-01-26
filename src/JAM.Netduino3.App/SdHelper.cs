using System;
using Microsoft.SPOT;
using System.IO;
using System.Collections;

namespace JAM.Netduino3.App
{
   public static class SdHelper
        {
            public static string[] GetFiles(string extension)
            {
                return GetFiles(null, extension, false);
            }

            public static string[] GetFiles(string extension,
            bool recursive)
            {
                return GetFiles(null, extension, recursive);
            }

            public static string[] GetFiles(string path,string extension)
            {
                return GetFiles(path, extension, false);
            }

            public static string[] GetFiles(string path,
            string extension, bool recursive)
            {
                if (extension[0] != '.')
                    extension = '.' + extension;

                ArrayList list = new ArrayList();
                GetFilesHelper(path, extension, recursive, list);
                return (string[])list.ToArray(typeof(string));
            }

            private static void GetFilesHelper(string path,
            string extension, bool recursive, ArrayList list)
            {
                var files = Directory.GetFiles(path);
                foreach (var fileName in files)
                {
                    if (System.IO.Path.GetExtension(fileName).ToLower() == extension.ToLower())
                        list.Add(fileName);
                }

                if (recursive)
                {
                    var directories = Directory.GetDirectories(path);
                    foreach (var directoryName in directories)
                        GetFilesHelper(directoryName, extension, recursive, list);
                }
            }
        }
}
