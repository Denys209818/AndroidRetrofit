using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RetrofitLesson.WEB.Data.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitLesson.WEB.Services
{
    public interface IJwtTokenService 
    {
        public string CreateToken(AppUser user);
    }
    public class JwtTokenService : IJwtTokenService
    {
        public IConfiguration _configuration { get; set; }
        public UserManager<AppUser> _userManager { get; set; }

        public JwtTokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public string CreateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("firstname", user.FirstName));
            claims.Add(new Claim("lastname", user.SecondName));
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("phone", user.Phone));
            claims.Add(new Claim("photo", user.Photo));

            foreach (string role in  _userManager.GetRolesAsync(user).Result)
            {
                claims.Add(new Claim("roles", role));
            }

            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("private_key").Value)), SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: credentials,
                expires: DateTime.Now.AddDays(10));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
