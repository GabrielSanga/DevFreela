using DevFreela.Core.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly DevFreelaDbContext _dbContext;

        public UsersController(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _dbContext.Users
                            .Include(u => u.Skills)
                                .ThenInclude(s => s.Skill)
                            .SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            var model = UserViewModel.FromEntity(user);

             return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/skills")]
        public IActionResult Post(int id, UserSkillsInputModel model)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            var userSkills = model.SkillsIds.Select(s => new UserSkill(id, s)).ToList();

            _dbContext.UserSkills.AddRange(userSkills);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //[HttpPut("{id}/profile-picture")]
        //[Consumes("multipart/form-data")]
        //public IActionResult PutProfilePicture(int id, [FromForm] IFormFile file)
        //{
        //    var description = $"File: {file.FileName}, Size: {file.Length}";

        //    return Ok(description);
        //}


    }

}
