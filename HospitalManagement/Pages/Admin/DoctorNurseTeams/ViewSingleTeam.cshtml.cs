using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using HospitalManagement.ViewModel.Admin.DoctorNurseTeam;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.DoctorNurseTeams
{
    public class ViewSingleTeamModel : PageModel
    {

        private readonly IDoctorNurseTeamService _DoctorNurseTeamService;
        private readonly IDoctorNurseTeamMemberServices _DoctorNurseTeamMemberService;

        [BindProperty]

        public DoctorNurseTeamListViewModel DoctorNurseTeamListViewModel { get; set; } = new DoctorNurseTeamListViewModel();


        [BindProperty(SupportsGet = true)]
        public int TeamId { get; set; }

        public List<DoctorNurseTeamMemberInnerJoin> NurseList { get; set; } = new List<DoctorNurseTeamMemberInnerJoin>();



        public ViewSingleTeamModel(IDoctorNurseTeamService _DoctorNurseTeamService, IDoctorNurseTeamMemberServices _DoctorNurseTeamMemberService)
        {
            this._DoctorNurseTeamService = _DoctorNurseTeamService;
            this._DoctorNurseTeamMemberService = _DoctorNurseTeamMemberService;
        }


        public void OnGet()
        {
            var DoctorNurseTeamData = _DoctorNurseTeamService.DoctorNurseTeamSingleData(TeamId);

            if (DoctorNurseTeamData!=null)
            {
                DoctorNurseTeamListViewModel.TeamName=DoctorNurseTeamData.TeamName;
                DoctorNurseTeamListViewModel.DoctorName = DoctorNurseTeamData.DoctorName;
                DoctorNurseTeamListViewModel.DepartmentName = DoctorNurseTeamData.DepartmentName;
                DoctorNurseTeamListViewModel.IsActive=DoctorNurseTeamData.IsActive;
                DoctorNurseTeamListViewModel.CreatedDate=DoctorNurseTeamData.CreatedDate;
                DoctorNurseTeamListViewModel.Description=DoctorNurseTeamData.Description;


            }
            NurseList = _DoctorNurseTeamMemberService.TeamNurse(TeamId);

        }
    }
}
