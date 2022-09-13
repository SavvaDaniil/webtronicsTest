using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebtronicsTestWPF.ViewModel.FileData
{
    public class FileDataLiteViewModel
    {
        public string Name { get; set; }
        public string TypeStr { get; set; }

        public FileDataLiteViewModel(WebtronicsTestWPF.Model.FileData? fileData)
        {
            Name = fileData != null ? fileData.Name : "";
            if (fileData != null)
            {
                switch (fileData.FileDataType)
                {
                    case Model.FileDataType.File:
                        TypeStr = "файл";
                        break;
                    case Model.FileDataType.Folder:
                        TypeStr = "папка";
                        break;
                    default:
                        TypeStr = "";
                        break;
                }
            } else
            {
                TypeStr = "";
            }
        }
    }
}
