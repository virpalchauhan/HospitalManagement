using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using HospitalManagement.ViewModel.Admin.DoctorNurseTeam;
using HospitalManagement.ViewModel.Admin.DoctorNurseTeamMember;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HospitalManagement.Pages.Admin.DoctorNurseTeams
{
    public class ManageMembersModel : PageModel
    {
        [BindProperty]


        public AddTeamMemberViewModel AddTeamMemberViewModel { get; set; }

        [BindProperty]

        public DoctorNurseTeamListViewModel DoctorNurseTeamListViewModel { get; set; } = new DoctorNurseTeamListViewModel();

        public List<DoctorsAndNurse> NursesDrop { get; set; } = new List<DoctorsAndNurse>();

        public List<DoctorNurseTeamMemberInnerJoin> NurseList { get; set; } = new List<DoctorNurseTeamMemberInnerJoin>();


        [BindProperty(SupportsGet = true)]
        public int TeamId { get; set; }

        private readonly IDoctorNurseTeamService _DoctorNurseTeamService;
        private readonly IDoctorAndNurseServices _DoctorAndNurseServices;
        private readonly IDoctorNurseTeamMemberServices _DoctorNurseTeamMemberServices;


        public ManageMembersModel(IDoctorNurseTeamService _DoctorNurseTeamService, IDoctorAndNurseServices DoctorAndNurseServices, IDoctorNurseTeamMemberServices _DoctorNurseTeamMemberServices)
        {
            this._DoctorNurseTeamService = _DoctorNurseTeamService;
            this._DoctorAndNurseServices = DoctorAndNurseServices;
            this._DoctorNurseTeamMemberServices = _DoctorNurseTeamMemberServices;
        }



        public void OnGet()
        {
            var DoctorNurseTeamData = _DoctorNurseTeamService.DoctorNurseTeamSingleData(TeamId);

            if (DoctorNurseTeamData != null)
            {
                DoctorNurseTeamListViewModel.TeamName = DoctorNurseTeamData.TeamName;
                DoctorNurseTeamListViewModel.DoctorName = DoctorNurseTeamData.DoctorName;
                DoctorNurseTeamListViewModel.DepartmentName = DoctorNurseTeamData.DepartmentName;
                DoctorNurseTeamListViewModel.IsActive = DoctorNurseTeamData.IsActive;
                DoctorNurseTeamListViewModel.CreatedDate = DoctorNurseTeamData.CreatedDate;
                DoctorNurseTeamListViewModel.Description = DoctorNurseTeamData.Description;


            }

            NursesDrop = _DoctorAndNurseServices.GetAvailableNurses();

            NurseList= _DoctorNurseTeamMemberServices.TeamNurse(TeamId);

            
        }


        public IActionResult OnPost()
        {


            ModelState.Clear();
            if (TryValidateModel(AddTeamMemberViewModel,nameof(AddTeamMemberViewModel)))
            {
                DoctorNurseTeamMember AddNurse = new DoctorNurseTeamMember()
                {
                    NurseId = AddTeamMemberViewModel.NurseId,
                    IsActive=true,
                    JoinedDate=System.DateTime.Now,
                    TeamId= TeamId



                };

                var Result = _DoctorNurseTeamMemberServices.AddNurseTeamMember(AddNurse);

                if (Result>0)
                {
                    TempData["Msg"] = "Nurse Add Successfully";
                    return RedirectToPage("/Admin/DoctorNurseTeams/TeamList");
                }

                return Page();
            }
            return Page();



        }


        public JsonResult OnPostUpdateNursActivity(int TeamMemberId)
        {
            var result = _DoctorNurseTeamMemberServices.UpdateNursActivity(TeamMemberId);

            if (result>0)
            {
                return new JsonResult(new
                {
                    success = true,
                    message = "Nurse removed from team successfully."
                });
            }

            return new JsonResult(new
            {
                success = false,
                message = "Nurse could not be removed."
            });
        }

    }
}
