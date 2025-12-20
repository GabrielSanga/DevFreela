using DevFreela.API.Modes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{

    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {

        private readonly FreelanceTotalCostConfig _config;

        public ProjectsController(IOptions<FreelanceTotalCostConfig> options) {
            _config = options.Value;    
        }

        [HttpGet]
        public IActionResult Get(string search)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            if (model.TotalCost < _config.Minimum || model.TotalCost > _config.Maximum)
            {
                return BadRequest("Valor acima ou abaixo do limite pré definido!");
            }

            return CreatedAtAction(nameof(GetById), new {id = 1}, model);
        }

        [HttpPut]
        public IActionResult Put(int Id, UpdateProjectInputModel model) {
            model.IdProject = Id;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            return NoContent(); 
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            return Ok();
        }

    }
}
