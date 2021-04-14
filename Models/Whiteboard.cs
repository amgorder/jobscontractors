namespace jobscontractors.Models
{
    public class Whiteboard
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public bool Public { get; set; }
        public Profile Creator { get; set; }
    }

    public class WhiteboardStickynoteViewModel : Whiteboard
    {
        public int StickynoteId { get; set; }
    }
}