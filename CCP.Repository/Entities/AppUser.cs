using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class AppUser : IdentityUser 
    {
        [Required, StringLength(100)]
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public ICollection<Child> Children { get; set; }
        public Expert? Expert { get; set; }
    }
}
