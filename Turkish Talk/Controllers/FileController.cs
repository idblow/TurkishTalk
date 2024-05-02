using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

namespace Turkish_Talk.Controllers
{
    [ApiController]
    [Route("api/storage")]
    public class FileController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public FileController(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        [HttpGet("getuserimage/{userid}")]
        public async Task<ActionResult> GetUserImage([Required][FromRoute] int userid)
        {
            var user = await _applicationDBContext.Set<User>().FirstOrDefaultAsync(x => x.Id == userid);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Image == null)
            {
                return NoContent();
            }

            return File(user.Image, user.ImageContentType);

        }

        [HttpPost("uploaduserimage/{userid}")]
        public async Task<ActionResult> UploadUserImage([Required][FromRoute] int userid, IFormFile formFile)
        {
            var user = await _applicationDBContext.Set<User>().FirstOrDefaultAsync(x => x.Id == userid);
            if (user == null)
            {
                return NotFound();
            }

            user.Image = formFile.OpenReadStream().ToArray();

            user.ImageContentType = formFile.ContentType;

            _applicationDBContext.Update(user);

            await _applicationDBContext.SaveChangesAsync();

            return Ok();

        }

    }
}

