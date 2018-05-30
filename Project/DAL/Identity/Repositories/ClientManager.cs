using DAL.Identity.Interfaces;
using DAL.EF;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public WorkPlaceContext Database { get; set; }
        public ClientManager(WorkPlaceContext db)
        {
            Database = db;
        }

        public void Create(User item)
        {
            Database.UsersProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}