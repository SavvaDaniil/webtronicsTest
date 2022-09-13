using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebtronicsTestWPF.Data;
using WebtronicsTestWPF.Repository;

namespace WebtronicsTestWPF.Facade
{
    public class FileVisitFacade
    {
        private ApplicationDbContext _dbc;
        public FileVisitFacade(ApplicationDbContext dbc)
        {
            _dbc = dbc;
        }

        public bool add(string fileName, DateTime dateVisited)
        {
            FileVisitRepository fileVisitRepository = new FileVisitRepository(_dbc);
            return fileVisitRepository.add(fileName, dateVisited) != null;
        }
    }
}
