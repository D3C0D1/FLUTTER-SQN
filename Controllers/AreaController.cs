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
    public class AreaController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IAreaService _service = new AreaService();

        public AreaController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        // GET: SQN/rest/<AreaController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> GetAllAreas()
        {
            _logger.LogInformation("Getting all Areas in the system");
            return new JsonResult(await _service.GetAll());
        }

        // GET SQN/rest/<AreaController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetAreaById(string id)
        {
            _logger.LogInformation($"Getting the Area {id} in the system");
            return new JsonResult(await _service.GetById(id));
        }

        // GET SQN/rest/<AreaController>/xyz/2
        [HttpGet("{name}/{order}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetAreaByNameAndOrder(string name, int order)
        {
            _logger.LogInformation($"Getting the Area with name {name} and order {order} in the system");
            return new JsonResult(await _service.GetByNameAndOrder(name, order));
        }

        // GET SQN/rest/<AreaController>/list/xyz
        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<JsonResult> GetAreaByName(string name)
        {
            _logger.LogInformation($"Getting the Areaa with name {name} in the system");
            return new JsonResult(await _service.GetByName(name));
        }

        // POST SQN/rest/<AreaController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> InsertArea([FromBody] AreaDTO AreaDto)
        {
            _logger.LogInformation("Inserting a new Area in the system");
            return new JsonResult(await _service.Insert(AreaDto, "wacor"));
        }

        // PUT SQN/rest/<AreaController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> UpdateArea([FromBody] AreaDTO AreaDto, string id)
        {
            _logger.LogInformation($"Updating in the system the Area {id}");
            return new JsonResult(await _service.Update(AreaDto, id, "wacor"));
        }

        // PUT SQN/rest/<AreaController>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public async Task<JsonResult> InsertRange([FromBody] RangeDTO AreaDto)
        {
            _logger.LogInformation("Updating in the system the Area adding a new Range");
            return new JsonResult(await _service.InsertRange(AreaDto,"wacor"));
        }

        // DELETE SQN/rest/<AreaController>/5
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