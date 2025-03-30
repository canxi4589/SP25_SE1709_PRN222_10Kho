using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class AppointmentDTO
    {
        public string UserId { get; set; }
        public Guid ChildId { get; set; }

        public Guid ExpertId { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime BookingDate { get; set; }

        public string Status { get; set; }
        public decimal Price { get; set; }
    }
}
