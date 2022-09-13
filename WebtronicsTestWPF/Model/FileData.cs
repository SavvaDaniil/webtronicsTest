using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebtronicsTestWPF.Model
{
    public class FileData
    {
        public string Name { get;set; }
        public string Path { get; set; }
        public FileDataType? FileDataType { get; set; }

        public FileData(string name, string path, FileDataType? fileDataType)
        {
            Name = name;
            Path = path;
            FileDataType = fileDataType;
        }
    }
}
