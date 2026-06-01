using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Entity.Model
{

    [Table("PendingRegistrationPatients")]

    public class PendingRegistrationPatients
    {

        [Key]
        public int PendingPatientsID { get; set; }

        public string? Email { get; set; }

        public string? OTP { get; set; }

        public DateTime? OTPExpiry { get; set; }

        public int? OTPAttempts { get; set; }

        public DateTime? LastOTPSentTime { get; set; }


    }
}
