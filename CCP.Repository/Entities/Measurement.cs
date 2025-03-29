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

        // Navigation property
        public Child Child { get; set; }
    }
}
