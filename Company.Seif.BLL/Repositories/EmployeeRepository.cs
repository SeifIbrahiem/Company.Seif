﻿using Company.Seif.BLL.Interfaces;
using Company.Seif.DAL.Data.Contexts;
using Company.Seif.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Seif.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task  <List<Employee>> GetByNameAsync(string name)
        {
          return await _context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
