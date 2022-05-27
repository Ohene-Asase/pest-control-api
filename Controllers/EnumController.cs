using PestControl.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace PestControl.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController, Authorize]
    public class EnumsController : ControllerBase
    {
        private readonly IEnumService _enumService;

        public EnumsController(IEnumService enumService) => _enumService = enumService;

        [HttpGet]
        public ActionResult GetList(string name)
        {
            try { return Ok(_enumService.GetList(name)); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}