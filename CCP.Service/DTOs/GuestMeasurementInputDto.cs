using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP.Repositori.Entities;
using CCP.Repositori.Enums;
namespace CCP.Service.DTOs
{
    public class GuestMeasurementInputDto
    {
        public Gender Gender { get; set; } = Gender.Male;
        public float Height { get; set; }
        public float Weight { get; set; } 
        public float HeadCircumference { get; set; } 
        public DateTime DateOfBirth { get; set; } 
    }
}
