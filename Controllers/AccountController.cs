using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using jobscontractors.Models;
using jobscontractors.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jobscontractors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // REVIEW[epic=Authentication] this tag enforces the user must be logged in
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _ps;
        private readonly WhiteboardsService _whtbrdServ;


        public AccountController(ProfilesService ps, WhiteboardsService whtbrdServ)
        {
            _ps = ps;
            _whtbrdServ = whtbrdServ;
        }

        [HttpGet]
        // REVIEW[epic=Authentication] async calls must return a System.Threading.Tasks, this is equivalent to a promise in JS
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                // REVIEW[epic=Authentication] how to get the user info from the request token
                // same as to req.userInfo
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("whiteboards")]
        public async Task<ActionResult<IEnumerable<WhiteboardStickynoteViewModel>>> GetPartiesAsync()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_whtbrdServ.GetByAccountId(userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}