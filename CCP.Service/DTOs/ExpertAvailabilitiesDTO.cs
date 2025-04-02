using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class ExpertAvailabilitiesDTO
    {
        public Guid Id { get; set; }
        public Guid ExpertId { get; set; }

        // Actual properties used in the entity
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Helper properties for binding to input type="time"
        //public TimeOnly? StartTimeInput
        //{
        //    get => StartTime == TimeSpan.Zero ? null : TimeOnly.FromTimeSpan(StartTime);
        //    set => StartTime = value.HasValue ? value.Value.ToTimeSpan() : TimeSpan.Zero;
        //}

        //public TimeOnly? EndTimeInput
        //{
        //    get => EndTime == TimeSpan.Zero ? null : TimeOnly.FromTimeSpan(EndTime);
        //    set => EndTime = value.HasValue ? value.Value.ToTimeSpan() : TimeSpan.Zero;
        //}

        [StringLength(20, ErrorMessage = "Day of Week cannot exceed 20 characters.")]
        public string DayOfWeek { get; set; }

        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }
    }
}
