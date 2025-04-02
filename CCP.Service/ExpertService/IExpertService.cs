using CCP.Repositori.Entities;
using CCP.Service.DTOs;
using Microsoft.AspNetCore.Http;

namespace CCP.Service.ExpertService
{
    public interface IExpertService
    {
        Task<List<ExpertAvailability>> ExpertAvailabilities(string expertId);
        Task<List<ExpertDTO>> GetExpertBySpecialityAsync(string id);
        Task<IEnumerable<ExpertDTO>> GetExpertsAsync();
        Task<List<Specialty>> GetSpecialtiesAsync();
        Task<ExpertDTO> GetExpertByIdAsync(string id);
        Task<ExpertDTO> UpdateExpertAsync(ExpertDTO expertDto, IFormFile certificateFile = null);
        Task<ExpertDTO> UpdateExpertAsync(ExpertDTO expertDto, byte[] fileContent, string fileName, string contentType);
        Task<ExpertDTO> GetExpertByUserIdAsync(string userId);
        Task<List<AppointmentHistoryDTO>> GetExpertAppointmentsAsync(Guid expertId);
        Task<ExpertDTO> AddExpertAsync(AddExpertDTO addExpertDto);
    }
}