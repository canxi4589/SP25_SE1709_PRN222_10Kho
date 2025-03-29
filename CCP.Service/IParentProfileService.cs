using CCP.Service.DTOs;

namespace CCP.Service
{
    public interface IParentProfileService
    {
        Task<IEnumerable<ChildDto>> GetChildrenAsync(string userId);
        Task<ChildDto?> GetChildByIdAsync(Guid childId, string userId);
        Task<ChildDto> CreateChildAsync(string userId, ChildDto dto);
        Task<ChildDto?> UpdateChildAsync(Guid childId, string userId, ChildDto dto);
        Task<bool> DeleteChildAsync(Guid childId, string userId);
        Task<ParentDto?> GetParentProfileAsync(string userId);
        Task<bool> UpdateParentProfileAsync(string userId, ParentDto dto);
    }
}
