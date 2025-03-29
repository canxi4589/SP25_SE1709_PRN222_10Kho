using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class Appointment : BaseEntity
    {
        [ForeignKey("AppUser")]
        public string UserId { get; set; }

        [ForeignKey("Child")]
        public Guid ChildId { get; set; }

        [ForeignKey("Expert")]
        public Guid ExpertId { get; set; }

        public TimeSpan StartTime {  get; set; }

        public TimeSpan EndTime { get; set; }
        
        [Required]
        public DateTime BookingDate { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

    }
}
