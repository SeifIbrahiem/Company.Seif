﻿using Company.Seif.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Seif.BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        // IEnumerable<Department> GetAll();

        //Department? Get(int id);

        //int Add(Department model);

        //int Update(Department model);

        //int Delete(Department model);

        // Task<List<Department>> GetByIdAsync(int id);

       // Task<IEnumerable<Department>> GetAllAsync();
    }
}
