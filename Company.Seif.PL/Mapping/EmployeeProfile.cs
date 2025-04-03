using AutoMapper;
using Company.Seif.DAL.Models;
using Company.Seif.PL.DTOS;

namespace Company.Seif.PL.Mapping
{
    public class EmployeeProfile :Profile
    {
        //CLR
        public EmployeeProfile() 
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<Employee, CreateEmployeeDto>();
        }
    }
}
