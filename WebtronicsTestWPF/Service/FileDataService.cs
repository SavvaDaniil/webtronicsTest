using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebtronicsTestWPF.Model;
using System.IO;

namespace WebtronicsTestWPF.Service
{
    internal class FileDataService
    {

        public List<FileData> listFilesByPath(string folderPath)
        {
            List<FileData> listOfFileDatas = new List<FileData>();
            string[] fileEntries = Directory.GetFiles(folderPath);
            if (fileEntries.Length == 0) return listOfFileDatas;
            string fileName;
            foreach (string filePath in fileEntries)
            {
                fileName = filePath.Split("\\").Last();
                listOfFileDatas.Add(new FileData(fileName, filePath, FileDataType.File));
            }

            return listOfFileDatas;
        }

        public List<FileData> liftFoldersByPath(string folderPath)
        {
            List<FileData> listOfFoldersDatas = new List<FileData>();
            string[] foldersPath = Directory.GetDirectories(folderPath);
            if (foldersPath.Length == 0) return listOfFoldersDatas;
            string fileName;
            foreach (string filePath in foldersPath)
            {
                fileName = filePath.Split("\\").Last();
                listOfFoldersDatas.Add(new FileData(fileName, filePath, FileDataType.Folder));
            }

            return listOfFoldersDatas;
        }



    }
}
