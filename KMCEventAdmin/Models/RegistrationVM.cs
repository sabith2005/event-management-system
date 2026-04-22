namespace KMCEventAdmin.Models
{
    public class RegistrationVM
    {
        public int RegistrationId { get; set; }
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public DateTime RegisteredAt { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public string? EventTitle { get; set; }
    }
}