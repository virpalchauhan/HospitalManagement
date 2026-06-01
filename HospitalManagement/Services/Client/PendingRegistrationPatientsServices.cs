using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HospitalManagement.Services.Client
{

    public interface IPendingRegistrationPatientsServices
    {
        PendingRegistrationPatients GetByEmail(string Email); 

        bool AddPendingRegistrationPatient(PendingRegistrationPatients Model);

        bool DeletePendingRegistrationPatient(string Email);

        bool UpdatePendingRegistrationPatient(PendingRegistrationPatients Model);

        bool UpdateOtpAttempts(PendingRegistrationPatients Model);

        

    }

    public class PendingRegistrationPatientsServices: IPendingRegistrationPatientsServices,IDisposable
    {

        private readonly EntityDbContext _EntityDbContext;

        public PendingRegistrationPatientsServices(EntityDbContext _EntityDbContext)
        {
            this._EntityDbContext = _EntityDbContext;
        }

        public bool AddPendingRegistrationPatient(PendingRegistrationPatients Model)
        {
           _EntityDbContext.pendingRegistrationPatients.Add(Model);
            int Count = _EntityDbContext.SaveChanges();
            if (Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeletePendingRegistrationPatient(string Email)
        {
           var Data = _EntityDbContext.pendingRegistrationPatients.FirstOrDefault(p => p.Email == Email);
            if (Data != null)
            {
                _EntityDbContext.pendingRegistrationPatients.Remove(Data);
                int Count = _EntityDbContext.SaveChanges();
                if (Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Dispose()
        {
            _EntityDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public PendingRegistrationPatients GetByEmail(string Email)
        {
           return _EntityDbContext.pendingRegistrationPatients.FirstOrDefault(p => p.Email == Email);
        }

        public bool UpdateOtpAttempts(PendingRegistrationPatients Model)
        {
            var Data = _EntityDbContext.pendingRegistrationPatients.FirstOrDefault(m => m.Email == Model.Email);

            if (Data!=null)
            {
                Data.OTPAttempts = Model.OTPAttempts;


              int Count =  _EntityDbContext.SaveChanges();

                if (Count>0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool UpdatePendingRegistrationPatient(PendingRegistrationPatients Model)
        {
            try
            {
                var Data = _EntityDbContext.pendingRegistrationPatients
                    .FirstOrDefault(m => m.Email == Model.Email);

                if (Data != null)
                {
                    Data.OTPAttempts = Model.OTPAttempts;
                    Data.Email = Model.Email;
                    Data.OTP = Model.OTP;
                    Data.OTPExpiry = Model.OTPExpiry;
                    Data.LastOTPSentTime = Model.LastOTPSentTime;

                    int Count = _EntityDbContext.SaveChanges();

                    if (Count > 0)
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Debug ke liye
                string ErrorMessage = ex.Message;

                // Agar inner exception ho
                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.InnerException.Message;
                }

                Console.WriteLine(ErrorMessage);

                return false;
            }
        }
    }
}
