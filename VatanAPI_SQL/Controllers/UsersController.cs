using System.Threading.Tasks;
using AutoMapper;
using VatanAPI.Controllers.Resources;
using VatanAPI.Core.Models;
using VatanAPI.Core.Services;
using Microsoft.AspNetCore.Mvc;
using VatanAPI.Resources;
using VatanAPI.Domain.Models;

namespace VatanAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserCredentialsResource, User>(userCredentials);

            var response = await _userService.CreateUserAsync(user, ERole.Common);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userResource = _mapper.Map<User, UserResource>(response.User);
            return Ok(userResource);
        }
        [HttpGet("{email}")]
        public async Task<UserResource> GetUserInfos(string email)
        {
            var userinfo =  await _userService.FindByEmailAsync(email);
            var resource = _mapper.Map<User, UserResource>(userinfo);
            resource.Password = null;
            resource.Roles = null;
            return resource;
        }
    }
}