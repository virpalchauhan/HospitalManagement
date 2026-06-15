using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace HospitalManagement.Pages.Client
{
    public class HomeModel : PageModel
    {



        public void OnGet()
        {
            //var token = Request.Cookies["AuthToken"];

            //if (string.IsNullOrEmpty(token))
            //{
            //    return RedirectToPage("/Client/Account/Login");
            //}

            //var handler = new JwtSecurityTokenHandler();
            //var jwtToken = handler.ReadJwtToken(token);

            //var patientId = jwtToken.Claims
            //    .FirstOrDefault(x => x.Type == "PatientId")?.Value;

            //if (string.IsNullOrEmpty(patientId))
            //{
            //    return RedirectToPage("/Client/Account/Login");
            //}

            //return Page();
        }
    }
}
