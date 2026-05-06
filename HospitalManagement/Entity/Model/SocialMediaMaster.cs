using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Entity.Model
{


    [Table("SocialMediaMaster")]
    public class SocialMediaMaster
    {

        [Key]


        public int SocialMediaId { get; set; }

        public string? PlatformName { get; set; }

        public string? SocialMediaLink { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
