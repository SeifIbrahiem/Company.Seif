using Company.Seif.BLL.Interfaces;
using Company.Seif.DAL.Data.Contexts;
using Company.Seif.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Company.Seif.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Department> GetByName(string name)
        {
            return _context.Departments.Include(E => E.Name).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
