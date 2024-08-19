using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Stage.Models;
using Projet_Stage.Services.Classes;
using Projet_Stage.Services.Interfaces;

namespace Projet_Stage.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;
        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }
        [HttpPost]
        [Route("AddContract")]
        public async Task<IActionResult> AddContract([FromBody] ContractModel contract)
        {
            bool res = false;

            res = await _contractService.AddContractAsync(contract);
            if (res)
            {
                return Ok("contract added");
            }
            else
            {
                return BadRequest("Employee with id "+contract.EmployeeId+" not found in database");
            }
        }
        [Route("GetAllContracts")]
        [HttpGet]
        public async Task<ActionResult<List<ContractGetModel>>> GetAllContractsAsync()
        {
            List<ContractGetModel> contracts = new List<ContractGetModel>();
            try
            {
                contracts = await _contractService.GetAllContractsAsync();
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteContract")]
        public async Task<IActionResult> DeleteContract(int IdContract)
        {
            bool res = false;
            res = await _contractService.DeleteContractAsync(IdContract);
            if (res)
            {
                return Ok("Contract deleted");
            }
            else
            {
                return BadRequest("Contract with id " + IdContract + " not found in database");
            }
        }
        [HttpPut("UpdateContract")]
        public async Task<IActionResult> UpdateContract([FromBody] ContractGetModel contract)
        {
            bool res = false;
            res = await _contractService.UpdateContractAsync(contract);
            if (res)
            {
                return Ok("Contract updated");
            }
            else
            {
                return BadRequest("Contract with id " + contract.id + " not found in database \n or Employee with id "+contract.EmployeeId+" not found ");
            }
        }
        [HttpGet("GetContractByType")]
        public async Task<ActionResult<IEnumerable<ContractGetModel>>> GetEmployeesByPoste(string type)
        {
            var Contracts = await _contractService.GetContractByType(type);
            if (Contracts == null || !Contracts.Any())
            {
                return NotFound("No Contracts found with the specified type.");
            }
            return Ok(Contracts);
        }
        [HttpGet("GetContractsByDateRange")]
        public async Task<ActionResult<List<ContractModel>>> GetContractsByDateRange(
       [FromQuery] DateTime startDate,
       [FromQuery] DateTime endDate)
        {
            try
            {
                var contracts = await _contractService.GetContractsByDateRangeAsync(startDate, endDate);
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("GetContractByEmployeeId")]
        public async Task<ActionResult<List<ContractModel>>> GetContractByEmployeeId(int EmployeeId)
        {
            try { 
            
                var contracts = await _contractService.GetContractByEmployeeIdAsync(EmployeeId);
            if(contracts == null || !contracts.Any())
            {
                return NotFound("No contracts found for the specified employee.");
            }
            else
            {
                return Ok(contracts);
            }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
