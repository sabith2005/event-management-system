using System.ComponentModel.DataAnnotations;

namespace KMCEventAPI.Model
{
    public class Organizer
    {
        [Key]
        public int OrganizerId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        // simple owner identifier for update protection demo
        [Required]
        public string Password { get; set; } = string.Empty;

        public List<Event> Events { get; set; } = new List<Event>();
    }
}