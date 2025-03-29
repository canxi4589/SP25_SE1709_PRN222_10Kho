using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class SleepPattern : BaseEntity
    {
        [ForeignKey("Child")]
        public Guid ChildId { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        [Required]
        public TimeSpan Bedtime { get; set; }

        [Required]
        public TimeSpan WakeTime { get; set; }
        public float? NapDuration { get; set; }

        [StringLength(50)]
        public string? SleepQuality { get; set; }

        public string? SleepQualityRating {  get; set; }

        // Navigation property
        public Child Child { get; set; }
    }
}
