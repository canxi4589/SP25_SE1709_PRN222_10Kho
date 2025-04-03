using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class AddExpertDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } // Added for AppUser

        [Required]
        public Guid SpecialtyId { get; set; }

        [StringLength(255)]
        public string ContactInfo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        public List<ExpertAvailabilitiesDTO> Availabilities { get; set; } = new List<ExpertAvailabilitiesDTO>();
    }
}
