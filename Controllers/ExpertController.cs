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
    public class ExpertController : ControllerBase
    {
        private readonly ILogger<ExpertController> _logger;
        private readonly IExpertService _service = new ExpertService();

        public ExpertController(ILogger<ExpertController> logger)
        {
            _logger = logger;
        }

        // GET: SQN/rest/<ExpertController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> GetAllExperts()
        {
            _logger.LogInformation("Getting all Experts in the system");
            return new JsonResult(await _service.GetAll());
        }

        // GET SQN/rest/<ExpertController>/56
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetExpertById(string id)
        {
            _logger.LogInformation($"Getting the Expert {id} in the system");
            return new JsonResult(await _service.GetById(id));
        }

        // GET SQN/rest/<ExpertController>/4/456
        [HttpGet("{tipo}/{documento}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetExpertById(string tipo, string documento)
        {
            _logger.LogInformation($"Getting the Expert document {tipo} with number {documento}");
            return new JsonResult(await _service.GetById(tipo, documento));
        }

        // GET SQN/rest/<ExpertController>/phone/5627
        [HttpGet("phone/{cellPhone}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetExpertByCellPhone(string cellPhone)
        {
            _logger.LogInformation($"Getting the Expert with cellular {cellPhone}");
            return new JsonResult(await _service.GetByCellPhone(cellPhone));
        }

        // GET SQN/rest/<ExpertController>/mail/mail@url.com
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetServicesByCustomerAndStatus(string email)
        {
            _logger.LogInformation($"Getting the Expert with email {email}");
            return new JsonResult(await _service.GetByEmail(email));
        }

        // POST SQN/rest/<ExpertController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> CreateExpert([FromBody] ExpertDTO expert)
        {
            _logger.LogInformation("Inserting a new Expert in the system");
            return new JsonResult(await _service.Insert(expert, "wacor"));
        }

        // PUT SQN/rest/<ExpertController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> UpdateExpert([FromBody] ExpertDTO expert, string id)
        {
            _logger.LogInformation($"Updating in the system the Expert {id}");
            return new JsonResult(await _service.Update(expert, "wacor", id));
        }

        // DELETE SQN/rest/<ExpertController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> DeleteExpert(string id)
        {
            _logger.LogInformation($"Deleting from the system the Expert {id}");
            return new JsonResult(await _service.Delete(id));
        }
    }
}