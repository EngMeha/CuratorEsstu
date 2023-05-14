using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class AllGroupModel
    {
        public List<GroupsOfTeacher> GroupsOfTeachers { get; set; }
        public List<GroupsDirectory> GroupsDirectory { get; set; }
    }
}
