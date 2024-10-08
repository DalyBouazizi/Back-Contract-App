﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Data.Features;
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
        [Route("GetAllLatestContracts")]
        [HttpGet]
        public async Task<ActionResult<List<ContractGetModel>>> GetAllLatestContractsAsync()
        {
            List<ContractGetModel> contracts = new List<ContractGetModel>();
            try
            {
                contracts = await _contractService.GetLatestContractsAsync();
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
        [HttpGet("GetLatestByEmpId")]
        public async Task<ActionResult<ContractGetModel>> GetLatestByEmpId(int EmployeeID)
        {
            var Contract = await _contractService.GetLatestContractByEmployeeIdAsync(EmployeeID);
            if (Contract == null )
            {
                return NotFound("No Contract found with the specified ID.");
            }
            return Ok(Contract);
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

        [HttpPost("RenewContract")]
        public async Task<IActionResult> RenewContract([FromBody] ContractRenewalModel renewalModel)
        {
            try
            {
                await _contractService.RenewContractAsync(renewalModel.EmployeeId, renewalModel.NewContract);
                return Ok("Contract renewed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("GetContractById")]
        public async Task<ActionResult<IEnumerable<ContractGetModel>>> GetContractById(int idContract)
        {
            var Contracts = await _contractService.GetContractByIdAsync(idContract);
            if (Contracts == null )
            {
                return NotFound("No Contracts found with the specified id.");
            }
            else
            {
                return Ok(Contracts);


            }
        }


        [HttpDelete("DeleteAllContractsByEmployeeId")]
        public async Task<IActionResult> DeleteAllContractsByEmployeeId(int employeeId)
        {
            bool res = await _contractService.DeleteAllContractsByEmployeeIdAsync(employeeId);
            if (res)
            {
                return Ok($"All contracts for employee ID {employeeId} have been deleted.");
            }
            else
            {
                return BadRequest($"No contracts found for employee ID {employeeId}.");
            }
        }


        [HttpGet("filter")]
        public async Task<IActionResult> GetContractsByFilters([FromQuery] ContractFilterCriteria criteria)
        {
            if (criteria == null)
            {
                return BadRequest("Invalid filter criteria.");
            }

            var contracts = await _contractService.GetContractsByFiltersAsync(criteria);

            if (contracts == null || !contracts.Any())
            {
                return NotFound("No contracts found matching the criteria.");
            }

            return Ok(contracts);
        }




        // --------------------------------------- 


        [HttpGet]
        [Route("GetContractsEndingInOneMonth")]
        public async Task<ActionResult<List<ContractGetModel>>> GetContractsEndingInOneMonth()
        {

            List<ContractGetModel> contracts = new List<ContractGetModel>();
            try
            {
                contracts = await _contractService.GetContractsEndingInOneMonthAsync();
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
