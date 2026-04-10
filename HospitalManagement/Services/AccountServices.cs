using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services
{

    public interface IAccountServices
    {
        DoctorsAndNurse Login(DoctorsAndNurse Model);
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

        public DoctorsAndNurse Login(DoctorsAndNurse Model)
        {
            var data=  db.DoctorsAndNurses.Where(m => m.Email == Model.Email && m.PasswordHash == Model.PasswordHash).FirstOrDefault();

            return data;
        }
    }
}
