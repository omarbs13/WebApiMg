using System.Threading.Tasks;
using MdbApi.Application.Interface;
using MdbApi.Application.Models;
using MdbApi.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MdbApi.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonApplication _personApplication;
        private readonly AppSettings _appSettings;
        public PersonController(IPersonApplication personApplication, IOptions<AppSettings> appSettings)
        {
            _personApplication = personApplication;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _personApplication.GetAll();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var response = await _personApplication.Get(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonModelAdd userModel)
        {
            if (userModel == null)
                return BadRequest();
            var response = await _personApplication.Add(userModel);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]PersonModel userModel)
        {
            if (userModel == null)
                return BadRequest();
            var response = await _personApplication.UpdateDocument(userModel);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var response = await _personApplication.Remove(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}