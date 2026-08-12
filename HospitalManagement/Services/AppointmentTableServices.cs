using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;

namespace HospitalManagement.Services
{

    public interface IAppointmentTableServices
    {
        int AddAppointment(AppointmentTable Model);

        List<AppointmentPatientDepartmentInnerJoin> GetAllAppointmentsforDoctor(int DoctorId);

        AppointmentPatientDepartmentInnerJoin AppointmentPatientDepartmentInnerJoinforDoctor(int AppointmentId);

        int DoctorResponse(AppointmentTable Model);

        List<AppointmentPatientDepartmentInnerJoin> GetAppointmentByStatus(int DoctorId, AppointmentStatusType? status);

        List<AppointmentPatientDepartmentInnerJoin> AppointmentPatientDepartmentInnerJoinForPatient(int PatientId);

        int UpdateAppointmentStatus(AppointmentTable Model);



    }


    public class AppointmentTableServices : IAppointmentTableServices, IDisposable
    {

        private readonly EntityDbContext _EntityDbContext;

        public AppointmentTableServices(EntityDbContext _EntityDbContext)
        {
            this._EntityDbContext = _EntityDbContext;
        }

        public int AddAppointment(AppointmentTable Model)
        {
             _EntityDbContext.AppointmentTables.Add(Model);
            int count = _EntityDbContext.SaveChanges();
            if (count>0)
            {
                return 1;
            }
           return 0;
        }

        public AppointmentPatientDepartmentInnerJoin AppointmentPatientDepartmentInnerJoinforDoctor(int AppointmentId)
        {
           var Data =(from Appointment in _EntityDbContext.AppointmentTables
                      join PatientTable in _EntityDbContext.patient
                      on Appointment.PatientId equals PatientTable.PatientId
                        join department in _EntityDbContext.DepartmentTbls
                        on Appointment.DepartmentId equals department.DepartmentId
                      where Appointment.AppointmentId == AppointmentId
                      join DoctorTable in _EntityDbContext.DoctorsAndNurses
                        on Appointment.DoctorId equals DoctorTable.DoctorNurceId

                      select new AppointmentPatientDepartmentInnerJoin
                        {
                            PatientName = PatientTable.FirstName + " " + PatientTable.LastName,
                            AppointmentBookDate = Appointment.AppointmentBookDate,
                            DepartmentName = department.DepartmentName,
                            AppointmentId = Appointment.AppointmentId,
                            PatientId=Appointment.PatientId,
                            DoctorId=Appointment.DoctorId,
                            AppointmentDate=Appointment.AppointmentDate,
                            AppointmentTime=Appointment.AppointmentTime,
                            DepartmentId=Appointment.DepartmentId,
                            Reason=Appointment.Reason,
                            DoctorName= DoctorTable.FirstName+" "+DoctorTable.LastName,
                            SuggestedDate =Appointment.SuggestedDate,
                            SuggestedTime=Appointment.SuggestedTime,
                            Status= Appointment.Status.Value,
                          PatientEmail=PatientTable.Email

                      }

                      ).
                      FirstOrDefault();

            return Data;

        }

       

        public List<AppointmentPatientDepartmentInnerJoin> AppointmentPatientDepartmentInnerJoinForPatient(int PatientId)
        {
            var Data = (from Appointment in _EntityDbContext.AppointmentTables
                        join PatientTable in _EntityDbContext.patient
                        on Appointment.PatientId equals PatientTable.PatientId
                        join department in _EntityDbContext.DepartmentTbls
                        on Appointment.DepartmentId equals department.DepartmentId
                        where Appointment.PatientId == PatientId

                        join DoctorTable in _EntityDbContext.DoctorsAndNurses
                        on Appointment.DoctorId equals DoctorTable.DoctorNurceId




                        select new AppointmentPatientDepartmentInnerJoin
                        {
                            PatientName = PatientTable.FirstName + " " + PatientTable.LastName,
                            AppointmentBookDate = Appointment.AppointmentBookDate,
                            DepartmentName = department.DepartmentName,
                            AppointmentId = Appointment.AppointmentId,
                            PatientId = Appointment.PatientId,
                            DoctorId = Appointment.DoctorId,
                            AppointmentDate = Appointment.AppointmentDate,
                            AppointmentTime = Appointment.AppointmentTime,
                            DepartmentId = Appointment.DepartmentId,
                            Reason = Appointment.Reason,
                            DoctorName = DoctorTable.FirstName + " " + DoctorTable.LastName,
                            SuggestedDate = Appointment.SuggestedDate,
                            SuggestedTime = Appointment.SuggestedTime,
                            Status = Appointment.Status.Value
                        }

                       ).ToList();


            return Data;
        }

