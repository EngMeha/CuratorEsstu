using DataLayer.Entity;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Mappers
{
    public static class DescriptionEventMapper
    {
        public static DescriptionEventModal FromDbToModal(Event @event)
        {
            return new DescriptionEventModal()
            {
                Url = @event.Url,
                Description = @event.Comment,
                Id = @event.Id
            };
        }
        public static DescriptionEventFromCalendar FromDbToModal(List<string> titleGroups, Event @event)
        {
            return new DescriptionEventFromCalendar()
            {
                Event = @event,
                TitleGroups = titleGroups
            };
        }
    }
}
