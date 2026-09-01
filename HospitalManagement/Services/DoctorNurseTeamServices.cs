using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.ViewModel.Admin.DoctorNurseTeam;

namespace HospitalManagement.Services
{

    public interface IDoctorNurseTeamService
    {
       int CreateDoctorNurseTeam(DoctorNurseTeam Model);

        List<DoctorNurseTeamListViewModel> DoctorNurseTeamList();

        DoctorNurseTeamListViewModel DoctorNurseTeamSingleData (int TeamId);




    }


    public class DoctorNurseTeamServices: IDoctorNurseTeamService,IDisposable
    {

        private readonly EntityDbContext _EntityDbContext;

        public DoctorNurseTeamServices(EntityDbContext _EntityDbContext)
        {
            this._EntityDbContext = _EntityDbContext;
        }

        public int CreateDoctorNurseTeam(DoctorNurseTeam Model)
        {
           _EntityDbContext.DoctorNurseTeams.Add(Model);
              return _EntityDbContext.SaveChanges();
        }

        public void Dispose()
        {
            
            _EntityDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<DoctorNurseTeamListViewModel> DoctorNurseTeamList()
        {
            var List= from Team in _EntityDbContext.DoctorNurseTeams
                      join Doctor in _EntityDbContext.DoctorsAndNurses
                      on Team.DoctorId equals Doctor.DoctorNurceId

                      join Department in _EntityDbContext.DepartmentTbls
                      on Team.DepartmentId equals Department.DepartmentId 

                      select new DoctorNurseTeamListViewModel
                      {
                          TeamId= Team.TeamId,
                          TeamName= Team.TeamName,
                          DoctorId= Doctor.DoctorNurceId,
                          DoctorName= Doctor.FirstName + " " + Doctor.LastName,
                          DepartmentId= Department.DepartmentId,
                          DepartmentName= Department.DepartmentName,
                          Description= Team.Description,
                          IsActive= Team.IsActive,
                          CreatedDate= Team.CreatedDate
                      };

            return List.ToList();
        }

        public DoctorNurseTeamListViewModel DoctorNurseTeamSingleData(int TeamId)
        {
            var List = from Team in _EntityDbContext.DoctorNurseTeams
                       join Doctor in _EntityDbContext.DoctorsAndNurses
                       on Team.DoctorId equals Doctor.DoctorNurceId

                       join Department in _EntityDbContext.DepartmentTbls
                       on Team.DepartmentId equals Department.DepartmentId
                       where Team.TeamId == TeamId

                       select new DoctorNurseTeamListViewModel
                       {
                           TeamId = Team.TeamId,
                           TeamName = Team.TeamName,
                           DoctorId = Doctor.DoctorNurceId,
                           DoctorName = Doctor.FirstName + " " + Doctor.LastName,
                           DepartmentId = Department.DepartmentId,
                           DepartmentName = Department.DepartmentName,
                           Description = Team.Description,
                           IsActive = Team.IsActive,
                           CreatedDate = Team.CreatedDate
                       };

            return List.FirstOrDefault();
        }
    }
}
