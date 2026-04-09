using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;
using Microsoft.IdentityModel.Tokens;

namespace HospitalManagement.Services
{


    public interface IDoctorNurseApplicationServices
    {

        int AddDoctorNurseApplications(DoctorNurseApplication Model);


        List<DoctorNurseApplicationInnerJoin> AllPendingDoctorApplications();
        List<DoctorNurseApplicationInnerJoin> AllRejectDoctorApplications();
        List<DoctorNurseApplicationInnerJoin> AllAcceptDoctorApplications();
        List<DoctorNurseApplicationInnerJoin> AlDoctorApplications();

        DoctorNurseApplicationInnerJoin SingleData(int Id);

        int DoctorApplicationUpdate(int id, byte ApplicationsStatusId);





    }
    public class DoctorNurseApplicationServices : IDoctorNurseApplicationServices, IDisposable
    {

        private readonly EntityDbContext db;
        public DoctorNurseApplicationServices(EntityDbContext db)
        {
            this.db = db;

        }

        public int AddDoctorNurseApplications(DoctorNurseApplication Model)
        {
            bool DoctorExists = db.doctorNurseApplications.Any(m => m.Email == Model.Email);
            if (DoctorExists)
            {
                return 2;
            }
            db.doctorNurseApplications.Add(Model);
            int Count = db.SaveChanges();
            if (Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public List<DoctorNurseApplicationInnerJoin> AlDoctorApplications()
        {
            var Data = (from d in db.doctorNurseApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                       
                        select new DoctorNurseApplicationInnerJoin
                        {

                            FirstName = d.FirstName,
                            LastName = d.LastName,
                            Gender = d.Gender,
                            DateOfBirth = d.DateOfBirth,
                            MobileNo = d.MobileNo,
                            Email = d.Email,
                            DepartmentName = dept.DepartmentName,
                            ProfilePhotoPath = d.ProfilePhotoPath,
                            ResumePath = d.ResumePath,
                            RequestDate = d.RequestDate,
                            DoctorApplicationsId = d.DoctorApplicationsId,
                            ApplicationStatus = d.ApplicationStatus,
                            RollType = d.RollType
                        }).ToList();

            return Data;
        }

        public List<DoctorNurseApplicationInnerJoin> AllAcceptDoctorApplications()
        {
            var Data = (from d in db.doctorNurseApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                        where d.ApplicationStatus == ApplicationStatusType.Accept
                       
                        select new DoctorNurseApplicationInnerJoin
                        {

                            FirstName = d.FirstName,
                            LastName = d.LastName,
                            Gender = d.Gender,
                            DateOfBirth = d.DateOfBirth,
                            MobileNo = d.MobileNo,
                            Email = d.Email,
                            DepartmentName = dept.DepartmentName,
                            ProfilePhotoPath = d.ProfilePhotoPath,
                            ResumePath = d.ResumePath,
                            RequestDate = d.RequestDate,
                            DoctorApplicationsId = d.DoctorApplicationsId,
                            ApplicationStatus = d.ApplicationStatus,
                            RollType = d.RollType
                        }).ToList();

            return Data;
        }

        public List<DoctorNurseApplicationInnerJoin> AllPendingDoctorApplications()
        {
            //return db.doctorApplications.Where(m => m.ApplicationStatus == 0).ToList();


            var Data = (from d in db.doctorNurseApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                        where d.ApplicationStatus == ApplicationStatusType.Pending 
                      
                        select new DoctorNurseApplicationInnerJoin
                        {

                            FirstName = d.FirstName,
                            LastName = d.LastName,
                            Gender = d.Gender,
                            DateOfBirth = d.DateOfBirth,
                            MobileNo = d.MobileNo,
                            Email = d.Email,
                            DepartmentName = dept.DepartmentName,
                            ProfilePhotoPath = d.ProfilePhotoPath,
                            ResumePath = d.ResumePath,
                            RequestDate = d.RequestDate,
                            DoctorApplicationsId = d.DoctorApplicationsId,
                            ApplicationStatus = d.ApplicationStatus,
                            RollType = d.RollType
                        }).ToList();

            return Data;
        }

        public List<DoctorNurseApplicationInnerJoin> AllRejectDoctorApplications()
        {
            var Data = (from d in db.doctorNurseApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                        where d.ApplicationStatus == ApplicationStatusType.Reject
                       
                        select new DoctorNurseApplicationInnerJoin
                        {

                            FirstName = d.FirstName,
                            LastName = d.LastName,
                            Gender = d.Gender,
                            DateOfBirth = d.DateOfBirth,
                            MobileNo = d.MobileNo,
                            Email = d.Email,
                            DepartmentName = dept.DepartmentName,
                            ProfilePhotoPath = d.ProfilePhotoPath,
                            ResumePath = d.ResumePath,
                            RequestDate = d.RequestDate,
                            DoctorApplicationsId = d.DoctorApplicationsId,
                            ApplicationStatus = d.ApplicationStatus,
                            RollType=d.RollType
                        }).ToList();

            return Data;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            db.Dispose();
        }

        public int DoctorApplicationUpdate(int id, byte ApplicationsStatusId)
        {
            var Data = db.doctorNurseApplications.Where(m => m.DoctorApplicationsId == id).FirstOrDefault();

            if (Data != null)
            {
                Data.ApplicationStatus = (ApplicationStatusType)ApplicationsStatusId;

                int Count = db.SaveChanges();
                if (Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        public DoctorNurseApplicationInnerJoin SingleData(int Id)
        {
            var Data = (from d in db.doctorNurseApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                        select new DoctorNurseApplicationInnerJoin
                        {

                            FirstName = d.FirstName,
                            LastName = d.LastName,
                            Gender = d.Gender,
                            DateOfBirth = d.DateOfBirth,
                            MobileNo = d.MobileNo,
                            Email = d.Email,
                            DepartmentName = dept.DepartmentName,
                            ProfilePhotoPath = d.ProfilePhotoPath,
                            ResumePath = d.ResumePath,
                            RequestDate = d.RequestDate,
                            DoctorApplicationsId = d.DoctorApplicationsId,
                            ApplicationStatus = d.ApplicationStatus,
                            DepartmentId = d.DepartmentId
                        }).Where(m => m.DoctorApplicationsId == Id).FirstOrDefault();

            return Data;
        }
    }
}
