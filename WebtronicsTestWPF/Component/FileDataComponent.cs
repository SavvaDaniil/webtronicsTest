using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebtronicsTestWPF.Component
{
    internal class FileDataComponent
    {

        public string SecurityCheckFolderPath(string folderPath)
        {
            folderPath = folderPath.Replace(@"/", @"\");
            if (folderPath.EndsWith(@"\")) folderPath = folderPath.Remove(folderPath.Length - 1);
            if (folderPath.EndsWith(":")) folderPath = folderPath + "\\";
            return folderPath;
        }

        public string? RemoveLastFolder(string path)
        {
            if (path == null) return null;

            List<string> pathAsArray = path.Split('\\').ToList();
            if (pathAsArray.Count() == 1) return path;

            pathAsArray.RemoveAt(pathAsArray.Count() - 1);
            System.Diagnostics.Debug.WriteLine("pathAsArray: " + String.Join("\\", pathAsArray));
            return String.Join("\\", pathAsArray);
        }

        public (long, bool) GetDirectorySizeOrWithErrorIfMoreDirectories(DirectoryInfo directoryInfo)
        {
            long size = 0;
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                size += fileInfo.Length;
            }

            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            if (directoryInfos.Any()) return (0, true);

            return (size, false) ;
        }

        public int GetAmountOfFilesInDirectory(DirectoryInfo directoryInfo)
        {
            int amount = 0;
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                amount++;
            }

            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfoFromInfos in directoryInfos)
            {
                amount++;
            }
            return amount;
        }

        public string BytesToString(long byteCount)
        {
            string[] labels = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)return "0" + labels[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + labels[place];
        }
    }
}
