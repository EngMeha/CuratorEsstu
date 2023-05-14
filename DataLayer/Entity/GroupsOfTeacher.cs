using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class GroupsOfTeacher
    {
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        public CraduationDepartament CraduationDepartament { get; set; }
        public int Well { get; set; }
        public GroupsDirectory GroupDirectory { get; set; }

    }
}
