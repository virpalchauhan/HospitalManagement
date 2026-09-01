using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Innerjoin;
using HospitalManagement.ViewModel.Admin.DoctorNurseTeam;
using System.Numerics;

namespace HospitalManagement.Services
{

    public interface IDoctorNurseTeamMemberServices
    {
        int AddNurseTeamMember(DoctorNurseTeamMember Model);

        List<DoctorNurseTeamMemberInnerJoin> TeamNurse(int TeamId);

        int UpdateNursActivity(int TeamMemberId);

        MyTeamModelInnerjoin GetDoctorMyTeam(int doctorId);

        SingleDoctorTeamInfo SingleDoctorTeamInfo(int doctorId);
        List<MultipulNurseByTeam> MultipulNurseByDoctorId(int DoctorId);

        SingleDoctorTeamInfo SingleDoctorTeamInforByNurse(int NurseId);

        List<MultipulNurseByTeam> MultipulNurseByNurseId(int nurseId);




    }




    public class DoctorNurseTeamMemberServices : IDoctorNurseTeamMemberServices, IDisposable
    {

        private readonly EntityDbContext _EntityDbContext;

        public DoctorNurseTeamMemberServices(EntityDbContext _EntityDbContext)
        {
            this._EntityDbContext = _EntityDbContext;
        }

        public int AddNurseTeamMember(DoctorNurseTeamMember Model)
        {
            _EntityDbContext.DoctorNurseTeamMembers.Add(Model);
            int Count = _EntityDbContext.SaveChanges();

            if (Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _EntityDbContext.Dispose();
        }

        public MyTeamModelInnerjoin GetDoctorMyTeam(int doctorId)
        {
            var SingleData = (from DoctorTeam in _EntityDbContext.DoctorNurseTeams
                              join Nurse in _EntityDbContext.DoctorNurseTeamMembers
                              on DoctorTeam.TeamId equals Nurse.TeamId

                              join Department in _EntityDbContext.DepartmentTbls
                              on DoctorTeam.DepartmentId equals Department.DepartmentId


                              select new MyTeamModelInnerjoin
                              {

                              }


                             ).SingleOrDefault();

            return SingleData;
        }

        public List<MultipulNurseByTeam> MultipulNurseByDoctorId(int doctorId)
        {
            var list =
                (from team in _EntityDbContext.DoctorNurseTeams

                 join member in _EntityDbContext.DoctorNurseTeamMembers
                 on team.TeamId equals member.TeamId

                 join nurse in _EntityDbContext.DoctorsAndNurses
                 on member.NurseId equals nurse.DoctorNurceId

                 where team.DoctorId == doctorId
                       //&& member.IsActive == true

                 select new MultipulNurseByTeam
                 {
                     TeamMemberId = member.TeamMemberId,

                     NurseId = nurse.DoctorNurceId,

                     NurseName = nurse.FirstName + " " + nurse.LastName,

                     CreateDate = member.JoinedDate,

                     IsActive = member.IsActive

                 }).ToList();

            return list;
        }

        public List<MultipulNurseByTeam> MultipulNurseByNurseId(int nurseId)
        {
            var list =
                (from loggedInNurseMember in _EntityDbContext.DoctorNurseTeamMembers

                 join member in _EntityDbContext.DoctorNurseTeamMembers
                 on loggedInNurseMember.TeamId equals member.TeamId

                 join nurse in _EntityDbContext.DoctorsAndNurses
                 on member.NurseId equals nurse.DoctorNurceId

                 where loggedInNurseMember.NurseId == nurseId
                       && loggedInNurseMember.IsActive == true
                       && member.IsActive == true

                 select new MultipulNurseByTeam
                 {
                     TeamMemberId = member.TeamMemberId,

                     NurseId = nurse.DoctorNurceId,

                     NurseName = nurse.FirstName + " " + nurse.LastName,

                     CreateDate = member.JoinedDate,

                     IsActive = member.IsActive,
                     RollType = nurse.RollType
                 })
                .ToList();

            return list;
        }

        public SingleDoctorTeamInfo SingleDoctorTeamInfo(int doctorId)
        {
            var SingleData = (from Team in _EntityDbContext.DoctorNurseTeams
                                  

                              join Doctor in _EntityDbContext.DoctorsAndNurses
                              on Team.DoctorId equals Doctor.DoctorNurceId

                              join Department in _EntityDbContext.DepartmentTbls
                              on Team.DepartmentId equals Department.DepartmentId

                              where Team.DoctorId == doctorId


                              select new SingleDoctorTeamInfo
                              {
                                  DoctorName = Doctor.FirstName + "  " + Doctor.LastName,
                                  TeamName = Team.TeamName,
                                  CreateDate = Team.CreatedDate,
                                  DepartmentName = Department.DepartmentName,
                                  IsActive = Team.IsActive





                              }
                       ).SingleOrDefault();
            return SingleData;
        }

        public SingleDoctorTeamInfo SingleDoctorTeamInforByNurse(int NurseId)
        {
            var SingleData = (from TeamMember in _EntityDbContext.DoctorNurseTeamMembers
                              join Team in _EntityDbContext.DoctorNurseTeams
                              on TeamMember.TeamId equals Team.TeamId

                              join Doctor in _EntityDbContext.DoctorsAndNurses
                              on Team.DoctorId equals Doctor.DoctorNurceId

                              join Department in _EntityDbContext.DepartmentTbls
                              on Team.DepartmentId equals Department.DepartmentId

                              where TeamMember.NurseId == NurseId

                              select new SingleDoctorTeamInfo
                              {
                                  DoctorName = Doctor.FirstName + "  " + Doctor.LastName,
                                  TeamName = Team.TeamName,
                                  CreateDate = Team.CreatedDate,
                                  DepartmentName = Department.DepartmentName,
                                  IsActive = Team.IsActive
                                  


                              }).SingleOrDefault();
            return SingleData;
        }

        public List<DoctorNurseTeamMemberInnerJoin> TeamNurse(int TeamId)
        {
            var List = (from Nurse in _EntityDbContext.DoctorsAndNurses
                        join Member in _EntityDbContext.DoctorNurseTeamMembers
                        on Nurse.DoctorNurceId equals Member.NurseId
                        where Member.TeamId == TeamId && Member.IsActive == true

                        select new DoctorNurseTeamMemberInnerJoin
                        {
                            Name = Nurse.FirstName + "  " + Nurse.LastName,
                            JoinedDate = Member.JoinedDate,
                            TeamMemberId = Member.TeamMemberId



                        }
                       ).ToList();

            return List;
        }

        public int UpdateNursActivity(int TeamMemberId)
        {
            var Data = _EntityDbContext.DoctorNurseTeamMembers.Where(m => m.TeamMemberId == TeamMemberId).FirstOrDefault();

            if (Data != null)
            {
                Data.IsActive = false;
                int Count = _EntityDbContext.SaveChanges();

                if (Count > 0)
                {
                    return 1;
                }
                return 0;
            }
            return 0;
        }
    }
}
