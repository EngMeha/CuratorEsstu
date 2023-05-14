using BusinessLayer;
using DataLayer.Entity;
using PresentationLayer.Mappers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Service
{
    public class EventService
    {
        DataManager _dataManager;
        public EventService(DataManager dataManager) 
        {
            _dataManager = dataManager;
        }

        public async Task<AllEventModel> ViewAllEvent()
        {
            return AllEventMapper.FromDbToModel(await _dataManager.Events.GetAllEventsWithOutStudent());
        }

        public async Task<DescriptionEventModal> ViewDescriptionEvent(int id)
        {
            return DescriptionEventMapper.FromDbToModal(await _dataManager.Events.GetEvent(id));
        }
        public async Task<DescriptionEventFromCalendar> ViewDescriptionEvent(int id, List<string> titleGroups)
        {
            return DescriptionEventMapper.FromDbToModal(titleGroups,  await _dataManager.Events.GetEvent(id));
        }

        public async Task<CreateEventModel> ViewCreateEvent(int idEvent, User user, string findGroup)
        {
            return CreateEventMapper.FromDbToModalWithEvent(await _dataManager.Groups.GetListGroupOfTeacherBySort(user, findGroup),
                await _dataManager.Events.GetEvent(idEvent, false));
        }
        public async Task<CreateEventModel> ViewCreateEvent(User user, string findGroup)
        {
            return CreateEventMapper.FromDbToModalWithOutEvent(await _dataManager.Groups.GetListGroupOfTeacherBySort(user, findGroup));
        }
        public async Task<EventOfCalendarModel> ViewEventOfCalendar(int mount)
        {
            return EventOfCalendarMapper.FromDbToModel(await _dataManager.Events.GetEventsByMounth(mount));
        }
    }
}
