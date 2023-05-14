using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class GroupInfoModel
    {
        public GroupsOfTeacher GroupsOfTeacher { get; set; }
        public List<Student> Students { get; set; }
    }
}
