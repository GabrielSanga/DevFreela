using DevFreela.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {

        // GET api/skills
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        // POST api/skills
        public IActionResult Post(CreateSkillInputModel model)
        {
            return Ok();
        }

    }

}
