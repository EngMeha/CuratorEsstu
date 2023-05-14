using DataLayer.Entity;

namespace PresentationLayer.Models
{
    public class DescriptionEventFromCalendar
    {
        public List<string> TitleGroups { get; set; }
        public Event Event { get; set; }
    }
}
