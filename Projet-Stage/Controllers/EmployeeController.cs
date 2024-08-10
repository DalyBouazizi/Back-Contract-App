using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Data.ModelsEF;
using Projet_Stage.Models;
using Projet_Stage.Services.Classes;
using Projet_Stage.Services.Interfaces;

namespace Projet_Stage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeModel employee)
        {
            bool res = false;

            res = await _employeeService.AddEmployeeAsync(employee);
            if (res)
            {
                return Ok("employee added");
            }
            else
            {
                return BadRequest("Matricule exists already");
            }
        }
    }
}
