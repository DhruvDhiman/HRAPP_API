using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Entities;

namespace App.Models
{
    public class EmployeeForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string EmailId { get; set; }
    }
}
