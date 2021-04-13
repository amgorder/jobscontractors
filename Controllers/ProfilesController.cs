using System;
using jobscontractors.Models;
using jobscontractors.Services;
using Microsoft.AspNetCore.Mvc;

namespace jobscontractors.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _service;

        public ProfilesController(ProfilesService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                Profile profile = _service.GetProfileById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}