using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.Entity.Model.Innerjoin;

namespace HospitalManagement.Services
{

    public interface IAppointmentTableServices
    {
        int AddAppointment(AppointmentTable Model);

        List<GetAllAppointmentsforDoctor> GetAllAppointmentsforDoctor(int DoctorId);

        AppointmentPatientDepartmentInnerJoin AppointmentPatientDepartmentInnerJoin(int AppointmentId);

        int DoctorResponse(AppointmentTable Model);

        List<GetAllAppointmentsforDoctor> GetAppointmentByStatus(int DoctorId, AppointmentStatusType? status);

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

        public AppointmentPatientDepartmentInnerJoin AppointmentPatientDepartmentInnerJoin(int AppointmentId)
        {
           var Data =(from Appointment in _EntityDbContext.AppointmentTables
                      join PatientTable in _EntityDbContext.patient
                      on Appointment.PatientId equals PatientTable.PatientId
                        join department in _EntityDbContext.DepartmentTbls
                        on Appointment.DepartmentId equals department.DepartmentId
                      where Appointment.AppointmentId == AppointmentId


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
                            
                            SuggestedDate=Appointment.SuggestedDate,
                            SuggestedTime=Appointment.SuggestedTime
                      }

                      ).
                      FirstOrDefault();

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
                Data.Status = AppointmentStatusType.approve;
                _EntityDbContext.AppointmentTables.Update(Data);
                int Count = _EntityDbContext.SaveChanges();
                if (Count > 0)
                {
                    return 1;
                }
                return 0;
            }
        }

                public List<GetAllAppointmentsforDoctor> GetAllAppointmentsforDoctor(int DoctorId)
                {
                    var Data = (from Appointment in _EntityDbContext.AppointmentTables
                                join PatientTable in _EntityDbContext.patient
                                on Appointment.PatientId equals PatientTable.PatientId
                                join department in _EntityDbContext.DepartmentTbls
                                on Appointment.DepartmentId equals department.DepartmentId
                                where Appointment.Status == AppointmentStatusType.Pending
                                && Appointment.DoctorId == DoctorId


                                select new GetAllAppointmentsforDoctor
                                {
                                    PatientName = PatientTable.FirstName + " " + PatientTable.LastName,
                                    AppointmentBookDate = Appointment.AppointmentBookDate,
                                    DepartmentName = department.DepartmentName,
                                    AppointmentId = Appointment.AppointmentId


                                }


                              ).ToList();


                    return Data;


                }

        public List<GetAllAppointmentsforDoctor> GetAppointmentByStatus(int DoctorId, AppointmentStatusType? status)
        {
            var Data = (from Appointment in _EntityDbContext.AppointmentTables
                        join PatientTable in _EntityDbContext.patient
                        on Appointment.PatientId equals PatientTable.PatientId
                        join department in _EntityDbContext.DepartmentTbls
                        on Appointment.DepartmentId equals department.DepartmentId
                        where Appointment.Status == status
                        && Appointment.DoctorId == DoctorId


                        select new GetAllAppointmentsforDoctor
                        {
                            PatientName = PatientTable.FirstName + " " + PatientTable.LastName,
                            AppointmentBookDate = Appointment.AppointmentBookDate,
                            DepartmentName = department.DepartmentName,
                            AppointmentId = Appointment.AppointmentId


                        }


                              ).ToList();


             return Data;
        }
    }
}
