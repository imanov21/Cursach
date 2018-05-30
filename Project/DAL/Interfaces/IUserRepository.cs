using System;
using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(string id);
        IEnumerable<User> Find(Func<User, bool> predicate);

        void Create(User item);
        void Delete(string id);
        
    }
}   
