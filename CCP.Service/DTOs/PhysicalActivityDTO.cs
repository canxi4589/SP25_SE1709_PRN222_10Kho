using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class PhysicalActivityDTO
    {
        public DateTime RecordDate { get; set; }

        public string ActivityType { get; set; }

        public float Duration { get; set; }

        public string? Intensity { get; set; }
    }
}
