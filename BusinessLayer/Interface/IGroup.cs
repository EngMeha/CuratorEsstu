using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IGroup
    {
        Task<Group> GetGroup(int id, bool include = true);
        Task<List<Group>> GetAllGroups(bool include = true);
        Task SaveGroup(Group group);
        Task DeleteGroup(Group group);
    }
}
