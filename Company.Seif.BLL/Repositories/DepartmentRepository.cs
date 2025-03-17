using Company.Seif.BLL.Interfaces;
using Company.Seif.DAL.Data.Contexts;
using Company.Seif.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Company.Seif.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context; //null
        
        //ASK CLR create object from companyDbcontext
        public DepartmentRepository(CompanyDbContext context) 
        {
            _context = context;
        }
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department? Get(int id)
        {
            return _context.Departments.Find(id);
        }
        public int Add(Department model)
        {
            _context.Departments.Add(model);
            return _context.SaveChanges();
        }

        public int Delete(Department model)
        {
            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }

        public int Update(Department model)
        {
            _context.Departments.Update(model);
            return _context.SaveChanges();
        }
    }
}
