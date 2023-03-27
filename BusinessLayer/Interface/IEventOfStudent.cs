using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IEventOfStudent
    {
        Task CreateEventOfStudent(Event @event, List<Student> student);
        Task DeleteEventOfStudent(Event @event);
    }
}
