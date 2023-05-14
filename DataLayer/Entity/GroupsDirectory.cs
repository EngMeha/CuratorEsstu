using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class GroupsDirectory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public DateTime CreateDate { get; set; }
        public List<GroupsOfTeacher> GroupsOfTeacher { get; set; }
        public Speciality Speciality { get; set; }
        public List<Student> Students { get; set; }

    }
}
