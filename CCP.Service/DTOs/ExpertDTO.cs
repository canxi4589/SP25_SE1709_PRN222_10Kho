using CCP.Repositori.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class ExpertDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Specialty { get; set; }

        public string? ContactInfo { get; set; }
        public string? CertificateUrl { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }

    }
}
