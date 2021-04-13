namespace jobscontractors.Models
{
    public class Contractor
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }
}