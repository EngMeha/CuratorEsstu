using BusinessLayer.Interface;
using DataLayer;
using DataLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class EFEvent : IEvent
    {
        DiplomContext _context;
        public EFEvent(DiplomContext context) 
        {
            _context = context;  
        }
        public async Task SaveEvent(List<Event> events)
        {
            await _context.Event.AddRangeAsync(events);
            await _context.SaveChangesAsync();
        }
        public async Task SaveEvent(Event @event)
        {
            await _context.Event.AddAsync(@event);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvent()
        {
            foreach (Event @event in await _context.Event.Include(x=>x.EventOfStudents).ToListAsync())
            {
                if (@event.EventOfStudents.Count == 0)
                {
                    _context.Event.Remove(@event);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetAllEventsWithOutStudent()
        {
            return await _context.Event.Include(x => x.EventOfStudents).Where(x => x.EventOfStudents.Count == 0).ToListAsync();
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _context.Event.ToListAsync();
        }

        public async Task<Event> GetEvent(int id, bool include = false)
        {
            if (include)
            {
                return await _context.Event.Include(x => x.EventOfStudents).FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                return await _context.Event.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<List<Event>> GetAllEventsWithStudent()
        {
            return await _context.Event.Include(x => x.EventOfStudents).Where(x => x.EventOfStudents.Count != 0).ToListAsync();
        }
        public async Task<List<Event>> GetEventsByMounth(int mount)
        {
            return await _context.Event.Include(x => x.EventOfStudents).Where(x => x.EventOfStudents.Count != 0 && x.DateTimeEvent.Month == mount && x.DateTimeEvent.Year == x.DateTimeEvent.Year).ToListAsync();
        }

        public async Task<List<EventOfStudent>> GetAllEventsByDate(string dat1, string dat2, User user)
        {
                //.Select(x => new { x.Event.Title, x.Event.DateTimeEvent, GroupTitle = x.Student.Group.Title }).Distinct();
            return await _context.EventOfStudent.Include(x => x.Event).Include(x => x.Student).Include(x => x.Student.Group).Where(x => x.Student.Group.GroupsOfTeacher.Any(x=>x.User.Id == user.Id) && x.Event.DateTimeEvent < DateTime.Parse(dat2) && x.Event.DateTimeEvent > DateTime.Parse(dat1)).ToListAsync();
        }
    }
}
