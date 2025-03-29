using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP.Repositori.Entities;

namespace CCP.Service.DTO
{
    public class GuestMeasurementInputDto
    {
        public string Gender { get; set; } = string.Empty;
        public float BirthWeight { get; set; }
        public int GestationalAge { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public float? HeadCircumference { get; set; }
    }
}
