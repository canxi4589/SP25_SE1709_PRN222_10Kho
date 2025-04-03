using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class AppointmentHistoryDTO
    {
        public Guid Id { get; set; }
        public string ParentId { get; set; }
        public Guid ChildId { get; set; }
        public Guid ExpertId { get; set; }
        public string ParentName { get; set; } // New property for parent's full name
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ChildName { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public Guid? MeasurementId { get; set; }
        public Guid? PhysicalActivityId { get; set; }
        public Guid? NutritionalIntake { get; set; }
        public Guid? SleepPatternId { get; set; }
        public string? Type { get; set; }
        // Optional: Add child name or parent name if needed
        //public string ChildName { get; set; }
        //public string ParentName { get; set; }
    }
}
