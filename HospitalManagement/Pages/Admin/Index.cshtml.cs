using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {

        public string DoctorNurceId { get; set; }
        public string RollType { get; set; }

        public void OnGet()
        {
            DoctorNurceId = User.FindFirst("DoctorNurceId")?.Value;
            RollType = User.FindFirst("RollType")?.Value;
        }
    }
}
