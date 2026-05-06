using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class SocialMediaMasterViewModel
    {

        public int SocialMediaId { get; set; }

        [Required(ErrorMessage = "Platform Name is required")]
        public string? PlatformName { get; set; }

        [Required(ErrorMessage = "Social Media Link is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? SocialMediaLink { get; set; }

        [Required(ErrorMessage = "Please select status")]
        public bool? IsActive { get; set; }

    }
}
