using CCP.Repositori.Entities;
using CCP.Service.DTOs;

namespace CCP.Service.HealthMetricService
{
    public interface IHealthMetricService
    {
        Task<HealthMetric> CreateHealthMetric(HealthMetric healthMetric);
        Task<HealthMetricDTO> GetHealthMetric(Guid healthMetricId);
        Task<List<HealthMetricDTO>> GetHealthMetrics(Guid childId);
        Task<HealthMetric> UpdateHealthMetric(HealthMetric healthMetric);
    }
}