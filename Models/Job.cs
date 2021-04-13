using System.ComponentModel.DataAnnotations;

namespace jobscontractors.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }
}