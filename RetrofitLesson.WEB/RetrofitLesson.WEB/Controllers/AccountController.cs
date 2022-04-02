using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetrofitLesson.WEB.Constants;
using RetrofitLesson.WEB.Data.Identity;
using RetrofitLesson.WEB.Models;
using RetrofitLesson.WEB.Services;

namespace RetrofitLesson.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMapper _mapper { get; set; }
        private UserManager<AppUser> _userManager { get; set; }
        private IJwtTokenService _jwtTokenService { get; set; }

        public AccountController(UserManager<AppUser> userManager, 
            IMapper mapper, IJwtTokenService jwtTokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register([FromBody] RegisterModel model) 
        {
            return await Task.Run(async () => {

                AppUser user = _mapper.Map<AppUser>(model);
                user.Photo = "default.jpg";
                await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, Roles.USER);
                string token = _jwtTokenService.CreateToken(user);

                return Ok(new RegisterReturnModel
                {
                    token = token
                });
            });
        }
    }
}
