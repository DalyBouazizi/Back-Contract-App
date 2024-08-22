using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;

namespace Projet_Stage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {

        private readonly IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpPost("AddAlert")]
        public async Task<IActionResult> CreateAlert([FromBody] AlertModel alertModel)
        {
            var res = await _alertService.CreateAlertAsync(alertModel);
            if (!res)
            {
                return BadRequest("Contract with id "+ alertModel.ContractId +" Not found");
            }
            else
            {
                return Ok("Alert added successfully!");
            }
            
        }

        [HttpDelete("DeleteAlert")]
        public async Task<IActionResult> DeleteAlert(int alertId)
        {
            var res = await _alertService.DeleteAlertAsync(alertId);
            if (!res)
            {
                return BadRequest("Something wrong");
            }
            else
            {
                return Ok("Alert deleted successfully!");
            }
           
        }

        [HttpGet("GetAlertByID")]
        public async Task<ActionResult<AlertModel>> GetAlertById(int alertId)
        {
            var alert = await _alertService.GetAlertByIdAsync(alertId);
            if (alert == null) return NotFound();
            return Ok(alert);
        }

        [HttpGet("GetAlertByContractId")]
        public async Task<ActionResult<IEnumerable<AlertModel>>> GetAlertsByContractId(int contractId)
        {
            var alerts = await _alertService.GetAlertsByContractIdAsync(contractId);
            return Ok(alerts);
        }

        [HttpGet("GetAllAlerts")]
        public async Task<ActionResult<IEnumerable<AlertModel>>> GetAllAlerts()
        {
            var alerts = await _alertService.GetAllAlertsAsync();
            return Ok(alerts);
        }

        [HttpDelete("DeleteAlertsByContractId")]
        public async Task<IActionResult> DeleteAlertsByContractId(int contractId)
        {
            var result = await _alertService.DeleteAlertsByContractId(contractId);

            if (result)
            {
                return Ok( "Alerts for contract ID"+ contractId+" deleted successfully." );
            }
            else
            {
                return NotFound("No alerts found for contract ID "+contractId);
            }
        }
    }
}
