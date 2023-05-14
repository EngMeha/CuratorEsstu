using DataLayer.Entity;
using PresentationLayer.Models;


namespace PresentationLayer.Mappers
{
    public static class CreateEventMapper
    {
        public static CreateEventModel FromDbToModalWithEvent(List<GroupsOfTeacher> groupsOfTeachers, Event @event)
        {
            return new CreateEventModel() { IdEvent = @event.Id, DateEvent = @event.DateTimeEvent, Title = @event.Title, GroupsOfTeacher = groupsOfTeachers };
        }
        public static CreateEventModel FromDbToModalWithOutEvent(List<GroupsOfTeacher> groupsOfTeachers)
        {
            return new CreateEventModel() { GroupsOfTeacher = groupsOfTeachers };
        }
    }
}
