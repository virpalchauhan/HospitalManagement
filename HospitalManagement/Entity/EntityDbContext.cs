using HospitalManagement.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Entity
{
    public class EntityDbContext: DbContext
    {
        public EntityDbContext(DbContextOptions options) : base(options) { }


        public DbSet<DepartmentTbl> DepartmentTbls { get; set; }

        public DbSet<DoctorApplication> doctorApplications { get; set; }

        public DbSet<Doctors> Doctors { get; set; }



    }
}
