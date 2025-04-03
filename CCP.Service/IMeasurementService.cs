using CCP.Repositori.Entities;

namespace CCP.Service
{
    public interface IMeasurementService
    {
        Task<List<Measurement>> GetMeasurementsByChild(Guid childId);
        Task<(Child?, List<Measurement>)> GetChildWithMeasurementsAsync(Guid childId);
        Task<List<Child>> GetChildrenByParent(Guid parentId);
        Task<List<SleepPattern>> GetSleepPatternsByChildAsync(Guid childId);
        Task<List<PhysicalActivity>> GetPhysicalActivitiesByChildAsync(Guid childId);
        Task<List<NutritionalIntake>> GetNutritionalIntakesByChildAsync(Guid childId);
        Task<List<HealthMetric>> GetHealthMetricsByChildAsync(Guid childId);
    }
}