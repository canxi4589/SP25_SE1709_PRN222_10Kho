using CCP.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service
{
    public class MeasurementAnalysisService : IMeasurementAnalysisService
    {
        public MeasurementResultDto Analyze(GuestMeasurementInputDto input)
        {
            var heightInMeters = input.Height / 100;
            var bmi = input.Weight / (heightInMeters * heightInMeters);

            return new MeasurementResultDto
            {
                HeightResult = AnalyzeHeight(input.Height, input.GestationalAge),
                WeightResult = AnalyzeWeight(input.Weight, input.GestationalAge),
                BMIResult = AnalyzeBMI(bmi),
                HeadCircumferenceResult = input.HeadCircumference.HasValue
                ? AnalyzeHead(input.HeadCircumference.Value, input.Gender) : null
            };


        }
        private string AnalyzeHeight(float height, int gesAge)
        {
            r
        }
    }
}
