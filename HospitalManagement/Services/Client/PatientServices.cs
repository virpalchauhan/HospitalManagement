using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services.Client
{


    public interface IPatientServices
    {
        int RegisterPatient(Patient Model);

        Patient Login(Patient Model);

        bool UserExist(string Email);
        Patient GetByid(int PatientId);

        int UpdateProfile(Patient Model);


    }

    public class PatientServices: IPatientServices, IDisposable
    {
        private readonly EntityDbContext _EntityDbContext;

        public PatientServices(EntityDbContext _EntityDbContext)
        {
            this._EntityDbContext = _EntityDbContext;
        }

        public void Dispose()
        {
           GC.SuppressFinalize(this);
            _EntityDbContext.Dispose();
        }

        public Patient GetByid(int PatientId)
        {
           return _EntityDbContext.patient.Where(M => M.PatientId == PatientId).FirstOrDefault();
        }

        public Patient Login(Patient Model)
        {
            return _EntityDbContext.patient.Where(M => M.Email == Model.Email && M.PasswordHash == Model.PasswordHash).FirstOrDefault();
        }

        public int RegisterPatient(Patient Model)
        {
            bool isEmailExist = _EntityDbContext.patient.Any(x => x.Email == Model.Email);
            
            if (isEmailExist)
            {
                return 2;
            }
            Model.CreateDate = DateTime.Now;
            _EntityDbContext.patient.Add(Model);
            int count = _EntityDbContext.SaveChanges();
            if (count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int UpdateProfile(Patient Model)
        {
            var Data = _EntityDbContext.patient.Where(m => m.PatientId == Model.PatientId).FirstOrDefault();
            if (Data != null)
            {
                Data.FirstName = Model.FirstName;
                Data.LastName = Model.LastName;
                Data.Gender = Model.Gender;
                Data.DateOfBirth = Model.DateOfBirth;
                Data.MobileNo = Model.MobileNo;
                Data.Address = Model.Address;
                Data.City = Model.City;
                Data.StateName = Model.StateName;
                Data.Pincode = Model.Pincode;
                Data.BloodGroup = Model.BloodGroup;
                Data.ProfilePhotoPath = Model.ProfilePhotoPath;


                int count = _EntityDbContext.SaveChanges();

                return 1;

            }
            return 0;
        }

        public bool UserExist(string Email)
        {
            var Data = _EntityDbContext.patient.Any(m => m.Email == Email);

            if (Data == false)
            {
                return false;
            }

            return true;
        }
    }
}
