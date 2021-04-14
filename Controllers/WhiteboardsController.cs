using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using jobscontractors.Models;
using jobscontractors.Services;
using Microsoft.AspNetCore.Mvc;

namespace jobscontractors.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhiteboardsController : ControllerBase
    {
        private readonly WhiteboardsService _service;
        private readonly StickynotesService _stkyservice;

        public WhiteboardsController(WhiteboardsService service, StickynotesService stkyservice)
        {
            _service = service;
            _stkyservice = stkyservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Whiteboard>> Get()
        {
            try
            {
                return Ok(_service.Get());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Whiteboard>> Get(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpGet("{id}/stickynotes")]
        public ActionResult<IEnumerable<Whiteboard>> GetStickynotesByWhiteboardId(int id)
        {
            try
            {
                return Ok(_stkyservice.GetByWhiteboardId(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Whiteboard>> CreateAsync([FromBody] Whiteboard newWhiteboard)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newWhiteboard.CreatorId = userInfo.Id;
                newWhiteboard.Creator = userInfo;
                return Ok(_service.Create(newWhiteboard));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Whiteboard>> Edit([FromBody] Whiteboard updated, int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                updated.CreatorId = userInfo.Id;
                updated.Id = id;
                updated.Creator = userInfo;
                return Ok(_service.Edit(updated));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Whiteboard>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_service.Delete(id, userInfo.Id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}