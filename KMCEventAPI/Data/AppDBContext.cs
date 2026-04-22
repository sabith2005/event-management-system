using Microsoft.EntityFrameworkCore;
using KMCEventAPI.Model;

namespace KMCEventAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organizer>();
            modelBuilder.Entity<Participant>();

            modelBuilder.Entity<Event>()
                .Property(e => e.Title)
                .HasMaxLength(150);

            modelBuilder.Entity<Event>()
                .Property(e => e.EventType)
                .HasMaxLength(100);

            modelBuilder.Entity<Event>()
                .Property(e => e.Venue)
                .HasMaxLength(150);

            modelBuilder.Entity<Registration>()
                .HasIndex(r => new { r.EventId, r.ParticipantId })
                .IsUnique();
        }
    }
}