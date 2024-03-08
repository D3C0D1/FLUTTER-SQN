using Microsoft.AspNetCore.Mvc;
using SQNBack.Models.DTO;
using SQNBack.Services;
using SQNBack.Services.Implement;
using SQNBack.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SQNBack.Controllers
{
    [Route($"{GeneralValidatons.ROUTE}[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IStatusService _service = new StatusService();

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        // GET: SQN/rest/<StatusController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> GetAllStatus()
        {
            _logger.LogInformation("Getting all Status in the system");
            return new JsonResult(await _service.GetAll());
        }

        // GET SQN/rest/<StatusController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetStatusById(string id)
        {
            _logger.LogInformation($"Getting the Status {id} in the system");
            return new JsonResult(await _service.GetById(id));
        }

        // GET SQN/rest/<StatusController>/list/true
        [HttpGet("list/{valid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> ValidateStatusById(bool valid)
        {
            _logger.LogInformation($"Getting the Statuses list with valid in: {valid}");
            return new JsonResult(await _service.ListByValid(valid));
        }

        // GET SQN/rest/<StatusController>/valid/xyz
        [HttpGet("valid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> ValidateStatusById(string id)
        {
            _logger.LogInformation($"Validating the Status with id: {id}");
            return new JsonResult(await _service.IsValid(id));
        }

        // GET SQN/rest/<StatusController>/verify/xyz
        [HttpGet("verify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> IsForCreation()
        {
            _logger.LogInformation("Verifying in the system if exist some Status for creation");
            return new JsonResult(await _service.IsForCreation());
        }

        // GET SQN/rest/<StatusController>/verify/xyz
        [HttpGet("verify/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> IsForCreation(string id)
        {
            _logger.LogInformation($"Verifying in the system if Status with id {id} is for creation");
            return new JsonResult(await _service.IsForCreation(id));
        }

        // POST SQN/rest/<StatusController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> InsertStatus([FromBody] StatusDTO statusDto)
        {
            _logger.LogInformation("Inserting a new Area in the system");
            return new JsonResult(await _service.Insert(statusDto, "wacor"));
        }

        // PUT SQN/rest/<StatusController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> UpdateStatus([FromBody] StatusDTO StatusDto, string id)
        {
            _logger.LogInformation($"Updating in the system the Status {id}");
            return new JsonResult(await _service.Update(StatusDto, id, "wacor"));
        }

        // DELETE SQN/rest/<StatusController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> DeleteArea(string id)
        {
            _logger.LogInformation($"Deleting from the system the Area {id}");
            return new JsonResult(await _service.Delete(id));
        }
    }
}