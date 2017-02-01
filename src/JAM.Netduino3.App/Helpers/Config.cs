using System;
using System.IO;

namespace JAM.Netduino3.App.Helpers
{
    public class Config : Toolbox.StringDictionary
    {
        public string ConfigurationFileFullPath { set; get; }

        private const char CommentPrefix = '#';
        private static readonly char[] ValueSeparators = { '=' };
    

        public Config()
        {
            ConfigurationFileFullPath = @"SD\webroot\config.ini";
            ReadConfiguration();
        }
        public Config(string configurationFile)
        {
            ConfigurationFileFullPath = configurationFile;
            ReadConfiguration();
        }
        internal void ReadConfiguration()
        {
            using (var fileStream = File.Open(ConfigurationFileFullPath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    string currentLine;
                    while (!StringHelper.IsNullOrEmpty(currentLine = reader.ReadLine()))
                    {
                        if (currentLine[0] == CommentPrefix) continue;
                        
                        var values = currentLine.Split(ValueSeparators, 2);
                        if (values == null || values.Length != 2)
                        {
                            throw new ApplicationException("Error reading configuration");
                        }
                        Add(values[0], values[1]);
                    }
                }
            }
        }
    }
}
