using CCP.Repositori.Entities;

namespace CCP.Service
{
    public interface IMeasurementService
    {
        Task<List<Measurement>> GetMeasurementsByChild(Guid childId);
        Task<(Child?, List<Measurement>)> GetChildWithMeasurementsAsync(Guid childId);
    }
}