using CCP.Repositori.Entities;
using CCP.Service.DTOs;

namespace CCP.Service.ExpertService
{
    public interface IExpertService
    {
        Task<List<ExpertAvailability>> ExpertAvailabilities(string expertId);
        Task<List<ExpertDTO>> GetExpertBySpecialityAsync(string id);
        Task<IEnumerable<ExpertDTO>> GetExpertsAsync();
        Task<List<Specialty>> GetSpecialtiesAsync();
    }
}