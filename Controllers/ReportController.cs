using Microsoft.AspNetCore.Mvc;
using SQNBack.Models.DTO;
using SQNBack.Services.Implement;
using SQNBack.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SQNBack.Controllers
{
    [Route($"{GeneralValidatons.ROUTE}[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportService _service = new ReportService();

        public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;
        }

        // GET: SQN/rest/<ReportController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> GetAllService()
        {
            _logger.LogInformation("Getting all Reports in the system");
            return new JsonResult(await _service.GetAll());
        }

        // GET SQN/rest/<ReportController>/56
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetReportById(string id)
        {
            _logger.LogInformation($"Getting the report {id} in the system");
            return new JsonResult(await _service.GetById(id));
        }

        // GET SQN/rest/<ReportController>/customer/27
        [HttpGet("customer/{customer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetReportsByCustomer(string customer)
        {
            _logger.LogInformation($"Getting the reports for customer {customer} in the system");
            return new JsonResult(await _service.GetByCustomer(customer));
        }

        // GET SQN/rest/<ReportController>/expert/83
        [HttpGet("expert/{expert}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetServiceByExpert(string expert)
        {
            _logger.LogInformation($"Getting the Services for the expert {expert} in the system");
            return new JsonResult(await _service.GetByExpert(expert));
        }
        
        // POST SQN/rest/<ReportController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> CreateReport([FromBody] ReportDTO reportDTO)
        {
            _logger.LogInformation("Inserting a new Service in the system");
            return new JsonResult(await _service.Insert(reportDTO.ToModel(), "wacor"));
        }

        // PUT SQN/rest/<ReportController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> UpdateService([FromBody] ReportDTO report, string id)
        {
            _logger.LogInformation($"Updating in the system the Service {id}");
            return new JsonResult(await _service.Update(report.ToModel(), id, "wacor"));
        }

        // DELETE SQN/rest/<ReportController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> DeleteService(string id)
        {
            _logger.LogInformation($"Deleting from the system the RangeArea {id}");
            return new JsonResult(await _service.Delete(id));
        }

        /** //GET SQN/rest/<ReportController>/list/customer/27/98
        [HttpGet("list/customers/{customer}/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetServicesByCustomerAndStatus(string customer, string status)
        {
            _logger.LogInformation("Getting the Services for customer " + customer +
                " with status " + status + " in the system");
            return new JsonResult(await _service.GetByCustomerAndStatus(customer, status));
        }
                
        // GET SQN/rest/<ReportController>/list/customer/27/98
        [HttpGet("list/experts/{expert}/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetServicesByExpertAndStatus(string customer, string status)
        {
            _logger.LogInformation("Getting the Services for customer " + customer +
                " with status " + status + " in the system");
            return new JsonResult(await _service.GetByExpertAndStatus(customer, status));
        }
        */
    }
}