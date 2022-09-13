using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebtronicsTestWPF.Entity
{
    [Table("file_visit")]
    public class FileVisit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Column("file_name")]
        public string FileName { get; set; }

        [Column("date_visited")]
        public DateTime? DateVisited { get; set; }
    }
}
