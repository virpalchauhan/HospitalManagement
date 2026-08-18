using HospitalManagement.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Entity
{
    public class EntityDbContext: DbContext
    {
        public EntityDbContext(DbContextOptions options) : base(options) { }


        public DbSet<DepartmentTbl> DepartmentTbls { get; set; }

        public DbSet<DoctorNurseApplication> doctorNurseApplications { get; set; }

        public DbSet<DoctorsAndNurse> DoctorsAndNurses { get; set; }

        public DbSet<SocialMediaMaster> SocialMediaMasters { get; set; }

        public DbSet<Patient> patient { get; set; }


        public DbSet<PendingRegistrationPatients> pendingRegistrationPatients { get; set; }


        public DbSet<AppointmentTable> AppointmentTables { get; set; }

        public DbSet<LeaveRequests> LeaveRequests { get; set; }

        public DbSet<DoctorNurseTeam> DoctorNurseTeams { get; set; }



    }
}
