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
            var adminOnlyPaths = new[]
{
                  "/Admin/Department",
                  "/Admin/DoctorApplications",
                  "/Admin/SocialMedia"
};

            var httpContext = context.HttpContext;

            var path = httpContext.Request.Path;


            if (path.StartsWithSegments("/Admin/Account/Login"))
            {
                return;
            }


            var token = httpContext.Request.Cookies["AdminAuthToken"];


            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToPageResult(
                    "/Admin/Account/Login"
                );

                return;
            }

            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = handler.ReadJwtToken(token);

                var userId = jwtToken.Claims
                    .FirstOrDefault(x => x.Type == "DoctorNurceId")?.Value;

                var role = jwtToken.Claims
                    .FirstOrDefault(x => x.Type == "RollType")?.Value;




                if (adminOnlyPaths.Any(x => path.StartsWithSegments(x)))
                {
                    if (role != "3")
                    {
                        context.Result = new RedirectToPageResult("/Admin/Index");
                        return;
                    }
                }



                if (string.IsNullOrEmpty(userId))
                {
                    context.Result = new RedirectToPageResult(
                        "/Admin/Account/Login"
                    );

                    return;
                }


                httpContext.Items["RollType"] = role;
            }
            catch
            {
                context.Result = new RedirectToPageResult(
                    "/Admin/Account/Login"
                );

                return;
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}