using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class SleepPatternDto
    {
        public Guid? Id { get; set; }
        public DateTime RecordDate { get; set; }

        public TimeSpan Bedtime { get; set; }
        public TimeSpan WakeTime { get; set; }

        public float? NapDuration { get; set; }

        public string? SleepQuality { get; set; }
        public string? SleepQualityRating { get; set; }

        public string? Status { get; set; }
    }
}
