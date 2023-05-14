using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DateTimeEvent { get; set; }
        public string Comment { get; set; }
        public List<EventOfStudent> EventOfStudents { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
    }
}
