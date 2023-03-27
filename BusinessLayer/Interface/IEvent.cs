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
        Task<List<Event>> GetAllEvents(bool include = false);
        Task SaveEvent(Event @event);
        Task DeleteEvent(Event @event);

    }
}
