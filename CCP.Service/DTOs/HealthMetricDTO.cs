using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class HealthMetricDTO
    {
        public DateTime MetricDate { get; set; }

        public float? Temperature { get; set; }
        public int? HeartRate { get; set; }
        public string? BloodPressure { get; set; }
        public string? AllergySymptoms { get; set; }
        public string? MedicationUse { get; set; }
    }
}
