using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.Services;

namespace DevFreela.API.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly DevFreelaDbContext _dbContext;
        private readonly IUserService _userService;

        public UsersController(DevFreelaDbContext dbContext, IUserService service)
        {
            _dbContext = dbContext;
            _userService = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {   
            var result = _userService.GetById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }   

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var result = _userService.Insert(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("{id}/skills")]
        public IActionResult Post(int id, UserSkillsInputModel model)
        {
            model.Id = id;  

            var result = _userService.InsertSkills(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return NoContent();
        }

    }

}
