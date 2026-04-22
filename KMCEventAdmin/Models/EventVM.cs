namespace KMCEventAdmin.Models
{
    public class EventVM
    {
        public int eventId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? EventType { get; set; }
        public string? Venue { get; set; }
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public int OrganizerId { get; set; }
    }
}