using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Services;

namespace DevFreela.API.Controllers
{

    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IProjectService _service;

        public ProjectsController(DevFreelaDbContext dbContext, IProjectService service) {
            _dbContext = dbContext;
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var result = _service.GetAll(search, page, size);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var result = _service.Insert(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return CreatedAtAction(nameof(GetById), new { id = result.Data}, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model) {
            model.IdProject = id;

            var result = _service.Update(model);

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent(); 
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var result = _service.Start(id);

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var result = _service.Complete(id);

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            var result = _service.InsertComment(id, model);

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent();
        }

    }
}
