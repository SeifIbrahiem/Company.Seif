using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Seif.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get;}

        IEmployeeRepository   EmployeeRepository   { get;}
    }
}
