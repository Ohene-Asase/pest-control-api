using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using PestControl.Data;
using PestControl.Models;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

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
        public AppointmentStatus AppointmentStatus { get; set; }
        public string Username { get; internal set; }
    }

    public interface IAppointmentService : IModelService<AppointmentDto>
    {
       Task<bool>Approve(long Id);
        Task<bool>disApprove(long Id);
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
            string accountSid = "AC32a6ad5f0f74b7c833f017ec585e77f9";
            string authToken = "249c2ff8b4837343dbe8c68baabe5ad7";

            //string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            //string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Your Appointment has been approved?",
                from: new Twilio.Types.PhoneNumber("+14782809299"),
                to: new Twilio.Types.PhoneNumber("+233547347529")
            );

            return true;
        }

        public async Task<bool> disApprove(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) throw new Exception("No Appointment");
            appointment.AppointmentStatus = AppointmentStatus.disapprove;
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
            return true;
        }
    }
}
