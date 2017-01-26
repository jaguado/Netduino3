using System;
using Microsoft.SPOT;
using System.IO;
using System.Collections;

namespace JAM.Netduino3.dto
{
    public class DirectoryList
    {
        public string[] Files { set; get; }
        public DirectoryList Folders { set; get; }
    }
}
