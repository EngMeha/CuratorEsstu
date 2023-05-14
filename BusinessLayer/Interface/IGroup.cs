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
        Task<GroupsOfTeacher> GetGroup(int id, bool include = true);
        Task<List<GroupsOfTeacher>> GetAllGroups(bool include = true);
        Task SaveGroup(GroupsOfTeacher group);
        Task DeleteGroup(GroupsOfTeacher group);
        Task<GroupsOfTeacher> GetFirstGroup(User user);
        Task<List<GroupsOfTeacher>> GetListGroupOfTeacher(User user, bool include);
        Task<List<GroupsOfTeacher>> GetListGroupOfTeacherBySort(User user, string findGroup);
    }
}
