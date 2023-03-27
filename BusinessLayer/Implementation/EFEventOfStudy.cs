using BusinessLayer.Interface;
using DataLayer;
using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class EFEventOfStudy: IEventOfStudent
    {
        DiplomContext _context;
        public EFEventOfStudy(DiplomContext context) 
        { 
            _context= context;
        }

        public async Task CreateEventOfStudent(Event @event, List<Student> student)
        {
            if (@event.Id == 0)
            {
                foreach (var item in student)
                {
                    EventOfStudent eventOfStudent = new EventOfStudent();
                    eventOfStudent.Event = @event;
                    eventOfStudent.Student = item;
                    _context.EventOfStudent.Add(eventOfStudent);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteEventOfStudent(Event @event)
        {
            List<EventOfStudent> eventOfStudents= _context.EventOfStudent.Where(x=>x.Event ==@event).ToList();
            foreach (var item in eventOfStudents)
            {
                _context.EventOfStudent.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
