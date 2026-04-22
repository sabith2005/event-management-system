using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMCEventAPI.Model
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string EventType { get; set; } = string.Empty;

        [Required]
        public string Venue { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("Organizer")]
        public int OrganizerId { get; set; }
        public Organizer? Organizer { get; set; }

        public List<Registration> Registrations { get; set; } = new List<Registration>();
    }
}