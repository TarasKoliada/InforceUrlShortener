using AutoMapper;
using InforceUrlShortener.Core.Abstractions;
using InforceUrlShortener.Core.Services;
using InforceUrlShortener.Models;
using InforceUrlShortener.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace InforceUrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginController(IConfiguration config, IUnitOfWork unit, IMapper mapper)
        {
            _config = config;
            _unitOfWork = unit;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLoginData userData)
        {
            var jwtProvider = new JwtTokenProvider();

            var user = _unitOfWork.Users.GetAllAsync().Result
                .FirstOrDefault(u => u.UserName.ToLower() == userData.UserName.ToLower()
                && Password.VerifyPassword(userData.Password, u.PasswordHash) == true);

            if (user != null)
            {
                var token = jwtProvider.GenerateToken(_config, _mapper.Map<UserDto>(user));
                return Ok(token);
            }
            return NotFound("User not found");
        }
    }
}
