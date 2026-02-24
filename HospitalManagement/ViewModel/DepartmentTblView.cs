using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class DepartmentTblView
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name Is required")]
        
        public string? DepartmentName { get; set; }

        [Required(ErrorMessage = "Description Is required ")]
      
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select status")]
        public byte? IsActive { get; set; }

        public DateTime CreationDate { get; set; }


    }
}
