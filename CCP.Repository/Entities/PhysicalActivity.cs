using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class PhysicalActivity : BaseEntity
    {
        [ForeignKey("Child")]
        public Guid ChildId { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        [Required, StringLength(50)]
        public string ActivityType { get; set; }

        [Required]
        public float Duration { get; set; }

        [StringLength(20)]
        public string? Intensity { get; set; }

        public string? Status {  get; set; }      

        // Navigation property
        public Child Child { get; set; }
    }
}
