using PestControl.Helpers;
using PestControl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PestControl.Controllers
{
    public class AppointmentController : BaseApi<IAppointmentService, AppointmentDto>
    {

        public AppointmentController(IAppointmentService service) : base(service) { }


        [HttpGet, Route("approval")]
        public async Task<IActionResult> Approval(long id)
        {
            try { return Ok(await Service.Approve(id)); }
            catch (Exception ex) { return BadRequest(ExceptionHelper.ProcessException(ex)); }
        }






    }
 }

