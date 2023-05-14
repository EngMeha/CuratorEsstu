using DataLayer.Entity;

namespace PresentationLayer.Models
{
    public class CreateEventModel
    {
        public List<GroupsOfTeacher> GroupsOfTeacher { get; set; }
        public int IdEvent { get; set; }
        public string Title { get; set; }
        public DateTime DateEvent { get; set; }
    }
}
