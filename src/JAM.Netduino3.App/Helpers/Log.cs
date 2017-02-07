using Microsoft.SPOT;
using System;
using System.IO;

namespace JAM.Netduino3.App.Helpers
{
    public static class Log
    {
        public static string LogPath { set; get; }
        private static bool firstTime = true;
        private static void init ()
        {
            LogPath = @"SD\webroot\log\";
            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);
            LogPath += "growcontrol_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            firstTime = false;
        }

        private static FileStream _fileStream = null;
        private static StreamWriter _streamWriter = null;
        public static void Save(string log, string source= "JAM.Netduino3.App")
        {
            if (firstTime)
                init();


            var mode = FileMode.Append;
            if (!File.Exists(LogPath)) mode = FileMode.CreateNew;
            if (_fileStream == null)
                _fileStream = File.Open(LogPath, mode, FileAccess.Write);
            if (_streamWriter == null)
            {
                _streamWriter = new StreamWriter(_fileStream);
                _streamWriter.WriteLine(Enviroment.NewLine + "##################################################################################" + Enviroment.NewLine);
            }          
            _streamWriter.WriteLine(string.Concat(DateTime.Now, "|", source, "|", log));
            _streamWriter.Flush();
        }

        public static void Print(string log, string source = "JAM.Netduino3.App")
        {
            Debug.Print(log);
            Save(log, source);
        }
    }
}
