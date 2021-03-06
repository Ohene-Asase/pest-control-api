using PestControl.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PestControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ImagesController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpGet]
        public ActionResult Get(string file)
        {
            try
            {
                var fileStream = new ImageHelpers(_hostingEnvironment.ContentRootPath).GetImage(file);
                return new FileStreamResult(fileStream, "image/png");
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.ProcessException(ex));
            }

        }
    }
}