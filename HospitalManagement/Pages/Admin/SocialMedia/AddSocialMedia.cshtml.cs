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

                if (SocialMediaIdd <= 0)
                {
                    SocialMediaMaster InsertSocialMedia = new SocialMediaMaster()
                    {
                        PlatformName = SocialMediaMasterViewModel.PlatformName,
                        SocialMediaLink = SocialMediaMasterViewModel.SocialMediaLink,
                        IsActive = Convert.ToBoolean(SocialMediaMasterViewModel.IsActive),
                        CreatedDate = System.DateTime.Now
                    };

                    var Result = ObjSocialMediaMastersServices.AddSocialMedia(InsertSocialMedia);

                    if (Result == 1)
                    {
                        TempData["MsgSuccess"] =
                            "Social Media Platform Added Successfully";
                    }
                    else if (Result == 2)
                    {
                        TempData["MsgNormal"] =
                            "This Social Media Platform Already Exists";
                    }
                    else
                    {
                        TempData["MsgDanger"] =
                            "Failed To Add Social Media Platform";
                    }
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

                if (OutputUpdateResult == 1)
                {
                    TempData["MsgSuccess"] =
                        "Social Media Platform Updated Successfully";
                }
                else if (OutputUpdateResult == 2)
                {
                    TempData["MsgNormal"] =
                        "This Social Media Platform Already Exists";
                }
                else if (OutputUpdateResult == 0)
                {
                    TempData["MsgNormal"] =
                        "Record Not Found";
                }
                else
                {
                    TempData["MsgDanger"] =
                        "Failed To Update Social Media Platform";
                }
                TempData["ClearForm"] = true;
                return RedirectToPage();




            }
            return RedirectToPage();


        }
    }
}
