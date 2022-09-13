using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebtronicsTestWPF.Data;
using WebtronicsTestWPF.Entity;

namespace WebtronicsTestWPF.Repository
{
    public class FileVisitRepository
    {
        private ApplicationDbContext _dbc;
        public FileVisitRepository(ApplicationDbContext dbc)
        {
            _dbc = dbc;
        }

        public FileVisit add(string fileName, DateTime dateVisited)
        {
            FileVisit fileVisit = new FileVisit();
            fileVisit.FileName = fileName;
            fileVisit.DateVisited = dateVisited;
            return add(fileVisit);
        }

        public FileVisit add(FileVisit fileVisit)
        {
            _dbc.FileVisits.Add(fileVisit);
            _dbc.SaveChanges();
            return fileVisit;
        }

        public List<FileVisit> listAll()
        {
            return _dbc.FileVisits.OrderByDescending(p => p.DateVisited).ToList();
        }

    }
}
