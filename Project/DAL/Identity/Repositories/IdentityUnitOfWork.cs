using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Identity.Entities;
using DAL.Identity.Interfaces;
using DAL.Repositories;

namespace DAL.Identity.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWorkIdentity
    {
        private WorkPlaceContext db;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IClientManager _clientManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new WorkPlaceContext(connectionString);
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            _clientManager = new ClientManager(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        public IClientManager ClientManager
        {
            get { return _clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _clientManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}