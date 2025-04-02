using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using CCP.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.HealthMetricService
{
    public class HealthMetricService : IHealthMetricService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HealthMetricService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HealthMetricDTO>> GetHealthMetrics(Guid childId)
        {
            var healthMetrics = await _unitOfWork.Repository<HealthMetric>().GetAll().Where(m => m.Child.Id.Equals(childId)).ToListAsync();

            return healthMetrics.Select(m => new HealthMetricDTO
            {
                MetricDate = m.MetricDate,
                Temperature = m.Temperature,
                HeartRate = m.HeartRate,
                BloodPressure = m.BloodPressure,
                AllergySymptoms = m.AllergySymptoms,
                MedicationUse = m.MedicationUse
            }).ToList();
        }

        public async Task<HealthMetricDTO> GetHealthMetric(Guid healthMetricId)
        {
            var healthMetric = _unitOfWork.Repository<HealthMetric>().GetById(healthMetricId);
            return new HealthMetricDTO
            {
                MetricDate = healthMetric.MetricDate,
                Temperature = healthMetric.Temperature,
                HeartRate = healthMetric.HeartRate,
                BloodPressure = healthMetric.BloodPressure,
                AllergySymptoms = healthMetric.AllergySymptoms,
                MedicationUse = healthMetric.MedicationUse
            };
        }

        public async Task<HealthMetric> CreateHealthMetric(HealthMetric healthMetric)
        {
            healthMetric.MetricDate = DateTime.Now;
            await _unitOfWork.Repository<HealthMetric>().AddAsync(healthMetric);
            await _unitOfWork.Complete();
            return healthMetric;
        }

        public async Task<HealthMetric> UpdateHealthMetric(HealthMetric healthMetric)
        {
            _unitOfWork.Repository<HealthMetric>().Update(healthMetric);
            await _unitOfWork.Complete();
            return healthMetric;
        }

    }
}
