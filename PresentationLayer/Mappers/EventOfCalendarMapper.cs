using DataLayer.Entity;
using PresentationLayer.Models;

namespace PresentationLayer.Mappers
{
    public static class EventOfCalendarMapper
    {
        public static EventOfCalendarModel FromDbToModel(List<Event> events)
        {
            return new () { Events = events };
        }
    }
}
