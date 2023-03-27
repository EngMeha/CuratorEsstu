using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class EventOfStudent
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Event Event { get; set; }
    }
}
