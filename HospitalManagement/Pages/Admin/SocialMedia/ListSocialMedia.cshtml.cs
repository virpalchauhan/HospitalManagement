using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.SocialMedia
{
    public class ListSocialMediaModel : PageModel
    {

        private readonly ISocialMediaMastersServices ObjSocialMediaMastersServices;

        [BindProperty]

        public List<SocialMediaMaster> SocialMediaMaster { get; set; }


        public ListSocialMediaModel(ISocialMediaMastersServices ObjSocialMediaMastersServices)
        {
            this.ObjSocialMediaMastersServices = ObjSocialMediaMastersServices;
        }
        public void OnGet()
        {

            SocialMediaMaster = ObjSocialMediaMastersServices.AllSocialMediaMasterData();

           

        }

        public IActionResult OnPost(int SocialMediaId)
        {
            int Result = ObjSocialMediaMastersServices.DeleteSocialMedia(SocialMediaId);

            if (Result == 1)
            {
                TempData["MsgSuccess"] =
                    "Social Media Platform Deleted Successfully";
            }
            else if (Result == 0)
            {
                TempData["MsgNormal"] =
                    "Record Not Found";
            }
            else
            {
                TempData["MsgDanger"] =
                    "Failed To Delete Social Media Platform";
            }
            return RedirectToPage();
            
           
        }
    }
}
