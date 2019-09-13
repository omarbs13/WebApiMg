using MdbApi.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MdbApi.Application.Models;
using MdbApi.Service.Helpers;
namespace MdbApi.Service.Controllers
{
    //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly AppSettings _appSettings;
        public UserController(IUserApplication userApplication, IOptions<AppSettings> appSettings)
        {
            _userApplication = userApplication;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userApplication.GetAll();
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
            var response = await _userApplication.Get(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserModel userModel)
        {
            if (userModel == null)
                return BadRequest();
            var response = await _userApplication.Add(userModel);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UserModel userModel)
        {
            if (userModel == null)
                return BadRequest();
            var response = await _userApplication.UpdateDocument(userModel);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var response = await _userApplication.Remove(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}