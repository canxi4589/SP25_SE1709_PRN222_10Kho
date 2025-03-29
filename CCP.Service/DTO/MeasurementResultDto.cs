using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP.Repositori.Enums;
namespace CCP.Service.DTO
{
    public class MeasurementResultDto
    {
        public  MeasurementRating HeightResult { get; set; }
        public MeasurementRating WeightResult { get; set; }
        public MeasurementRating BMIResult { get; set; }
        public MeasurementRating? HeadCircumferenceResult { get; set; }
    }
}