        public void Dispose()
        {
           GC.SuppressFinalize(this);
            _EntityDbContext.Dispose();
        }

        public int DoctorResponse(AppointmentTable Model)
        {
            var Data = _EntityDbContext.AppointmentTables.FirstOrDefault(x => x.AppointmentId == Model.AppointmentId);

            if (Data == null)
            {
                return 0;
            }
            else
            {
                Data.AppointmentDate = Model.AppointmentDate;
                Data.AppointmentTime = Model.AppointmentTime;
                Data.Status = Model.Status;
                Data.PatientResponse = Model.PatientResponse;
                _EntityDbContext.AppointmentTables.Update(Data);
                int Count = _EntityDbContext.SaveChanges();
                if (Count > 0)
                {
                    return 1;
                }
                return 0;
            }
        }

                public List<AppointmentPatientDepartmentInnerJoin> GetAllAppointmentsforDoctor(int DoctorId)
                {
                    var Data = (from Appointment in _EntityDbContext.AppointmentTables
                                join PatientTable in _EntityDbContext.patient
                                on Appointment.PatientId equals PatientTable.PatientId
                                join department in _EntityDbContext.DepartmentTbls
                                on Appointment.DepartmentId equals department.DepartmentId

                                where Appointment.DoctorId == DoctorId


                                select new AppointmentPatientDepartmentInnerJoin
                                {
                                    PatientName = PatientTable.FirstName + " " + PatientTable.LastName,
                                    AppointmentBookDate = Appointment.AppointmentBookDate,
                                    DepartmentName = department.DepartmentName,
                                    AppointmentId = Appointment.AppointmentId,
                                    Status = Appointment.Status.Value



                                }


                              ).ToList();


                    return Data;


                }

        public List<AppointmentPatientDepartmentInnerJoin> GetAppointmentByStatus(int DoctorId, AppointmentStatusType? status)
        {
            var Data = (from Appointment in _EntityDbContext.AppointmentTables
                        join PatientTable in _EntityDbContext.patient
                        on Appointment.PatientId equals PatientTable.PatientId
                        join department in _EntityDbContext.DepartmentTbls
                        on Appointment.DepartmentId equals department.DepartmentId
                        where Appointment.Status == status
                        && Appointment.DoctorId == DoctorId


                        select new AppointmentPatientDepartmentInnerJoin
                        {
                            PatientName = PatientTable.FirstName + " " + PatientTable.LastName,
                            AppointmentBookDate = Appointment.AppointmentBookDate,
                            DepartmentName = department.DepartmentName,
                            AppointmentId = Appointment.AppointmentId,
                            Status = Appointment.Status.Value


                        }


                              ).ToList();


             return Data;
        }

        public int UpdateAppointmentStatus(AppointmentTable Model)
        {
            var Data = _EntityDbContext.AppointmentTables.Where(m => m.AppointmentId == Model.AppointmentId).FirstOrDefault();

            Data.Status = Model.Status;
            _EntityDbContext.AppointmentTables.Update(Data);
            int Count = _EntityDbContext.SaveChanges();

            if (Count>0)
            {
                return Count;
            }
            return 0;
        }
    }
}
