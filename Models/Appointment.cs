using System;

namespace PestControl.Models
{
    public class Appointment: AuditFields
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public int PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
    }

    public enum AppointmentStatus
    {
        Pending,
        Approved,
        disapprove
        
    }
}
