using HospitalManagement.EmailServices;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;

namespace HospitalManagement.Pages.Client.Careers
{
    public class CareersDoctorModel : PageModel
    {


        private readonly IDepartmentTblServices ObjDepartmentTbl;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices;
        private readonly DoctorApplicationSubmittedEmailCode _DoctorNurseApplicationSubmittedEmailCode;


        [BindProperty]

        public List<SelectListItem> DepartmentList { get; set; }

        [BindProperty]

        public DoctorNurseApplicationsView DoctorNurseApplicationsView { get; set; }

        public string ProfilePath { get; set; }




        public CareersDoctorModel(IWebHostEnvironment webHostEnvironment, IDepartmentTblServices ObjDepartmentTbl, IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices, DoctorApplicationSubmittedEmailCode _DoctorNurseApplicationSubmittedEmailCode)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.ObjDepartmentTbl = ObjDepartmentTbl;
            this.ObjDoctorNurseApplicationServices = ObjDoctorNurseApplicationServices;
            this._DoctorNurseApplicationSubmittedEmailCode = _DoctorNurseApplicationSubmittedEmailCode;
        }
        public void OnGet()
        {
            var deptData = ObjDepartmentTbl.AllDepartment();

            DepartmentList = deptData.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

              
                var ResumePath = "";
                var ProfilePath = "";

                if (DoctorNurseApplicationsView.ProfilePhoto!=null && DoctorNurseApplicationsView.ProfilePhoto.Length>0)
                {
                    using FileStream fs = new FileStream(Path.Combine(webHostEnvironment.WebRootPath, "Client/ProfilePhoto/", DoctorNurseApplicationsView.ProfilePhoto.FileName), FileMode.Create);
                    DoctorNurseApplicationsView.ProfilePhoto.CopyTo(fs);
                    fs.Close();
                    ProfilePath = "Client/ProfilePhoto/" + DoctorNurseApplicationsView.ProfilePhoto.FileName;

                }
                if (DoctorNurseApplicationsView.Resume!=null && DoctorNurseApplicationsView.Resume.Length>0)
                {
                    using FileStream fs = new FileStream(Path.Combine(webHostEnvironment.WebRootPath, "Client/ResumePhoto/", DoctorNurseApplicationsView.Resume.FileName), FileMode.Create);
                    DoctorNurseApplicationsView.Resume.CopyTo(fs);
                    fs.Close();
                    ResumePath = "Client/ResumePhoto/" + DoctorNurseApplicationsView.Resume.FileName;
                }



                DoctorNurseApplication InsertDoctorData = new DoctorNurseApplication()
                {
                    FirstName = DoctorNurseApplicationsView.FirstName,
                    LastName = DoctorNurseApplicationsView.LastName,
                    Gender = DoctorNurseApplicationsView.Gender,
                    DateOfBirth = DoctorNurseApplicationsView.DateOfBirth,
                    MobileNo = DoctorNurseApplicationsView.MobileNo,
                    Email = DoctorNurseApplicationsView.Email,
                    DepartmentId = DoctorNurseApplicationsView.DepartmentId,
                    ProfilePhotoPath = ProfilePath,
                    ResumePath = ResumePath,                  
                    RequestDate = System.DateTime.Now,
                    ApplicationStatus = 0,
                    RollType= DoctorNurseApplicationsRollType.Doctor



                };
                int Result = ObjDoctorNurseApplicationServices.AddDoctorNurseApplications(InsertDoctorData);

                if (Result==1)
                {
                    TempData["MsgSuccess"] =
                        "Your Application Has Been Submitted Successfully! Thank You For Your Interest In Joining Our Hospital. We Will Review Your Profile And Inform You About The Next Steps Soon.";
                    TempData["ClearForm"] = true;

                    string path = Path.Combine(webHostEnvironment.WebRootPath, "EmailTemplet", "DoctorApplicationSubmittedEmail.html");
                    string MailBody = System.IO.File.ReadAllText(path);
                    MailBody = MailBody.Replace("{{FirstName}}", DoctorNurseApplicationsView.FirstName);
                    MailBody = MailBody.Replace("{{LastName}}", DoctorNurseApplicationsView.LastName);
                    MailBody=MailBody.Replace("{{DepartmentName}}", ObjDepartmentTbl.SingleDepartment(DoctorNurseApplicationsView.DepartmentId).DepartmentName);
                    MailBody=MailBody.Replace("{{ApplicationDate}}", System.DateTime.Now.ToString());


                    _DoctorNurseApplicationSubmittedEmailCode.DoctorApplicationSubmittedEmailTempletCodeSend(DoctorNurseApplicationsView.Email, MailBody);

                    return RedirectToPage();
                }
                else if(Result==2)
                {
                    TempData["MsgNormal"] =
    "You Have Already Applied Using This Email Address.";
                    TempData["ClearForm"] = true;
                    return RedirectToPage();
                }


                TempData["MsgDanger"] =
     "Something Went Wrong.";
                TempData["ClearForm"] = true;
                return RedirectToPage();


            }
            return RedirectToPage();
        }

    }
}
