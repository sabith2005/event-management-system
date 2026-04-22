using KMCEventAPI.Model;

namespace KMCEventAPI.Data
{
    public class OrganizerRepo
    {
        private readonly AppDBContext db;

        public OrganizerRepo(AppDBContext appDB)
        {
            db = appDB;
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }

        public bool Create(Organizer organizer)
        {
            if (organizer == null) return false;
            db.Organizers.Add(organizer);
            return Save();
        }

        public List<Organizer> GetAll()
        {
            return db.Organizers.ToList();
        }

        public Organizer? GetById(int id)
        {
            return db.Organizers.Find(id);
        }
    }
}