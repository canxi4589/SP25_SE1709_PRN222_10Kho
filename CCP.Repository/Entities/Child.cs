using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class Child : BaseEntity
    {
        [ForeignKey("AppUser")]
        public string UserId { get; set; } 

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(10)]
        public string Gender { get; set; }
        public float? BirthWeight { get; set; }
        public int? GestationalAge { get; set; }
        public AppUser User { get; set; }
        public ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
        public ICollection<HealthMetric> HealthMetrics { get; set; } = new List<HealthMetric>();
        public ICollection<NutritionalIntake> NutritionalIntakes { get; set; } = new List<NutritionalIntake>();
        public ICollection<SleepPattern> SleepPatterns { get; set; } = new List<SleepPattern>();
        public ICollection<PhysicalActivity> PhysicalActivities { get; set; } = new List<PhysicalActivity>();

    }
}
