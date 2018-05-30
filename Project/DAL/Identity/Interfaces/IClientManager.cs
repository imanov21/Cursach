using System;
using DAL.Entities;

namespace DAL.Identity.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(User item);
    }
}
