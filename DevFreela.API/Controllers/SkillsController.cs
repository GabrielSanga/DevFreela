using DevFreela.API.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {

        private readonly DevFreelaDbContext _dbContext;

        public SkillsController(DevFreelaDbContext dbContent)
        {
            _dbContext = dbContent;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var skills = _dbContext.Skills.Where(s => !s.IsDeleted).ToList();

            var model = skills.Select(s => SkillViewModel.FromEntity(s)).ToList();

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _dbContext.Skills.Add(skill);
            _dbContext.SaveChanges();

            return NoContent();
        }

    }

}
