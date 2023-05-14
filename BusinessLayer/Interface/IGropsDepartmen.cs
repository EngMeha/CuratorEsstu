using DataLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IGropsDepartmen
    {
        Task<bool> CheckGroup();
        Task CreateGroup(string href, string title, Speciality speciality);
        Task DeleteAllGroup();
        Task<List<GroupsDirectory>> GetAllGroupByTitle(string[] title);
        Task<List<GroupsDirectory>> GetAllGroupWithOutTeacher();
        Task<GroupsDirectory> GetGroupByTitle(string title, bool include = false);
    }
}
