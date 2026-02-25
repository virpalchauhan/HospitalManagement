using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class DoctorApproveViewModel
    {

      public   int DoctorApplicationsId { get; set; }


        [Required(ErrorMessage = "SalaryAmount is required.")]
        
        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        
        public decimal SalaryAmount { get; set; }

        [Required(ErrorMessage = "JoiningDate is required")]
        public DateTime? JoiningDate { get; set; }


    }
}
