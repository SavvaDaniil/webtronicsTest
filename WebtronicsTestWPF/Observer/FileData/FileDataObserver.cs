using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebtronicsTestWPF.Data;
using WebtronicsTestWPF.Facade;

namespace WebtronicsTestWPF.Observer.FileData
{
    public class FileDataObserver
    {
        private ApplicationDbContext _dbc;
        public FileDataObserver(ApplicationDbContext dbc)
        {
            _dbc = dbc;
        }

        public void FileDataVisited(string filePath)
        {
            FileVisitFacade fileVisitFacade = new FileVisitFacade(_dbc);
            fileVisitFacade.add(filePath, DateTime.Now);
        }
    }
}
