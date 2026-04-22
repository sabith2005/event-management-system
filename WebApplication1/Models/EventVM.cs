namespace KMCEventWeb.Models
{
    public class EventVM
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public int OrganizerId { get; set; }
    }
}