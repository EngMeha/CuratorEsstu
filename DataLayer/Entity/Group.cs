using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        public string Title { get; set; }
        public List<Student> Students { get; set; }
        public CraduationDepartament CraduationDepartament { get; set; }
        public FormOfStudy FormOfStudy { get; set; }
    }
}
