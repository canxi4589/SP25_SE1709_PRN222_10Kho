using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class Measurement : BaseEntity
    {
        [ForeignKey("Child")]
        public Guid ChildId { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        [Required]
        public float Height { get; set; }

        [Required]
        public float Weight { get; set; }

        public float? HeadCircumference { get; set; }

        public string? HeightResult { get; set; }
        public string? HeightResultRating { get; set; }
        public string? WeightResult { get; set; }
        public string? WeightResultRating { get; set; }
        public double? BMIResult { get; set; }
        public string? BMIResultRaing { get; set; }
        public string? HeadCircumferenceResult {  get; set; }
        public string? HeadCircumferenceResultRating {  get; set; }

        public string? Status { get; set; }       //status check if this has been booked for appointment

        // Navigation property
        public Child Child { get; set; }
    }
}
