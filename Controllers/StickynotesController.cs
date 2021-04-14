using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using jobscontractors.Models;
using jobscontractors.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jobscontractors.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StickynotesController : ControllerBase
    {
        private readonly StickynotesService _service;

        public StickynotesController(StickynotesService service)
        {
            _service = service;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> CreateAsync([FromBody] Stickynote pm)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                pm.CreatorId = userInfo.Id;
                return Ok(_service.Create(pm));

            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteAsync(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _service.Delete(id, userInfo.Id);
                return Ok("Deleted");

            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}