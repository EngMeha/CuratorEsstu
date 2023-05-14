using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entity
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public List<GroupsDirectory> GroupsDirectory { get; set; }
    }
}
