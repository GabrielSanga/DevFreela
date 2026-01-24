using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Application.Commands.InsertComment;
using DevFreela.Application.Services;
using DevFreela.Application.Models;

namespace DevFreela.API.Controllers
{

    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProjectService _service;

        public ProjectsController(IMediator mediator, IProjectService service) {
            _mediator = mediator;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search = "", int page = 0, int size = 3)
        {
            var result = await _mediator.Send(new GetAllProjectQuery(search, page, size));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertProjectCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return CreatedAtAction(nameof(GetById), new { id = result.Data}, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProjectCommand command) {
            command.IdProject = id;

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent(); 
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            var result = await _mediator.Send(new StartProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var result = await _mediator.Send(new CompleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, InsertCommentCommand command)
        {
            command.IdProject = id;

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            };

            return NoContent();
        }

    }
}
