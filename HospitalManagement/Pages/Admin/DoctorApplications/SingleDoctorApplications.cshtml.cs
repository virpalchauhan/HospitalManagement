using HospitalManagement.EmailServices;
using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagement.Pages.Admin.DoctorApplications
{
    public class SingleDoctorApplicationsModel : PageModel
    {
        //[BindProperty]


        public DoctorNurseApplicationsView DoctorApplicationView { get; set; } = new DoctorNurseApplicationsView();

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

        public string GenerateSecurePassword(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$!";
            var result = new StringBuilder();
            var bytes = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            foreach (var b in bytes)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
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
                DoctorApplicationInnerJoin.ApplicationStatus = Data.ApplicationStatus;
                DoctorApplicationInnerJoin.RollType = Data.RollType;


            }

        }   

        public IActionResult OnPostReject()
        {

            using var transaction = db.Database.BeginTransaction();

            try
            {       
            var result = ObjDoctorApplication.DoctorApplicationUpdate(DoctorApproveViewModel.DoctorApplicationsId, (int)ApplicationStatusType.Reject);
            if (result != 1)
            {
                  return  RedirectToPage();
            }
                var Application = ObjDoctorApplication.SingleData(DoctorApproveViewModel.DoctorApplicationsId);

                if (Application==null)
                {
                    return RedirectToPage();
                }

                var DepartmentData =ObjDepartmentTblServices.SingleDepartment(Application.DepartmentId);

                if (DepartmentData == null)
                {
                    return RedirectToPage();
                }

                //transaction.Commit();

                string path = Path.Combine(WebHostEnvironment.WebRootPath,"EmailTemplet","DoctorApplicationRejectTemplet.html");

                string MailBody = System.IO.File.ReadAllText(path);

                MailBody = MailBody.Replace("{{FirstName}}", Application.FirstName);
                MailBody = MailBody.Replace("{{LastName}}", Application.LastName);
                MailBody = MailBody.Replace("{{DepartmentName}}", DepartmentData.DepartmentName);
                MailBody = MailBody.Replace("{{ApplicationDate}}", Application.RequestDate.ToString("dd MMM yyyy"));


              
                try
                {
                    DoctorApplicationRejectTempletCode
                        .DoctorApplicationRejectTempletCodeSend(Application.Email, MailBody);
                }
                catch (Exception ex)
                {
                  
                    Console.WriteLine("Email sending failed: " + ex.Message);
                }

                TempData["Msg"] = "Doctor Application Rejected";
                transaction.Commit();
                return RedirectToPage("AllDoctorApplications");

                
            }
            catch (Exception)
            {

                transaction.Rollback();
                throw;
            }
        }

        public IActionResult OnPostFinalApprove()
        {

            string TempPassword = GenerateSecurePassword();
            int DoctorApplicationresult = 0;

            if (!ModelState.IsValid)
                return RedirectToPage();

            using var transaction = db.Database.BeginTransaction();

            try
            {
                var Application = ObjDoctorApplication
                    .SingleData(DoctorApproveViewModel.DoctorApplicationsId);

                if (Application == null)
                    return RedirectToPage();

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
                    PasswordHash = TempPassword,
                    AccountStatus = 1,
                    OfferLetterSent = true,
                    CreatedDate = DateTime.Now,
                    ProfilePhotoPath = Application.ProfilePhotoPath
                };

                int result = ObjDoctorsServices.AddDoctor(InsertDoctors);

                if (result != 1)
                {
                    throw new Exception("Doctor insert failed");
                }

                DoctorApplicationresult =
                    ObjDoctorApplication.DoctorApplicationUpdate(
                        DoctorApproveViewModel.DoctorApplicationsId, 1);

                if (DoctorApplicationresult != 1)
                {
                    throw new Exception("Application status update failed");
                }

                transaction.Commit();

                var DepartmentData =
                    ObjDepartmentTblServices.SingleDepartment(Application.DepartmentId);

                if (DepartmentData == null)
                {
                    throw new Exception("Department not found");
                }

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
                MailBody = MailBody.Replace("{{Password}}", TempPassword);

                if (DoctorActivationTempletCode.DoctorActivationTempletCodeSend(Application.Email, MailBody))
                {
                    TempData["Msg"] = "Doctor Application Approw";
                    return RedirectToPage("AllAcceptDoctorApplications");
                }

                else
                {
                    TempData["Msg"] = "Something Wrong";
                    return RedirectToPage("AllAcceptDoctorApplications");
                }



               
            }
            catch
            {
                transaction.Rollback();
                throw;

            }

           
        }
    }
}


