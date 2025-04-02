using System;
using System.ComponentModel.DataAnnotations;

namespace CCP.Service.DTOs
{
    public class MeasurementInputDto
    {
        [Required(ErrorMessage = "Height is required.")]
        public float Height { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public float Weight { get; set; }

        public float? HeadCircumference { get; set; }
    }
}
