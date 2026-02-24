using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services
{
    

        public interface IDepartmentTblServices
        {
            int AddDepartment(DepartmentTbl Model);

        List<DepartmentTbl> AllDepartment();

        DepartmentTbl SingleDepartment(int DepartmentId);

        int Delete(int DepartmentId);

        int Update(DepartmentTbl Model, int DepartmentId);

        }



    public class DepartmentTblServices: IDepartmentTblServices,IDisposable
    {

        private readonly EntityDbContext db;

        public DepartmentTblServices(EntityDbContext db)
        {
            this.db = db;  
        }

        public int AddDepartment(DepartmentTbl Model)
        {
            bool DepartmentExists = db.DepartmentTbls.Any(m => m.DepartmentName == Model.DepartmentName);

            if (DepartmentExists)
            {
                return 2;
            }
            db.DepartmentTbls.Add(Model);
         int Count= db.SaveChanges();

            if (Count>0)
            {
                return 1;
            }

            return 0;
        }

        public List<DepartmentTbl> AllDepartment()
        {
            return db.DepartmentTbls.ToList();
        }

        public int Delete(int DepartmentId)
        {
           var SingleDepartmentData = db.DepartmentTbls.Where(m => m.DepartmentId == DepartmentId).SingleOrDefault();

            if (SingleDepartmentData!=null)
            {
                db.DepartmentTbls.Remove(SingleDepartmentData);
                int count = db.SaveChanges();

                if (count>0)
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

        public DepartmentTbl SingleDepartment(int DepartmentId)
        {
            return db.DepartmentTbls.Where(m => m.DepartmentId == DepartmentId).SingleOrDefault();
        }

        public int Update(DepartmentTbl model, int DepartmentId)
        {
           
            var duplicate = db.DepartmentTbls
                              .Where(x => x.DepartmentName == model.DepartmentName
                                       && x.DepartmentId != DepartmentId)
                              .FirstOrDefault();

            if (duplicate != null)
            {
                return 2;  
            }

            var updateData = db.DepartmentTbls
                               .Where(m => m.DepartmentId == DepartmentId)
                               .SingleOrDefault();

            if (updateData != null)
            {
                updateData.DepartmentName = model.DepartmentName;
                updateData.Description = model.Description;
                updateData.IsActive = model.IsActive;

                int count = db.SaveChanges();

                if (count > 0)
                {
                    return 1;  
                }
            }

            return 0; 
        }
    }
}
