using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services
{


            public  interface ISocialMediaMastersServices
    {

        string AddSocialMedia(SocialMediaMaster Model);

        List<SocialMediaMaster> AllSocialMediaMasterData();

        string DeleteSocialMedia(int SocialMediaId);

        string UpdateSocialMedia(SocialMediaMaster Model);

        SocialMediaMaster GetSocialMediaById(int SocialMediaId);




    }
    public class SocialMediaMastersServices: ISocialMediaMastersServices, IDisposable
    {
        private readonly EntityDbContext db;

        public SocialMediaMastersServices(EntityDbContext db)
        {
            this.db = db;
        }

        public string AddSocialMedia(SocialMediaMaster Model)
        {
           bool SocialMediaExists = db.SocialMediaMasters.Any(m=>m.PlatformName==Model.PlatformName);
            if (SocialMediaExists)
            {
                return "This Social Media Platform already exists.";
            }
            db.SocialMediaMasters.Add(Model);
            int Count = db.SaveChanges();
            if (Count > 0)
            {
                return "Social Media Platform added successfully.";
            }
            return "Failed to add Social Media Platform.";
        }

        public List<SocialMediaMaster> AllSocialMediaMasterData()
        {
           return db.SocialMediaMasters.ToList();
        }

        public string DeleteSocialMedia(int SocialMediaId)
        {
            var Data = db.SocialMediaMasters.Find(SocialMediaId);

            if (Data != null)
            {
                db.SocialMediaMasters.Remove(Data);
                int Count = db.SaveChanges();
                if (Count > 0)
                {
                    return "Social Media Platform deleted successfully.";
                }
                return "Failed to delete Social Media Platform.";
            }
            return "Failed to delete Social Media Platform.";
        }

        public void Dispose()
        {
           db.Dispose();
            GC.SuppressFinalize(this);
        }

        public SocialMediaMaster GetSocialMediaById(int SocialMediaId)
        {
            
                return db.SocialMediaMasters.Where(m => m.SocialMediaId == SocialMediaId).FirstOrDefault();
                     
        }

        public string UpdateSocialMedia(SocialMediaMaster Model)
        {
            var Data = db.SocialMediaMasters.Find(Model.SocialMediaId);

            if (Data != null)
            {
                // Duplicate Name Check
                bool SocialMediaExists = db.SocialMediaMasters
                    .Any(m => m.PlatformName == Model.PlatformName
                           && m.SocialMediaId != Model.SocialMediaId);

                if (SocialMediaExists)
                {
                    return "This Social Media Platform already exists.";
                }

                Data.PlatformName = Model.PlatformName;
                Data.SocialMediaLink = Model.SocialMediaLink;
                Data.IsActive = Model.IsActive;

                int Count = db.SaveChanges();

                if (Count > 0)
                {
                    return "Social Media Platform updated successfully.";
                }

                return "Failed to update Social Media Platform.";
            }

            return "Record not found.";
        }
    }
}
