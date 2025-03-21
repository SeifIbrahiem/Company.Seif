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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) 
        {
            _context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee? Get(int id)
        {
            return _context.Employees.Find(id);
        }
        public int Update(Employee model)
        {
            _context.Employees.Update(model);
            return _context.SaveChanges();
        }
        public int Add(Employee model)
        {
            _context.Employees.Add(model);
            return _context.SaveChanges();
        }
        public int Delete(Employee model)
        {
            _context.Employees.Remove(model);
            return _context.SaveChanges();
        }
    }
}
