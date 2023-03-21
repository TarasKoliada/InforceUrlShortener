using AutoMapper;
using InforceUrlShortener.Core.Abstractions;
using InforceUrlShortener.Models;
using InforceUrlShortener.Services.BitlyShortener;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InforceUrlShortener.Controllers
{
    [Route("api/url/{action}")]
    [ApiController]
    public class ShortUrlController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ShortUrlController(IUnitOfWork unit, IMapper mapper)
        {
            _unitOfWork = unit;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUrl([FromBody] ShortUrlDto shortUrl)
        {
            if (shortUrl == null)
                return BadRequest();

            if (User.Identity.IsAuthenticated)
            {
                var current = HttpContext.User.Identity as ClaimsIdentity;
                var currentUser = _unitOfWork.Users.GetAllAsync().Result.FirstOrDefault(u => u.UserName == current.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                if (currentUser == null)
                    BadRequest("Current user is null");

                shortUrl.CreatedByUserId = currentUser.Id;
            }
            else
                shortUrl.CreatedByUserId = "Guest";

            shortUrl.ShortenedUrl = UrlShortener.ShortenUrl(shortUrl.OriginalUrl).Result;

            var mappedUrl = _mapper.Map<Domain.Entities.ShortUrl>(shortUrl);
            return _unitOfWork.ShortUrls.Add(mappedUrl) ? Ok(await _unitOfWork.SaveAsync()) : BadRequest("Cannot add Url");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUrls()
        {
            return Ok(await _unitOfWork.ShortUrls.GetAllAsync());
        }
    }
}
