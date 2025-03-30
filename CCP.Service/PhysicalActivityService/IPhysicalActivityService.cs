using CCP.Repositori.Entities;
using CCP.Service.DTOs;

namespace CCP.Service.PhysicalActivityService
{
    public interface IPhysicalActivityService
    {
        Task AddPhysicalActivity(PhysicalActivity physicalActivity);
        Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivities();
        Task<List<PhysicalActivityDTO>> GetPhysicalActivityByChildId(Guid childId);
        Task<PhysicalActivityDTO> GetPhysicalActivityById(Guid id);
    }
}