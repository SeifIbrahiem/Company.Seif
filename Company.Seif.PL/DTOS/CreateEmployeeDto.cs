﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Seif.PL.DTOS
{
      public class CreateEmployeeDto
        {
            [Required(ErrorMessage = "Name is Required !!")]
            public string Name { get; set; }
            [Range(22, 60, ErrorMessage = "Age must be between 22 and 60")]
            public int? Age { get; set; }
            [DataType(DataType.EmailAddress, ErrorMessage = "Email is NOTVALID !!")]
            public string Email { get; set; }
            [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
                                 ErrorMessage = " Adress must be like 123-street-city-country")]
            public string Adress { get; set; }
            [Phone]
            public string Phone { get; set; }
            [DataType(DataType.Currency)]
            public decimal Salary { get; set; }
            public bool IsActive { get; set; }
            public bool IsDeleted { get; set; }
            [DisplayName("Hiring Date")]
            public DateTime HiringDate { get; set; }
            [DisplayName("Data of create")]
            public DateTime CreateAt { get; set; }
        [DisplayName("Department")]

         public int? DepartmentId { get; set; }
         public int? Id { get; set; }
        public string? DepartmentName { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImageName { get; set; }

    }
    }

