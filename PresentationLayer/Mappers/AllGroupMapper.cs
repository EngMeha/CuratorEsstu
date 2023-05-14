using DataLayer.Entity;
using PresentationLayer.Models;
namespace PresentationLayer.Mappers
{
    public static class AllGroupMapper
    {
        public static AllGroupModel FromDbToModel(List<GroupsOfTeacher> groupsOfTeachers, List<GroupsDirectory> groupsDirectories)
        {
            return new() { GroupsOfTeachers = groupsOfTeachers, GroupsDirectory = groupsDirectories };
        }
    }
}
