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
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IServiceService _service = new ServiceService();

        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
        }

        // GET: SQN/rest/<ServiceController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> GetAllService()
        {
            _logger.LogInformation("Getting all Services in the system");
            return new JsonResult(await _service.GetAll());
        }

        // GET SQN/rest/<ServiceController>/56
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetServiceById(string id)
        {
            _logger.LogInformation($"Getting the service {id} in the system");
            return new JsonResult(await _service.GetById(id));
        }

        // GET SQN/rest/<ServiceController>/list/customer/27
        [HttpGet("list/customers/{customer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetServicesByCustomer(string customer)
        {
            _logger.LogInformation($"Getting the Services for customer {customer} in the system");
            return new JsonResult(await _service.GetByCustomer(customer));
        }

        // GET SQN/rest/<ServiceController>/customer/27/98
        [HttpGet("customer/{customer}/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetServicesByCustomerAndStatus(string customer, string status)
        {
            _logger.LogInformation($"Getting the Services for customer {customer} with status {status} in the system");
            return new JsonResult(await _service.GetByCustomerAndStatus(customer, status));
        }

        // GET SQN/rest/<ServiceController>/expert/83
        [HttpGet("expert/{expert}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetServiceByExpert(string expert)
        {
            _logger.LogInformation($"Getting the Services for the expert {expert} in the system");
            return new JsonResult(await _service.GetByExpert(expert));
        }

        // GET SQN/rest/<ServiceController>/expert/27/98
        [HttpGet("expert/{expert}/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetServicesByExpertAndStatus(string customer, string status)
        {
            _logger.LogInformation($"Getting the Services for customer {customer} with status {status} in the system");
            return new JsonResult(await _service.GetByExpertAndStatus(customer, status));
        }

        // POST SQN/rest/<ServiceController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> CreateService([FromBody] ServiceDTO serviceDTO)
        {
            _logger.LogInformation("Inserting a new Service in the system");
            return new JsonResult(await _service.CreateService(serviceDTO, "wacor"));
        }

        // PUT SQN/rest/<ServiceController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> UpdateService([FromBody] ServiceDTO service, string id)
        {
            _logger.LogInformation($"Updating in the system the Service {id}");
            return new JsonResult(await _service.Update(service, id, "wacor"));
        }

        // DELETE SQN/rest/<ServiceController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> DeleteService(string id)
        {
            _logger.LogInformation($"Deleting from the system the RangeArea {id}");
            return new JsonResult(await _service.Delete(id));
        }
    }
}