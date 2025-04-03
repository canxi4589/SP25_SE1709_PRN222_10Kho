using CCP.Repositori.Enums;
using CCP.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP.Repositori.ResultData;
namespace CCP.Service
{
    public class MeasurementAnalysisService : IMeasurementAnalysisService
    {
        public MeasurementResultDto Analyze(GuestMeasurementInputDto input)
        {
            int ageInYears = CalculateAgeFromDateOfBirth(input.DateOfBirth);
            if (ageInYears < 1) ageInYears = 1;
            if (ageInYears > 20) ageInYears = 20;
            var gender = input.Gender;

            float heightStandard = gender == Gender.Male ? MeasurementStandards.MaleHeightStandard.GetValueOrDefault(ageInYears, 0)
                : MeasurementStandards.FemaleHeightStandard.GetValueOrDefault(ageInYears, 0);
            float weightStandard = gender == Gender.Male ? MeasurementStandards.MaleWeightStandard.GetValueOrDefault(ageInYears, 0)
                : MeasurementStandards.FemaleWeightStandard.GetValueOrDefault(ageInYears, 0);
            float headStandard = gender == Gender.Male ? MeasurementStandards.MaleHeadCircumferenceStandard.GetValueOrDefault(ageInYears, 0)
                : MeasurementStandards.FemaleHeadCircumferenceStandard.GetValueOrDefault(ageInYears, 0);
            float bmi = CalculateBMI(input.Height, input.Weight);

            return new MeasurementResultDto
            {
                HeightResult = GetHeightRating(input.Height, heightStandard),
                WeightResult = GetWeightRating(input.Weight, weightStandard),
                BMIResult = MeasurementStandards.GetBmiRating(bmi),
                BMIResultConsul = MeasurementStandards.GetBMIResultString(MeasurementStandards.GetBmiRating(bmi)),
                HeightResultConsul = GetHeightResultString(GetHeightRating(input.Height, heightStandard)),
                WeightResultConsul = GetWeightResultString(GetWeightRating(input.Weight, weightStandard)),
                HeadCircumferenceResult = GetHeadCircumferenceRating(input.HeadCircumference, headStandard),
                HeadCircumferenceResultConsul = GetHeadCircumferenceResultString(GetHeadCircumferenceRating(input.HeadCircumference, headStandard)),
            };
        }

        public string GetHeightResultString(HeightRating rating)
        {
            return rating == HeightRating.NormalHeight ? "Normal" : "Need consultation";
        }

        public string GetWeightResultString(WeightRating rating)
        {
            return rating == WeightRating.NormalWeight ? "Normal" : "Need consultation";
        }

        public string GetHeadCircumferenceResultString(HeadCircumferenceRating rating)
        {
            return rating == HeadCircumferenceRating.NormalHeadCircumference ? "Normal" : "Need consultation";
        }

        private int CalculateAgeFromDateOfBirth(DateTime dob)
        {
            var today = DateTime.Today;
            int age = today.Year - dob.Year;

            if (dob > today.AddYears(-age))
                age--;
            return age;
        }
        private float CalculateBMI(float heightCm, float weightKg)
        {
            float heightM = heightCm / 100f;
            return weightKg / (heightM * heightM);
        }
        private HeightRating GetHeightRating(float actual, float standard)
        {
            if (standard == 0) return HeightRating.NormalHeight;
            if (actual < standard * 0.9f) return HeightRating.UnderHeight;
            if (actual > standard * 1.1f) return HeightRating.OverHeight;
            return HeightRating.NormalHeight;
        }

        private WeightRating GetWeightRating(float actual, float standard)
        {
            if (standard == 0) return WeightRating.NormalWeight;
            if (actual < standard * 0.9f) return WeightRating.UnderWeight;
            if (actual > standard * 1.1f) return WeightRating.OverWeight;
            return WeightRating.NormalWeight;
        }
        private HeadCircumferenceRating GetHeadCircumferenceRating(float actual, float standard)
        {
            if (standard == 0) return HeadCircumferenceRating.NormalHeadCircumference;
            if (actual < standard * 0.9f) return HeadCircumferenceRating.UnderHeadCircumference;
            if (actual > standard * 1.1f) return HeadCircumferenceRating.OverHeadCircumference;
            return HeadCircumferenceRating.NormalHeadCircumference;
        }

    }
}
