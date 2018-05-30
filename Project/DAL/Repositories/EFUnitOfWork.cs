using System;
using System.Data.Entity;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private WorkPlaceContext _database;
        private IRepository<Vacancy> _vacancyRepository;
        private IRepository<Offer> _offerRepository;
        private IRepository<Resume> _resumeRepository;
        private IRepository<Experience> _experienceRepository;
        private IRepository<Heading> _headingsRepository;
        private IUserRepository _userRepository;

        public EfUnitOfWork(string connectionString, IRepository<Vacancy> vacancies)
        {
            Vacancies = vacancies;
            _database = new WorkPlaceContext(connectionString);
        }

        public EfUnitOfWork(WorkPlaceContext context, IRepository<Vacancy> vacancies)
        {
            _database = context;
            Vacancies = vacancies;
        }

        public IRepository<Vacancy> Valancies
        {
            get
            {
                if (_vacancyRepository == null)
                    _vacancyRepository = new GRepository<Vacancy>(_database);

                return _vacancyRepository;
            }
        }

        public IRepository<Vacancy> Vacancies { get; }

        public IRepository<Offer> Offers
        {
            get
            {
                if (_offerRepository == null)
                    _offerRepository = new GRepository<Offer>(_database);
                return _offerRepository;
            }
        }

        public IRepository<Resume> Resumes
        {
            get
            {
                if (_resumeRepository == null)
                    _resumeRepository = new GRepository<Resume>(_database);
                return _resumeRepository;
            }
        }

        public IRepository<Experience> Experiences
        {
            get
            {
                if (_experienceRepository == null)
                    _experienceRepository = new GRepository<Experience>(_database);
                return _experienceRepository;
            }
        }

        public IRepository<Heading> Headings
        {
            get
            {
                if (_headingsRepository == null)
                    _headingsRepository = new GRepository<Heading>(_database);
                return _headingsRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_database);
                return _userRepository;
            }
        }

        public void Save()
        {
            _database.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _database.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
