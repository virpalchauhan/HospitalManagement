namespace HospitalManagement.Entity.Model.Enums
{
    public enum AppointmentStatusType : byte
    {
        Pending = 0,
        Confirmed = 1,
        Rejected = 2,
        Rescheduled = 3,
        Cancelled = 4,
        Completed = 5,
        NoShow = 6,
        approve = 7
    }
}
