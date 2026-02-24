using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
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
        private readonly IDoctorApplicationservices ObjDoctorTable;


        [BindProperty]

        public List<SelectListItem> DepartmentList { get; set; }

        [BindProperty]

        public DoctorApplicationView DoctorTableView { get; set; }

        public string ProfilePath { get; set; }




        public CareersDoctorModel(IWebHostEnvironment webHostEnvironment, IDepartmentTblServices ObjDepartmentTbl, IDoctorApplicationservices ObjDoctorTable)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.ObjDepartmentTbl = ObjDepartmentTbl;
            this.ObjDoctorTable = ObjDoctorTable;
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

                if (DoctorTableView.ProfilePhoto!=null && DoctorTableView.ProfilePhoto.Length>0)
                {
                    using FileStream fs = new FileStream(Path.Combine(webHostEnvironment.WebRootPath, "Client/ProfilePhoto/", DoctorTableView.ProfilePhoto.FileName), FileMode.Create);
                    DoctorTableView.ProfilePhoto.CopyTo(fs);
                    fs.Close();
                    ProfilePath = "Client/ProfilePhoto/" + DoctorTableView.ProfilePhoto.FileName;

                }
                if (DoctorTableView.Resume!=null && DoctorTableView.Resume.Length>0)
                {
                    using FileStream fs = new FileStream(Path.Combine(webHostEnvironment.WebRootPath, "Client/ResumePhoto/", DoctorTableView.Resume.FileName), FileMode.Create);
                    DoctorTableView.Resume.CopyTo(fs);
                    fs.Close();
                    ResumePath = "Client/ResumePhoto/" + DoctorTableView.Resume.FileName;
                }



                DoctorApplication InsertDoctorData = new DoctorApplication()
                {
                    FirstName = DoctorTableView.FirstName,
                    LastName = DoctorTableView.LastName,
                    Gender = DoctorTableView.Gender,
                    DateOfBirth = DoctorTableView.DateOfBirth,
                    MobileNo = DoctorTableView.MobileNo,
                    Email = DoctorTableView.Email,
                    DepartmentId = DoctorTableView.DepartmentId,
                    ProfilePhotoPath = ProfilePath,
                    ResumePath = ResumePath,                  
                    RequestDate = System.DateTime.Now,
                    ApplicationStatus = 0,
                  


                };
                int Result = ObjDoctorTable.AddDoctor(InsertDoctorData);

                if (Result==1)
                {
                    TempData["Msg"] = "Your application has been submitted successfully! Thank you for your interest in joining our hospital. We will review your profile and inform you about the next steps soon.";
                    TempData["ClearForm"] = true;
                    return RedirectToPage();
                }
                else if(Result==2)
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
