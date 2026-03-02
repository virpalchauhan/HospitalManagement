using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;
using Microsoft.IdentityModel.Tokens;

namespace HospitalManagement.Services
{


    public interface IDoctorApplicationservices
    {

        int AddDoctor(DoctorApplication Model);


        List<DoctorApplicationInnerJoin> AllPendingDoctorApplications();
        List<DoctorApplicationInnerJoin> AllRejectDoctorApplications();
        List<DoctorApplicationInnerJoin> AllAcceptDoctorApplications();
        List<DoctorApplicationInnerJoin> AlDoctorApplications();

        DoctorApplicationInnerJoin SingleData(int Id);

        int DoctorApplicationUpdate(int id, byte ApplicationsStatusId);





    }
    public class DoctorApplicationServices : IDoctorApplicationservices, IDisposable
    {

        private readonly EntityDbContext db;
        public DoctorApplicationServices(EntityDbContext db)
        {
            this.db = db;

        }

        public int AddDoctor(DoctorApplication Model)
        {
            bool DoctorExists = db.doctorApplications.Any(m => m.Email == Model.Email);
            if (DoctorExists)
            {
                return 2;
            }
            db.doctorApplications.Add(Model);
            int Count = db.SaveChanges();
            if (Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public List<DoctorApplicationInnerJoin> AlDoctorApplications()
        {
            var Data = (from d in db.doctorApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                        select new DoctorApplicationInnerJoin
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
                            ApplicationStatus = d.ApplicationStatus
                        }).ToList();

            return Data;
        }

        public List<DoctorApplicationInnerJoin> AllAcceptDoctorApplications()
        {
            var Data = (from d in db.doctorApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                        where d.ApplicationStatus == ApplicationStatusType.Accept
                        select new DoctorApplicationInnerJoin
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
                            ApplicationStatus = d.ApplicationStatus
                        }).ToList();

            return Data;
        }

        public List<DoctorApplicationInnerJoin> AllPendingDoctorApplications()
        {
            //return db.doctorApplications.Where(m => m.ApplicationStatus == 0).ToList();


            var Data = (from d in db.doctorApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                        where d.ApplicationStatus == ApplicationStatusType.Pending
                        select new DoctorApplicationInnerJoin
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
                            ApplicationStatus = d.ApplicationStatus
                        }).ToList();

            return Data;
        }

        public List<DoctorApplicationInnerJoin> AllRejectDoctorApplications()
        {
            var Data = (from d in db.doctorApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                        where d.ApplicationStatus == ApplicationStatusType.Reject
                        select new DoctorApplicationInnerJoin
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
                            ApplicationStatus = d.ApplicationStatus
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
            var Data = db.doctorApplications.Where(m => m.DoctorApplicationsId == id).FirstOrDefault();

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

        public DoctorApplicationInnerJoin SingleData(int Id)
        {
            var Data = (from d in db.doctorApplications
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId               
                        select new DoctorApplicationInnerJoin
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
