using BusinessLayer;
using DataLayer.Entity;
using PresentationLayer.Mappers;
using PresentationLayer.Models;


namespace PresentationLayer.Service
{
    public class GroupOfTeacherService
    {
        DataManager _dataManager;
        public GroupOfTeacherService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<GroupInfoModel> ViewInfoGroup(User user)
        {
            GroupsOfTeacher groupsOfTeacher = await _dataManager.Groups.GetFirstGroup(user);
            return GroupInfoMapper.FromDbToModel(groupsOfTeacher, await _dataManager.Students.GetStudentsByGroup(groupsOfTeacher));
        }
        public async Task<GroupInfoModel> ViewInfoGroup(int idGroup)
        {
            GroupsOfTeacher groupsOfTeacher = await _dataManager.Groups.GetGroup(idGroup, true);
            return GroupInfoMapper.FromDbToModel(groupsOfTeacher, await _dataManager.Students.GetStudentsByGroup(groupsOfTeacher));
        }
        public async Task<AllGroupModel> ViewAllGroup(User user)
        {
            return AllGroupMapper.FromDbToModel(await _dataManager.Groups.GetListGroupOfTeacher(user, false),
                await _dataManager.GropsDepartmen.GetAllGroupWithOutTeacher());
        }
    }
}
