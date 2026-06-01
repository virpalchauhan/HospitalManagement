using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;

namespace HospitalManagement.Services
{


            public  interface ISocialMediaMastersServices
    {

        int AddSocialMedia(SocialMediaMaster Model);

        List<SocialMediaMaster> AllSocialMediaMasterData();

        int DeleteSocialMedia(int SocialMediaId);

        int UpdateSocialMedia(SocialMediaMaster Model);

        SocialMediaMaster GetSocialMediaById(int SocialMediaId);




    }
    public class SocialMediaMastersServices: ISocialMediaMastersServices, IDisposable
    {
        private readonly EntityDbContext db;

        public SocialMediaMastersServices(EntityDbContext db)
        {
            this.db = db;
        }

        public int AddSocialMedia(SocialMediaMaster Model)
        {
            bool SocialMediaExists =
                db.SocialMediaMasters
                .Any(m => m.PlatformName == Model.PlatformName);

            if (SocialMediaExists)
            {
                return 2;
            }

            db.SocialMediaMasters.Add(Model);

            int Count = db.SaveChanges();

            if (Count > 0)
            {
                return 1;
            }

            return 0;
        }

        public List<SocialMediaMaster> AllSocialMediaMasterData()
        {
           return db.SocialMediaMasters.ToList();
        }

        public int DeleteSocialMedia(int SocialMediaId)
        {
            var Data =
                db.SocialMediaMasters
                .Find(SocialMediaId);

            if (Data != null)
            {
                db.SocialMediaMasters.Remove(Data);

                int Count = db.SaveChanges();

                if (Count > 0)
                {
                    return 1;
                }

                return 2;
            }

            return 0;
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

        public int UpdateSocialMedia(SocialMediaMaster Model)
        {
            var Data =
                db.SocialMediaMasters
                .Find(Model.SocialMediaId);

            if (Data != null)
            {
                // Duplicate Name Check

                bool SocialMediaExists =
                    db.SocialMediaMasters
                    .Any(m => m.PlatformName == Model.PlatformName
                           && m.SocialMediaId != Model.SocialMediaId);

                if (SocialMediaExists)
                {
                    return 2;
                }

                Data.PlatformName = Model.PlatformName;
                Data.SocialMediaLink = Model.SocialMediaLink;
                Data.IsActive = Model.IsActive;

                int Count = db.SaveChanges();

                if (Count > 0)
                {
                    return 1;
                }

                return 3;
            }

            return 0;
        }
    }
}
