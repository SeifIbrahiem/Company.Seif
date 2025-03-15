using Company.Seif.BLL.Interfaces;
using Company.Seif.DAL.Data.Contexts;
using Company.Seif.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Seif.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context;
        public DepartmentRepository() 
        { 
           _context = new CompanyDbContext();
        }

        int IDepartmentRepository.Add(Department model)
        {
           
            _context.Departments.Add(model);
            return _context.SaveChanges();
        }

        int IDepartmentRepository.Delete(Department model)
        {
            
            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }

        Department? IDepartmentRepository.Get(int id)
        {

            return _context.Departments.Find(id);
        }

        IEnumerable<Department> IDepartmentRepository.GetAll()
        {
            return _context.Departments.ToList();
        }

        int IDepartmentRepository.Update(Department model)
        {
            _context.Departments.Update(model);
            return _context.SaveChanges();
        }
    }
}
