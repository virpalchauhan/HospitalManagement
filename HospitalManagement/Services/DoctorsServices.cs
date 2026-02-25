using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services
{

    public interface IDoctorsServices
    {
        int AddDoctor(Doctors Model);


    }


    public class DoctorsServices : IDoctorsServices, IDisposable
    {
        private readonly EntityDbContext db;

        public DoctorsServices(EntityDbContext db)
        {
            this.db = db;
        }

        public int AddDoctor(Doctors Model)
        {
            var data = db.Doctors.Add(Model);
            db.SaveChanges();
            return 1;
        }

        public void Dispose()
        {
           db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
