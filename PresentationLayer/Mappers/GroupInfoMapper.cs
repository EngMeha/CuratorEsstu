using DataLayer.Entity;
using PresentationLayer.Models;

namespace PresentationLayer.Mappers
{
    public static class GroupInfoMapper
    {
        public static GroupInfoModel FromDbToModel(GroupsOfTeacher group, List<Student> students)
        {
            return new GroupInfoModel() { GroupsOfTeacher = group, Students = students };
        }
    }
}
