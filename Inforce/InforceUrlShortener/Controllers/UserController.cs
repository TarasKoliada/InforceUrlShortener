using AutoMapper;
using InforceUrlShortener.Core.Abstractions;
using InforceUrlShortener.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InforceUrlShortener.Controllers
{
    [Route("api/user/{action}")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unit, IMapper mapper)
        {
            _unitOfWork = unit;
            _mapper = mapper;
        }
        private UserDto GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserDto
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Admin Interface. You are {currentUser.Role}. Name: {currentUser.FirstName}");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult UsersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"User Interface. You are {currentUser.Role}. Name: {currentUser.FirstName}");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,User")]
        public IActionResult AdminsAndUsersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"General Interface. You are {currentUser.Role}. Name: {currentUser.FirstName}");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest();
            
            var mappedUser = _mapper.Map<Domain.Entities.User>(userDto);

            if (mappedUser == null)
                return BadRequest();

            return _unitOfWork.Users.Add(mappedUser) ? Ok(await _unitOfWork.SaveAsync()) : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _unitOfWork.Users.GetAllAsync());
        }

        [Route("{userId}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveUser(string userId)
        {
            try
            {

                if (!await _unitOfWork.Users.Delete(userId))
                    return NotFound($"Couldn`t find the customer with id: {userId}");



                await _unitOfWork.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Couldn`t Delete User. {ex.Message}");
            }
            

        }
    }
}
