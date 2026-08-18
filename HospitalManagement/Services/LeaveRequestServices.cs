using HospitalManagement.Entity;
using HospitalManagement.Entity.Model;
using HospitalManagement.Entity.Model.Enums;
using HospitalManagement.ViewModel.Admin.Appointment.Leave;
using System.Reflection.Metadata.Ecma335;

namespace HospitalManagement.Services
{


    public interface ILeaveRequestServices
    {
        int AddLeaveRequest(LeaveRequests Model);

        List<LeaveRequests> GetLeaveRequestsByEmployeeId(int EmployeeId);
        LeaveRequests GetLeaveRequestsByLeaveId(int LeaveRequestId);

        List<LeaveRequests> GetLeaveRequestsByStatus(byte? status);

        List<LeaveRequests> GetAllLeaveRequests();

        LeaveRequestSingleViewModel GetSingleLeaveRequest(int leaveRequestId);

        int UpdateLeaveRequest(LeaveRequests Model);

        List<LeaveRequests> GetMyLeaveRequestsByStatus(int employeeId, byte? status);


    }

    public class LeaveRequestServices: ILeaveRequestServices, IDisposable
    {
        private readonly EntityDbContext _EntityDbContext;
        public LeaveRequestServices(EntityDbContext _EntityDbContext)
        {
            this._EntityDbContext = _EntityDbContext;
        }
        public List<LeaveRequests> GetAllLeaveRequests()
        {
            return _EntityDbContext.LeaveRequests
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }

        public int AddLeaveRequest(LeaveRequests Model)
        {
           _EntityDbContext.LeaveRequests.Add(Model);
            int Count = _EntityDbContext.SaveChanges();
            if (Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public void Dispose()
        {
           _EntityDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<LeaveRequests> GetLeaveRequestsByStatus(byte? status)
        {
            if (status == null)
            {
                return _EntityDbContext.LeaveRequests
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();
            }

            return _EntityDbContext.LeaveRequests
                .Where(x => (byte)x.Status == status)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }

        public List<LeaveRequests> GetLeaveRequestsByEmployeeId(int EmployeeId)
        {
            return _EntityDbContext.LeaveRequests.Where(m=>m.EmployeeId == EmployeeId).ToList();
        }

        public LeaveRequests GetLeaveRequestsByLeaveId(int LeaveRequestId)
        {
           return _EntityDbContext.LeaveRequests.FirstOrDefault(m => m.LeaveRequestId == LeaveRequestId);
        }

        public LeaveRequestSingleViewModel GetSingleLeaveRequest(int leaveRequestId)
        {
            var data = (from l in _EntityDbContext.LeaveRequests

                        join d in _EntityDbContext.DoctorsAndNurses
                            on l.EmployeeId equals d.DoctorNurceId

                        join dept in _EntityDbContext.DepartmentTbls
                            on d.DepartmentId equals dept.DepartmentId

                        where l.LeaveRequestId == leaveRequestId

                        select new LeaveRequestSingleViewModel
                        {
                            LeaveRequestId = l.LeaveRequestId,
                            EmployeeId = l.EmployeeId,

                            EmployeeName = d.FirstName + " " + d.LastName,

                            EmployeeType = l.EmployeeType,

                            DepartmentName = dept.DepartmentName,

                            LeaveType = l.LeaveType,

                            FromDate = l.FromDate,
                            ToDate = l.ToDate,

                            Reason = l.Reason,

                            Status = l.Status,

                            AdminRemark = l.AdminRemark,

                            ReviewedBy = l.ReviewedBy,

                            ReviewedAt = l.ReviewedAt,

                            CreatedAt = l.CreatedAt,
                            EmployeeEmail = d.Email
                        })
                .FirstOrDefault();

            return data;
        }

        public int UpdateLeaveRequest(LeaveRequests Model)
        {
           var Data =_EntityDbContext.LeaveRequests.FirstOrDefault(m => m.LeaveRequestId == Model.LeaveRequestId);
            if (Data != null)
            {
                Data.AdminRemark = Model.AdminRemark;
                Data.Status = Model.Status;
                Data.ReviewedBy = Model.ReviewedBy;
                Data.ReviewedAt = DateTime.Now;
                int Count = _EntityDbContext.SaveChanges();
                if (Count > 0)
                {
                    return 1;
                }
            }
            return 0;
        }

        public List<LeaveRequests> GetMyLeaveRequestsByStatus(
      int employeeId,
      byte? status)
        {
            var query = _EntityDbContext.LeaveRequests
                .Where(x => x.EmployeeId == employeeId);


            if (status.HasValue)
            {
                query = query
                    .Where(x => (byte)x.Status == status.Value);
            }


            return query
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }
    }
}
