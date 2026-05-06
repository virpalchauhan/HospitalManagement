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
            string Delete = ObjSocialMediaMastersServices.DeleteSocialMedia(SocialMediaId);
           
                TempData["Msg"] = Delete;
                return RedirectToPage();
            
           
        }
    }
}
