﻿using App.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class EmployeeForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public string Location { get; set; }
        [EmailAddress]
        public string EmailId { get; set; }
        public EmployeeSalary EmployeeSalary { get; set; }
    }
}
