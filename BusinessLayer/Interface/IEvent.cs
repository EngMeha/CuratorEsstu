using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IEvent
    {
        Task<Event> GetEvent(int id, bool include = false);
        Task<List<Event>> GetAllEventsWithOutStudent();
        Task<List<Event>> GetAllEventsWithStudent();
        Task SaveEvent(List<Event> events);
        Task SaveEvent(Event @event);
        Task DeleteEvent();
        Task<List<Event>> GetEventsByMounth(int mount);
        Task<List<Event>> GetAllEvents();
        Task<List<EventOfStudent>> GetAllEventsByDate(string dat1, string dat2, User user);
    }
}
