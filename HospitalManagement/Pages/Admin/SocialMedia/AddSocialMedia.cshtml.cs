using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.SocialMedia
{

    

    public class AddSocialMediaModel : PageModel
    {

        private readonly ISocialMediaMastersServices ObjSocialMediaMastersServices;



        [BindProperty]


        public SocialMediaMasterViewModel SocialMediaMasterViewModel { get; set; } =new SocialMediaMasterViewModel();



        public AddSocialMediaModel(ISocialMediaMastersServices ObjSocialMediaMastersServices)
        {
         this.ObjSocialMediaMastersServices= ObjSocialMediaMastersServices;   
        }
        public void OnGet(int SocialMediaIdd)
        {

            if (SocialMediaIdd > 0)
            {
                var SocialMediaData = ObjSocialMediaMastersServices.GetSocialMediaById(SocialMediaIdd);

                if (SocialMediaData!=null)
                {
                    SocialMediaMasterViewModel.PlatformName = SocialMediaData.PlatformName;
                    SocialMediaMasterViewModel.SocialMediaLink = SocialMediaData.SocialMediaLink;
                    SocialMediaMasterViewModel.IsActive = SocialMediaData.IsActive;
                }

            }

        }

        public IActionResult OnPost(int SocialMediaIdd)
        {


            if (ModelState.IsValid)
            {

                if (SocialMediaIdd < 0)
                {
                    SocialMediaMaster InsertSocialMedia = new SocialMediaMaster()
                    {
                        PlatformName = SocialMediaMasterViewModel.PlatformName,
                        SocialMediaLink = SocialMediaMasterViewModel.SocialMediaLink,
                        IsActive = Convert.ToBoolean(SocialMediaMasterViewModel.IsActive),
                        CreatedDate = System.DateTime.Now
                    };

                    var OutputResult = ObjSocialMediaMastersServices.AddSocialMedia(InsertSocialMedia);

                    TempData["Msg"] = OutputResult;
                    TempData["ClearForm"] = true;
                    return RedirectToPage();
                }

                SocialMediaMaster UpdateSocialMedia = new SocialMediaMaster()
                {
                    PlatformName = SocialMediaMasterViewModel.PlatformName,
                    SocialMediaLink = SocialMediaMasterViewModel.SocialMediaLink,
                    IsActive = Convert.ToBoolean(SocialMediaMasterViewModel.IsActive),
                    SocialMediaId= SocialMediaIdd
                };

                var OutputUpdateResult = ObjSocialMediaMastersServices.UpdateSocialMedia(UpdateSocialMedia);

                TempData["Msg"] = OutputUpdateResult;
                TempData["ClearForm"] = true;
                return RedirectToPage();




            }
            return RedirectToPage();


        }
    }
}
