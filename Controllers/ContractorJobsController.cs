using System.Threading.Tasks;
using jobscontractors.Models;
using jobscontractors.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jobscontractors.Controllers
{
    public class ContractorJobsController
    {
        private readonly ContractorJobsService _service;

        public ContractorJobsController(ContractorJobsService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ContractorJob>> CreateAsync([FromBody] ContractorJob newWLP)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newWLP.CreatorId = userInfo.Id;
                return Ok(_service.Create(newWLP));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("deleted");
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}