using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class ExpertAvailability : BaseEntity
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayOfWeek { get; set; }
        public string Status { get; set; }
        public Expert Expert { get; set; }

    }
}
