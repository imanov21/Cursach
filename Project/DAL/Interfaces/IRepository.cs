﻿using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        void Add(TEntity item);
        void Delete(int id);
        
    }
}