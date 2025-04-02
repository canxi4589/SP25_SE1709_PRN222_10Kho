using CCP.Repositori.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class ChildDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<PhysicalActivityDto>? PhysicalActivities { get; set; }
        public List<SleepPatternDto>? SleepPatterns { get; set; }
        public List<HealthMetricDto>? HealthMetrics { get; set; }
        public List<NutritionalIntakeDto>? NutritionalIntakes { get; set; }


    }
}
