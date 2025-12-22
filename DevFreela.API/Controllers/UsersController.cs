using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        // POST api/users
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpPut("{id}/profile-picture")]
        [Consumes("multipart/form-data")]
        public IActionResult PutProfilePicture(int id, [FromForm] IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            return Ok(description);
        }


    }

}
