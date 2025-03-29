using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Specialty : BaseEntity
    {
        [Required, StringLength(100)]
        public string? Name { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        // Navigation property
        public ICollection<Expert> Experts { get; set; }
    }
}
