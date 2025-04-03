using CCP.Repositori.Data;
using CCP.Repositori.Entities;
using CCP.Repositori.Enums;
using CCP.Service.DTOs;
using System;
using System.Threading.Tasks;

namespace CCP.Service
{
    public class MeasurementInputService : IMeasurementInputService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMeasurementAnalysisService _measurementAnalysisService;
        private readonly IParentProfileService _parentProfileService;
        public MeasurementInputService(ApplicationDbContext context, IMeasurementAnalysisService measurementAnalysisService, IParentProfileService parentProfileService)
        {
            _context = context;
            _measurementAnalysisService = measurementAnalysisService;
            _parentProfileService = parentProfileService;
        }
        public static BmiRating GetBmiRating(double bmi)
        {
            return bmi switch
            {
                < 18.5f => BmiRating.UnderWeight,
                >= 18.5f and < 24.9f => BmiRating.Normal,
                >= 25f and < 27f => BmiRating.OverWeight,
                >= 27f and < 30f => BmiRating.PreObesity,
                >= 30f and < 35f => BmiRating.ObesityI,
                >= 35f and < 40f => BmiRating.ObesityII,
                _ => BmiRating.ObesityIII
            };
        }

        public async Task<bool> SaveAsync(Guid childId, MeasurementInputDto input)
        {
            var child = await _parentProfileService.GetChildren(childId);
            var result = _measurementAnalysisService.Analyze(new GuestMeasurementInputDto
            {
                Height = input.Height,
                Weight = input.Weight,
                HeadCircumference = input.HeadCircumference ?? 23,
                DateOfBirth = child.DateOfBirth,
                Gender = Enum.Parse<Gender>(child.Gender)
            });

            try
            {
                var measurement = new Measurement
                {
                    Id = Guid.NewGuid(),
                    ChildId = childId,
                    RecordDate = DateTime.Now,
                    Height = input.Height,
                    Weight = input.Weight,
                    BMIResult = CalculateBmi(input.Weight, input.Height),
                    BMIResultRaing = GetBmiRating(CalculateBmi(input.Weight, input.Height)).ToString(),
                    HeightResultRating = result.HeightResultConsul,
                    HeightResult = result.HeightResult.ToString(),
                    WeightResultRating = result.WeightResultConsul,
                    WeightResult =  result.WeightResult.ToString(),
                    HeadCircumferenceResult = result.HeadCircumferenceResultConsul,
                    HeadCircumferenceResultRating = result.HeadCircumferenceResult.ToString(),
                    HeadCircumference = input.HeadCircumference,
                    Status = "New"
                };

                _context.Measurements.Add(measurement);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine($"[Error] Saving measurement failed: {ex.Message}");
                return false;
            }
        }

        private double CalculateBmi(float weight, float heightCm)
        {
            double heightM = heightCm / 100.0;
            if (heightM <= 0) return 0; // Avoid division by zero
            return Math.Round((weight / (heightM * heightM)), 2);
        }
    }
}
