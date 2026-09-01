using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace HospitalManagement.Pages.Admin.DoctorNurseTeams
{
    public class MyTeamModel : PageModel
    {
        public string CookieRollType { get; set; }

        public string CookieDoctor { get; set; }
        public string CookieNurse { get; set; }

        public SingleDoctorTeamInfo SingleDoctorTeamInfo { get; set; }

        public List<MultipulNurseByTeam> MultipulNurseByTeam { get; set; }

        private readonly IDoctorNurseTeamMemberServices _DoctorNurseTeamMemberServices;

        public MyTeamModel(IDoctorNurseTeamMemberServices _DoctorNurseTeamMemberServices)
        {
            this._DoctorNurseTeamMemberServices= _DoctorNurseTeamMemberServices;
        }


        public void OnGet()
        {
            CookieRollType = User.FindFirst("RollType")?.Value;

            if (CookieRollType == "Doctor")
            {
                CookieDoctor = User.FindFirst("DoctorNurceId")?.Value;


                SingleDoctorTeamInfo = _DoctorNurseTeamMemberServices.SingleDoctorTeamInfo(Convert.ToInt32(CookieDoctor));

                MultipulNurseByTeam = _DoctorNurseTeamMemberServices.MultipulNurseByDoctorId(Convert.ToInt32(CookieDoctor));



            }
            else if (CookieRollType == "Nurse")
            {
                CookieNurse = User.FindFirst("DoctorNurceId")?.Value;

                SingleDoctorTeamInfo = _DoctorNurseTeamMemberServices.SingleDoctorTeamInforByNurse(Convert.ToInt32(CookieNurse));

                MultipulNurseByTeam = _DoctorNurseTeamMemberServices.MultipulNurseByNurseId(Convert.ToInt32(CookieNurse));
            }
        }


    }
}
