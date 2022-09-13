using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebtronicsTestWPF.ViewModel.FileData
{
    public class FileDataInfoViewModel
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public long Size { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime CreationTime { get; set; }

        public bool IsCountSizeError { get; set; } = false;
        public int AmountOfFiles { get; set; }

        public bool IsFile {get;} 

        public FileDataInfoViewModel(string name, string fullPath, long size, DateTime lastWriteTime, DateTime lastAccessTime, DateTime creationTime)
        {
            Name = name;
            FullPath = fullPath;
            Size = size;
            LastWriteTime = lastWriteTime;
            LastAccessTime = lastAccessTime;
            CreationTime = creationTime;

            IsFile = true;
        }

        public FileDataInfoViewModel(string name, string fullPath, long size, bool isCountSizeError, int amountOfFiles, DateTime lastWriteTime, DateTime lastAccessTime, DateTime creationTime)
        {
            Name = name;
            FullPath = fullPath;
            Size = size;
            IsCountSizeError = isCountSizeError;
            LastWriteTime = lastWriteTime;
            LastAccessTime = lastAccessTime;
            CreationTime = creationTime;
            AmountOfFiles = amountOfFiles;

            IsFile = false;
        }
    }
}
