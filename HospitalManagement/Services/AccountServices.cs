using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services
{

    public interface IAccountServices
    {
        int Login(DoctorsAndNurse Model);
    }



    public class AccountServices : IAccountServices, IDisposable
    {
        private readonly EntityDbContext db;

        public AccountServices(EntityDbContext db)
        {
            this.db = db;
        }

        public void Dispose()
        {
           db.Dispose();
            GC.SuppressFinalize(this);
        }

        public int Login(DoctorsAndNurse Model)
        {
            var data = db.DoctorsAndNurses.Where(m => m.Email == Model.Email && m.PasswordHash == Model.PasswordHash).Take(1).FirstOrDefault();

            if (data!=null)
            {
                return data.DoctorId;
            }
            return 0;
        }
    }
}
