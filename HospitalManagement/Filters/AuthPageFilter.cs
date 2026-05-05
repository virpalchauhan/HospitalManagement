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
            //    var httpContext = context.HttpContext;

            //    var path = httpContext.Request.Path;



            //    string[] PageList = new string[] {

            //    "/Admin/Account/ForgotPassword",


            //    };


            //    if (path.StartsWithSegments("/Admin/Account/Login"))
            //    {
            //        return;
            //    }


            //    var token = httpContext.Request.Cookies["AuthToken"];

            //    if (string.IsNullOrEmpty(token))
            //    {
            //        context.Result = new RedirectToPageResult("/Admin/Account/Login");
            //        return;
            //    }

            //    var handler = new JwtSecurityTokenHandler();

            //    try
            //    {
            //        var jwtToken = handler.ReadJwtToken(token);

            //        var userId = jwtToken.Claims
            //            .FirstOrDefault(x => x.Type == "DoctorNurceId")?.Value;

            //        var role = jwtToken.Claims
            //            .FirstOrDefault(x => x.Type == "RollType")?.Value;

            //        if (string.IsNullOrEmpty(userId))
            //        {
            //            context.Result = new RedirectToPageResult("/Admin/Account/Login");
            //            return;
            //        }


            //    }
            //    catch
            //    {
            //        context.Result = new RedirectToPageResult("/Admin/Account/Login");
            //    }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}