using HospitalManagement.EmailServices;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagement.Pages.Client.Careers
{
    public class CareersNurseModel : PageModel
    {


        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices;
        private readonly IDepartmentTblServices ObjDepartmentTbl;

        public CareersNurseModel(IWebHostEnvironment webHostEnvironment, IDoctorNurseApplicationServices ObjDoctorNurseApplicationServices, IDepartmentTblServices ObjDepartmentTbl)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.ObjDoctorNurseApplicationServices = ObjDoctorNurseApplicationServices;
            this.ObjDepartmentTbl = ObjDepartmentTbl;
        }


        [BindProperty]

        public List<SelectListItem> DepartmentList { get; set; }

        [BindProperty]

        public DoctorNurseApplicationsView DoctorNurseApplicationsView { get; set; }


        public void OnGet()
        {
            var deptData = ObjDepartmentTbl.AllDepartment();

            DepartmentList = deptData.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            }).ToList();
        }


        public IActionResult Onpost()
        {
            if (ModelState.IsValid)
            {
                var ResumePath = "";
                var ProfilePath = "";


                if (DoctorNurseApplicationsView.ProfilePhoto !=null && DoctorNurseApplicationsView.ProfilePhoto.Length>0)
                {
                    using FileStream fs = new FileStream(Path.Combine(webHostEnvironment.WebRootPath, "Client/ProfilePhoto/", DoctorNurseApplicationsView.ProfilePhoto.FileName), FileMode.Create);
                    DoctorNurseApplicationsView.ProfilePhoto.CopyTo(fs);
                    fs.Close();

                    ProfilePath= Path.Combine("Client/ProfilePhoto/", DoctorNurseApplicationsView.ProfilePhoto.FileName);
                }

                if (DoctorNurseApplicationsView.Resume!=null && DoctorNurseApplicationsView.Resume.Length>0)
                {
                    using FileStream fs = new FileStream(Path.Combine(webHostEnvironment.WebRootPath, "Client/ResumePhoto/", DoctorNurseApplicationsView.Resume.FileName), FileMode.Create);
                    DoctorNurseApplicationsView.Resume.CopyTo(fs);
                    fs.Close();
                    ResumePath=Path.Combine("Client/ResumePhoto/", DoctorNurseApplicationsView.Resume.FileName);
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
                    RollType = DoctorNurseApplicationsRollType.Nurse



                };

                int Result = ObjDoctorNurseApplicationServices.AddDoctorNurseApplications(InsertDoctorData);


                if (Result==1)
                {
                    TempData["Msg"] = "Your application has been submitted successfully! Thank you for your interest in joining our hospital. We will review your profile and inform you about the next steps soon.";
                    TempData["ClearForm"] = true;


                    string path = Path.Combine(webHostEnvironment.WebRootPath, "EmailTemplet", "DoctorApplicationSubmittedEmail.html");
                    string MailBody = System.IO.File.ReadAllText(path);
                    MailBody = MailBody.Replace("{{FirstName}}", DoctorNurseApplicationsView.FirstName);
                    MailBody = MailBody.Replace("{{LastName}}", DoctorNurseApplicationsView.LastName);
                    MailBody = MailBody.Replace("{{DepartmentName}}", ObjDepartmentTbl.SingleDepartment(DoctorNurseApplicationsView.DepartmentId).DepartmentName);
                    MailBody = MailBody.Replace("{{ApplicationDate}}", System.DateTime.Now.ToString());


                    DoctorApplicationSubmittedEmailCode.DoctorApplicationSubmittedEmailTempletCodeSend(DoctorNurseApplicationsView.Email, MailBody);

                    return RedirectToPage();
                }
                else if (Result == 2)
                {
                    TempData["Msg"] = "You have already applied using this email address.";
                    TempData["ClearForm"] = true;
                    return RedirectToPage();
                }


                TempData["Msg"] = "SomeThing Wrong.";
                TempData["ClearForm"] = true;
                return RedirectToPage();


            }
            return RedirectToPage();


        }


            }
        }
    
