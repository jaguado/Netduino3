using System.IO;
using Microsoft.SPOT.IO;

namespace JAM.Netduino3.App.Helpers
{
    public class IO
    {
        // Check id the SD card is mounted
        public static bool VolumeExist()
        {
            VolumeInfo[] volumes = VolumeInfo.GetVolumes();
            foreach (VolumeInfo volumeInfo in volumes)
            {
                if (volumeInfo.Name.Equals("SD"))
                    return true;
            }

            return false;
        }

        // Append to a file.
        // Create the file if it doesn't exist
        public static void AppendToFile(
            string filename,
            string message)
        {
            if (!VolumeExist())
                return;

                using(FileStream file = File.Exists(filename)
                                      ? new FileStream(filename, FileMode.Append)
                                      : new FileStream(filename, FileMode.Create)){;
                
                    StreamWriter streamWriter = new StreamWriter(file);
                    streamWriter.WriteLine(message);
                    streamWriter.Flush();
                    streamWriter.Close();
                
                    file.Close();
                }

        }

        public static string ReadFromFile(
           string filename)
        {
            string strRes = string.Empty;
            if (!VolumeExist())
                return strRes;

                if (File.Exists(filename))
                {
                    using(FileStream file = new FileStream(filename, FileMode.Open,FileAccess.Read)){
                    StreamReader streamReader = new StreamReader(file);
                    strRes = streamReader.ReadToEnd();
                    streamReader.Close();

                    file.Close();
                    }
                }
                else
                {
                    return strRes;
                }
              
           return strRes;
        }
        /// <summary>
        /// Converts a byte array to a string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string bytesToString(byte[] bytes)
        {
            string s = string.Empty;

            for (int i = 0; i < bytes.Length; ++i)
                s += (char)bytes[i];

            return s;
        }
    }
}
