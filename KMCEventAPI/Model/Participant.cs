using System.ComponentModel.DataAnnotations;

namespace KMCEventAPI.Model
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public List<Registration> Registrations { get; set; } = new List<Registration>();
    }
}