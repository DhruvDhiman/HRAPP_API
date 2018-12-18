using App.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Entities;
using App.Helpers;

namespace App.Controllers
{
    [EnableCors("AllowCors"), Route("api/employees/paycheks")]
    public class PayCheckController:Controller
    {
        private IAppRepository _appRepository;

        public PayCheckController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpPost("{empId}")]
        public IActionResult CreatePayCheck(Guid empId)
        {
            var employeeFromRepo = _appRepository.GetEmployee(empId);

            if( employeeFromRepo == null)
            {
                return NotFound();
            }

            var payCheckEntity = PayCalculator.GeneratePayCheck(employeeFromRepo);
            _appRepository.AddPayCheck(payCheckEntity);

            if (!_appRepository.Save())
            {
                throw new Exception($"Creation of pay check for employee {empId} failed on save.");
            }

            var payCheckToReturn = Mapper.Map<PayCheckDto>(payCheckEntity);
            return CreatedAtRoute("GetPayCheck", new { empId = employeeFromRepo.EmpId, txnId = payCheckEntity.TxnId }, payCheckToReturn);
        }

        [HttpGet("{empId}/{txnId}", Name ="GetPayCheck")]
        public IActionResult GetPayCheck(Guid empId, Guid txnId)
        {
            if (!_appRepository.EmployeeExists(empId))
            {
                return NotFound();
            }

            var payCheckFromRepo = _appRepository.GetPayCheck(txnId);

            var payCheckToReturn = Mapper.Map<PayCheckDto>(payCheckFromRepo);

            return Ok(payCheckToReturn);
        }

        [HttpGet()]
        public IActionResult GetPayChecks()
        {
            var payChecksFromRepo = _appRepository.GetPayCheck();

            var payChecksToReturn = Mapper.Map<IEnumerable<PayCheckDto>>(payChecksFromRepo);

            return Ok(payChecksToReturn);
        }

    }
}
