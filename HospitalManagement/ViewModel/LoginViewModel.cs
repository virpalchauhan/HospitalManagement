using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.ViewModel
{
    public class LoginViewModel
    {






        [Required(ErrorMessage = "Email is Required")]

        public string? Email { get; set; }

        [Required(ErrorMessage ="Password is Required")]

        public string? PasswordHash { get; set; }



    }
}
