using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalManagement.Helper
{


    public interface IJwtTokenHelper
    {
        string JWTGenerateToken(string userId, string role);
    }


    public class JwtTokenHelper: IJwtTokenHelper
    {
        private readonly IConfiguration ObjConfiguration;

        public JwtTokenHelper(IConfiguration ObjConfiguration)
        {
            this.ObjConfiguration = ObjConfiguration;
        }


        public string JWTGenerateToken(string DoctorNurceId, string RollType)
        {
           var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ObjConfiguration["JwtSettings:Key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);


            var Claims= new[]
            {
                new System.Security.Claims.Claim("DoctorNurceId", DoctorNurceId),
                new System.Security.Claims.Claim("RollType", RollType)
            };

            var token = new JwtSecurityToken(
    issuer: ObjConfiguration["JwtSettings:Issuer"],
    audience: ObjConfiguration["JwtSettings:Audience"],
    claims: Claims,
    expires: DateTime.Now.AddMinutes(Convert.ToDouble(ObjConfiguration["JwtSettings:ExpireMinutes"])),
    signingCredentials: Credentials
);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
