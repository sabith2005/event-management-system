using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMCEventAPI.Model
{
    public class Registration
    {
        [Key]
        public int RegistrationId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event? Event { get; set; }

        [ForeignKey("Participant")]
        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }
}