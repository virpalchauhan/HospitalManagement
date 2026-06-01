using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services.Client
{


    public interface IPatientServices
    {
        int RegisterPatient(Patient Model);

        Patient Login(Patient Model);

        bool UserExist(string Email);

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
