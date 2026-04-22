using Microsoft.EntityFrameworkCore;
using KMCEventAPI.Model;

namespace KMCEventAPI.Data
{
    public class RegistrationRepo
    {
        private readonly AppDBContext db;

        public RegistrationRepo(AppDBContext appDB)
        {
            db = appDB;
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }

        public Participant? FindParticipantByEmail(string email)
        {
            return db.Participants.FirstOrDefault(p => p.Email == email);
        }

        public Event? FindEvent(int eventId)
        {
            return db.Events.Include(e => e.Registrations)
                            .FirstOrDefault(e => e.EventId == eventId);
        }

        public bool AlreadyRegistered(int eventId, int participantId)
        {
            return db.Registrations.Any(r => r.EventId == eventId && r.ParticipantId == participantId);
        }

        public Registration? Register(int eventId, Participant participant)
        {
            var ev = FindEvent(eventId);
            if (ev == null || !ev.IsActive) return null;

            if (ev.Capacity > 0 && ev.Registrations.Count >= ev.Capacity)
                return null;

            var existingParticipant = FindParticipantByEmail(participant.Email);
            if (existingParticipant == null)
            {
                db.Participants.Add(participant);
                db.SaveChanges();
                existingParticipant = participant;
            }

            if (AlreadyRegistered(eventId, existingParticipant.ParticipantId))
                return null;

            var registration = new Registration
            {
                EventId = eventId,
                ParticipantId = existingParticipant.ParticipantId,
                RegisteredAt = DateTime.Now
            };

            db.Registrations.Add(registration);
            db.SaveChanges();
            return registration;
        }

        public List<Registration> GetAll()
        {
            return db.Registrations
                     .Include(r => r.Event)
                     .Include(r => r.Participant)
                     .ToList();
        }
    }
}