using KMCEventAPI.Model;

namespace KMCEventAPI.Data
{
    public class EventRepo
    {
        private readonly AppDBContext db;

        public EventRepo(AppDBContext _db)
        {
            db = _db;
        }

        public bool Create(Event model)
        {
            db.Events.Add(model);
            return db.SaveChanges() > 0;
        }

        public List<Event> GetAll()
        {
            return db.Events.ToList();
        }

        public Event? GetById(int id)
        {
            return db.Events.FirstOrDefault(x => x.EventId == id);
        }

        public bool Update(Event model)
        {
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ev = db.Events.FirstOrDefault(x => x.EventId == id);
            if (ev == null)
                return false;

            db.Events.Remove(ev);
            return db.SaveChanges() > 0;
        }

        public bool Remove(Event model)
        {
            db.Events.Remove(model);
            return db.SaveChanges() > 0;
        }

        public List<Event> Search(string? type, DateTime? date, string? venue)
        {
            var query = db.Events.AsQueryable();

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(x => x.EventType != null && x.EventType.Contains(type));
            }

            if (!string.IsNullOrWhiteSpace(venue))
            {
                query = query.Where(x => x.Venue != null && x.Venue.Contains(venue));
            }

            if (date.HasValue)
            {
                query = query.Where(x => x.EventDate.Date == date.Value.Date);
            }

            return query.ToList();
        }

        public List<Event> GetByOrganizer(int organizerId)
        {
            return db.Events.Where(x => x.OrganizerId == organizerId).ToList();
        }
    }
}