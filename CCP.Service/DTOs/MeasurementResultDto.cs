using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP.Repositori.Enums;
namespace CCP.Service.DTOs
{
    public class MeasurementResultDto
    {
        public HeightRating HeightResult { get; set; }
        public WeightRating WeightResult { get; set; }
        public BmiRating BMIResult { get; set; }
        public HeadCircumferenceRating HeadCircumferenceResult { get; set; }
    }
}
