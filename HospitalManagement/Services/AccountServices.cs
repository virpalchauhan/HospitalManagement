using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services
{

    public interface IAccountServices
    {
        DoctorsAndNurse Login(DoctorsAndNurse Model);

        DoctorsAndNurse ForgotPassword(string email);
        int SetOtpForUser(int DoctorApplicationsId, string Otp, DateTime OtpExpiry);

        int SetPasswordForUser(DoctorsAndNurse Model);

        int UpdateOTPAttempts(int DoctorApplicationsId, int attempts,DateTime? LastFailedAttempt);

        int UpdateLockoutEndTime(int DoctorApplicationsId, int attempts, DateTime? LockoutEndTime);

        int UpdateOnlyOtpAttemts(int DoctorApplicationsId, int attempts);
    }



    public class AccountServices : IAccountServices, IDisposable
    {
        private readonly EntityDbContext db;

        public AccountServices(EntityDbContext db)
        {
            this.db = db;
        }

        public int SetPasswordForUser(DoctorsAndNurse Model)
        {
            var Data = db.DoctorsAndNurses.FirstOrDefault(x => x.DoctorNurceId == Model.DoctorNurceId);

            if (Data != null)
            {
                Data.PasswordHash = Model.PasswordHash;
                Data.OTP = null;
                Data.OTPAttempts = 0;
                Data.LockoutEndTime =null;
                Data.OTPExpiry = null;
                Data.LastFailedAttempt = null;
                db.DoctorsAndNurses.Update(Data);
                int count = db.SaveChanges();
                if (count > 0)
                {
                    return 1;
                }
                return 0;
            }
            return 0;
        }

        public void Dispose()
        {
           db.Dispose();
            GC.SuppressFinalize(this);
        }

        public int SetOtpForUser(int DoctorApplicationsId, string Otp, DateTime OtpExpiry)
        {
            var UserData = db.DoctorsAndNurses.FirstOrDefault(x => x.DoctorNurceId == DoctorApplicationsId);


            if (UserData == null)
            {
                return 0;
            }

            UserData.OTP = Otp;
            UserData.OTPExpiry = OtpExpiry;
            db.DoctorsAndNurses.Update(UserData);
            int count = db.SaveChanges();
            if (count > 0)
            {
                return 1;
            }
            return 2;
        }

        public DoctorsAndNurse ForgotPassword(string email)
        {
           var Data = db.DoctorsAndNurses.Where(m => m.Email == email).FirstOrDefault();
            if (Data != null)
            {
               
                return Data; 
            }
            else
            {
                return Data;
            }
        }

        public DoctorsAndNurse Login(DoctorsAndNurse Model)
        {
            var data=  db.DoctorsAndNurses.Where(m => m.Email == Model.Email && m.PasswordHash == Model.PasswordHash).FirstOrDefault();

            return data;
        }

        public int UpdateOTPAttempts(int DoctorApplicationsId, int attempts, DateTime? LastFailedAttempt)
        {
           var UserData = db.DoctorsAndNurses.FirstOrDefault(x => x.DoctorNurceId == DoctorApplicationsId);

            if (UserData == null)
            {
                return 0;
            }
            UserData.OTPAttempts = attempts;
            UserData.LastFailedAttempt = LastFailedAttempt;
            db.DoctorsAndNurses.Update(UserData);
            int count = db.SaveChanges();
            if (count > 0)
            {
                return 1;
            }
            return 2;
        }

        public int UpdateLockoutEndTime(int DoctorApplicationsId, int attempts, DateTime? LockoutEndTime)
        {
            var UserData = db.DoctorsAndNurses.FirstOrDefault(m => m.DoctorNurceId == DoctorApplicationsId);

            if (UserData==null)
            {
                return 0;
            }

            UserData.OTPAttempts = attempts;
            UserData.LockoutEndTime = LockoutEndTime;
            db.DoctorsAndNurses.Update(UserData);
            int Count = db.SaveChanges();
            if (Count>0)
            {
                return 1;
            }
            return 0;
        }

        public int UpdateOnlyOtpAttemts(int DoctorApplicationsId, int attempts)
        {
            var UserData = db.DoctorsAndNurses.FirstOrDefault(m => m.DoctorNurceId == DoctorApplicationsId);

            if (UserData == null)
            {
                return 0;
            }

            UserData.OTPAttempts = attempts;
            
            db.DoctorsAndNurses.Update(UserData);
            int Count = db.SaveChanges();
            if (Count > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
