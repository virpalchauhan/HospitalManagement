using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;

namespace HospitalManagement.Services
{

    public interface IDoctorAndNurseServices
    {
        int AddDoctor(DoctorsAndNurse Model);



        DoctorsAndNurse GetByEmail(string Email);

        DoctorNurseEditProfile GetByID(int id);


        int UpdateProfile(DoctorsAndNurse Model);




    }


    public class DoctorAndNurseServices : IDoctorAndNurseServices, IDisposable
    {
        private readonly EntityDbContext db;

        public DoctorAndNurseServices(EntityDbContext db)
        {
            this.db = db;
        }

        public int AddDoctor(DoctorsAndNurse Model)
        {
            var data = db.DoctorsAndNurses.Add(Model);
            int count = db.SaveChanges();

            if (count > 0)
            {
                return 1;

            }
            return 0;

        }



        public DoctorsAndNurse GetByEmail(string Email)
        {
            return db.DoctorsAndNurses.FirstOrDefault(x => x.Email == Email);
        }




        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

        public DoctorNurseEditProfile GetByID(int id)
        {
            var Data = (from d in db.DoctorsAndNurses
                        join dept in db.DepartmentTbls
                        on d.DepartmentId equals dept.DepartmentId
                        where d.DoctorNurceId == id

                        select new DoctorNurseEditProfile
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
                            DoctorNurceId = d.DoctorNurceId,
                            RollType = d.RollType,
                            SalaryAmount = d.SalaryAmount,
                            JoiningDate = d.JoiningDate,
                            AccountStatus = d.AccountStatus,
                            OfferLetterSent = d.OfferLetterSent,
                            CreatedDate = d.CreatedDate,




                        }).FirstOrDefault();

            return Data;
        }

        public int UpdateProfile(DoctorsAndNurse Model)
        {
            var Data =
                db.DoctorsAndNurses
                .FirstOrDefault(x => x.DoctorNurceId == Model.DoctorNurceId);

            if (Data != null)
            {
                Data.FirstName = Model.FirstName;
                Data.LastName = Model.LastName;
                Data.Gender = Model.Gender;
                Data.DateOfBirth = Model.DateOfBirth;
                Data.ProfilePhotoPath = Model.ProfilePhotoPath;

                db.DoctorsAndNurses.Update(Data);

                int Count = db.SaveChanges();

                if (Count > 0)
                {
                    return 1;
                }

                return 2;
            }

            return 0;
        }
    }

}