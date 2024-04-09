using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVCDemo.Models;
using WebMVCDemo.Services;

namespace WebMVCDemo.Controllers
{
    [ApiController]
    // [Route("api/[controller]")]
    [Route("api/employees")]
    public class WebApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;


        public WebApiController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        public List<Employee> GetEmployees()
        {
            return _employeeService.GetEmployees();
        }
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployees(int employeeId)
        {

            var employee = _employeeService.GetEmployee(employeeId);
            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{employeeId}/supervisor/{suerpvisorId}")]
        public IActionResult SetSuperVisor(int employeeId, int suerpvisorId)
        {
            Employee employee = _employeeService.SetSupervisor(employeeId, suerpvisorId);
            return Ok(employee);
        }


    }
}