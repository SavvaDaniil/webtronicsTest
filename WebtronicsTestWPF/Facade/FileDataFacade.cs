using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebtronicsTestWPF.Model;
using WebtronicsTestWPF.ViewModel.FileData;
using System.IO;
using WebtronicsTestWPF.Service;
using System.Windows;
using System.Diagnostics;
using WebtronicsTestWPF.Component;

namespace WebtronicsTestWPF.Facade
{
    internal class FileDataFacade
    {
        private FileDataService _fileDataService;

        public FileDataFacade()
        {
            _fileDataService = new FileDataService();
        }

        public List<FileDataLiteViewModel> ListFileDataLiteViewModels(string folderPath)
        {
            List<FileDataLiteViewModel> fileDataLiteViewModels = listFilesByPath(folderPath);
            fileDataLiteViewModels.AddRange(this.listFoldersByPath(folderPath));
            return fileDataLiteViewModels;
        }

        public List<FileDataLiteViewModel> listFilesByPath(string folderPath)
        {
            List<FileDataLiteViewModel> fileDataLiteViewModels = new List<FileDataLiteViewModel>();

            List<FileData> fileDatas = _fileDataService.listFilesByPath(folderPath);
            if (fileDatas.Count() == 0) return fileDataLiteViewModels;
            foreach (FileData fileData in fileDatas)
            {
                fileDataLiteViewModels.Add(new FileDataLiteViewModel(fileData));
            }

            return fileDataLiteViewModels;
        }

        public List<FileDataLiteViewModel> listFoldersByPath(string folderPath)
        {
            List<FileDataLiteViewModel> fileDataLiteViewModels = new List<FileDataLiteViewModel>();

            List<FileData> fileDatas = _fileDataService.liftFoldersByPath(folderPath);
            if (fileDatas.Count() == 0) return fileDataLiteViewModels;
            foreach (FileData fileData in fileDatas)
            {
                fileDataLiteViewModels.Add(new FileDataLiteViewModel(fileData));
            }
            return fileDataLiteViewModels;
        }

        public FileDataInfoViewModel? getMetaData(string filePath)
        {
            try
            {
                if (Directory.Exists(filePath))
                {
                    FileDataComponent fileDataComponent = new FileDataComponent();
                    DirectoryInfo directoryInfo = new DirectoryInfo(filePath);

                    (long, bool) sizeAndIsError = fileDataComponent.GetDirectorySizeOrWithErrorIfMoreDirectories(directoryInfo);

                    return new FileDataInfoViewModel(
                        directoryInfo.Name,
                        filePath,
                        sizeAndIsError.Item1,
                        sizeAndIsError.Item2,
                        fileDataComponent.GetAmountOfFilesInDirectory(directoryInfo),
                        directoryInfo.LastWriteTime,
                        directoryInfo.LastAccessTime,
                        directoryInfo.CreationTime
                    );
                } else if (File.Exists(filePath))
                {
                    FileInfo fileInfo = new System.IO.FileInfo(filePath);
                    if (fileInfo == null) return null;
                    return new FileDataInfoViewModel(
                        fileInfo.Name,
                        fileInfo.FullName,
                        fileInfo.Length,
                        fileInfo.LastWriteTime,
                        fileInfo.LastAccessTime,
                        fileInfo.CreationTime
                    );
                } else
                {
                    MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
                    return null;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Не удалось считать метаданные\n" + exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
                //System.Diagnostics.Debug.WriteLine(exception.Message);
            }
            return null;
        }

        public void openFile(string filePath)
        {
            try
            {
                /*
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    //FileName = (filePath),
                    //UseShellExecute = true
                    //FileName = "cmd.exe",
                    //Arguments = "/c start '"+ filePath +"'"
                };
                process.StartInfo = startInfo;
                process.Start();
                */
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });

            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("Try Launch file: " + exception.Message);
                MessageBox.Show(exception.Message, "Ошибка открытия файла", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
            }
        }
    }
}
