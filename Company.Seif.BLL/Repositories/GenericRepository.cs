﻿using Company.Seif.BLL.Interfaces;
using Company.Seif.DAL.Data.Contexts;
using Company.Seif.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Seif.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext context) 
        {
            this._context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T? Get(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public int Add(T model)
        {
            _context.Set<T>().Add(model);
               return _context.SaveChanges();
        }

        public int Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }
        public int Update(T model)
        {
            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }
    }
}
