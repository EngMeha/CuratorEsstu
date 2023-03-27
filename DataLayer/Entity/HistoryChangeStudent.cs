using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class HistoryChangeStudent
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public Student Student { get; set; }
        public int PercentOfAttedence { get; set; }
        public int PercentOfProgress { get; set; }
    }
}
