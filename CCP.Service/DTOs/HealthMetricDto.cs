using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class HealthMetricDto
    {
        public Guid? Id { get; set; }

        public DateTime MetricDate { get; set; } // Ngày ghi nhận chỉ số

        public float? Temperature { get; set; } // Nhiệt độ cơ thể (°C)
        public int? HeartRate { get; set; } // Nhịp tim (bpm)
        public string? BloodPressure { get; set; } // Huyết áp (VD: \"120/80\")

        public string? AllergySymptoms { get; set; } // Triệu chứng dị ứng
        public string? MedicationUse { get; set; } // Thuốc đã dùng (nếu có)
    }
}
