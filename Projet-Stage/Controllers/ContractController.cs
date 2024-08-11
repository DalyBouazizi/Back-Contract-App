using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;

namespace Projet_Stage.Controllers
{
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
    }
}
