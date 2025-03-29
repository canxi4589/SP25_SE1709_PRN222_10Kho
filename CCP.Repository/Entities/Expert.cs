using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    using Microsoft.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Expert : BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("Specialty")]
        public Guid SpecialtyId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public  AppUser User { get; set; }

        [StringLength(255)]
        public string? ContactInfo { get; set; }
        public string? Certificate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        // Navigation properties
        public Specialty Specialty { get; set; }
        public ICollection<ExpertAvailability> ExpertAvailabilities { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
