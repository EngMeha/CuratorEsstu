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
        public async Task SaveEvent(Event @event)
        {
            if (@event.Id == 0)
            {
                _context.Event.Add(@event);
            }
            else
            {
                _context.Entry(@event).State = EntityState.Modified;
                _context.Entry(@event).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvent(Event @event)
        {
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetAllEvents(bool include = false)
        {
            if (include)
            {
                return await _context.Event.Include(x => x.EventOfStudents).ToListAsync();
            }
            else
            {
                return await _context.Event.ToListAsync();
            }
        }

        public async Task<Event> GetEvent(int id, bool include = false)
        {
            if (include)
            {
                return await _context.Event.Include(x => x.EventOfStudents).FirstOrDefaultAsync(x=>x.Id == id);
            }
            else
            {
                return await _context.Event.FirstOrDefaultAsync(x=>x.Id==id);
            }
        }
    }
}
