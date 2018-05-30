using System;
using DAL.Entities;

namespace DAL.Interfaces
{
        public interface IUnitOfWork : IDisposable
        {
            IRepository<Vacancy> Vacancies { get; }
            IRepository<Offer> Offers { get; }
            IRepository<Resume> Resumes { get; }
            IRepository<Experience> Experiences { get; }
            IRepository<Heading> Headings { get; }
            IUserRepository Users { get; }
            void Save();
    }
    }
