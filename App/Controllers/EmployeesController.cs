using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using App.Models;
using App.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace App.Controllers
{
    [Route("api/employees")]
    public class EmployeesController:Controller
    {
        private IAppRepository _appRepository;

        public EmployeesController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet()]
        public IActionResult GetEmployees()
        {
            var employeesFromRepo = _appRepository.GetEmployees();

            var employeesToReturn = Mapper.Map<IEnumerable<EmployeeDto>>(employeesFromRepo);

            return Ok(employeesToReturn);
        }

        [HttpGet("{empId}", Name = "GetEmployee")] 
        public IActionResult GetEmployee(Guid empId)
        {
            var employeeFromRepo = _appRepository.GetEmployee(empId);

            if(employeeFromRepo == null)
            {
                return NotFound();
            }

            var employeeToReturn = Mapper.Map<EmployeeDto>(employeeFromRepo);

            return Ok(employeeToReturn);
        }

        [HttpPost()]
        public IActionResult CreateEmployee([FromBody]EmployeeForCreationDto employee)
        {
            if (employee == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }
            var employeeEntity = Mapper.Map<Employee>(employee);

            _appRepository.AddEmployee(employeeEntity);

            if (!_appRepository.Save())
            {
                throw new Exception("Employee creation failed on save.");
            }

            var employeeToReturn = Mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployee", new { empId = employeeToReturn.EmpId }, employeeToReturn);
        }

        //method to return correct response codes
        [HttpPost("{EmpId}")]
        public IActionResult BlockEmployeeCreation(Guid EmpId)
        {
            if (_appRepository.EmployeeExists(EmpId))
            {
                return Conflict();
            }
            return NotFound();
        }

        [HttpDelete("{EmpId}")]
        public IActionResult DeleteEmployee(Guid EmpId)
        {
            var employeeFromRepo = _appRepository.GetEmployee(EmpId);

            if(employeeFromRepo == null)
            {
                return NotFound();
            }

            _appRepository.DeleteEmployee(employeeFromRepo);

            if (!_appRepository.Save())
            {
                throw new Exception($"Deletion of employee with id { EmpId} failed on save.");
            }

            return NoContent();
        }

        [HttpPut("{EmpId}")]
        public IActionResult UpdateEmployee(Guid EmpId, [FromBody] EmployeeForUpdateDto employee)
        {
            var employeeFromRepo = _appRepository.GetEmployee(EmpId);
            if(employeeFromRepo == null)
            {
                //Not found if you are stoppping upserting
                //else create and entity, save and return 201 createatroute
                //return NotFound();
                var employeeEntity = Mapper.Map<Employee>(employee);
                employeeEntity.EmpId = EmpId;

                if (!_appRepository.Save())
                {
                    throw new Exception($"Upserting of Employee with id {EmpId} failed on save.");
                }

                var employeeToReturn = Mapper.Map<EmployeeDto>(employeeEntity);

                return CreatedAtRoute("GetEmployee", new { empId = employeeToReturn.EmpId }, employeeToReturn);
            }

            Mapper.Map(employee, employeeFromRepo);

            _appRepository.UpdateEmployee(employeeFromRepo);

            if (!_appRepository.Save())
            {
                throw new Exception($"Updation for Employee with id {EmpId} failed on save.");
            }

            return NoContent();
        }

        [HttpPatch("{EmpId}")]
        public IActionResult PartiallyUpdateEmployee(Guid EmpId, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> pathDoc)
        {
            if(pathDoc == null)
            {
                return BadRequest();
            }

            var employeeFromRepo = _appRepository.GetEmployee(EmpId);

            if(employeeFromRepo == null)
            {
                return NotFound($"Employee with id {EmpId} not found.");
            }

            //get the same updatedto object
            var employeeToPath = Mapper.Map<EmployeeForUpdateDto>(employeeFromRepo);

            //apply patch to updatedto
            pathDoc.ApplyTo(employeeToPath);

            //apply changes to entity, update and save
            Mapper.Map(employeeToPath, employeeFromRepo);

            _appRepository.UpdateEmployee(employeeFromRepo);

            if (!_appRepository.Save())
            {
                throw new Exception($"Updation for employee with id {EmpId} failed on save.");
            }

            return NoContent();
        }
    }
}
