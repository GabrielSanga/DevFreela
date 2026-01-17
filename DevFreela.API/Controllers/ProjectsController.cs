using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{

    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectsController(DevFreelaDbContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var projects = _dbContext.Projects
                                .Include(p => p.Client)
                                .Include(p => p.Freelancer)
                                .Skip(page * size)
                                .Take(size)
                                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search))).ToList();

            var model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _dbContext.Projects
                    .Include(p => p.Client)
                    .Include(p => p.Freelancer)
                    .Include(p => p.Comments)
                    .SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();  
            }

            var model = ProjectViewModel.FromEntity(project);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Post), new {id = project.Id}, model);
        }

        [HttpPut]
        public IActionResult Put(int Id, UpdateProjectInputModel model) {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == Id);

            if (project == null)
            {
                return NoContent();
            }

            project.Update(model.Title, model.Description, model.TotalCost);

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NoContent();
            }

            project.SetAsDeleted();

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent(); 
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NoContent();
            }

            project.Start();

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();   

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NoContent();
            }

            project.Complete();

            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == model.IdUser);

            if ( project is null || user is null)
            {
                return NotFound();
            }

            var comment = model.ToEntity();

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(PostComment), new { id = comment.Id }, model);
        }

    }
}
