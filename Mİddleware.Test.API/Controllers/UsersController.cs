using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mİddleware.Test.API.Models;
using System;

namespace Mİddleware.Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public IActionResult GetUserInfo(int id)
        {
            var user = new UserLoginResponseModel()
            {
                Success = true,
                UserName = "oguzhancengiz"
            };

            return Ok(user);
        }

        [HttpPost]
        [Route("loginonly")]
        public IActionResult UserOnly([FromBody] UserLoginRequestModel model)
        {
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserLoginRequestModel model)
        {
            var user = new UserLoginResponseModel()
            {
                Success = true,
                UserName = "oguzhancengiz"
            };

            return Ok(user);
        }
    }
}
