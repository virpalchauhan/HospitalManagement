using HospitalManagement.EmailServices;
using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.DoctorApplications
{
    public class SingleDoctorApplicationsModel : PageModel
    {
        //[BindProperty]


        public DoctorApplicationView DoctorApplicationView { get; set; } = new DoctorApplicationView();

        //[BindProperty]


        public DoctorApplicationInnerJoin DoctorApplicationInnerJoin { get; set; } = new DoctorApplicationInnerJoin();

        [BindProperty]

        public DoctorApproveViewModel DoctorApproveViewModel { get; set; } = new DoctorApproveViewModel();

        private readonly IDoctorsServices ObjDoctorsServices;
        private readonly IDoctorApplicationservices ObjDoctorApplication;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly IDepartmentTblServices ObjDepartmentTblServices;
        private readonly EntityDbContext db;



        public SingleDoctorApplicationsModel(IDoctorApplicationservices ObjDoctorApplication, IDoctorsServices ObjDoctorsServices, IWebHostEnvironment WebHostEnvironment, IDepartmentTblServices ObjDepartmentTblServices, EntityDbContext db)
        {
            this.ObjDoctorApplication = ObjDoctorApplication;
            this.ObjDoctorsServices = ObjDoctorsServices;
            this.WebHostEnvironment = WebHostEnvironment;
            this.ObjDepartmentTblServices = ObjDepartmentTblServices;
            this.db = db;

        }

        public void OnGet(int id)
        {

            var Data = ObjDoctorApplication.SingleData(id);

            if (Data != null)
            {
                DoctorApplicationInnerJoin.FirstName = Data.FirstName;
                DoctorApplicationInnerJoin.LastName = Data.LastName;
                DoctorApplicationInnerJoin.Email = Data.Email;
                DoctorApplicationInnerJoin.MobileNo = Data.MobileNo;
                DoctorApplicationInnerJoin.Gender = Data.Gender;
                DoctorApplicationInnerJoin.DateOfBirth = Data.DateOfBirth;
                DoctorApplicationInnerJoin.DepartmentName = Data.DepartmentName;
                DoctorApplicationInnerJoin.ResumePath = Data.ResumePath;
                DoctorApplicationInnerJoin.ProfilePhotoPath = Data.ProfilePhotoPath;
                DoctorApproveViewModel.DoctorApplicationsId = id;
            }

        }

        //public IActionResult OnPostApprove(int id)
        //{


        //    return RedirectToPage(new { id = id });
        //}

        public IActionResult OnPostReject()
        {
            var result = ObjDoctorApplication.DoctorApplicationUpdate(DoctorApproveViewModel.DoctorApplicationsId, 2);
            if (result == 1)
            {
                return RedirectToPage("/Admin/DoctorApplications/AllDoctorApplications");
            }
            return Page();
        }

        public void OnPostFinalApprove()
        {
            int DoctorApplicationresult = 0;

            if (!ModelState.IsValid)
                return;

            using var transaction = db.Database.BeginTransaction();

            try
            {
                var Application = ObjDoctorApplication
                    .SingleData(DoctorApproveViewModel.DoctorApplicationsId);

                if (Application == null)
                    return;

                Doctors InsertDoctors = new Doctors()
                {
                    FirstName = Application.FirstName,
                    LastName = Application.LastName,
                    Gender = Application.Gender,
                    DateOfBirth = Application.DateOfBirth,
                    MobileNo = Application.MobileNo,
                    Email = Application.Email,
                    DepartmentId = Application.DepartmentId,
                    SalaryAmount = DoctorApproveViewModel.SalaryAmount,
                    JoiningDate = DoctorApproveViewModel.JoiningDate,
                    PasswordHash = "hello", 
                    AccountStatus = 1,
                    OfferLetterSent = true,
                    CreatedDate = DateTime.Now,
                    ProfilePhotoPath = Application.ProfilePhotoPath
                };

                int result = ObjDoctorsServices.AddDoctor(InsertDoctors);

                if (result != 1)
                {
                    transaction.Rollback();
                    return;
                }

                DoctorApplicationresult =
                    ObjDoctorApplication.DoctorApplicationUpdate(
                        DoctorApproveViewModel.DoctorApplicationsId, 1);

                if (DoctorApplicationresult != 1)
                {
                    transaction.Rollback();
                    return;
                }

                transaction.Commit();

                var DepartmentData =
                    ObjDepartmentTblServices.SingleDepartment(Application.DepartmentId);

                string path = Path.Combine(WebHostEnvironment.WebRootPath,
                                           "EmailTemplet",
                                           "DoctorActivationTemplet.html");

                string MailBody = System.IO.File.ReadAllText(path);

                MailBody = MailBody.Replace("{{FirstName}}", Application.FirstName);
                MailBody = MailBody.Replace("{{LastName}}", Application.LastName);
                MailBody = MailBody.Replace("{{Department}}", DepartmentData.DepartmentName);
                MailBody = MailBody.Replace("{{JoiningDate}}", DoctorApproveViewModel.JoiningDate.ToString());
                MailBody = MailBody.Replace("{{SalaryAmount}}", DoctorApproveViewModel.SalaryAmount.ToString());
                MailBody = MailBody.Replace("{{Email}}", Application.Email);
                MailBody = MailBody.Replace("{{Password}}", "Password");

                DoctorActivationTempletCode
                    .DoctorActivationTempletCodeSend(Application.Email, MailBody);
            }
            catch
            {
                transaction.Rollback();
            }
        }
    }
}


