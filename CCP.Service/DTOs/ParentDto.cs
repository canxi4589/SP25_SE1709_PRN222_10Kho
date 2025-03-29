using CCP.Repositori.Entities;
using CCP.Repositori.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class ParentDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<ChildDto>? Children { get; set; }

        
    }

}
