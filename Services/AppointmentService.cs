using AutoMapper;
using PestControl.Data;
using PestControl.Models;
using System;
using System.Threading.Tasks;

namespace PestControl.Services

{
    public class AppointmentDto
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public int PhoneNumber { get; set; }
        public string location { get; set; }
        public string Note { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Username { get; internal set; }
    }

    public interface IAppointmentService : IModelService<AppointmentDto>
    {
       Task<bool>Approve(long Id);
    }

    public class AppointmentService : BaseService<AppointmentDto, Appointment>, IAppointmentService
    {
        public AppointmentService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async  Task<long> Save(AppointmentDto record)
        {
            var appointment = new Appointment();

            appointment.CreatedAt = DateTime.Now;
            appointment.CreatedBy = record.Username;
            appointment.ModifiedAt = DateTime.Now;
            appointment.ModifiedBy = record.Username;
            appointment.CustomerName = record.CustomerName;
            appointment.Date = record.Date;
            appointment.PhoneNumber = record.PhoneNumber;
            appointment.AppointmentStatus = AppointmentStatus.Pending;
            appointment.Note = record.Note;
            appointment.Location = record.location;

            await _context.Appointments.AddAsync(appointment);
            _context.SaveChanges();
            return appointment.Id;
        }

      public async Task<bool> Approve(long Id)
        {
            var appointment = await _context.Appointments.FindAsync(Id);
            if (appointment == null) throw new Exception("No Appointment");
            appointment.AppointmentStatus = AppointmentStatus.Approved;
            _context.Appointments.Update(appointment);
            _context.SaveChanges();

             return true;
        }
    }
}
