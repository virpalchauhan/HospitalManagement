using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {

        public string UserId { get; set; }
        public string Role { get; set; }

        public void OnGet()
        {
            UserId = User.FindFirst("UserId")?.Value;
            Role = User.FindFirst("Role")?.Value;
        }
    }
}
