using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class HealthMetric : BaseEntity
    {
        [ForeignKey("Child")]
        public Guid ChildId { get; set; }

        [Required]
        public DateTime MetricDate { get; set; }

        public float? Temperature { get; set; }
        public int? HeartRate { get; set; }
        public string? BloodPressure { get; set; }
        public string? AllergySymptoms { get; set; }
        public string? MedicationUse { get; set; }

        // Navigation property
        public Child Child { get; set; }
    }
}
