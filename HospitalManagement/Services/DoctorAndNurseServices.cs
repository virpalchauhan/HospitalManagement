using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services
{

    public interface IDoctorAndNurseServices
    {
        int AddDoctor(DoctorsAndNurse Model);

      

        DoctorsAndNurse GetByEmail(string Email);

      


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
       int count=     db.SaveChanges();

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

      
    }
}
