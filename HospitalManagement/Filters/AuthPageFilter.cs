using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace HospitalManagement.Filters
{
    public class AuthPageFilter : IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {

        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var httpContext = context.HttpContext;

            var path = httpContext.Request.Path;



            //string[] PageList = new string[] { 
            
            //"/Admin/Account/Login",


            //};

           
            if (path.StartsWithSegments("/Admin/Account/Login"))
            {
                return;
            }

            // ✅ Cookie se token lo (IMPORTANT FIX)
            var token = httpContext.Request.Cookies["AuthToken"];

            // ❌ Token nahi mila → redirect
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToPageResult("/Admin/Account/Login");
                return;
            }

            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = handler.ReadJwtToken(token);

                // 🔹 Claims read
                var userId = jwtToken.Claims
                    .FirstOrDefault(x => x.Type == "DoctorNurceId")?.Value;

                var role = jwtToken.Claims
                    .FirstOrDefault(x => x.Type == "RollType")?.Value;

                // ❌ Invalid user → redirect
                if (string.IsNullOrEmpty(userId))
                {
                    context.Result = new RedirectToPageResult("/Admin/Account/Login");
                    return;
                }

                // 🔥 Optional Role check
                // if (role != "Admin")
                // {
                //     context.Result = new RedirectToPageResult("/AccessDenied");
                // }
            }
            catch
            {
                context.Result = new RedirectToPageResult("/Admin/Account/Login");
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}