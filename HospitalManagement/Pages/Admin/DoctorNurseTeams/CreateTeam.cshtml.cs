using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel.Admin.DoctorNurseTeam;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace HospitalManagement.Pages.Admin.Doctor_Nurse_Teams
{
    public class CreateTeamModel : PageModel
    {

        private readonly IDoctorNurseTeamService _DoctorNurseTeamService;
        private readonly IDepartmentTblServices _DepartmentTblServices;
        private readonly IDoctorAndNurseServices _DoctorAndNurseServices;

        [BindProperty]

        public DoctorNurseTeamViewModel DoctorNurseTeamViewModel { get; set; } = new DoctorNurseTeamViewModel();

        public List<DepartmentTbl> Departments { get; set; } = new List<DepartmentTbl>();


        public CreateTeamModel(IDoctorNurseTeamService _DoctorNurseTeamService, IDepartmentTblServices _DepartmentTblServices, IDoctorAndNurseServices _DoctorAndNurseServices)
        {
            this._DoctorNurseTeamService = _DoctorNurseTeamService;
            this._DepartmentTblServices = _DepartmentTblServices;
            this._DoctorAndNurseServices = _DoctorAndNurseServices;
        }

        public void OnGet()
        {
            Departments = _DepartmentTblServices.AllDepartment();
        }

        public JsonResult OnGetDoctorsByDepartmentAsync(int departmentId)
        {
            var doctors = _DoctorAndNurseServices.GetDoctorsByDepartment(departmentId);

            return new JsonResult(doctors);
        }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                DoctorNurseTeam InsertData = new DoctorNurseTeam()
                {
                    TeamName= DoctorNurseTeamViewModel.TeamName,
                    DoctorId= DoctorNurseTeamViewModel.DoctorId,
                    DepartmentId= DoctorNurseTeamViewModel.DepartmentId,
                    Description= DoctorNurseTeamViewModel.Description,
                    IsActive= DoctorNurseTeamViewModel.IsActive,
                    CreatedDate = DateTime.Now
                    
                };

                var Result = _DoctorNurseTeamService.CreateDoctorNurseTeam(InsertData);

                if (Result>=1)
                {
                    TempData["Msg"] = "Team Created Successfully.";
                    return RedirectToPage("/Admin/DoctorNurseTeams/TeamList");
                }

                return Page();
            }
            return Page();
        }
    }
}

