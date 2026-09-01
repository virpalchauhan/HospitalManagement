using HospitalManagement.Services;
using HospitalManagement.ViewModel.Admin.DoctorNurseTeam;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.DoctorNurseTeams
{
    public class TeamListModel : PageModel
    {
        private readonly IDoctorNurseTeamService _DoctorNurseTeamService;



     public List<DoctorNurseTeamListViewModel> DoctorNurseTeamListViewModelList { get; set; }
        public TeamListModel(IDoctorNurseTeamService _DoctorNurseTeamService)
        {
            this._DoctorNurseTeamService = _DoctorNurseTeamService;
        }

        public void OnGet()
        {
            DoctorNurseTeamListViewModelList = _DoctorNurseTeamService.DoctorNurseTeamList();
        }
    }
}
