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
    public class DataElementController : ControllerBase
    {
        private readonly ILogger<DataElementController> _logger;
        private readonly IDataElementService _service = new DataElementService();

        public DataElementController(ILogger<DataElementController> logger)
        {
            _logger = logger;
        }

        // GET: SQN/rest/<AreaController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> GetAllDataElement()
        {
            _logger.LogInformation("Getting all RangeArea in the system");
            return new JsonResult(await _service.GetAll());
        }

        // GET SQN/rest/<AreaController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetDataElementById(string id)
        {
            _logger.LogInformation($"Getting the DataElement {id} in the system");
            return new JsonResult(await _service.GetById(id));
        }

        // GET SQN/rest/<AreaController>/group/xyz
        [HttpGet("group/{group}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetDataElementByGroup(string group)
        {
            _logger.LogInformation($"Getting the Data Elements with group {group} in the system");
            return new JsonResult(await _service.GetByGroup(group));
        }

        // POST SQN/rest/<AreaController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> InsertDataElement([FromBody] DataElementDTO dataDTO)
        {
            _logger.LogInformation("Inserting a new Data Element in the system");
            return new JsonResult(await _service.Insert(dataDTO, "wacor"));
        }

        // PUT SQN/rest/<AreaController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> UpdateDataElement([FromBody] DataElementDTO dataDTO, string id)
        {
            _logger.LogInformation($"Updating in the system the Data Element {id}");
            return new JsonResult(await _service.Update(dataDTO, id, "wacor"));
        }

        // DELETE SQN/rest/<AreaController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> DeleteDataElement(string id)
        {
            _logger.LogInformation($"Deleting from the system the Data element {id}");
            return new JsonResult(await _service.Delete(id));
        }
    }
}