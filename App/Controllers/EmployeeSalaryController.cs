using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;

namespace App.Controllers
{
    [Route("api/employee/{EmpId}/salary")]
    public class EmployeeSalaryController:Controller
    {
        private IAppRepository _appRepository;
        public EmployeeSalaryController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
    }
}
