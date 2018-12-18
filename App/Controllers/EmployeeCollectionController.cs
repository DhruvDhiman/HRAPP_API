using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Services;
using App.Models;
using AutoMapper;
using App.Entities;
using App.Helpers;
using Microsoft.AspNetCore.Cors;

namespace App.Controllers
{
    [EnableCors("AllowCors"), Route("api/employeecollections")]
    public class EmployeeCollectionController:Controller
    {
        private IAppRepository _appRepository;
        public EmployeeCollectionController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpPost()]
        public IActionResult CreateEmployeeCollections([FromBody]IEnumerable<EmployeeForCreationDto> employeeCollection)
        {
            if (employeeCollection == null)
                return BadRequest();

            var employeeEntities = Mapper.Map<IEnumerable<Employee>>(employeeCollection);

            foreach(var employee in employeeEntities)
            {
                _appRepository.AddEmployee(employee);
            }

            if (!_appRepository.Save())
            {
                throw new Exception($"Creation of employees failed on save.");
            }

            //return dto
            var employeesToReturn = Mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);

            var empIds = string.Join(",", employeesToReturn.Select(e => e.EmpId));

            return CreatedAtRoute("GetEmployeeCollection", new { empIds = empIds }, employeesToReturn);
        }

        [HttpGet("empIds", Name = "GetEmployeeCollection")]
        public IActionResult GetEmployeeCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> empIds)
        {
            if (empIds == null)
                return BadRequest();

            var employeeEntities = _appRepository.GetEmployees(empIds);

            var employeeToReturn = Mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);

            return Ok(employeeToReturn);
        }
    }
}
